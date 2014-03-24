using UnityEngine;
using System.Collections;

// https://onedrive.live.com/?cid=a34b954f9ac67fe5&id=A34B954F9AC67FE5%21575&ithint=folder,&authkey=!AOnmKNMKCo4q3tI
public class Spider : MonoBehaviour 
{
	
	enum SPIDERSTATE
	{
		NONE = -1,
		IDLE = 0,
		MOVE,
		ATTACK,
		DAMAGE,
		PATROL,
		DEAD,
	}
	
	Transform _target;
	SPIDERSTATE _spdierState = SPIDERSTATE.IDLE;
	float _stateTime = 0.0f;
	float _attackStateMaxTime = 2.0f;
	float _patorlMaxTime = 5.0f;
	float _patorlTime = 2.0f;
	int _healthPoint = 5;
	
	
	public float _chaseDistance = 100.0f;
	public float _idleStateMaxTime = 2.0f;
	public float _speed = 5.0f;
	public float _roationSpeed = 10.0f;
	public float _attackableRange = 1.5f;
	
	CharacterController _characterController;
	
	Vector3 _endPostion = new Vector3(0.0f,0.0f,0.0f);

	PlayerState _playerState;
	public GameObject explosionParticle = null;
	public GameObject deadObject = null;
	
	// 두번 이상 호출될수 있다.
	// 씬이 변경될때 호출된다.
	void Awake () 
	{
		// 애니메이션 타입이 레거시면 animation 객체를 이용하여 컨트롤한다.
		_spdierState = SPIDERSTATE.IDLE;
		animation.Play("iddle");
	}
	
	// 무조건 딱 한번(씬이 바껴도..)
	void Start()
	{
		_target = GameObject.Find("Player").transform;
		_playerState = _target.GetComponent<PlayerState>();
		_characterController = GetComponent<CharacterController>();
	}
	
	void Update () 
	{
		switch(_spdierState)
		{
		case SPIDERSTATE.IDLE: State_Idle(); break;
		case SPIDERSTATE.MOVE: State_Move(); break;
		case SPIDERSTATE.ATTACK: State_Attack(); break;
		case SPIDERSTATE.DAMAGE: State_Damage(); break;
		case SPIDERSTATE.DEAD: State_Dead(); break;
		case SPIDERSTATE.PATROL: State_Patrol(); break;
		}
	}
	
	void OnCollisionEnter(Collision coll)
	{
		Debug.Log (coll.gameObject.name);
		if(_spdierState == SPIDERSTATE.DEAD || _spdierState == SPIDERSTATE.NONE)
			return;

		if(coll.gameObject.name.Contains("EyeBall") == false)
			return;
		
		_spdierState = SPIDERSTATE.DAMAGE;
	}
	
	void State_Idle()
	{
		Debug.Log("State_Idle");
		
		//animation.Play("iddle");
		
		_stateTime += Time.deltaTime;
		if(_stateTime > _idleStateMaxTime)
		{
			_stateTime = 0.0f;
			_spdierState = SPIDERSTATE.MOVE;

			/*
			float distance = (_target.position - transform.position).magnitude;
			
			Debug.Log("State_Patrol : " + distance);
			Debug.Log("State_Patrol : " + _chaseDistance);
			
			if(distance < _chaseDistance)
			{
				_spdierState = SPIDERSTATE.MOVE;
			}
			else
			{
				_spdierState = SPIDERSTATE.PATROL;
			}
			*/
			
			//_stateTime = 0.0f;
			//_spdierState = SPIDERSTATE.MOVE;
			//_spdierState = SPIDERSTATE.PATROL;
		}
	}
	
	void State_Move()
	{
		animation.Play("walk");
		
		// magnitude : a제곱  * b제곱 후 루트 씌우기(magnitude)
		float distance = (_target.position - transform.position).magnitude;

		if(distance < _attackableRange)
		{
			_spdierState = SPIDERSTATE.ATTACK;
			_stateTime = _attackStateMaxTime;
		}
		else
		{
			// 방향값만 구해야 한다.
			Vector3 dir = _target.position - transform.position; // 방향 구하기 (A-B) = A <- B 바라본다.
			dir.y = 0.0f;
			dir.Normalize(); // (크기를 날려버리는 계산 - 방향만 구함)
			
			// 델타는 내부적으로 처리해서 델타 타임은 안곱해줘도 된다.
			// move() : 복잡한 이동 : 점프가 포함될때
			// simplemove()  : 점프가 포함 안될때 (x, y)만 이동할때 ..
			// 거미는 점프가 없다. 
			_characterController.SimpleMove(dir * _speed);
			
			// 타겟을 바라보게 하는 계산(Quaternion.Lerp) - 바라보고 있는 방향을 천천히 자연스럽게 움직이도록 해준다.(유도탄등에 응용)
			// Quaternion.Lerp : 보간을 하는 계산
			// 첫번째 인자 : 바라보는 축
			// 두번째 인자 : 내가 보고자 하는 축
			// 세번째 인자 : 0~1 사이에 있는 수자로 만든다. 
			transform.rotation = Quaternion.Lerp(transform.rotation, 
			                                     Quaternion.LookRotation(dir),
			                                     _roationSpeed * Time.deltaTime);
		}
	}
	
