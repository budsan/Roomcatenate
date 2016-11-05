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
	public int roomId;
	public Vector2 position;
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

    const string level1_room0 = @"
W W W W W W W W W W W W W W W W W W W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ V2_ _ _ _ B2_ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W W W W H2_ W W W W W W W H3_ W W W W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ B3_ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ SB_ _ _ _ ER_ W _ SR_ _ _ _ EB_ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W W W W W W W W W W W W W W W W W W W ";

    const string level2_room0 = @"
W W W W W W W W W W W W W W W W W W W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ D1 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ 
W _ SB_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W W W W W W W W H0_ W W W W W W W W W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ SR_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W W W W W W W W W W W W W W W W W W W ";

    const string level2_room1 = @"
W W W W W W W W W W W W W W W W W W W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ ER_ _ _ _ _ _ _ _ _ EB_ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W W W W H1_ W W W W W W W D0_ W W W W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W _ _ _ _ B0_ _ _ W _ _ _ _ B1_ _ _ W 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
D1_ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ D1 
_ _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ _ 
W _ _ _ _ _ _ _ _ W _ _ _ _ _ _ _ _ W 
W W W W W W W W W W W W W W W W W W W ";

    private static readonly string[] level0_strs = { level1_room0 };
    private static readonly string[] level1_strs = { level0_room0 };
    private static readonly string[] level2_strs = { level2_room0, level2_room1 };



    public readonly LevelInfo[] Levels =
	{
		new LevelInfo(120, 8, level0_strs ),
		new LevelInfo(120, 8, level1_strs ),
        new LevelInfo(120, 8, level2_strs ),
    };

	[HideInInspector] public LevelInfoPlayer[] infoPlayer;

	public int TestLevel = -1;

	private LevelPosition p1Spawn;
	private LevelPosition p2Spawn;

	private bool p1OnEnd;
	private bool p2OnEnd;
	private bool completed;

	private List<ButtonDoorItem> buttonDoors = new List<ButtonDoorItem>();
	public void AddButtonDoor(ButtonDoorItem door)
	{
		buttonDoors.Add(door);
	}

	private int[] groupsEnabled;

	private float _startTime;
	private float _endTime;
	public float LevelTime
	{
		get
		{
			if (completed)
			{
				return _endTime - _startTime;
			}

			return Time.time - _startTime;
		}
	}

	private bool LevelLoaded = false;
	public bool IsLevelLoaded
	{
		get
		{
			return LevelLoaded;
		}
	}

	public bool IsLevelCompleted
	{
		get
		{
			return completed;
		}
	}

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

	void Start()
	{
		if (TestLevel >= 0)
		{
			LoadLevel(TestLevel);
		}
	}

	void Update()
	{
		if (LevelLoaded)
		{
			bool lastCompleted = completed;
			completed = completed || (p1OnEnd && p2OnEnd);
			if (!lastCompleted && completed)
			{
				_endTime = Time.time;
			}
		}
	}

	CameraController _camera;
	public void SetCamera(CameraController camera)
	{
		_camera = camera;
	}

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
		groupsEnabled = new int[10];
		for (int i = 0; i < groupsEnabled.Length; i++)
			groupsEnabled[i] = 0;

		buttonDoors.Clear();

		InstantiateLevel(Levels[levelId].level);

		GameObject p1 = Instantiate(PlayerPrefab);
		p1.transform.SetParent(rooms[p1Spawn.roomId].transform, false);
		p1.transform.localPosition = new Vector3(p1Spawn.position.x, 0.6f, -p1Spawn.position.y);
		p1.transform.SetParent(transform, true);
		PlayerController p1Contr = p1.GetComponent<PlayerController>();
		p1Contr.camera = _camera;
		p1Contr.level = this;
		p1Contr.id = 0;

		GameObject p2 = Instantiate(PlayerPrefab);
		p2.transform.SetParent(rooms[p2Spawn.roomId].transform, false);
		p2.transform.localPosition = new Vector3(p2Spawn.position.x, 0.6f, -p2Spawn.position.y);
		p2.transform.SetParent(transform, true);
		PlayerController p2Contr = p2.GetComponent<PlayerController>();
		p2Contr.camera = _camera;
		p2Contr.level = this;
		p2Contr.id = 1;

		_camera.Target = rooms[p1Spawn.roomId].transform.FindChild("Floor/Quad").transform;
		_camera.angle = 45;

		_startTime = Time.time;
		LevelLoaded = true;
	}

	public void SetPlayer1Spawn(int roomId, int x, int y)
	{
		p1Spawn.roomId = roomId;
		p1Spawn.position.x = x;
		p1Spawn.position.y = y;
	}

	public void SetPlayer2Spawn(int roomId, int x, int y)
	{
		p2Spawn.roomId = roomId;
		p2Spawn.position.x = x;
		p2Spawn.position.y = y;
	}

	public void EnableButtonGroup(int groupId)
	{
		if (groupsEnabled[groupId] == 0)
		{
			foreach (ButtonDoorItem door in buttonDoors)
			{
				if (door.group == groupId)
				{
					door.SetDoorOpened(true);
				}
			}
		}

		groupsEnabled[groupId]++;
	}

	public void DisableButtonGroup(int groupId)
	{
		groupsEnabled[groupId]--;

		if (groupsEnabled[groupId] == 0)
		{
			foreach (ButtonDoorItem door in buttonDoors)
			{
				if (door.group == groupId)
				{
					door.SetDoorOpened(false);
				}
			}
		}
	}

	public void PlayerOnEnd(int playerId, bool onEnd)
	{
		switch(playerId)
		{
			case 0:
				p1OnEnd = onEnd;
				break;
			case 1:
				p2OnEnd = onEnd;
				break;
			default:
				break;
		}
	}
}