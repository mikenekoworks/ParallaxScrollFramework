#if false
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

using System.Collections;

[CustomPropertyDrawer( typeof( ParallaxScrollObject ) )]
public class ParallaxScrollObjectPropertyDrawer : PropertyDrawer {

	public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {

		using ( new EditorGUI.PropertyScope( position, label, property ) ) {

		}

		//int count = property.CountInProperty();

		//do {
		//	Debug.Log( property.name );
		//} while ( property.NextVisible( false ) );

		//for ( int i = 0; i < scroll_objects.arraySize; ++i ) {
		//	var obj = scroll_objects.GetArrayElementAtIndex( i );

		//	Debug.Log( obj.name );
		//}

		//EditorGUI.ObjectField( position, property.FindPropertyRelative( "ScrollObjects" ) );
	}

	//	ReorderableList objectList;

	//SerializedProperty orderProperty;
	//SerializedProperty spriteProperty;
	//SerializedProperty speedCoefficientProperty;
	//SerializedProperty objectsProperty;

	//void OnEnable() {
	//	orderProperty = serializedObject.FindProperty( "order" );
	//	spriteProperty = serializedObject.FindProperty( "Sprite" );
	//	objectsProperty = serializedObject.FindProperty( "Objects" );
	//	speedCoefficientProperty = serializedObject.FindProperty( "SpeedCoefficient" );

	//	objectList = new ReorderableList( serializedObject, objectsProperty );

	//	// ヘッダーの描画
	//	objectList.drawHeaderCallback = ( rect ) => EditorGUI.LabelField( rect, "Scroll Objects" );

	//	objectList.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) => {

	//		var element = objectsProperty.GetArrayElementAtIndex( index );

	//		float margin = ( rect.height - EditorGUIUtility.singleLineHeight ) / 2.0f;
	//		rect.y += margin;
	//		rect.height = EditorGUIUtility.singleLineHeight;
	//		EditorGUI.PropertyField( rect, element );

	//	};

	//}

	//public override void OnInspectorGUI() {
	//	//DrawDefaultInspector();
	//	var scriptProperty = serializedObject.FindProperty( "m_Script" );
	//	EditorGUI.BeginDisabledGroup( true );
	//	EditorGUILayout.PropertyField( scriptProperty );
	//	EditorGUI.EndDisabledGroup();
	//	serializedObject.Update();

	//	EditorGUI.BeginDisabledGroup( true );
	//	EditorGUILayout.PropertyField( orderProperty );
	//	EditorGUI.EndDisabledGroup();

	//	EditorGUILayout.PropertyField( spriteProperty );
	//	EditorGUILayout.PropertyField( speedCoefficientProperty );
	//	//EditorGUILayout.PropertyField( objectsProperty );
	//	objectList.DoLayoutList();

	//	serializedObject.ApplyModifiedProperties();

	//}

}
#endif
