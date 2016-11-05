using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchButton : MonoBehaviour
{
	public Color[] playerBase;
	private Button button;
	private int lastPlayerId = -1;
	private ColorBlock originals;
    [HideInInspector]
    public LevelController _levelController;
    
    void Start ()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(() => { Toggle(); });
		originals = button.colors;
	}

	void Toggle()
	{
		DpadController.Instance.SelectedPlayer = DpadController.Instance.SelectedPlayer == 0 ? 1 : 0;
        _levelController.AddTimesChange();

    }

	Color MultColor(Color a, Color b)
	{
		return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
	}
	
	void Update ()
	{
		int selected = DpadController.Instance.SelectedPlayer;
		if (selected != lastPlayerId)
		{
			ColorBlock cblock = originals;
			cblock.normalColor = MultColor(playerBase[selected], originals.normalColor);
			cblock.highlightedColor = MultColor(playerBase[selected], originals.highlightedColor);
			cblock.pressedColor = MultColor(playerBase[selected], originals.pressedColor);
			cblock.disabledColor = MultColor(playerBase[selected], originals.disabledColor);
			button.colors = cblock;
			lastPlayerId = selected;
		}
	}
}
