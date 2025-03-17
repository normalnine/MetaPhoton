using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviourPun, IPunObservable
{
    // 속력
    float speed = 5;

    // Character Controller 담을 변수
    CharacterController cc;

    // 점프 파워
    float jumpPower = 5;
    // 중력
    float gravity = -9.81f;
    // y 속력
    float yVelocity = 0;

    // 서버에서 넘어오는 위치값
    Vector3 receivePos;
    // 서버에서 넘어오는 회전값
    Quaternion receiveRot = Quaternion.identity;
    // 보정하는 속력
    float lerpSpeed = 50;

    // NickName Text를 가져오자
    public Text nickName;

    // UI Canvas
    public GameObject myUI;

    void Start()
    {
        // Character Controller 가져오자
        cc = GetComponent<CharacterController>();

        // 만약에 내가 만든 Player 라면
        if(photonView.IsMine == true)
        {
            // UI를 비활성화 하자
            myUI.SetActive(false);
        }
        else
        {
            // NickName 설정
            nickName.text = photonView.Owner.NickName;
        }
    }

    void Update()
    {
        // 내가 만든 플레어이라면
        if (photonView.IsMine)
        {
            // 만약에 마우스 커서가 활성화 되어 있으면 함수를 나가자
            if (Cursor.visible == true) return;

            // W, S, A, D 키를 누르면 앞뒤좌우로 움직이고 싶다.

            // 1. 사용자의 입력을 받자.
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // 2. 방향을 만든다
            // 좌우
            Vector3 dirH = transform.right * h;
            // 앞뒤
            Vector3 dirV = transform.forward * v;
            // 최종
            Vector3 dir = dirH + dirV;
            dir.Normalize();

            // 만약에 땅에 닿아있다면
            if(cc.isGrounded == true)
            {
                // yVelocity를 0 으로 하자
                yVelocity = 0;
            }

            // 스페이스바를 누르면 점프를 하고 싶다.
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity 에 jumpPower 를 셋팅
                yVelocity = jumpPower;
            }

            // yVelocity를 중력만큼 감소시키자.
            yVelocity += gravity * Time.deltaTime;

            // yVelocity 값을 dir의 y값에 셋팅
            dir.y = yVelocity;

            // 3. 그 방향으로 움직이자.
            // transform.position += dir * speed * Time.deltaTime;
            cc.Move(dir * speed * Time.deltaTime);

        }
        else
        {
            // 위치 보정
            transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
            // 회전 보정
            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 내 Player라면
        if(stream.IsWriting)
        {
            // 나의 위치, 회전 값을 보낸다.
            stream.SendNext(transform.position);
            // 나의 회전값을 보낸다.
            stream.SendNext(transform.rotation);
        }
        // 내 Player가 아니라면
        else
        {
            // 위치값 받자.
            receivePos = (Vector3)stream.ReceiveNext();
            // 회전값을 받자.
            receiveRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
