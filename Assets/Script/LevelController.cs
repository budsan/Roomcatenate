using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public GameObject RoomPrefab;

	const string level0_room0= @"
W W W W W W W W W W W W W W W W W W W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ SR_ _ _ _ _ _ _ _ _ _ _ _ ER_ _ W 
W _ _ _ _ _ _ _ D1_ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W W W W W W W W D0_ W W W W W W W W W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ L _ _ _ _ P _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ SB_ _ _ _ _ B _ _ _ _ _ _ EB_ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ W 
W W W W W W W W W W W W W W W W W W W ";

	private readonly string[] level0 = { level0_room0 };

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

	void Start()
	{
		LoadLevel(0);
	}

	void LoadLevel(int levelId)
	{
		switch(levelId)
		{
			case 0: // test
				InstantiateLevel(level0);
				break;
		}
	}
}