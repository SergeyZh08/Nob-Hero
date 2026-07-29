using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActiveEffect), true)]
public class ActiveEffectEditor : Editor
{
    SerializedProperty _currentLevel;
    Dictionary<string, bool> _effectsForShow;

    private void OnEnable()
    {
        _currentLevel = serializedObject.FindProperty("_level");

        _effectsForShow = new Dictionary<string, bool>
        {
            {"Cooldown", false},
            {"Damage", false},
            {"Radius", false},
            {"Number", false},
            {"DPS", false},
            {"PassCount", false},
            {"LifeTime", false},
            {"Speed", false}
        };

        Dictionary<string, bool> temp = new(_effectsForShow);

        foreach (var effect in temp)
        {
            _effectsForShow[effect.Key] = EditorPrefs.GetBool(GetKey(effect.Key));
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Dictionary<string, bool> temp = new(_effectsForShow);

        DrawPropertiesExcluding(serializedObject, new string[] {"_level"});

        foreach (var effect in temp)
        {
            bool show = EditorGUILayout.Toggle(effect.Key, effect.Value);
            _effectsForShow[effect.Key] = show;

            if (show)
            {
                for (int i = 0; i < _currentLevel.arraySize; i++)
                {
                    SerializedProperty property = _currentLevel.GetArrayElementAtIndex(i).FindPropertyRelative(effect.Key);
                    EditorGUILayout.PropertyField(property, new GUIContent("Level: " + i));
                }

                GUILayout.Space(20);
            }

            EditorPrefs.SetBool(GetKey(effect.Key), show);
        }
        serializedObject.ApplyModifiedProperties();
    }

    string GetKey(string property)
    {
        string path = AssetDatabase.GetAssetPath(target);
        string guid = AssetDatabase.AssetPathToGUID(path);

        return $"ActiveEffect_{guid}_{property}";
    }
}
