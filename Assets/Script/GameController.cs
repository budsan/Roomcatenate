using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public Canvas mainCanvas;
	public GameObject AdvertisingWindowPrefab;

	private ConnectionController _connectionController;
	private AdvertisingWindowController _advertising;

	void Start ()
	{
		if (mainCanvas == null)
		{
			Debug.LogError("MAIN CANVAS IS NULL SON OF A!");
		}

		_advertising = Instantiate(AdvertisingWindowPrefab).GetComponent<AdvertisingWindowController>();
		_advertising.transform.SetParent(mainCanvas.transform, false);
		_advertising.gameObject.SetActive(true);

		_connectionController = GetComponent<ConnectionController>();
		if (_connectionController == null)
			_connectionController = gameObject.AddComponent<ConnectionController>();

		_connectionController.Connect();
	}

	void Update ()
	{
		if (!_connectionController.IsConnected())
		{
			_advertising.text.text = "Connecting to server";
		}
		else if (!_connectionController.HasLocalPlayerJoined())
		{
			_advertising.text.text = "Creating or joining game";
		}
		else if (!_connectionController.IsGameReady())
		{
			_advertising.text.text = "Waiting for players...";
		}
		else
		{
			_advertising.text.text = "Game is ready";
		}
	}
}
