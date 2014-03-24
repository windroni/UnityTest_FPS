using UnityEngine;
using System.Collections;

/* 2014-03-10
//.fbx : 3D 모델링 파일
// rig 에서 애니메이션 타입을  none으로 주면 animation 도 자동으로 비활성화 된다.

// 프리팹은 저장은 scene 저장을 반드시 해야 한다.

// 하이라키 뷰에서 빨간색으로 된 오브젝트는 프리팹 링크가 깨진거임.
// 링크를 명시적으로 끊어버리자.(깨진 오브젝트르 선택한 후 GameObject-> Break Prefabs instance)
//  revert : 원래 프리팹으로 전환
//  apply : Secne에서 변경된 값을  프리팹에 적용.

// 콜라이더 충돌 연산 비용 (적은순): 원형 -> 캡슐 -> 사각형 -> 메쉬

// 프로젝트 복사시 필요있는 폴더 및 파일 Assets, Library, ProjectSettings

// 프로젝트 복사시 필요없는 폴더 및 파일
// - Temp
// - Library
// 	- metadata
// 	- ScriptAssemblies

// 리지디 바디 - 매번 움직이는  오브젝트에 적용하는 물리 연산 객체(움직이는 대상으로 유니티 엔진이 관리한다.)
//            - 리지디 바디를 안붙이는 오브젝트도 움직이긴 한다. 많은 계산이 필요. 따라서 움직이는 객체는 리지디 바디를 붙여줘야 한다.
// mess - 
// drag -  저항
// angular drag - 회전 저항
// use gravity -  중력 여부
// is kinematic - 물체로 부터 캐릭터의 관전 계산등을 하는것. (쓰면 느리다. - 모바일에선 굳이 쓸 필요가..)
// inerpolate - 일단 생략
// collision detection - 오즈젝트를 뚫고 가는 경우 - 연속적인 충돌 계산이 필요한 경우 (쓰면 느리다.)
// Constraints
// - Freeze Posiotn - 특정 축에 대한 이동을 막고 싶을때
// - Freeze Roataion - 특정 축에 대한 회전을 막고 싶을때

// 물리 엔진은 회전, 크기, 이동 모두를 바꼈다고 생각하고 다시 계산한다.

// 컬라이더 - 한번만 계산되는 오브젝트에 붙는 물이 연산 객체

// 캐릭터 컨트롤러 - 기본적으로 리지디 바디가 붙어있다.
*/

/*
 2014-03-13

[애니매이션  - clip이라고 부를수도 있다.]
 - add loop frame
 	- 
 - wrap mode
 	- default - (once)
 	- once (딱 한번)
 	- loop (애니매이션 무한반복)
 	- ping pong (앞으로 갔다 뒤로 갔다... (주먹질))
 	- clamp forver ( once랑 비슷하지만 마지막 프레임만 계속 재생한다 (블렌딩 연산을 위해서.. - 자연스런 애니매이션이 필요할때 )) 

- culling Type
	- always animate :  항상 애니매이션 재생
	- based on Renderers : 카메라 앞에만 있을때 애니매이션 재생 (애니매이션 이벤트(클립 중 특정연산등)도 고려해야 한다.)
 */

public class FireManager : MonoBehaviour 
{
	public Transform cameraTransform;
	public Transform firePosition;
	public GameObject fireObject;
	public float power = 20.0f;
	PlayerState _playerState = null;

	void Start()
	{
		_playerState = GetComponent<PlayerState>();
	}

	// Update is called once per frame
	void Update () 
	{
		if(_playerState.isDead)
			return;

		if(Input.GetButtonDown("Fire1"))
		{
			GameObject obj = Instantiate(fireObject) as GameObject; // 오브젝트의 클론 버전


			//obj.transform.position = transform.position; // 플레이어의 포지션 
			//obj.transform.position  = this.transform.right * 3;
			//obj.transform.position  = this.transform.position + Vector3(3, 0, 0)
			//obj.transform.position = transform.position + (cameraTransform.right * 3.0f);
			obj.transform.position  = firePosition.position;
			obj.rigidbody.velocity = cameraTransform.forward * power; // 강체의 속도.

			float x = Random.Range(-180.0f, 180.0f);
			float y = Random.Range(-180.0f, 180.0f);
			float z = Random.Range(-180.0f, 180.0f);

			obj.rigidbody.angularVelocity = new Vector3(x, y, z);

		}
	}
}
