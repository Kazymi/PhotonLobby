using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

public class PhotonRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomItem roomItemPrefab;
    [SerializeField] private Transform container;
    private IPool<RoomItem> _roomPool;
    private List<RoomItem> _activatedRooms = new List<RoomItem>();

    private void Awake()
    {
        InitializePool();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("RoomListUpdate");
        foreach (var roomItem in _activatedRooms)
        {
            roomItem.ReturnToPool();
        }
        _activatedRooms = new List<RoomItem>();

        var id = 0;
        foreach (var roomInfo in roomList)
        {
            id++;
            if(id == 50) break;
            var newItem = _roomPool.Pull();
            newItem.transform.parent = container;
            newItem.Initialize(roomInfo);
            _activatedRooms.Add(newItem);
        }
    }

    private void InitializePool()
    {
        var factory = new FactoryMonoObject<RoomItem>(roomItemPrefab.gameObject, transform);
        _roomPool = new Pool<RoomItem>(factory, 4);
    }
}