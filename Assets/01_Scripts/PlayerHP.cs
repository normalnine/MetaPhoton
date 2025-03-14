using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    // 최대 HP
    float maxHP = 100;
    // 현재 HP
    float currHP = 0;
    // HPBar
    public Image hpBar;

    void Start()
    {
        // 현재 HP 를 최대 HP 로 설정
        currHP = maxHP;
    }

    // 데미지 맞았을 때 HP를 줄여주는 함수
    public void UpdateHP(float damage)
    {
        // 현재 HP 를 damage만큼 줄여준다.
        currHP += damage;
        // HPBar 갱신한다.
        hpBar.fillAmount = currHP / maxHP;
    }
}
