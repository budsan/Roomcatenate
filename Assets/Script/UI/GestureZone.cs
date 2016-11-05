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
	public float angleIncrAccum;
	public float angleIncr;
	public float last;

	public void OnBeginDrag(PointerEventData eventData)
	{
		momentum = 0;
		angleIncrAccum = 0;
		last = eventData.position.x;
	}

	public void OnDrag(PointerEventData eventData)
	{
		float incr = (last - eventData.position.x) * 0.15f;
		angleIncr += incr;
		angleIncrAccum *= 0.5f;
		angleIncrAccum += incr;
		last = eventData.position.x;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		float incr = (last - eventData.position.x) * 0.15f;
		angleIncr += incr;
		angleIncrAccum *= 0.5f;
		angleIncrAccum += incr;
		momentum = incr + angleIncrAccum;
	}

	public void FixedUpdate()
	{
		momentum *= 0.9f;
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
