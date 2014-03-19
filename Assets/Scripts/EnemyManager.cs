using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour 
{
	public GameObject _enemy;
	public float _spawnTime = 2.0f;

	public float _deltaSpawnTime = 2.0f;
	public int _maxSpawnCount = 10;
	int spawnCount = 1;



	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(spawnCount > _maxSpawnCount)
			return;

		_deltaSpawnTime += Time.deltaTime;

		if(_deltaSpawnTime > _spawnTime)
		{
			_deltaSpawnTime = 0.0f;

			GameObject obj = Instantiate(_enemy) as GameObject;
			float x = Random.Range(-20.0f, 20.0f);
			obj.transform.position = new Vector3(Random.Range(-20.0f, 20.0f), 0.1f, Random.Range(-20.0f, 20.0f));
			obj.name = "Enemy_" + spawnCount;

			++spawnCount;
		}
	}
}
