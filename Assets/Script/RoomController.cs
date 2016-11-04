using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class RoomController : MonoBehaviour
{
	public GameObject FloorPrefab;
	public GameObject WallPrefab;
	public GameObject StartRedPrefab;
	public GameObject StartBluePrefab;
	public GameObject EndRedPrefab;
	public GameObject EndBluePrefab;
	public GameObject DoorPrefab;
	public GameObject LeverPrefab;
	public GameObject ButtonPrefab;
	public GameObject PipePrefab;

	public const int ROOM_SIZE = 19;

	private LevelController parent;
	private int roomId;

	void InstantiateOn(int pos, GameObject toInstantiate)
	{
		int x = pos % ROOM_SIZE;
		int y = pos / ROOM_SIZE;

		GameObject obj = Instantiate(toInstantiate);
		obj.transform.SetParent(transform, false);
		obj.transform.localPosition = new Vector3(x, 0, -y);
	}

	public void CreateRoom(LevelController _parent, int _roomId, string room)
	{
		parent = _parent;
		roomId = _roomId;

		GameObject obj = Instantiate(FloorPrefab);
		obj.transform.SetParent(transform, false);

		int pos = 0;
		for (int i = 0; i < room.Length; i++)
		{
			char c = room[i];
			char c2;
			switch (c)
			{
				case 'W': // WALL
					InstantiateOn(pos, WallPrefab);
					pos++;
					break;
				case '_': // EMPTY
					pos++;
					break;
				case 'S': // START
					i++;
					c2 = room[i];
					switch(c2)
					{
						case 'R':
							InstantiateOn(pos, StartRedPrefab);
							break;
						case 'B':
							InstantiateOn(pos, StartBluePrefab);
							break;
					}
					pos++;
					break;
				case 'E': // END
					i++;
					c2 = room[i];
					switch (c2)
					{
						case 'R':
							InstantiateOn(pos, EndRedPrefab);
							break;
						case 'B':
							InstantiateOn(pos, EndBluePrefab);
							break;
					}
					pos++;
					break;
				case 'L': // LEVER
					InstantiateOn(pos, LeverPrefab);
					pos++;
					break;
				case 'P': // PIPE
					InstantiateOn(pos, PipePrefab);
					pos++;
					break;
				case 'B': // BUTTON
					InstantiateOn(pos, ButtonPrefab);
					pos++;
					break;
				case 'D': // DOOR
					InstantiateOn(pos, DoorPrefab);
					pos++;
					break;
				default: // ignore
					break;
			}
		}
	}
}
