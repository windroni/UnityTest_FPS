using UnityEngine;
using System.Collections;

// 유니티의 기본적인 물리 처리 시간 0.02초
// Fixedupdate() : 입력 처리는 이곳에서 안됨. - 입력으로 인한 딜레이로 인해 물리 연산에 문제가 발생할 소지가 있다. (물리 관련된 연산)
// Update() : 입력 처리는 무조건 Update에서 해야 한다.
// 오일러 앵글 : x축으로 Y축으로의 각도들..(일반적인 각도)

// 회전 : 
// 오일러 			: 사람이 알아보기 쉬운 값
// 사원수(로테이션) 	: 임의의 축을 만들어서 회전 (계산이 쉽다.)
// 유니티는 내부적으로 사원수 사용
// 코딩에서 사용할때는 오일러 앵글 속성을 사용


// 진벌락 : FPS에서 하늘을 볼때 미친듯이 돌아가는 현상
// 원하는 회전값 계산을 못할때(오일러 앵글을 사용할 경우)
// 요즘은 사원수 : 사원수는 진벌락 현상이 발생하지 않는다.


// transform.positon : 월드 포지션
// transform.localpostion : 로컬 포지션 (부모에 종속적인 포지션)

// 병목 지점을 찾기 위해 Dont Sync
// 최적화 시 프레임 락 Every VBlank
// applicaton.TagetFrame~ 으로 잡는게 좋다.
public class MouseLook : MonoBehaviour 
{
	PlayerState _playerState = null;
	public float senstivity = 700.0f;
	float rotationX;
	float rotationY;

	void Start()
	{

		_playerState = transform.parent.GetComponent<PlayerState>();
	}

	void Update()
	{

		if(_playerState.isDead)
			return;

		float mouseMoveValueX = Input.GetAxis ("Mouse X");
		float mouseMoveValueY = Input.GetAxis ("Mouse Y");

		rotationY += mouseMoveValueX * senstivity * Time.deltaTime;
		rotationX += mouseMoveValueY * senstivity * Time.deltaTime;

		rotationY %= 360;
		rotationX %= 360;

		if (rotationX > 45.0f ) rotationX = 45.0f;
		else if(rotationX < -25.0f) rotationX = -25.0f;

		transform.eulerAngles = new Vector3 (-rotationX, rotationY, 0.0f);

		//Debug.Log (rotationX);
	}
}
