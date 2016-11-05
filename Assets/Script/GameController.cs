using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
	public Canvas mainCanvas;
	public GameObject AdvertisingWindowPrefab;
	public GameObject FinishWindowPrefab;
	public GameObject LevelControllerPrefab;
	public GameObject SwitchButtonPrefab;
	public GameObject TimeTextPrefab;

	public CameraController Camera;

	private AdvertisingWindowController _advertising = null;
	private FinishWindowController _finish = null;
	private LevelController _levelController = null;
	private SwitchButton _switchButton = null;
	private Text _timeText;

	void Start ()
	{
		if (mainCanvas == null)
		{
			Debug.LogError("MAIN CANVAS IS NULL SON OF A!");
		}

		_advertising = Instantiate(AdvertisingWindowPrefab).GetComponent<AdvertisingWindowController>();
		_advertising.transform.SetParent(mainCanvas.transform, false);
		_advertising.Hide = false;

		_finish = Instantiate(FinishWindowPrefab).GetComponent<FinishWindowController>();
		_finish.transform.SetParent(mainCanvas.transform, false);
		_finish.Hide = true;

		_levelController = Instantiate(LevelControllerPrefab).GetComponent<LevelController>();
		_levelController.transform.SetParent(transform, false);
		_levelController.LoadPlayerInfo();
		_levelController.SetCamera(Camera);

		_switchButton = Instantiate(SwitchButtonPrefab).GetComponent<SwitchButton>();
		_switchButton.transform.SetParent(mainCanvas.transform, false);
		_switchButton.gameObject.SetActive(false);

		_timeText = Instantiate(TimeTextPrefab).GetComponent<Text>();
		_timeText.transform.SetParent(mainCanvas.transform, false);
		_timeText.gameObject.SetActive(false);

		_advertising.ShowLevels(this, _levelController.Levels, _levelController.infoPlayer);
		DpadController.Instance.gameObject.SetActive(false);
	}

	public void LoadLevel(int levelId)
	{
		_levelController.LoadLevel(levelId);
		_advertising.Hide = true;
		DpadController.Instance.gameObject.SetActive(true);
		_switchButton.gameObject.SetActive(true);
		_timeText.gameObject.SetActive(true);
	}

	void Update ()
	{
		if (_levelController != null && _levelController.IsLevelLoaded)
		{
			int time = (int) _levelController.LevelTime;
			_timeText.text = string.Format("{0}:{1:00}", time / 60, time % 60);

			if (_levelController.IsLevelCompleted)
			{

			}
		}
	}
}
