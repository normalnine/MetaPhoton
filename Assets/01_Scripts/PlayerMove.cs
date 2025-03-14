using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
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

    void Start()
    {
        // Character Controller 가져오자
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 내가 만든 플레어이라면
        if (photonView.IsMine)
        {
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
                // yVelocity 를 0 으로 하자
                yVelocity = 0;
            }

            // 스페이스바를 누르면 점프를 하고 싶다.
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity 에 jumpPower 를 셋팅
                yVelocity = jumpPower;
            }

            // yVelocity 를 중력만큼 감소시키자.
            yVelocity += gravity * Time.deltaTime;

            // yVelocity 값을 dir 의 y 값에 셋팅
            dir.y = yVelocity;

            // 3. 그 방향으로 움직이자.
            // transform.position += dir * speed * Time.deltaTime;
            cc.Move(dir * speed * Time.deltaTime);

        }
    }
}
