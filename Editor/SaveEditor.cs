using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Goo.EditorTools;

namespace Goo.Saves.Editor
{
#if UNITY_EDITOR
    public class SaveEditor : EditorWindowRelatedToManager<SaveManager>
    {
        private Save _saves;
        private Dictionary<string, bool> _toggles = new Dictionary<string, bool>();
        private IEnumerable<string> _sortedKeys;
        private bool _showSorted;

        public IEnumerable<string> Keys => _showSorted ? _sortedKeys : _saves.Keys as IEnumerable<string>;

        private void Awake()
        {
            _saves = Manager.GetFileProvider().Load();
            foreach (var item in _saves)
                _toggles[item.Key] = false;
            UpdateKeys();
        }

        private void OnGUI()
        {
            UpdateToggles();
            UpdateButtons();
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            _showSorted = EditorGUILayout.Toggle("Show key sorted", _showSorted);
        }

        private void UpdateButtons()
        {
            if (GUILayout.Button("Delete selected"))
            {
                DeleteSelected();
                Close();
            }
        }

        private void UpdateToggles()
        {
            foreach (var key in Keys)
                _toggles[key] = EditorGUILayout.Toggle(key, _toggles[key]);
        }

        private void UpdateKeys()
        {
            _sortedKeys = _saves.Keys.OrderBy((key) => key).ToList();
        }

        private void DeleteSelected()
        {
            foreach (var toggle in _toggles)
            {
                if (toggle.Value)
                    _saves.Remove(toggle.Key);
            }
            Manager.GetFileProvider().Save(_saves);
            UpdateKeys();
        }
    }
#endif
}