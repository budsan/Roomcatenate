using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuButton : MonoBehaviour {

	public Button Menu;
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
		pivot.x = -1.0f;
		Rect.pivot = pivot;
	}
	
	void FixedUpdate ()
	{
		const float factor = 0.9f;
		Vector2 pivot = Rect.pivot;
		if (Hide)
		{
			pivot.x = pivot.x * factor + -1.0f * (1.0f - factor);
		}
		else
		{
			pivot.x = pivot.x * factor + 1.0f * (1.0f - factor);
		}

		Rect.pivot = pivot;
	}
}
