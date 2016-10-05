using UnityEngine;
using System.Collections;

[System.Serializable]
public class ParallaxScrollObject : MonoBehaviour {

	[SerializeField]
	private int order;
	public int Order
	{
		get
		{
			return order;
		}
		private set
		{
			order = value;
		}
	}

	[SerializeField]
	private GameObject[] scrollObjects;

	// 画像元スプライト
	[SerializeField]
	private Sprite objectSprite;
	public Sprite ObjectSprite
	{
		get
		{
			return objectSprite;
		}
		set
		{
			if ( objectSprite != value ) {
				SetSprite( value );
			}
		}
	}

	// デフォルトスピードとどの程度違うかの係数
	[ SerializeField]
	private float speedCoefficient = 1.0f;


	private int orderInLayerStart;
	private int orderInLayerInterval;


	private void Awake() {

	}

	public void Initialize( int create_object_count, int order, int layer_in_order_start, int layer_in_order_interval ) {

		RemoveAll();

		orderInLayerStart = layer_in_order_start;
		orderInLayerInterval = layer_in_order_interval;

		scrollObjects = new GameObject[ create_object_count ];

		for ( int i = 0; i < create_object_count; ++i ) {

			scrollObjects[ i ] = Add();
		}
		SetSortingLayerOrder( order );
	}

	public GameObject Add() {

		var scroll_obj = new GameObject();
		scroll_obj.name = "Sprite";
		scroll_obj.transform.localPosition = Vector3.zero;
		scroll_obj.transform.SetParent( gameObject.transform );

		var sr = scroll_obj.AddComponent<SpriteRenderer>();
		sr.sprite = objectSprite;

		return scroll_obj;
	}
	public void Remove() {


	}

	public void RemoveAll() {

		if ( scrollObjects == null ) {
			return;
		}

		for ( int i = 0; i < scrollObjects.Length; ++i ) {
			Destroy( scrollObjects[ i ] );

			scrollObjects[ i ] = null;
		}

	}

	public void SetSortingLayer( string sorting_layer_name ) {

		if ( scrollObjects == null ) {
			return;
		}

		for ( int i = 0; i < scrollObjects.Length; ++i ) {
			var sr = scrollObjects[ i ].GetComponent<SpriteRenderer>();
			sr.sortingLayerName = sorting_layer_name;
		}

	}

	public void SetSortingLayerOrder( int order ) {

		if ( scrollObjects == null ) {
			return;
		}


		Order = order;

		for ( int i = 0; i < scrollObjects.Length; ++i ) {
			var sr = scrollObjects[ i ].GetComponent<SpriteRenderer>();
			sr.sortingOrder = orderInLayerStart + order * orderInLayerInterval;
		}

	}

	public void SetSprite( Sprite new_sprite ) {

		objectSprite = new_sprite;

		if ( scrollObjects == null ) {
			return;
		}


		Vector3 v = Vector3.zero;
		for ( int i = 0; i < scrollObjects.Length; ++i ) {
			var sr = scrollObjects[ i ].GetComponent<SpriteRenderer>();
			sr.sprite = objectSprite;

			scrollObjects[ i ].transform.localPosition = v;

			v.x += objectSprite.rect.width / objectSprite.pixelsPerUnit;

		}
	}

	// Update is called once per frame
	public void ScrollUpdate( float base_speed ) {

		for ( int i = 0; i < scrollObjects.Length; ++i ) {

			Vector3 v = scrollObjects[ i ].transform.localPosition;
			v.x += base_speed * speedCoefficient;

			var sr = scrollObjects[ i ].GetComponent<SpriteRenderer>();

			// ピボットによって移動座標を補正
			// objectSprite.pivot.x * objectSprite.rect.width;
			float threshold_x = -objectSprite.rect.width / objectSprite.pixelsPerUnit;
			float move_size = objectSprite.rect.width / objectSprite.pixelsPerUnit * scrollObjects.Length;

			if ( Mathf.Sign( base_speed ) > 0.0f ) {
				threshold_x = objectSprite.rect.width / objectSprite.pixelsPerUnit;
				move_size *= -1.0f;
			}

			if ( Mathf.Sign( base_speed ) > 0.0f ) {
				if ( v.x > threshold_x ) {
					v.x += move_size;
				}
			} else {
				if ( v.x < threshold_x ) {
					v.x += move_size;
				}
			}
			scrollObjects[ i ].transform.localPosition = v;

		}
	}
}
