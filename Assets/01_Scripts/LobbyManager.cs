using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    // Input Room Name
    public InputField inputRoomName;
    // Input Max Player
    public InputField inputMaxPlayer;
    // 방 참여 버튼
    public Button btnJoinRoom;
    // 방 생성 버튼
    public Button btnCreateRoom;
    
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

    void Update()
    {
        
    }
}
