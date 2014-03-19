using UnityEngine;
using System.Collections;

// awake(여러번 호출 가능) -> start(무조건 한번만)
// horizontal : 좌우 이동
// vertical : 전후 이동
// characterController.simpleMove() : 점프가 없는 단순한 Move
// characterController.Move() : 일반 Move

// TransformDirection local 좌표를 world 좌표로 변경


// 키보드를 누를시 누른  현재 마우스로 조정되고 있는 카메라의 방향(카메라가 정면으로 바로 보고 있는 방향)에 따라 움직이게 하기 위해서
// TransformDirection를 이용한다. 플레이어 밑에 있는 카메라의 로컬 좌표를 월드 좌표로 바꾸고 움직일 방향을 월드 좌표에서 움직이게끔 전환해 준다.
// 카메라를 언제나 월드 좌표로 하기 위해 카메라를 부모로 만드는 것도 방법이지만 게임의 컨셉이나 기획에 따라 아래와 같은 현상이 발생할 수 있다.

// 카메라를 player 밑에 두는 이유
// 카메라가 부모가 되면 (최상단) 부모가 흔들리면 밑에 자식들까지 전부 흔들리게 된다.
// FPS에서 카메라가 흔들리는 경우를 표현할때 (플레이어가 죽는다던지)  플레이가 오브젝트는 흔들리지 않고 
// 카메라만 단독으로 흔들리면 다른 오브젝트는 흔들리지 않고 처리할 수 있다.

// Export 할때 주의점
// 한글 이름을 쓰지 말자.(무슨 오류가 날지 모름)

// 질문거리
// 유니티 asset 에서 다른 폴더에 있는 것을 공유할수 있는가?



public class CharacterMove : MonoBehaviour 
{
	public Transform cameraTransform;

	public float moveSpeed = 10.0f;
	public float jumpSpeed = 10.0f;
	public float gravity = -20.0f;

	CharacterController characterController = null;
	float yVelocity = 0.0f;

	void Start()
	{
		characterController = GetComponent<CharacterController> ();
	}

	void Update()
	{
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");

		Vector3 moveDirection = new Vector3 (x, 0, z);
		moveDirection = cameraTransform.TransformDirection (moveDirection);
		moveDirection *= moveSpeed;

		if (Input.GetButtonDown ("Jump")) 
		{
			yVelocity = jumpSpeed;
		}

		yVelocity += (gravity * Time.deltaTime);
		moveDirection.y = yVelocity;

		characterController.Move (moveDirection * Time.deltaTime);

		if (characterController.collisionFlags == CollisionFlags.Below)
			yVelocity = 0.0f;
	}

}
