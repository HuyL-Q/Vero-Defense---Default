using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Effect))]
public class EffectEditor : Editor
{
    private Effect effect;

    private void OnEnable()
    {
        effect = (Effect)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginVertical();
        effect.isMovementAffected = EditorGUILayout.Toggle("Bool", effect.isMovementAffected);
        GUI.enabled = effect.isMovementAffected;
        //effect._type = EditorGUILayout.EnumFlagsField(ImpairedType);

    }
}
