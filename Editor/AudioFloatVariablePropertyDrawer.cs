using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WaterKat.AudioManagement
{
    [CustomPropertyDrawer(typeof(AudioFloatVariable))]
    public class AudioFloatVariablePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("Value");
            EditorGUI.PropertyField(position, valueProperty, label);
//            base.OnGUI(position, property, label);
        }
    }
}
