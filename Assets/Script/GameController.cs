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
	public GameObject ChallengeWindowPrefab;
	public GameObject MenuButtonPrefab;
	public GameObject PauseMenuPrefab;

	public CameraController Camera;

	private AdvertisingWindowController _advertising = null;
	private FinishWindowController _finish = null;
	private LevelController _levelController = null;
	private SwitchButton _switchButton = null;
	private Text _timeText;
	private ChallengeWindowController _challengeWindow = null;
	private MenuButton _menuButton = null;
	private PauseMenu _pauseMenu = null;

	private int _levelLoaded = -1;
	private bool _showingChallenge = false;

	void Start ()
	{
		if (mainCanvas == null)
		{
			Debug.LogError("MAIN CANVAS IS NULL SON OF A!");
		}

		_advertising = Instantiate(AdvertisingWindowPrefab).GetComponent<AdvertisingWindowController>();
		_advertising.transform.SetParent(mainCanvas.transform, false);
		_advertising.Hide = false;

		_challengeWindow = Instantiate(ChallengeWindowPrefab).GetComponent<ChallengeWindowController>();
		_challengeWindow.transform.SetParent(mainCanvas.transform, false);
		_challengeWindow.gameObject.SetActive(false);
		_challengeWindow.Hide = true;

		_finish = Instantiate(FinishWindowPrefab).GetComponent<FinishWindowController>();
		_finish.transform.SetParent(mainCanvas.transform, false);
		_finish.controller = this;
		_finish.Hide = true;

		_menuButton = Instantiate(MenuButtonPrefab).GetComponent<MenuButton>();
		_menuButton.transform.SetParent(mainCanvas.transform, false);
		_menuButton.Hide = true;
		_menuButton.Menu.onClick.AddListener(OnMenuButton);

		_pauseMenu = Instantiate(PauseMenuPrefab).GetComponent<PauseMenu>();
		_pauseMenu.transform.SetParent(mainCanvas.transform, false);
		_pauseMenu.Hide = true;
		_pauseMenu.Restart.onClick.AddListener(OnRestart);
		_pauseMenu.Back.onClick.AddListener(OnBack);

		_switchButton = Instantiate(SwitchButtonPrefab).GetComponent<SwitchButton>();
        _switchButton._levelController = _levelController;
        _switchButton.transform.SetParent(mainCanvas.transform, false);
		_switchButton.gameObject.SetActive(false);

		_timeText = Instantiate(TimeTextPrefab).GetComponent<Text>();
		_timeText.transform.SetParent(mainCanvas.transform, false);
		_timeText.gameObject.SetActive(false);

		MainMenu();
	}

	public void OnMenuButton()
	{
		_pauseMenu.Hide = !_pauseMenu.Hide;
	}

	public void OnRestart()
	{
		var award = _levelController.award;
		Destroy(_levelController.gameObject);
		_levelController = null;

		CreateLevelController();
		_levelController.award = award;

		_pauseMenu.Hide = true;
		LoadLevelFinally();
	}

	public void OnBack()
	{
		Destroy(_levelController.gameObject);
		_levelController = null;

		DpadController.Instance.gameObject.SetActive(false);
		_switchButton.gameObject.SetActive(false);
		_timeText.gameObject.SetActive(false);

		_finish.Hide = false;
		_menuButton.Hide = true;
		_pauseMenu.Hide = true;

		MainMenu();
	}

	public void CreateLevelController()
	{
		_levelController = Instantiate(LevelControllerPrefab).GetComponent<LevelController>();
		_levelController.transform.SetParent(transform, false);
		_levelController.LoadPlayerInfo();
		_levelController.SetCamera(Camera);

        _switchButton._levelController = _levelController;

        _advertising.ShowLevels(this, _levelController.Levels, _levelController.infoPlayer);
		DpadController.Instance.gameObject.SetActive(false);
	}

	public void LoadLevel(int levelId)
	{
		_levelLoaded = levelId;
		_showingChallenge = true;

		_challengeWindow.Hide = false;
		_advertising.Hide = true;

		_challengeWindow.gameObject.SetActive(true);
		_challengeWindow.rulleteText.status = 25;
		_challengeWindow.rulleteText.result = (LevelController.AwardState) UnityEngine.Random.Range(0, 2);
		_challengeWindow.rulleteText.currentLevel = _levelController.Levels[_levelLoaded];

		_levelController.award = _challengeWindow.rulleteText.result;
	}

	public void LoadLevelFinally()
	{
		_levelController.LoadLevel(_levelLoaded);
		DpadController.Instance.gameObject.SetActive(true);
		_switchButton.gameObject.SetActive(true);
		_timeText.gameObject.SetActive(true);
		_menuButton.Hide = false;
		Camera.up = 40;
	}

	public void MainMenu()
	{
		_advertising.Hide = false;
		_finish.Hide = true;

		CreateLevelController();
	}

	void Update ()
	{
		if (_showingChallenge)
		{
			if (_challengeWindow.Hide)
			{
				LoadLevelFinally();
				_showingChallenge = false;
			}
		}
		else if (_levelController != null && _levelController.IsLevelLoaded)
		{
            switch(_levelController.award)
            {
                case LevelController.AwardState.Time:
                    int time = (int)_levelController.LevelTime;
                    _timeText.text = string.Format("{0}:{1:00}", (time / 60), (time % 60));
                    break;
                case LevelController.AwardState.Changes:
                    _timeText.text = _levelController.TimesChanged.ToString();
                    break;
                default:
                    break;

            }
			
			if (_levelController.IsLevelCompleted)
			{
				Camera.up = Camera.up * 0.9f + -40.0f * 0.1f;
				if (Camera.up < -38.0f)
				{
					switch (_levelController.award)
					{
						case LevelController.AwardState.Time:
							_finish.TargetLabel.text = "Expected time:";
							int parTime = (int)_levelController.Levels[_levelLoaded].parTime;
							_finish.TargetValue.text = string.Format("{0}:{1:00}", (parTime / 60), (parTime % 60));
							_finish.YourTargetLabel.text = "Your time:";
							int time = (int)_levelController.LevelTime;
							string text = string.Format("{0}:{1:00}", (time / 60), (time % 60));
							_finish.YourValue.text = text;
							if (time <= parTime)
							{
								_finish.ResultText.text = "Congratulations, you did it!";
								_finish.ResultText.color = Color.green * 0.5f;
							}
							else
							{
								_finish.ResultText.text = "Sorry, good luck next time.";
								_finish.ResultText.color = Color.red * 0.5f;
							}
							break;
						case LevelController.AwardState.Changes:
							_finish.TargetLabel.text = "Changes allowed:";
							int par = _levelController.Levels[_levelLoaded].parCrosses;
							_finish.TargetValue.text = par.ToString();
							_finish.YourTargetLabel.text = "Your changes:";
							int yourChanges = _levelController.TimesChanged;
							_finish.YourValue.text = yourChanges.ToString();
							if (yourChanges <= par)
							{
								_finish.ResultText.text = "Congratulations, you did it!";
								_finish.ResultText.color = Color.green * 0.5f;
							}
							else
							{
								_finish.ResultText.text = "Sorry, good luck next time.";
								_finish.ResultText.color = Color.red * 0.5f;
							}
							break;
						default:
							break;

					}

					_levelController.infoPlayer[_levelLoaded].clear = true;
					_levelController.SavePlayerInfo();

					Destroy(_levelController.gameObject);
					_levelController = null;

					DpadController.Instance.gameObject.SetActive(false);
					_switchButton.gameObject.SetActive(false);
					_timeText.gameObject.SetActive(false);

					_finish.Hide = false;
					_menuButton.Hide = true;
					_pauseMenu.Hide = true;
				}
			}
			else
			{
				Camera.up = Camera.up * 0.9f + 0.1f;
			}
		}
	}
}
