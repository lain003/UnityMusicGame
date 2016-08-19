using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
#endif

public class NameAttribute : PropertyAttribute
{
	public System.Type type;

	public NameAttribute(System.Type type)
	{
		this.type = type;
	}
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NameAttribute))]
public class NameDrawer : PropertyDrawer
{
	const string emptyText = "--EMPTY--";

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		NameAttribute nameAttribute = (NameAttribute)attribute;

		string[] emptyValues = {emptyText};
		string[] constValues = nameAttribute.type.GetFields()
			.Where(f=>f.FieldType==typeof(string[]))
			.Select(f=>(string[])f.GetValue(null))
			.FirstOrDefault();
		string[] values = emptyValues.Concat(constValues).ToArray();

		int[] indexes = Enumerable.Range(0,values.Length).ToArray();

		string currentValue = property.stringValue;
		int selected = Mathf.Max(Array.IndexOf(values, currentValue),0);		
		selected = EditorGUI.IntPopup(position, label.text, selected, values, indexes);
		string selectedValue = (selected == 0) ? "" : values[selected];

		property.stringValue = selectedValue;
	}
}
#endif