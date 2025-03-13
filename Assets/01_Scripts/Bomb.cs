using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // 총알 속력
    float speed = 10;

    // 폭발효과공장
    public GameObject exploFactory;

    void Start()
    {
        
    }

    void Update()
    {
        // 계속 앞으로 가고 싶다.
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 폭발효과공장에서 폭발효과를 만들자.
        GameObject explo = Instantiate(exploFactory);
        // 만든 효과를 나의 위치에 놓자.
        explo.transform.position = transform.position;
        // 만든 효과에서 ParticleSystem 을 가져오자.
        ParticleSystem ps = explo.GetComponent<ParticleSystem>();
        // 가져온 ParticleSystem 의 기능의 Play 를 실행하자.
        ps.Play();
        // 2초뒤에 ps 를 파괴하자.
        Destroy(explo, 2);

        // 나를 파괴하자
        Destroy(gameObject);
    }
}
