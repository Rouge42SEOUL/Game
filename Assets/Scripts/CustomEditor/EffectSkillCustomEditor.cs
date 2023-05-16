using Actor.Skill;
using UnityEditor;

namespace CustomEditor
{
    [UnityEditor.CustomEditor(typeof(EffectSkillObject))]
    public class EffectSkillCustomEditor : Editor
    {
        private SerializedProperty _targetType;
        private SerializedProperty _hasDotDamage;
        private SerializedProperty _isMultiplication;

        private SerializedProperty _range;
        private SerializedProperty _dotDamage;
        private SerializedProperty _dotDuration;

        private SerializedProperty _effect;

        private void OnEnable()
        {
            _targetType = serializedObject.FindProperty("targetType");
            _hasDotDamage = serializedObject.FindProperty("hasDotDamage");
            _isMultiplication = serializedObject.FindProperty("isMultiplication");

            _range = serializedObject.FindProperty("range");
            _dotDamage = serializedObject.FindProperty("dotDamage");
            _dotDuration = serializedObject.FindProperty("dotDuration");

            _effect = serializedObject.FindProperty("effect");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_targetType);
            if (_targetType.enumValueIndex == (int)TargetType.Area)
            {
                EditorGUILayout.PropertyField(_range);
            }

            EditorGUILayout.PropertyField(_hasDotDamage);
            if (_hasDotDamage.boolValue)
            {
                EditorGUILayout.PropertyField(_dotDamage);
                EditorGUILayout.PropertyField(_dotDuration);
            }

            EditorGUILayout.PropertyField(_effect);
            
            EditorGUILayout.PropertyField(_isMultiplication);
            EditorGUILayout.HelpBox("If this skill has percent effect, mark [Is Multiplication] true", MessageType.Info);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}