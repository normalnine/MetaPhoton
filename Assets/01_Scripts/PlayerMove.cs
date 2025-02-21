using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //속력
    float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //W, S, A, D 키를 누르면 앞뒤좌우로 움직이고 싶다.

        //1. 사용자의 입력을 받자.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //2. 방향을 만든다
        //좌우
        Vector3 dirH = transform.right * h;
        //앞뒤
        Vector3 dirV = transform.forward * v;
        //최종
        Vector3 dir = dirH + dirV;
        dir.Normalize();

        //3. 그 방향으로 움직이자.
        transform.position += dir * speed * Time.deltaTime;
    }
}
