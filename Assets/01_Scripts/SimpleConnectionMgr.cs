using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SimpleConnectionMgr : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon 환경설정을 기반으로 접속을 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        
    }

    // 마스터 서버 접속 완료
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        print(nameof(OnConnectedToMaster));

        // 로비진입
        JoinLobby();
    }

    // 로비진입
    void JoinLobby()
    {
        // 닉네임 설정
        PhotonNetwork.NickName = "강동현";
        // 기본 Lobby 입장
        PhotonNetwork.JoinLobby();
    }

    // 로비진입 완료
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(nameof(OnJoinedLobby));
    }
}
