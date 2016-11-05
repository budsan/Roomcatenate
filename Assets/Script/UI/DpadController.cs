using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class DpadController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField]
	private RectTransform Inner;

	private Vector2 begin;
	private Vector2 relative = Vector2.zero;
	private Vector2 axis;
	private float mag;

	public Vector2 Axis
	{
		get
		{
			return axis;
		}
	}

	public int SelectedPlayer = 0;

	static public DpadController _instance;
	static public DpadController Instance
	{
		get
		{
			return _instance;
		}
	}

	void OnDisable()
	{
		axis = Vector2.zero;
	}

	void Awake()
	{
		_instance = this;
	}

	void OnDestroy()
	{
		_instance = null;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		begin = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		relative = eventData.position - begin;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		relative = Vector2.zero;
	}

	void OnGUI ()
	{
		mag = relative.magnitude;
		if (mag > 100.0f)
			mag = 100.0f;

		mag *= (1.0f / 100.0f);
		Inner.anchoredPosition = relative.normalized * mag * 40.0f;

		mag = mag * mag;
		axis = relative.normalized * mag;
		
	}
}
