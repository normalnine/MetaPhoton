using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 폭탄 공장
    public GameObject bombFactory;

    void Start()
    {
        
    }

    void Update()
    {
        // 1번키를 누르면
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 폭탄공장에서 폭탄을 만든다.
            GameObject bomb = Instantiate(bombFactory);
            // 만들어진 폭탄을 카메라 앞방향으로 1만큼 떨어진 지점에 놓는다.
            bomb.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
            // 만들어진 총알의 앞방향을 카메라가 보는 방향으로 설정
            bomb.transform.forward = Camera.main.transform.forward;
        }
    }
}
