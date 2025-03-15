using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPun
{
    // InputField
    public InputField chatInput;

    // ChatItem Prefab
    public GameObject chatItemFactory;
    // ScrollView에 있는 Content의 RectTransform
    public RectTransform rtContent;
    // ScrollView의 RectTransform
    public RectTransform rtScrollView;
    // 채팅이 추가되기 전의 Content H의 값을 가지고 있는 변수
    float prevContentH;

    // 닉네임 색상
    public Color nickNameColor;

    void Start()
    {
        // 닉네임 색상 랜덤하게 설정
        nickNameColor = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);

        // 엔터키를 누르면 InputField에 있는 텍스트 내용 알려주는 함수 등록
        chatInput.onSubmit.AddListener(OnSubmit);

        // InputField의 내용이 변경될 때마다 호출해주는 함수 등록
        chatInput.onValueChanged.AddListener(OnValueChanged);

        // InputField의 Focusing이 사라졌을 때 호출해주는 함수 등록
        chatInput.onEndEdit.AddListener(OnEndEdit);
    }

    void OnSubmit(string s)
    {
        // 새로운 채팅이 추가되기 전의 content의 H값을 저장
        prevContentH = rtContent.sizeDelta.y;

        // 닉네임을 붙여서 채팅내용을 만들자
        string chat = "<color=#" + ColorUtility.ToHtmlStringRGB(nickNameColor) + ">" + PhotonNetwork.NickName + "</color>" + " : " + s;

        // Rpc 함수로 모든 사람한테 채팅 내용을 전달
        photonView.RPC(nameof(AddChatRpc), RpcTarget.All, chat);

        // chatInput 값을 초기화
        chatInput.text = "";

        // chatInput 을 활성화 하자
        chatInput.ActivateInputField();
    }

    [PunRPC]
    void AddChatRpc(string chat)
    {
        //print("OnSubmit : " + s);
        // ChatItem 을 만든다.
        GameObject ci = Instantiate(chatItemFactory);
        // 만들어진 item의 부모를 content로 한다.
        ci.transform.SetParent(rtContent);


        // 만들어진 item에서 ChatItem 컴포넌트를 가져온다.
        ChatItem item = ci.GetComponent<ChatItem>();
        // 가져온 컴포넌트에서 SetText 함수를 실행
        item.SetText(chat);

        StartCoroutine(AutoScrollBottom());
    }

    IEnumerator AutoScrollBottom()
    {
        yield return 0;

        // scrollView의 H보다 content의 H값이 크다면 (스크롤이 가능한 상태라면)
        if(rtContent.sizeDelta.y > rtScrollView.sizeDelta.y)
        {
            // 이전에 바닥에 닿아있었다면
            if(prevContentH - rtScrollView.sizeDelta.y <= rtContent.anchoredPosition.y)
            {
                // content의 y값을 재설정한다.
                rtContent.anchoredPosition = new Vector2(0, rtContent.sizeDelta.y - rtScrollView.sizeDelta.y);
            }
        }
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
