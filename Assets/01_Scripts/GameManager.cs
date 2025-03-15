using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 60;

        // 나의 Player 생성
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}
