using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuletteText : MonoBehaviour
{
	public LevelController.AwardState result = LevelController.AwardState.Time;
	public float status = 60.0f;
	[HideInInspector] public LevelInfo currentLevel;

	private Text _text;
	private Text TextToShow
	{
		get
		{
			if (_text == null)
			{
				_text = GetComponent<Text>();
			}

			return _text;
		}
	}

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

	string GetAwardText(LevelController.AwardState foo)
	{
		switch(foo)
		{
			case LevelController.AwardState.Time:
				return "Par time!!";
			case LevelController.AwardState.Changes:
				return "Less changes!!";
		}

		return "";
	}

	string GetFullAwardText(LevelController.AwardState foo)
	{
		switch (foo)
		{
			case LevelController.AwardState.Time:
			{
				string time = string.Format("{0}:{1:00}", currentLevel.parTime / 60, currentLevel.parTime % 60);
				return "Beat par time:\n" + time;
			}
			case LevelController.AwardState.Changes:
				return "Change characters less than:\n" + currentLevel.parCrosses.ToString();
		}

		return "";
	}

	void Start ()
	{
		
	}
	
	void FixedUpdate ()
	{
		if (status > 0 && status < 0.1f)
		{
			status = 0;
			TextToShow.text = GetFullAwardText(result);
		}
		else if (status <= 0)
		{
			status -= Time.deltaTime;
		}
		else
		{
			status = status * 0.98f;
			int istatus = (int)status + (int) result;
			istatus = istatus % (int)LevelController.AwardState.Count;
			TextToShow.text = GetAwardText((LevelController.AwardState)istatus);
		}
	}
}
