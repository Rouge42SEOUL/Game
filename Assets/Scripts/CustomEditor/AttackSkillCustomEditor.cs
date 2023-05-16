using Actor.Skill;
using UnityEditor;

namespace CustomEditor
{
    [UnityEditor.CustomEditor(typeof(AttackSkillObject))]
    public class AttackSkillCustomEditor : Editor
    {
        private SerializedProperty _targetType;
        private SerializedProperty _hasDotDamage;
        private SerializedProperty _hasEffect;
        private SerializedProperty _isMultiplication;

        private SerializedProperty _range;
        private SerializedProperty _dotDamage;
        private SerializedProperty _dotDuration;

        private SerializedProperty _effect;
        private SerializedProperty _damage;
        private SerializedProperty _projectile;

        private void OnEnable()
        {
            _targetType = serializedObject.FindProperty("targetType");
            _hasDotDamage = serializedObject.FindProperty("hasDotDamage");
            _isMultiplication = serializedObject.FindProperty("isMultiplication");

            _range = serializedObject.FindProperty("range");
            _dotDamage = serializedObject.FindProperty("dotDamage");
            _dotDuration = serializedObject.FindProperty("dotDuration");

            _hasEffect = serializedObject.FindProperty("hasEffect");
            _effect = serializedObject.FindProperty("effect");
            _damage = serializedObject.FindProperty("_damage");
            _projectile = serializedObject.FindProperty("_projectile");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_targetType);
            EditorGUILayout.PropertyField(_damage);
            switch (_targetType.enumValueIndex)
            {
                case (int)TargetType.Area:
                    EditorGUILayout.PropertyField(_range);
                    break;
                case (int)TargetType.Projectile:
                    EditorGUILayout.PropertyField(_projectile);
                    break;
            }

            EditorGUILayout.PropertyField(_hasDotDamage);
            if (_hasDotDamage.boolValue)
            {
                EditorGUILayout.PropertyField(_dotDamage);
                EditorGUILayout.PropertyField(_dotDuration);
            }

            EditorGUILayout.PropertyField(_hasEffect);
            if (_hasEffect.boolValue)
            {
                EditorGUILayout.PropertyField(_effect);
                EditorGUILayout.PropertyField(_isMultiplication);
                EditorGUILayout.HelpBox("If this skill has percent effect, mark [Is Multiplication] true", MessageType.Info);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}