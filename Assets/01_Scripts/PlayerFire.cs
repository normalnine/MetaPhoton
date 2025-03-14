using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFire : MonoBehaviourPun
{
    // 폭탄 공장
    public GameObject bombFactory;
    // 파편 공장
    public GameObject fragmentFactory;

    void Start()
    {
        // 내가 만든 Player 가 아닐 때
        if(photonView.IsMine == false)
        {
            // PlayerFire 컴포넌트를 비활성화
            this.enabled = false;
        }
        // PlayerFire 컴포넌트를 비활성화
    }

    void Update()
    {
        // 1번키를 누르면
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //FireBulletByInstantiate();
            photonView.RPC("FireBulletByRpc", RpcTarget.All);
        }

        // 2번키 누르면
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 카메라위치, 카메라 앞방향으로 Ray 를 만들자.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 만약에 Ray 를 발사해서 부딪힌 곳이 있다면
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                // 그 위치에 파편효과공장에서 파편효과를 만든다.
                GameObject fragment = Instantiate(fragmentFactory);
                // 만들어진 파편효과를 부딪힌 위치에 놓는다.
                fragment.transform.position = hitInfo.point;
                // 파편효과의 방향을 부딪힌 위치의 noraml 방향으로 설정
                fragment.transform.forward = hitInfo.normal;
                // 2초 뒤에 파편효과를 파괴하자.
                Destroy(fragment, 2);
            }
        }
    }

    void FireBulletByInstantiate()
    {
        // 만들어진 폭탄을 카메라 앞방향으로 1만큼 떨어진 지점에 놓는다.
        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward;

        // 만들어진 총알의 앞방향을 카메라가 보는 방향으로 설정
        Quaternion rot = Camera.main.transform.rotation;

        // 폭탄공장에서 폭탄을 만든다.
        GameObject bomb = PhotonNetwork.Instantiate("Bomb", pos, rot);
    }

    [PunRPC]
    void FireBulletByRpc()
    {
        // 만들어진 폭탄을 카메라 앞방향으로 1만큼 떨어진 지점에 놓는다.
        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward;

        // 만들어진 총알의 앞방향을 카메라가 보는 방향으로 설정
        Quaternion rot = Camera.main.transform.rotation;

        GameObject bomb = Instantiate(bombFactory);
        bomb.transform.position = pos;
        bomb.transform.rotation = rot;
    }
}
