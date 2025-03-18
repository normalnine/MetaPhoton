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
        // 내가 만든 Player가 아닐 때
        if (photonView.IsMine == false)
        {
            // PlayerFire 컴포넌트를 비활성화
            this.enabled = false;
        }
        // PlayerFire 컴포넌트를 비활성화
    }

    void Update()
    {
        // 만약에 마우스 커서가 활성화 되어 있으면 함수를 나가자
        if (Cursor.visible == true) return;

        // 만약에 내가 총을 쏠 수 없는 상태라면 함수를 나가자
        if (canFire == false) return;

        // 1번키를 누르면
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //FireBulletByInstantiate();

            //만들어진 폭탄을 카메라 앞방향으로 1만큼 떨어진 지점에 놓는다.
            Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward;

            // 만들어진 총알의 앞방향을 카메라가 보는 방향으로 설정
            Vector3 forward = Camera.main.transform.forward;

            photonView.RPC(nameof(FireBulletByRpc), RpcTarget.All, pos, forward);
        }

        // 2번키 누르면
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            photonView.RPC(nameof(FireRayByRpc), RpcTarget.All, Camera.main.transform.position, Camera.main.transform.forward);
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
    void FireBulletByRpc(Vector3 firePos, Vector3 fireFoward)
    {
        GameObject bomb = Instantiate(bombFactory);
        bomb.transform.position = firePos;
        bomb.transform.forward = fireFoward;
    }

    [PunRPC]
    void FireRayByRpc(Vector3 firePos, Vector3 firFoward)
    {
        // 카메라위치, 카메라 앞방향으로 Ray를 만들자.
        Ray ray = new Ray(firePos, firFoward);
        // 만약에 Ray를 발사해서 부딪힌 곳이 있다면
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            // 그 위치에 파편효과공장에서 파편효과를 만든다.
            GameObject fragment = Instantiate(fragmentFactory);
            // 만들어진 파편효과를 부딪힌 위치에 놓는다.
            fragment.transform.position = hitInfo.point;
            // 파편효과의 방향을 부딪힌 위치의 noraml 방향으로 설정
            fragment.transform.forward = hitInfo.normal;
            // 2초 뒤에 파편효과를 파괴하자.
            Destroy(fragment, 2);

            // 만약에 맞은 놈의 이름이 Player를 포함하고 있다면
            if(hitInfo.transform.gameObject.name.Contains("Player"))
            {
                // 플레이어가 가지고 있는  PlayerHP 컴포넌트 가져오자
                PlayerHP hp = hitInfo.transform.GetComponent<PlayerHP>();
                // 가져온 컴포넌트의 UpdateHP 함수를 실행
                hp.UpdateHP(-10);
            }
        }

        // 턴을 넘긴다
        GameManager.instance.ChageTurn();
    }


    // 내가 총을 쏠 수 있는지 판단
    bool canFire;

    [PunRPC]
    void ChangeTurnRpc(bool fire)
    {
        canFire = fire;
    }
}
