using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSController : MonoBehaviour
{
    [SerializeField] private Transform marblePrefab;
    [SerializeField, Range(0, 200)] private int marbleNum = 60;
    private Transform[] _marbles;
    [SerializeField] private int speed = 1;  
    [SerializeField] private int startupTime = 10;
    private bool _gameStart = false;
    private int _realStartupTime;
    void Start()
    {
        _realStartupTime = startupTime + 3;
        _marbles = new Transform[marbleNum];
        for (int i = 0; i < marbleNum; i++)
        {
            _marbles[i] = Instantiate(marblePrefab);
            _marbles[i].GetComponent<Transform>().parent = transform;
            _marbles[i].GetComponent<Transform>().position = _marbles[i].GetComponent<Transform>().parent.position;
            _marbles[i].GetComponent<MoveMarble>().direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            _marbles[i].GetComponent<MoveMarble>().direction *= speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStart == false && Time.realtimeSinceStartup > _realStartupTime)
        {
            _gameStart = true;
            foreach (var child in _marbles)
            {
                child.gameObject.GetComponent<MoveMarble>().GameStart = _gameStart;
            }
            Debug.Log("Game Started!");
        }

        if (_gameStart == false) Debug.Log("Game Starts In " + (_realStartupTime - (int)Time.realtimeSinceStartup));
    }
}