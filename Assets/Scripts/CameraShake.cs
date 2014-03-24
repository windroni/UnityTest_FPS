using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour 
{
	Vector3 myLocalPostion = Vector3.zero;
	
	// Use this for initialization
	void Start () 
	{
		// 카메라 흔든 후 원상 복귀를 위한 백업코드
		// 
		myLocalPostion = transform.localPosition;
	}


	public void PlayCameraShake()
	{
		Debug.Log("sldjfslkdjldjfsds");

		// 함수 이름 난독화를 위해 문자열로 함수 이름을 적지 않는다.

		StopAllCoroutines(); //현재 컴포넌트가 붙어있는 모든 코루틴을 종료(현재 프로세스의 모든 코루틴이 아님)
		StartCoroutine(CameraShakeProcess(1.0f, 0.2f));
	}

	// 코루틴은 프레임 단위를 설정할수 있다.
	IEnumerator CameraShakeProcess(float shakeTime, float shakeSense)
	{
		// 코루틴은 반드시 리턴을 한번은 해주는 코드가 들어가야 한다.
		yield return null; // 한 프레임 대기 (호출되는 순간 한프레임 대기 후) 아래 코드 실행 - cpu프레임 단위 (WaitForEndOfFrame 보다 약간 빠른 느낌)

		float deltaTime = 0.0f;
		while(deltaTime < shakeTime)
		{
			deltaTime += Time.deltaTime;

			transform.localPosition = myLocalPostion;
			Vector3 pos =- Vector3.zero;

			pos.x = Random.Range(-shakeSense , shakeSense);
			pos.y = Random.Range(-shakeSense , shakeSense);
			pos.z = Random.Range(-shakeSense , shakeSense);
			transform.localPosition += pos;

			yield return new WaitForEndOfFrame(); // 1프레임 대기 (렌더링 대기)
		}

		transform.localPosition = myLocalPostion;
	}
}
