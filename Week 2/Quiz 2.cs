using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MoveSelectionAttribute))]
public class Quiz2 : PropertyDrawer
{
    private readonly string[] options = { "rock", "paper", "scissors" };

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            int selectedIndex = 0;
            string currentValue = property.stringValue;

            for (int i = 0; i < options.Length; i++)
            {
                if (options[i] == currentValue)
                {
                    selectedIndex = i;
                    break;
                }
            }

            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, options);
            property.stringValue = options[selectedIndex];

            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
    public class MoveSelectionAttribute : PropertyAttribute { }
}

