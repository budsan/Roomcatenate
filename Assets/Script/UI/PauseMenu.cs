using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public Button Restart;
	public Button Back;

	public bool Hide = false;

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
		pivot.y = 3.0f;
		Rect.pivot = pivot;
	}
	
	void FixedUpdate ()
	{
		const float factor = 0.9f;
		Vector2 pivot = Rect.pivot;
		if (Hide)
		{
			pivot.y = pivot.y * factor + 3.0f * (1.0f - factor);
		}
		else
		{
			pivot.y = pivot.y * factor + 0.5f * (1.0f - factor);
		}

		Rect.pivot = pivot;
	}
}
