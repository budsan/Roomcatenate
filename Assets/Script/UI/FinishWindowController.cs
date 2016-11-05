using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishWindowController : MonoBehaviour
{
	public bool Hide = false;

	public Text TargetLabel;
	public Text TargetValue;
	public Text YourTargetLabel;
	public Text YourValue;
	public Text ResultText;
	public Button MainMenuButton;

	[HideInInspector]
	public GameController controller;

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

		MainMenuButton.onClick.AddListener(OnMainMenuClicked);
	}

	void OnMainMenuClicked()
	{
		controller.MainMenu();
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
