using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal;

[CustomEditor( typeof( ParallaxScroll ) )]
public class ParallaxScrollEditor : Editor {

	ReorderableList objectList;

	SerializedProperty scrollObjectsProperty;
	//SerializedProperty scrollWayProperty;
	//SerializedProperty scrollDirectionProperty;
	SerializedProperty scrollBaseSpeedProperty;
	SerializedProperty smoothStartProperty;
	SerializedProperty smoothDurationTimeProperty;
	SerializedProperty createScrollUnitProperty;
	SerializedProperty sortingLayerNameProperty;
	SerializedProperty orderInLayerStartProperty;
	SerializedProperty orderInLayerInterval;


	SerializedProperty sortingLayerProperty;
	List<string> sortingLayerNameList;
	void OnEnable() {

		scrollObjectsProperty = serializedObject.FindProperty( "scrollObjects" );
		//scrollWayProperty = serializedObject.FindProperty( "scrollWay" );
		//scrollDirectionProperty = serializedObject.FindProperty( "scrollDirection" );
		scrollBaseSpeedProperty = serializedObject.FindProperty( "scrollBaseSpeed" );
		smoothStartProperty = serializedObject.FindProperty( "smoothStart" );
		smoothDurationTimeProperty = serializedObject.FindProperty( "smoothDurationTime" );


	createScrollUnitProperty = serializedObject.FindProperty( "createScrollUnit");

		sortingLayerNameProperty = serializedObject.FindProperty( "sortingLayerName" );
		orderInLayerStartProperty = serializedObject.FindProperty( "orderInLayerStart" );
		orderInLayerInterval = serializedObject.FindProperty( "orderInLayerInterval" );

		// Sorting Layer の名前を取得して配列に格納しておく
//		var tagManager = new SerializedObject( AssetDatabase.LoadAllAssetsAtPath( "ProjectSettings/TagManager.asset" )[ 0 ] );
//		sortingLayerProperty = tagManager.FindProperty( "m_SortingLayers" );

		sortingLayerNameList = new List<string>();
		for ( int i = 0; i < SortingLayer.layers.Length; ++i ) {
			sortingLayerNameList.Add( SortingLayer.layers[ i ].name );
		}

		objectList = new ReorderableList( serializedObject, scrollObjectsProperty );
		objectList.elementHeight = ( EditorGUIUtility.singleLineHeight ) * 5 + EditorGUIUtility.standardVerticalSpacing * 3.0f;
		// ヘッダーの描画
		objectList.drawHeaderCallback = ( rect ) => EditorGUI.LabelField( rect, "Scroll Objects" );

		// +ボタンを押した時のコールバック
		objectList.onAddCallback += ( list ) => {
			////要素を追加
			//scrollObjectsProperty.arraySize++;
			//最後の要素を選択状態にする
			//list.index = scrollObjectsProperty.arraySize - 1;

			// 追加した要素の設定をする。
			//var element = scrollObjectsProperty.GetArrayElementAtIndex( list.index );

			ParallaxScroll parallaxScroll = ( ParallaxScroll )target;
			var pso = parallaxScroll.Add();

			list.index = scrollObjectsProperty.arraySize;
		};

		// +ボタンを押した時のコールバック
		objectList.onRemoveCallback += ( list ) => {

			// 追加した要素の設定をする。
			var element = scrollObjectsProperty.GetArrayElementAtIndex( list.index );

			ParallaxScroll parallaxScroll = ( ParallaxScroll )target;
			parallaxScroll.Remove( ( ParallaxScrollObject )element.objectReferenceValue );

		};

		//// 要素の描画コールバック
		objectList.drawElementCallback = ( rect, index, isActive, isFocused ) => {
			float width = rect.width;

			var element = scrollObjectsProperty.GetArrayElementAtIndex( index );

//			Debug.Log( rect.height + " : " + EditorGUIUtility.singleLineHeight );

			//float margin = ( rect.height - EditorGUIUtility.singleLineHeight ) / 2.0f;
			rect.height = EditorGUIUtility.singleLineHeight;
			rect.y += EditorGUIUtility.standardVerticalSpacing;
			//			EditorGUI.ObjectField( rect, element.objectReferenceValue, typeof( ParallaxScrollObject ), false );

			EditorGUI.BeginDisabledGroup( true );
			EditorGUI.ObjectField( rect, "", element.objectReferenceValue, typeof( ParallaxScrollObject ), true );
			EditorGUI.EndDisabledGroup();

			rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

			//EditorGUI.PropertyField( rect, element );//, typeof( ParallaxScrollObject ), false );
			var element_so = new SerializedObject( element.objectReferenceValue );
			element_so.Update();
			var sprite = element_so.FindProperty( "objectSprite" );
			//rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

			rect.height = EditorGUIUtility.singleLineHeight * 4.0f;
			rect.width = EditorGUIUtility.singleLineHeight * 4.0f;

			var new_sprite = EditorGUI.ObjectField( rect, sprite.objectReferenceValue, typeof( Sprite ), false );
			if ( new_sprite != sprite.objectReferenceValue ) {
				sprite.objectReferenceValue = new_sprite;

				( ( ParallaxScrollObject )element.objectReferenceValue ).SetSprite( ( Sprite )new_sprite );

			}
				//			EditorGUI.PropertyField( rect, sprite );

				width -= EditorGUIUtility.singleLineHeight * 4.0f;

			rect.x += EditorGUIUtility.singleLineHeight * 4.0f + EditorGUIUtility.standardVerticalSpacing;
			rect.height = EditorGUIUtility.singleLineHeight;
			rect.width = width;

			//rect.y += ( EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing );

			if ( sprite.objectReferenceValue != null ) {

				Sprite s = ( Sprite )sprite.objectReferenceValue;
				//string path = AssetDatabase.GetAssetPath( s.texture );
				//var importer = AssetImporter.GetAtPath( path ) as TextureImporter;

				//EditorGUI.LabelField( rect, importer.assetPath );
				//rect.y += ( EditorGUIUtility.singleLineHeight );
				//EditorGUI.LabelField( rect, "Sprite Mode: " + importer.spriteImportMode );
				//rect.y += ( EditorGUIUtility.singleLineHeight );
				//EditorGUI.LabelField( rect, "Packing Tag: " + importer.spritePackingTag );
				//rect.y += ( EditorGUIUtility.singleLineHeight );
				EditorGUI.LabelField( rect, s.name );
				rect.y += ( EditorGUIUtility.singleLineHeight );
				EditorGUI.LabelField( rect, "(" + s.rect.width + " x " + s.rect.height + ")" );

			}



			element_so.ApplyModifiedProperties();
		};

		// 要素が入れ変わった時のコールバック
		objectList.onChangedCallback = ( ReorderableList list ) => {

			for ( int i = 0; i < list.count; ++i ) {

				var list_property = list.serializedProperty;

				var element = list_property.GetArrayElementAtIndex( i );

				var scroll_obj = ( ParallaxScrollObject )element.objectReferenceValue;
				//scroll_obj.Order = i;

				//var serialize_scroll_obj = new SerializedObject( scroll_obj );
				//var order_property = serialize_scroll_obj.FindProperty( "order" );
				//order_property.intValue = i;
				//serialize_scroll_obj.ApplyModifiedProperties();
				if ( scroll_obj != null ) {
					scroll_obj.SetSortingLayerOrder( i );
				}

			}

		};
	}

