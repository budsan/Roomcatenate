using UnityEngine;
using System.Collections;
using Biribit;
using System;

public class ConnectionController : MonoBehaviour, BiribitManagerListener
{
	private string address = "thatguystudio.com";
	private string clientName = "David";
	private string appId = "roomcatenate-0";
	private bool debug = false;

	private uint connectionId = Biribit.Client.UnassignedId;
	private uint joinedRoomId = Biribit.Client.UnassignedId;
	private int joinedSlotsMask = 0;
	private int joinedRoomIndex = 0;
	private uint joinedRoomSlot = Biribit.Client.UnassignedId;

	private const byte SLOTS_COUNT = 3;

	private BiribitManager manager;

	private enum GameState
	{
		WaitingPlayers,
		GameRunning
	}

	static private string[] randomNames = {
		"Jesus",
		"David",
		"Goliath",
		"Isaac",
		"Abraham",
		"Adam",
		"Eva",
		"Maria"
	};

	public bool IsConnected()
	{
		return connectionId != Biribit.Client.UnassignedId;
	}

	public bool HasLocalPlayerJoined()
	{
		return joinedRoomId != Biribit.Client.UnassignedId;
	}

	public bool IsLocalPlayerHosting()
	{
		return HasLocalPlayerJoined() && (joinedRoomSlot == (byte) 1);
	}

	public bool IsGameReady()
	{
		return (joinedSlotsMask & 0x03) == 0x03;
	}

	public void Connect()
	{
		manager = BiribitManager.Instance;
		manager.AddListener(this);

		System.Random ran = new System.Random();
		clientName = randomNames[ran.Next() % randomNames.Length];

		manager.Connect(address, 0, "");
	}

	private void FindRoomToJoin()
	{
		var connection = manager.GetConnection(connectionId);
		uint roomid = 0;
		byte slotid = 0;
		foreach (Room room in connection.Rooms)
		{
			for (byte i = 0; i < 2; i++)
			{
				if (room.slots[i] == 0)
				{
					roomid = room.id;
					slotid = i;
					break;
				}
			}

			if (roomid != 0)
			{
				break;
			}
		}

		if (roomid == 0)
		{
			manager.CreateRoom(connectionId, SLOTS_COUNT);
			Debug.Log("Creating new room");
		}
		else
		{
			manager.JoinRoom(connectionId, roomid, slotid);
			Debug.Log("Joining existing one");
		}
	}

	private void UpdateJoinedSlotsMask()
	{
		var connection = manager.GetConnection(connectionId);
		for (int i = 0; i < connection.Rooms.Length; i++)
			if(connection.Rooms[i].id == joinedRoomId)
			{
				joinedSlotsMask = 0;
				for (int j = 0; j < connection.Rooms[i].slots.Length; j++ )
					if (connection.Rooms[i].slots[j] != Biribit.Client.UnassignedId)
						joinedSlotsMask |= (1 << j); 
			}
	}

	public void OnServerListUpdated()
	{
		FindRoomToJoin();
	}

	public void OnConnected(uint _connectionId)
	{
		if (connectionId == 0)
		{
			connectionId = _connectionId;
			manager.SetLocalClientParameters(connectionId, clientName, appId);
			manager.RefreshRooms(connectionId);
		}
	}

	public void OnDisconnected(uint _connectionId)
	{
		if (connectionId == _connectionId)
		{
			connectionId = Biribit.Client.UnassignedId;
			joinedRoomId = Biribit.Client.UnassignedId;
			joinedRoomIndex = 0;
			joinedRoomSlot = Biribit.Client.UnassignedId;

			Debug.Log("Disconnected of connection " + _connectionId.ToString());
		}
	}

	public void OnJoinedRoom(uint _connectionId, uint roomId, byte slotId)
	{
		if (connectionId == _connectionId)
		{
			joinedRoomId = roomId;
			joinedRoomSlot = slotId;
			UpdateJoinedSlotsMask();
			Debug.Log("Joined room " + roomId + " on slot " + (slotId + 1).ToString() + ". Mask: " + joinedSlotsMask);
		}
	}

	public void OnJoinedRoomPlayerJoined(uint _connectionId, uint clientId, byte slotId)
	{
		if (connectionId == _connectionId)
		{
			UpdateJoinedSlotsMask();
			Debug.Log("Player " + clientId + " joined on slot " + (slotId + 1).ToString() + ". Mask: " + joinedSlotsMask);
		}
	}

	public void OnJoinedRoomPlayerLeft(uint _connectionId, uint clientId, byte slotId)
	{
		if (connectionId == _connectionId)
		{
			Debug.Log("OnJoinedRoomPlayerLeft");
		}
	}

	public void OnBroadcast(BroadcastEvent evnt)
	{
		if (connectionId == evnt.connection)
		{
			Debug.Log("OnBroadcast");
		}
	}

	public void OnEntriesChanged(uint _connectionId)
	{
		if (connectionId == _connectionId)
		{
			Debug.Log("OnEntriesChanged");
		}
	}

	public void OnLeaveRoom(uint _connectionId)
	{
		if (connectionId == _connectionId)
		{
			Debug.Log("OnLeaveRoom");
		}
	}
}
