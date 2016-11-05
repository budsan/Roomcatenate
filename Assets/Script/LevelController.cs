using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
	public float parTime;
	public int parCrosses;
	public string[] level;

	public LevelInfo(float _parTime, int _parCrosses, string[] _level)
	{
		parTime = _parTime;
		parCrosses = _parCrosses;
		level = _level;
	}
}

[Serializable]
public class LevelInfoPlayer
{
	public bool clear = false;
	public float time;
	public int crosses;
}

struct LevelPosition
{
	int roomId;
	Vector2 position;
}

public class LevelController : MonoBehaviour
{

	public GameObject RoomPrefab;
	public GameObject PlayerPrefab;

	//------------------------------------------------//

	const string level0_room0= @"
W W W W W W W W W W W W W W W W W W W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ D1 
W _ SR_ _ _ _ _ _ _ _ _ _ _ _ ER_ _ _ 
W _ _ _ _ _ _ _ D1_ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W W W W W W W W H0_ W W W W W W W W W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ L _ _ _ _ P _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ SB_ B0B1B2B3B4B5B6B7B8B9_ EB_ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W W W W W W W W W W W W W W W W W W W ";

	private static readonly string[] level0_strs = { level0_room0 };

	public readonly LevelInfo[] Levels =
	{
		new LevelInfo(120, 8, level0_strs ),
		new LevelInfo(120, 8, level0_strs ),
	};

	public LevelInfoPlayer[] infoPlayer;

	private List<ButtonDoorItem> buttonDoors = new List<ButtonDoorItem>();

	//------------------------------------------------//

	public void LoadPlayerInfo()
	{
		infoPlayer = new LevelInfoPlayer[Levels.Length];
		for (int i = 0; i < infoPlayer.Length; i++)
		{
			string key = "LevelInfo" + i;
			if (PlayerPrefs.HasKey(key))
			{
				string json = PlayerPrefs.GetString(key);
				infoPlayer[i] = JsonUtility.FromJson<LevelInfoPlayer>(json);
			}
			else
			{
				infoPlayer[i] = new LevelInfoPlayer();
			}
		}
	}

	public void SavePlayerInfo()
	{
		for (int i = 0; i < infoPlayer.Length; i++)
		{
			string key = "LevelInfo" + i;
			if (infoPlayer[i].clear)
			{
				string json = JsonUtility.ToJson(infoPlayer[i]);
				PlayerPrefs.SetString(key, json);
			}
			else
			{
				if (PlayerPrefs.HasKey(key))
				{
					PlayerPrefs.DeleteKey(key);
				}
			}
		}

		PlayerPrefs.Save();
	}

	//------------------------------------------------//

	private LevelPosition playerRed;
	private RoomController[] rooms = null;
	void InstantiateLevel(string[] level)
	{
		if (rooms != null)
		{
			for (int i = 0; i < rooms.Length; i++)
			{
				Destroy(rooms[i].gameObject);
			}
		}

		rooms = new RoomController[level.Length];

		for (int i = 0; i < level.Length; i++)
		{
			GameObject roomObject = Instantiate(RoomPrefab);
			roomObject.name = "Room" + i;
			roomObject.transform.SetParent(transform, false);
			roomObject.transform.localPosition = new Vector3((RoomController.ROOM_SIZE + 1) * i, 0, 0);
			rooms[i] = roomObject.GetComponent<RoomController>();
			rooms[i].CreateRoom(this, i, level[i]);
		}
	}

	public void LoadLevel(int levelId)
	{
		InstantiateLevel(Levels[levelId].level);
	}
}