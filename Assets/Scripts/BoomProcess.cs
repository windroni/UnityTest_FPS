using UnityEngine;
using System.Collections;

// ctrl + shift + c- log 창
public class BoomProcess : MonoBehaviour {

	public GameObject particleObject_;

	public GameObject groundPartiObj_;
	public GameObject airPartiObj_;


	// 움직이는 물체만 이 이벤트를 받을 수 있다. (리지디 바디도 있어야 한다)
	/*void OnCollisionEnter (Collision coll) 
	{
		Debug.Log("Collision Object Name : " + coll.gameObject.name);

		GameObject partiObj = Instantiate(particleObject_) as GameObject;
		partiObj.transform.position = transform.position;

		Destroy(gameObject); // 파괴 호출은 바로 지우진 않고 이번 프레임이 끝난 후 지우도록 명령하는것. 생성은 바로 생성
	}*/

	void OnCollisionEnter (Collision coll) 
	{
		Debug.Log("Collision Object Name : " + coll.gameObject.name);

		int collLayer = coll.gameObject.layer;
		if(collLayer == LayerMask.NameToLayer("Ground"))
		{
			GameObject partiObj = Instantiate(groundPartiObj_) as GameObject;
			partiObj.transform.position = transform.position;
		}
		else
		{
			GameObject partiObj = Instantiate(airPartiObj_) as GameObject;
			partiObj.transform.position = transform.position;
		}

		Destroy(gameObject); // 파괴 호출은 바로 지우진 않고 이번 프레임이 끝난 후 지우도록 명령하는것. 생성은 바로 생성
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Rotate(new Vector3(Random.Range(1.0f, 10.0f), Random.Range(1.0f, 10.0f), Random.Range(1.0f, 3.0f)));
	}
}
