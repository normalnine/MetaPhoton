using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // RPC 호출 빈도
        PhotonNetwork.SendRate = 30;

        // OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 30;

        SetSpawnPos();

        // 내가 위치해야 하는 idx 구하자
        int idx = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        // 나의 Player 생성
        PhotonNetwork.Instantiate("Player", spawnPos[idx], Quaternion.identity);

        // 마우스 포인터를 비활성화
        Cursor.visible = false;
    }

    // spanwPosGroup Transform
    public Transform trSpawnPosGroup;

    // Spawn 위치를 담아놓을 변수
    public Vector3[] spawnPos;

    void SetSpawnPos()
    {
        // 최대 인원 만큼 spawnPos의 공간 할당
        spawnPos = new Vector3[PhotonNetwork.CurrentRoom.MaxPlayers];
        // PhotonNetwork.CurrentRoom.MaxPlayers

        // 간격(angle)
        float angle = 360 / spawnPos.Length;

        for(int i=0; i<spawnPos.Length; i++)
        {
            trSpawnPosGroup.Rotate(0, angle, 0);

            spawnPos[i] = trSpawnPosGroup.forward + trSpawnPosGroup.forward * 5;

            //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //go.transform.position = pos;
        }
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

    // 새로운 인원이 방에 들어왔을 때 호출되는 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        print(newPlayer.NickName + "님이 들어왔습니다!");
    }
}
