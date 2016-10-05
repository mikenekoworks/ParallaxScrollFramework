using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ParallaxScroll : MonoBehaviour {


	[SerializeField]
	private List<ParallaxScrollObject> scrollObjects = new List<ParallaxScrollObject>();

	//[SerializeField]
	//private TypeScrollWay scrollWay;
	//[SerializeField]
	//private TypeScrollDirection scrollDirection;
	[SerializeField]
	private float scrollBaseSpeed = 1;
	public float ScrollBaseSpeed
	{
		get
		{
			return scrollBaseSpeed;
		}
	}


	[ SerializeField]
	private bool smoothStart = true;

	[SerializeField]
	private float smoothDurationTime = 1.0f;

	private bool moveTrigger;
	private TypeScrollDirection moveDirection;
	private TypeScrollDirection moveSmoothDirection;
	private float smoothElapsedTime;

	[ SerializeField]
	private int createScrollUnit = 1;

	[SerializeField]
	private string sortingLayerName;
	/// <summary>
	/// Order in Layer の開始値
	/// </summary>
	[SerializeField]
	private int orderInLayerStart = 0;

	/// <summary>
	/// Order in Layer の間隔
	/// </summary>
	[SerializeField]
	private int orderInLayerInterval = 10;


	//public ParallaxScrollObject CreateScrollObject() {

	//	GameObject new_obj = new GameObject();

	//	ParallaxScrollObject pso = new_obj.AddComponent<ParallaxScrollObject>();

	//	new_obj.transform.parent = this.gameObject.transform;
	//	new_obj.transform.localPosition = Vector3.zero;

	//	Objects.Add( pso );

	//	return pso;
	//}

	public ParallaxScrollObject Add() {

		GameObject new_obj = new GameObject();
		new_obj.name = "ScrollObject";
		new_obj.transform.parent = gameObject.transform;
		new_obj.transform.localPosition = Vector3.zero;

		ParallaxScrollObject pso = new_obj.AddComponent<ParallaxScrollObject>();
		pso.Initialize( createScrollUnit, scrollObjects.Count, orderInLayerStart, orderInLayerInterval );

		scrollObjects.Add( pso );

		return pso;
	}

	public void Remove( ParallaxScrollObject target ) {

		scrollObjects.Remove( target );
#if UNITY_EDITOR
		DestroyImmediate( target.gameObject );
#else
		Destroy( target.gameObject );
#endif
	}


	public void SetSortingLayer( string sorting_layer_name ) {

		sortingLayerName = sorting_layer_name;

		Debug.Log( sortingLayerName );

		for ( int i = 0; i < scrollObjects.Count; ++i ) {
			scrollObjects[ i ].SetSortingLayer( sorting_layer_name );
		}
	}

	// Use this for initialization
	void Start() {

	}

	public void ScrollUpdate( TypeScrollDirection direction ) {

		moveDirection = direction;
		moveSmoothDirection = direction;
		moveTrigger = true;

	}

	public void Update() {

		TypeScrollDirection direction;

		if ( moveTrigger == true ) {
			direction = moveDirection;
		} else {
			direction = moveSmoothDirection;
		}

		float direction_sign = 1.0f;
		switch ( direction ) {
		case TypeScrollDirection.RightToLeft:
			direction_sign *= -1.0f;
			break;
		case TypeScrollDirection.LeftToRight:
			break;
		}

		float speed = scrollBaseSpeed * direction_sign * ( smoothElapsedTime / smoothDurationTime );

		for ( int i = 0; i < scrollObjects.Count; ++i ) {
			scrollObjects[ i ].ScrollUpdate( speed );
		}

		if ( moveTrigger == true ) {
			smoothElapsedTime += Time.deltaTime;
		} else {
			smoothElapsedTime -= Time.deltaTime;
		}
		smoothElapsedTime = Mathf.Clamp( smoothElapsedTime, 0.0f, smoothDurationTime );

		moveTrigger = false;
		moveDirection = TypeScrollDirection.None;

	}

}