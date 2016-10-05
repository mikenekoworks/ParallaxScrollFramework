using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

using System.Collections;

[CustomEditor( typeof( ParallaxScrollObject ) )]
public class ParallaxScrollObjectEditor : Editor {

	ReorderableList objectList;

	SerializedProperty orderProperty;
	SerializedProperty spriteProperty;
	SerializedProperty speedCoefficientProperty;
	SerializedProperty objectsProperty;

	void OnEnable() {
		orderProperty = serializedObject.FindProperty( "order" );
		spriteProperty = serializedObject.FindProperty( "objectSprite" );
		objectsProperty = serializedObject.FindProperty( "scrollObjects" );
		speedCoefficientProperty = serializedObject.FindProperty( "speedCoefficient" );

		objectList = new ReorderableList( serializedObject, objectsProperty );

		// ヘッダーの描画
		objectList.drawHeaderCallback = ( rect ) => EditorGUI.LabelField( rect, "Scroll Objects" );

		objectList.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) => {

			var element = objectsProperty.GetArrayElementAtIndex( index );

			float margin = ( rect.height - EditorGUIUtility.singleLineHeight ) / 2.0f;
			rect.y += margin;
			rect.height = EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField( rect, element );

		};

	}

	public override void OnInspectorGUI() {
		//DrawDefaultInspector();
		var scriptProperty = serializedObject.FindProperty( "m_Script" );
		EditorGUI.BeginDisabledGroup( true );
		EditorGUILayout.PropertyField( scriptProperty );
		EditorGUI.EndDisabledGroup();
		serializedObject.Update();

		EditorGUI.BeginDisabledGroup( true );
		EditorGUILayout.PropertyField( orderProperty );
		EditorGUI.EndDisabledGroup();

		EditorGUILayout.PropertyField( spriteProperty );
		EditorGUILayout.PropertyField( speedCoefficientProperty );
		//EditorGUILayout.PropertyField( objectsProperty );
		objectList.DoLayoutList();

		serializedObject.ApplyModifiedProperties();

	}

}