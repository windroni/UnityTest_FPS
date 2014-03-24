using UnityEngine;
using System.Collections;

// http://game.dongguk.ac.kr/bbs/zboard.php?id=links&page=1&select_arrange=headnum&desc=asc&category=&sn=off&ss=on&sc=on&keyword=&sn1=&divpage=1
public class PlayerState : MonoBehaviour 
{
	public int healthPoint = 5;
	public bool gameOver = false;
	public bool isDead = false;

	CameraShake cameraShake = null;

	void Start()
	{
		//GetComponentsInChildren // 배열형태로 모든 오브젝트에서 해당되는 컴포넌트를 가져온다.
		cameraShake = GetComponentInChildren<CameraShake>();
	}
	
	void OnGUI () 
	{
		float x = Screen.width / 2.0f - 100;

		Rect rec = new Rect(x, 10, 200, 25);
		if(isDead == false)
			GUI.Box(rec, "My Health : "  + healthPoint);
		else
			GUI.Box(rec, "Game Over!");
	}

	public void DamageByEnemy()
	{
		if(true == isDead)
			return;

		--healthPoint;

		cameraShake.PlayCameraShake();

		if(healthPoint <= 0)
		{
			isDead = true;
		}
	}
}
