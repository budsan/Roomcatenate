using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishWindowController : MonoBehaviour
{
	public bool Hide = false;

	public Text parTime;
	public Text yourTime;
	public Text parMovements;
	public Text yourMovements;

	private RectTransform _rect;
	private RectTransform Rect
	{
		get
		{
			if (_rect == null)
			{
				_rect = GetComponent<RectTransform>();
			}

			return _rect;
		}
	}

	void Start ()
	{
		Vector2 pivot = Rect.pivot;
		pivot.x = 2.0f;
		Rect.pivot = pivot;
	}
	
	void Update ()
	{
		const float factor = 0.9f;
		Vector2 pivot = Rect.pivot;
		if (Hide)
		{
			pivot.x = pivot.x * factor + 2.0f * (1.0f - factor);
		}
		else
		{
			pivot.x = pivot.x * factor + 0.5f * (1.0f - factor);
		}

		Rect.pivot = pivot;
	}
}
