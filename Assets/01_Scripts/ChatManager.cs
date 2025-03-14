using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    // InputField
    public InputField chatInput;

    void Start()
    {
        // 엔터키를 누르면 InputField에 있는 텍스트 내용 알려주는 함수 등록
        chatInput.onSubmit.AddListener(OnSubmit);

        // InputField의 내용이 변경될 때마다 호출해주는 함수 등록
        chatInput.onValueChanged.AddListener(OnValueChanged);

        // InputField의 Focusing이 사라졌을 때 호출해주는 함수 등록
        chatInput.onEndEdit.AddListener(OnEndEdit);
    }

    void Update()
    {
        
    }

    void OnSubmit(string s)
    {
        print("OnSubmit : " + s);
    }

    void OnValueChanged(string s)
    {
        print("OnValueChanged : " + s);
    }

    void OnEndEdit(string s)
    {
        print("OnEndEdit : " + s);
    }
}
