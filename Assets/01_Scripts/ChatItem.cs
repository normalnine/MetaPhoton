using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatItem : MonoBehaviour
{
    public Text chatText;
    public RectTransform rt;

    void Awake()
    {
        chatText = GetComponent<Text>();
        rt = GetComponent<RectTransform>();
    }

    public void SetText(string s)
    {
        // 텍스트 갱신
        chatText.text = s;

        // 텍스트에 맞춰서 크기를 조절
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, chatText.preferredHeight);
    }
}
