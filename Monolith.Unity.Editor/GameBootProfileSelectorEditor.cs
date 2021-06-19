using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Monolith.Unity.Editor
{
    
    [CustomEditor(typeof(GameBootProfileSelector))]
    public sealed class GameBuildProfileSelectorEditor : UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
            bool enabled = GUI.enabled;
            
            GUI.enabled = !EditorApplication.isPlaying;
            
            var selector = (GameBootProfileSelector)target;

            string[] profileIds = Enumerable.Repeat("<null>", 1).Concat(selector.Profiles.Select((profile) => profile.Id)).ToArray();
            
            int index = EditorGUILayout.Popup("Active", selector.ActiveIndex + 1, profileIds) - 1;

            if (selector.ActiveIndex != index)
            {
                Undo.RecordObject(selector, "Profile change");
                selector.Select(index);
                EditorUtility.SetDirty(selector);
            }

            base.OnInspectorGUI();
            
            if (selector.NeedsSanitizing)
            {
                EditorGUILayout.HelpBox("One or more profiles contain invalid data. You can hit the Sanitize button to clean the invalid bits off them. Please do note that this functionality is destructive. You can undo the changes if the output is not what you expected.", MessageType.Error);
            }
            else if (selector.NeedsSynchronization)
            {
                EditorGUILayout.HelpBox("Your project's scripting define symbols don't match the ones for the active profile. Hit the Synchronize button to update them.", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.HelpBox("Deleting scripting define symbols from a profile can result in them turning into shared symbols. You can view and edit all active symbols in Project Settings/Player/Script Compilation/Scripting Define Symbols.", MessageType.Info);
            }
            
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Sanitize"))
            {
                Undo.RecordObject(selector, "Sanitization");
                selector.Sanitize();
                EditorUtility.SetDirty(selector);
            }
            
            if (GUILayout.Button("Synchronize"))
            {
                selector.Synchronize();
            }

            GUILayout.EndHorizontal();

            GUI.enabled = enabled;
        }
        
    }
    
}
