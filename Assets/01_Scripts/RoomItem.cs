using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text roomInfo;

    // 클릭 되었을 때 호출 해줄 함수를 담을 변수
    public Action<string> onChangeRoomName;

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

    public void OnClick()
    {
        // onChangeRoomName가 null이 아니라면
        if(onChangeRoomName != null)
        {
            onChangeRoomName(name);
        }

        //// 1. InputRoomName 게임오브젝트 찾자.
        //GameObject go = GameObject.Find("InputRoomName");
        //// 2. 찾은 게임오브젝트에서 InputField 컴포넌트 가져오자
        //InputField inputField = go.GetComponent<InputField>();
        //// 3. 가져온 컴포넌트를 이용해서 Text값 변경
        //inputField.text = name;
    }
}
