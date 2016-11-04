using UnityEngine;
using System.Collections;
using Biribit;
using System;

public class ConnectionController : MonoBehaviour, BiribitManagerListener
{
	[SerializeField] private string address = "thatguystudio.com";
	[SerializeField] private string clientName = "David";
	[SerializeField] private string appId = "roomcatenate-0";
	[SerializeField] private bool debug = false;

	private uint connectionId = Biribit.Client.UnassignedId;
	private uint joinedRoomId = Biribit.Client.UnassignedId;
	private int joinedRoomIndex = 0;
	private uint joinedRoomSlot = Biribit.Client.UnassignedId;

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

	public void Start()
	{
		manager = BiribitManager.Instance;
		manager.AddListener(this);

		System.Random ran = new System.Random();
		clientName = randomNames[ran.Next() % randomNames.Length];

		manager.Connect(address, 0, "");
	}

	public void OnServerListUpdated()
	{
		manager.JoinRandomOrCreateRoom(connectionId, (byte)3);
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
		}
	}

	public void OnJoinedRoom(uint _connectionId, uint roomId, byte slotId)
	{
		if (connectionId == _connectionId)
		{
			joinedRoomId = roomId;
			//joinedRoomIndex = manager.Rooms
			joinedRoomSlot = slotId;
			//entries_read = 0;

			//ReadForEntries();
			Debug.Log("Joined " + (slotId + 1).ToString() + ": " + name + " joined!");
		}
	}

	public void OnJoinedRoomPlayerJoined(uint _connectionId, uint clientId, byte slotId)
	{
		if (connectionId == _connectionId)
		{
			//Biribit.Room[] roomArray = manager.RefreshRooms();
			//Biribit.Room rm = roomArray[joinedRoomIndex];

			//int pos = manager.RemoteClients(connectionId, rm.slots[(int)slotId]);
			//string name = (pos < 0) ? rm.slots[(int)slotId].ToString() : manager.RemoteClients(connectionId)[pos].name;
			Debug.Log("Player " + (slotId + 1).ToString() + " " + " joined!");
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
