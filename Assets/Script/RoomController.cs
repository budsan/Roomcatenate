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
	public GameObject DoorButtonPrefab;
	public GameObject LeverPrefab;
	public GameObject ButtonPrefab;
	public GameObject PipePrefab;

	public Color[] paletteId;

	public const int ROOM_SIZE = 19;

	private LevelController parent;
	private int roomId;

	GameObject InstantiateOn(int pos, GameObject toInstantiate)
	{
		int x = pos % ROOM_SIZE;
		int y = pos / ROOM_SIZE;

		GameObject obj = Instantiate(toInstantiate);
		obj.transform.SetParent(transform, false);
		obj.transform.localPosition = new Vector3(x, 0, -y);

		return obj;
	}

	GameObject InstantiateOnWithRotation(int pos, int rot, GameObject toInstantiate)
	{
		GameObject obj = InstantiateOn(pos, toInstantiate);
		obj.transform.Rotate(Vector3.up, rot * 90, Space.Self);
		return obj;
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
						default:
							Debug.LogError("Expecting R/B: " + Pos2Str(pos));
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
						default:
							Debug.LogError("Expecting R/B: " + Pos2Str(pos));
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
					i++;
					c2 = room[i];
					{
						GameObject butGo = InstantiateOn(pos, ButtonPrefab);
						ButtonItem butItem = butGo.GetComponent<ButtonItem>();
						butItem.baseColor = paletteId[Integer_Char2Int(c2, pos)];
					}
					
					pos++;
					break;
				case 'H': // DOOR
					i++;
					c2 = room[i];
					{
						GameObject doorGo = InstantiateOnWithRotation(pos, 0, ButtonPrefab);
						//ButtonDoorItem doorItem = doorGo.GetComponent<ButtonDoorItem>();
						//doorItem.SetColor(paletteId[Integer_Char2Int(c2, pos));
					}
					pos++;
					break;
				case 'V': // DOOR
					i++;
					c2 = room[i];
					{
						GameObject doorGo = InstantiateOnWithRotation(pos, 0, ButtonPrefab);
						//ButtonDoorItem doorItem = doorGo.GetComponent<ButtonDoorItem>();
						//doorItem.SetColor(paletteId[Integer_Char2Int(c2, pos));
					}
					pos++;
					break;
				case 'D': // DOOR
					i++;
					c2 = room[i];
					{
						int rot = Rotation_Char2Int(c2, pos);
						InstantiateOnWithRotation(pos, rot, DoorPrefab);
					}
					pos++;
					break;
				default: // ignore
					break;
			}
		}
	}

	int Integer_Char2Int(char value, int pos)
	{
		int valint = value;
		valint -= (int)'0';
		if (valint < 0 || valint > 9)
		{
			Debug.LogError("Expecting INT: " + Pos2Str(pos));
			valint = 0;
		}

		return valint;
	}

	int Rotation_Char2Int(char rot, int pos)
	{
		switch (rot)
		{
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			default:
				Debug.LogError("Expecting ROT: " + Pos2Str(pos));
				break;
		}

		return 0;
	}

	string Pos2Str(int pos)
	{
		int x = (pos % ROOM_SIZE) + 1;
		int y = (pos / ROOM_SIZE) + 1;
		return x.ToString() + "x" + y.ToString();
	}
}
