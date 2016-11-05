using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public Canvas mainCanvas;
	public GameObject AdvertisingWindowPrefab;
	public GameObject FinishWindowPrefab;
	public GameObject LevelControllerPrefab;

	private AdvertisingWindowController _advertising = null;
	private FinishWindowController _finish = null;
	private LevelController _levelController = null;

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

		_advertising.ShowLevels(this, _levelController.Levels, _levelController.infoPlayer);
	}

	public void LoadLevel(int levelId)
	{
		_levelController.LoadLevel(levelId);
		_advertising.Hide = true;
	}

	void Update ()
	{
		if (_levelController != null && _levelController.IsLevelLoaded)
		{
			if (_levelController.IsLevelCompleted)
			{

			}
		}
	}
}
