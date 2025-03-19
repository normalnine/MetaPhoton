using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Input Room Name
    public InputField inputRoomName;
    // Input Max Player
    public InputField inputMaxPlayer;
    // 방 참여 버튼
    public Button btnJoinRoom;
    // 방 생성 버튼
    public Button btnCreateRoom;

    // RoomItem Prefab
    public GameObject roomItemFactory;
    // RoomListView -> Content -> RectTrnasform
    public RectTransform rtContent;
    
    void Start()
    {
        // 방 참여, 생성 비활성화
        btnJoinRoom.interactable = btnCreateRoom.interactable = false;
        // inputRoomName의 내용이 변경될 때 호출되는 함수
        inputRoomName.onValueChanged.AddListener(OnValueChangedRoomName);
        // inputMaxPlayer의 내용이 변경될 때 호출되는 함수
        inputRoomName.onValueChanged.AddListener(OnValueChangedMaxPlayer);
    }

    // 참여 & 생성 버튼에 관여
    void OnValueChangedRoomName(string room)
    {
        // 참여 버튼 활성 / 비활성
        btnJoinRoom.interactable = room.Length > 0;
        // 참여 버튼 활성 / 비활성
        btnCreateRoom.interactable = room.Length > 0 && inputMaxPlayer.text.Length > 0;
    }

    // 생성 버튼에 관여
    void OnValueChangedMaxPlayer(string max)
    {
        btnCreateRoom.interactable = max.Length > 0 && inputRoomName.text.Length > 0;
    }

    public void CreateRoom()
    {
        // 방 옵션 설정 (최대 인원)
        RoomOptions option = new RoomOptions();
        option.MaxPlayers = int.Parse(inputMaxPlayer.text);

        // 방 목록에 보이게 하냐? 안하냐?
        option.IsVisible = true;

        // 방에 참여할 수 있니? 없니?
        option.IsOpen = true;

        // 특정 로비에 방 생성 요청
        //TypedLobby typedLobby = new TypedLobby("Meta Lobby", LobbyType.Default);
        //PhotonNetwork.CreateRoom(inputRoomName.text, option, typedLobby);

        // 기본 로비에 방 생성 요청
        PhotonNetwork.CreateRoom(inputRoomName.text, option);
    }
    
    // 방 생성 완료시 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("방 생성 완료");
    }

    // 방 생성 실패시 호출 되는 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("방 생성 실패 : " + message);
    }

    public void JoinRoom()
    {
        // 방 입장 요청
        PhotonNetwork.JoinRoom(inputRoomName.text);
    }

    // 방 입장 완료 시 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("방 입장 완료");

        // GameScene으로 이동
    }

    // 방 입장 실패 시 호출되는 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("방 입장 실패 : " + message);
    }

    // 누군가 방을 만들거나 수정했을 때 호출되는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        for(int i=0; i<roomList.Count; i++)
        {
            print(i + "번째 방 : " + roomList[i].Name);

            // roomItem prefab을 이용해서 roomItem을 만든다
            GameObject goRoomItem = Instantiate(roomItemFactory, rtContent);
            // 만들어진 roomItem의 부모를 scrollView -> Content의 transfor으로 한다.
            goRoomItem.transform.parent = rtContent;
            // 만들어진 roomItem에서 RoomItem 컴포넌트 가져온다
            RoomItem roomItem = goRoomItem.GetComponent<RoomItem>();
            // 가져온 컴포넌트가 가지고 있는 SetInfo 함수 실행
            roomItem.SetInfo(roomList[i].Name, roomList[i].PlayerCount, roomList[i].MaxPlayers);
        }
    }
}
