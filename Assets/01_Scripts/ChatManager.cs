using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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

    void OnSubmit(string s)
    {
        //print("OnSubmit : " + s);
        // ChatItem 을 만든다.
        GameObject ci = Instantiate(chatItemFactory);
        // 만들어진 item의 부모를 content로 한다.
        ci.transform.SetParent(rtContent);

        // 닉네임을 붙여서 채팅내용을 만들자
        string chat = "<color=#" + ColorUtility.ToHtmlStringRGB(Color.blue) + ">" + PhotonNetwork.NickName + "</color>" + " : " + s; 
        
        // 만들어진 item에서 ChatItem 컴포넌트를 가져온다.
        ChatItem item = ci.GetComponent<ChatItem>();
        // 가져온 컴포넌트에서 SetText 함수를 실행
        item.SetText(chat);

        // chatInput 값을 초기화
        chatInput.text = "";

        // chatInput 을 활성화 하자
        chatInput.ActivateInputField();
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