	public override void OnInspectorGUI() {

		serializedObject.Update();
		//EditorStyles.objectField
		//EditorGUIUtility.PingObject( 1092 );


		var scriptProperty = serializedObject.FindProperty( "m_Script" );
		EditorGUI.BeginDisabledGroup( true );
		EditorGUILayout.PropertyField( scriptProperty );
		EditorGUI.EndDisabledGroup();

		ParallaxScroll manager = ( ParallaxScroll )target;
		//string[] guids = AssetDatabase.FindAssets( "t: Script ParallaxScroll" );
		//if ( guids.Length != 0 ) {
		//	Debug.Log( guids.Length );
		//	var path = AssetDatabase.GUIDToAssetPath( guids[ 0 ] );
		//	Object target_object = AssetDatabase.LoadAssetAtPath( path, typeof( Object ) );

		//	EditorGUI.BeginDisabledGroup( true );
		//	EditorGUILayout.ObjectField( "Script", scriptProperty, typeof( Object ), true );
		//	EditorGUI.EndDisabledGroup();

		//}




//		EditorGUILayout.PropertyField( scrollWayProperty );
//		EditorGUILayout.PropertyField( scrollDirectionProperty );
		EditorGUILayout.PropertyField( scrollBaseSpeedProperty );
		EditorGUILayout.PropertyField( smoothStartProperty );
		EditorGUILayout.PropertyField( smoothDurationTimeProperty );

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField( createScrollUnitProperty );
		if ( EditorGUI.EndChangeCheck() ) {

		}

		EditorGUILayout.Space();

//		EditorGUILayout.PropertyField( sortingLayerProperty );

		var selectedIndex = sortingLayerNameList.FindIndex( item => item.Equals( sortingLayerNameProperty.stringValue ) );
		if ( selectedIndex == -1 ) {
			selectedIndex = sortingLayerNameList.FindIndex( item => item.Equals( "Default" ) );
		}

		int new_selected_index = EditorGUILayout.Popup( "Sorting Layer", selectedIndex, sortingLayerNameList.ToArray() );
		if ( selectedIndex != new_selected_index ) {
			manager.SetSortingLayer( sortingLayerNameList[ new_selected_index ] );
		}

		EditorGUILayout.PropertyField( orderInLayerStartProperty );
		EditorGUILayout.PropertyField( orderInLayerInterval );

		objectList.DoLayoutList();

		//ParallaxScroll manager = ( ParallaxScroll )target;

		//DrawDefaultInspector();

		//if ( GUILayout.Button( "Create" ) == true ) {

		//	manager.CreateScrollObject();



		//}

		if ( createScrollUnitProperty.intValue < 1 ) {
			createScrollUnitProperty.intValue = 1;
		}

		serializedObject.ApplyModifiedProperties();

	}

	//private static Assembly editorAsm;
	//private static MethodInfo AddSortingLayer_Method;

	///// <summary> add a new sorting layer with default name </summary>
	//public static void AddSortingLayer() {
	//	if ( AddSortingLayer_Method == null ) {
	//		if ( editorAsm == null ) editorAsm = Assembly.GetAssembly( typeof( Editor ) );
	//		System.Type t = editorAsm.GetType( "UnityEditorInternal.InternalEditorUtility" );
	//		AddSortingLayer_Method = t.GetMethod( "AddSortingLayer", ( BindingFlags.Static | BindingFlags.NonPublic ), null, new System.Type[ 0 ], null );
	//	}
	//	AddSortingLayer_Method.Invoke( null, null );
	//}

}