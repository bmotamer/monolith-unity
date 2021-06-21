using System;
using System.Collections.ObjectModel;
using System.Linq;
using Monolith.Extensions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Monolith.Unity
{
    
    [CreateAssetMenu(fileName = "BootProfileSelector", menuName = "Monolith/Boot Profile Selector")]
    public sealed class GameBootProfileSelector : ScriptableObject
    {
        
        [SerializeField] private GameBootProfile[] _profiles = { new GameBootProfile() };
        [SerializeField, HideInInspector] private int _activeIndex = -1;
        
        public ReadOnlyCollection<GameBootProfile> Profiles => new ReadOnlyCollection<GameBootProfile>(_profiles);
        public int ActiveIndex => _activeIndex;

        public void Select(int index)
        {
            if (Application.isPlaying) throw new InvalidOperationException();
            
            if ((index < 0) || (index >= _profiles.Length)) index = -1;

            _activeIndex = index;
        }
        
        public bool NeedsSanitizing =>
        (
            (_profiles == null) ||
            (_activeIndex < -1) ||
            (_activeIndex >= _profiles.Length) ||
            _profiles.Any((profile) => (profile == null) || profile.NeedsSanitizing) ||
            (_profiles.Select((profile) => profile.Id).Distinct().Count() != _profiles.Length)
        );

        public void Sanitize()
        {
            if (Application.isPlaying) throw new InvalidOperationException();
            
            _profiles ??= new GameBootProfile[0];
            
            foreach (GameBootProfile profile in _profiles) profile?.Sanitize();
            
            _profiles = _profiles
                .Where((profile) => (profile != null) && (profile.Id.Length > 0))
                .GroupBy((profile) => profile.Id)
                .Select((group) => group.First())
                .ToArray();

            if ((_activeIndex < -1) || (_activeIndex >= _profiles.Length)) _activeIndex = -1;
        }

#if UNITY_EDITOR
        private bool HasMismatchedDefines(BuildTargetGroup buildTargetGroup, out string[] sanitizedDefines)
        {
            PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup, out string[] defines);
        
            var sharedDefines = defines.Except(_profiles.SelectMany((profile) => profile.ScriptingDefineSymbols));

            if (_activeIndex >= 0)
            {
                ReadOnlyCollection<string> profileDefines = _profiles[_activeIndex].ScriptingDefineSymbols;
        
                sanitizedDefines = sharedDefines
                    .Concat(profileDefines)
                    .ToArray();
            }
            else
            {
                sanitizedDefines = sharedDefines.ToArray();
            }

            return !defines.UnorderedSequenceEqual(sanitizedDefines);
        }
#endif
        
        public bool NeedsSynchronization =>
        (
#if UNITY_EDITOR
            HasMismatchedDefines(EditorUserBuildSettings.selectedBuildTargetGroup, out _)
#else
            false
#endif
        );

        public void Synchronize()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying) throw new InvalidOperationException();
            
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            
            if (HasMismatchedDefines(buildTargetGroup, out string[] sanitizedDefines))
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, sanitizedDefines);
                EditorUtility.RequestScriptReload();
            }
#endif
        }
        
    }
    
}