	void State_Patrol()
	{
		animation.Play("walk");
		
		_patorlTime += Time.deltaTime;
		
		Vector3 a = new Vector3(0.0f, 0.0f, 0.0f);
		if(_endPostion ==  a)
		{
			Debug.Log("State_Patrol init");
			_endPostion = new Vector3(Random.Range(1.0f, 20.0f), 0, Random.Range(1.0f, 20.0f));
		}
		
		float distance = (_endPostion - transform.position).magnitude;
		
		if(distance <= 0.3)
		{
			Debug.Log("State_Patrol _endPostion == transform.position");
			_stateTime = 0.0f;
			_spdierState = SPIDERSTATE.IDLE;
			_endPostion = new Vector3(0.0f, 0.0f, 0.0f);
		}
		else
		{
			Debug.Log("State_Patrol ing : " + distance);
			Vector3 dir = _endPostion - transform.position;
			dir.y = 0.0f;
			dir.Normalize();
			_characterController.SimpleMove(dir * _speed);
			transform.rotation = Quaternion.Lerp(transform.rotation, 
			                                     Quaternion.LookRotation(dir),
			                                     _roationSpeed * Time.deltaTime);
		}
	}
	
	void State_Attack()
	{
		Debug.Log("State_Attack");
		
		// 공격의 텀을 주기 위해 델타타임을 계산한다.
		_stateTime += Time.deltaTime;
		if(_stateTime > _attackStateMaxTime)
		{
			_stateTime = 0.0f;
			animation.Play("attack_Melee");
			
			// animation.PlayQueued : 애니매이션이 끝난 후 바로 재생할 애니매이션을 등록해 둔다.
			// 그렇지 않고 상태를 바꾸면 애니가 끝나기 전에 바꾼 상태의 애니가 재생된다.
			// QueueMode.CompleteOthers : 
			animation.PlayQueued("iddle", QueueMode.CompleteOthers);
			
			// magnitude : CPU 연산이 많다.
			// 단순 크다/작다만 필요할 경우 magnitude를 사용하지 않는
			// sqrMagnitude는 루트 연산이 없다.
			// 정확한 거리가 필요한게 아니면 sqrMagnitude를 쓰는게 권장사항.
			float distance = (_target.position - transform.position).magnitude;
			//float distance = (_target.position - transform.position).sqrMagnitude;
			if(distance > _attackableRange)
			{
				_spdierState = SPIDERSTATE.MOVE;
				//_spdierState = SPIDERSTATE.IDLE; : 너무 빠릴 쫓아오면..
			}
			else
				_playerState.DamageByEnemy();
		}
	}
	
	void State_Damage()
	{
		Debug.Log(_healthPoint);
		--_healthPoint;
		
		animation["damage"].speed = 0.5f; //레거시 애니매이션에서 속도 조정 (play)직전에 바꾸고 시작하자.
		animation.Play("damage");
		
		animation.PlayQueued("iddle", QueueMode.CompleteOthers);
		
		_stateTime = 0.0f;
		_spdierState = SPIDERSTATE.IDLE;
		
		if(_healthPoint <=0)
		{
			_spdierState = SPIDERSTATE.DEAD;
		}
	}
	
	void State_Dead()
	{

		//Destroy(gameObject);
		StartCoroutine(DeadProcess());
		_spdierState = SPIDERSTATE.NONE;

	}

	IEnumerator DeadProcess()
	{
		animation["death"].speed = 0.5f; //애니 타임이 짧기 때문에 약간 시간을 준다.
		animation.Play("death");

		while(animation.isPlaying)
		{
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(1.0f);

		GameObject expolsionObj = Instantiate(explosionParticle) as GameObject;
		Vector3 explosionObjPos = transform.position;
		explosionObjPos.y = 0.6f;
		expolsionObj.transform.position = explosionObjPos;

		yield return new WaitForSeconds(0.5f);

		GameObject deadObj = Instantiate(deadObject) as GameObject;
		Vector3 deadObjPos = transform.position;
		deadObjPos.y = 0.6f;
		deadObj.transform.position = deadObjPos;

		Destroy(gameObject);

	}
}
