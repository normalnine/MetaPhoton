using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // RPC 호출 빈도
        PhotonNetwork.SendRate = 30;

        // OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 30;

        // 나의 Player 생성
        PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity);

        // 마우스 포인터를 비활성화
        Cursor.visible = false;
    }

    void Update()
    {
        // 만약에 ESC 키를 누르면
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // 마우스 포인터를 활성화
            Cursor.visible = true;
        }

        // 마우스 클릭했을 때
        if(Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭 시 해당 위치에 UI가 없으면
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                // 마우스 포인터를 비활성화
                Cursor.visible = false;
            }
        }
    }
}
