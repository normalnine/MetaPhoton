using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    // InputField
    public InputField chatInput;

    // ChatItem Prefab
    public GameObject chatItemFactory;
    // ScrollView에 있는 Content의 RectTransform
    public RectTransform rtContent;

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
        //print("OnSubmit : " + s);
        // ChatItem 을 만든다.
        GameObject chatItem = Instantiate(chatItemFactory);
        // 만들어진 item의 부모를 content로 한다.
        chatItem.transform.SetParent(rtContent);
        // 만들어진 item에서 Text 컴포넌트를 가져온다.
        Text item = chatItem.GetComponent<Text>();
        // 가져온 컴포넌트에서 text 값을 s 로 셋팅
        item.text = s;
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
