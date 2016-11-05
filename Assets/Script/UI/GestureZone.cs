using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class GestureZone : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	static public GestureZone _instance;
	static public GestureZone Instance
	{
		get
		{
			return _instance;
		}
	}

	void Awake()
	{
		_instance = this;
	}

	void OnDestroy()
	{
		_instance = null;
	}

	public float momentum;
	public float angleIncr;
	public float last;

	public void OnBeginDrag(PointerEventData eventData)
	{
		momentum = 0;
		last = eventData.position.x;
	}

	public void OnDrag(PointerEventData eventData)
	{
		angleIncr += (last - eventData.position.x) * 0.15f;
		last = eventData.position.x;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		momentum = (last - eventData.position.x) * 0.1f;
		angleIncr += momentum;
	}

	public void FixedUpdate()
	{
		momentum = momentum * 0.95f;
		if (momentum > 0.001)
		{
			angleIncr += momentum;
		}
		else
		{
			momentum = 0;
		}
	}
}
