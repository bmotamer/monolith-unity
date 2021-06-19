using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Monolith.Extensions;
using UnityEngine;

namespace Monolith.Unity
{
    
    [Serializable]
    public sealed class GameBootProfile : IGameBootProfile
    {

        [SerializeField] private string _id;
        [SerializeField] private GameBootOptions bootOptions;
        [SerializeField] private string[] _scriptingDefineSymbols;
        
        public GameBootProfile()
        {
            _id = "profile";
            bootOptions = null;
            _scriptingDefineSymbols = new string[0];
        }
        
        public string Id => _id ?? string.Empty;
        public IGameBootOptions BootOptions => bootOptions;
        public ReadOnlyCollection<string> ScriptingDefineSymbols => new ReadOnlyCollection<string>(_scriptingDefineSymbols ?? new string[0]);
        
        public bool NeedsSanitizing
        {
            get
            {
                return (
                    string.IsNullOrEmpty(_id) ||
                    (_scriptingDefineSymbols == null) ||
                    _scriptingDefineSymbols.Any((symbol) => !symbol.IsDefineDirectiveSafe()) ||
                    (_scriptingDefineSymbols.Distinct().Count() < _scriptingDefineSymbols.Length)
                );
            }
        }

        public void Sanitize()
        {
            if (Application.isPlaying) throw new InvalidOperationException();
            
            _id ??= string.Empty;
            _scriptingDefineSymbols ??= new string[0];

            int size = _scriptingDefineSymbols.Any() ? _scriptingDefineSymbols.Max((symbol) => symbol.Length) : 0;

            var stringBuilder = new StringBuilder(size);
            var list = new List<string>(_scriptingDefineSymbols.Length);

            foreach (string symbol in _scriptingDefineSymbols)
            {
                if (symbol == null) continue;
                
                foreach (char chr in symbol)
                {
                    if (chr.IsDefineDirectiveSafe()) stringBuilder.Append(chr);
                }
                
                if (stringBuilder.Length == 0) continue;
                
                list.Add(stringBuilder.ToString());
                
                stringBuilder.Clear();
            }

            _scriptingDefineSymbols = list.Distinct().ToArray();
        }
        
    }
    
}