using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _monsterReference;
    [SerializeField] private Transform _leftPosition, _rightPosition;
    private GameObject _spawnedEnemies;
    private string _walkAnimation = "Walk";
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;

    private int _randomIndex;
    private int _randomSide;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            _randomIndex = Random.Range(0, _monsterReference.Length);
            _randomSide = Random.Range(0, 2);

            _spawnedEnemies = Instantiate(_monsterReference[_randomIndex]);

            if (_randomSide == 0)//if monster stau in the left
            {
                _spawnedEnemies.transform.position = _leftPosition.position;
                _spawnedEnemies.GetComponent<Monster>()._movementX = Random.Range(4, 15);
                _spawnedEnemies.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else//if monster stau in the right
            {
                _spawnedEnemies.transform.position = _rightPosition.position;
                _spawnedEnemies.GetComponent<Monster>()._movementX = Random.Range(-4, -15);
            }
        }
    }

    void Update()
    {
        
    }
}
