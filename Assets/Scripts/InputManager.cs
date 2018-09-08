using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour {
	private static InputManager instance;
	public static InputManager getInstance()
	{
		return instance;
	}

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		if(instance != this)
		{
			DestroyImmediate(this);
		}
		DontDestroyOnLoad(this);
	}
	
	
	public event Action Button_W = ()=> { };
    public event Action Button_A = () => { };
	public event Action Button_S = () => { };
    public event Action Button_D = () => { };

    public event Action mouseLeftClick = () => { };
    public event Action<float> mouseWheel = (_) => { };

    public event Action mouseRightDragStart = () => { };
    public event Action<float> mouseRightDragging = (_) => { };
    public event Action mouseRightDragEnd = () => { };
    


    // Update is called once per frame
    void Update () {
		if(Input.GetKey(KeyCode.W))
		{
			Button_W();
		}
		if(Input.GetKey(KeyCode.A))
		{
			Button_A();
		}
		if(Input.GetKey(KeyCode.S))
		{
			Button_S();
		}
		if(Input.GetKey(KeyCode.D))
		{
			Button_D();
		}
        mouseWheel(Input.mouseScrollDelta.y);
        
	}

	/*
	기본적으로 방향키 입력은 캐릭터의 움직임을 만드는게 맞다.
	?1. 방향키 (WASD) 입력이 캐릭터의 움직임 이외를 만드는 경우의 수가 존재하는가? 
	
	F키와 같은  상호작용은 맥락에 따라 다른 결과가 나와야한다. 
	>1. StatePattern 을 통해 State에 따라 처리한다.
	>2. Trigger를 통해 다른 manager들이 event를 구독 해지한다. (추천)

	그럼 각 입력에 대한 반응을 모두 이벤트 처리해아하나?
	delegate event? 

	모두 이벤트 처리할 때 단점.
	-> (1) 다른 모든 매니저들이 Input을 이용하려면 이벤트 규격에 맞춰서 함수를 짜야한다. 
		?1. 규격이 변동되는 경우가 생길까?
	-> (2) 각 버튼에 따라 이벤트를 만들기 때문에 초반 Load가 있다. 
		W,A,S,D, F, 마우스 휠, 좌클릭, 오른쪽 드래그, 스크롤
	-> (3) 구독은 괜찮지만 해지관리를 명백히 하지 않으면 에러가 나기 쉽다. 디버깅 할 때 Event에 디버깅 포인트를 걸면 디버깅 하기 어렵다. 

	모두 이벤트 처리할 때 장점.
	-> (1) 다른 매니저들이 입력에 따라 Trigger 될 때 Callback 형식으로 만들기 쉽다.
	-> (2) 여러 매니저에 동시에 이벤트를 보내기 용이하다. (ex. 커서도 바꾸고 카메라도 바꾸고 효과음도 내고)

	 */
}
