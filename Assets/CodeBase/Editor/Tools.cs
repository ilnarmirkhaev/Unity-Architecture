using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public static class Tools
    {
        [MenuItem("Tools/Clear Prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
