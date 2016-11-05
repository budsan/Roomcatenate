using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public Canvas mainCanvas;
	public GameObject AdvertisingWindowPrefab;
	public GameObject LevelControllerPrefab;

	private AdvertisingWindowController _advertising;
	private LevelController _levelController;

	void Start ()
	{
		if (mainCanvas == null)
		{
			Debug.LogError("MAIN CANVAS IS NULL SON OF A!");
		}

		_advertising = Instantiate(AdvertisingWindowPrefab).GetComponent<AdvertisingWindowController>();
		_advertising.transform.SetParent(mainCanvas.transform, false);
		_advertising.Hide = false;

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
		
	}
}
