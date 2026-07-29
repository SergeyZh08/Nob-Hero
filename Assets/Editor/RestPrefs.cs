using UnityEditor;
using UnityEngine;

public class ResetPrefs : MonoBehaviour
{
    [MenuItem("Edit/Reset Editor Prefs")]
    static void ClearPrefs()
    {
        EditorPrefs.DeleteAll();
        Debug.Log("EditorPrefs cleared!");
    }
}