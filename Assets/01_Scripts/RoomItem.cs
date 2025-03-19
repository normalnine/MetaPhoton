using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text roomInfo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetInfo(string roomName, int currPlayer, int maxPlayer)
    {
        // 나의 게임오브젝트 이름을 방 이름으로 하자
        name = roomName;

        // 방 정보를 Text에 설정
        roomInfo.text = roomName + " ( " + currPlayer + " / " + maxPlayer + " ) "; 
    }
}
