using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdvertisingWindowController : MonoBehaviour
{
	public GameObject Content;
	public GameObject ButtonLevelPrefab;

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

	public void ShowLevels(GameController parent, LevelInfo[] levels, LevelInfoPlayer[] infoPlayer)
	{
		foreach(Transform child in Content.transform)
			Destroy(child.gameObject);

		bool lastLevelClearFound = false;
		for (int i = 0; i < levels.Length; i++)
		{
			GameObject buttonObj = Instantiate(ButtonLevelPrefab);
			buttonObj.transform.SetParent(Content.transform, false);

			Button button = buttonObj.GetComponentInChildren<Button>();

			int levelId = i; //capture this, for god sake
			button.onClick.AddListener(() =>
			{
				parent.LoadLevel(levelId);
			});

			Text text = buttonObj.GetComponentInChildren<Text>();
			text.text = string.Format("Level {0}", i + 1);

			if (!infoPlayer[i].clear)
			{
				if (!lastLevelClearFound)
				{
					lastLevelClearFound = true;
				}
				else
				{
					button.interactable = false;
				}
			}
		}
	}

	void Start()
	{
		Vector2 pivot = Rect.pivot;
		pivot.x = 2.0f;
		Rect.pivot = pivot;
	}

	void FixedUpdate()
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
