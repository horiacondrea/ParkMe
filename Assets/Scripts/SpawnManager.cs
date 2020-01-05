using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const int MAX_X_CELLS = 5;
    private const int MAX_Y_CELLS = 3;

    private const int MAX_X = 25;
    private const int MAX_Y = 12;

    private const float GARAGE_WIDTH = 3.6f;
    private const float GARAGE_HEIGHT = 2f;

    private const float PLAYER_WIDTH = 1f;
    private const float PLAYER_HEIGHT = 1.4f;

    private const float OBSTACLE_WIDTH = 3f;
    private const float OBSTACLE_HEIGHT = 0.5f;

    //private const float OVERPLUS = 1f;

    [SerializeField]
    GameObject _playerPrefab;
    [SerializeField]
    GameObject _garagePrefab;
    [SerializeField]
    GameObject _obstaclePrefab;

    GameObject m_Player;

    bool[][] _screenMatrix = new bool[MAX_X_CELLS][];
    Vector2 _screenSize = new Vector2(MAX_X, MAX_Y);

    float _stepX;
    float _stepY;

    struct Position
    {
        public Vector3 _position;
        public Vector2 _cells;

        public Position(Vector3 position, Vector2 cells)
        {
            _position = position;
            _cells = cells;
        }
    }

    void Start()
    {
        _stepX = _screenSize.x / MAX_X_CELLS;
        _stepY = _screenSize.y / MAX_Y_CELLS;

        for(int i = 0; i < MAX_X_CELLS; ++i)
        {
            _screenMatrix[i] = new bool[MAX_Y_CELLS];
        }

        foreach(bool[] array in _screenMatrix)
        {
           for(int i = 0; i < MAX_Y_CELLS; ++i)
            {
                array[i] = false;
            }
        }
    }

    public void Spawn(int level)
    {
        Position garagePosition = CalculatePosition(GARAGE_WIDTH, GARAGE_HEIGHT, 0f);
        GameObject garage =  Instantiate(_garagePrefab, garagePosition._position, Quaternion.identity);
        if(garagePosition._cells.y == 0)
        {
            garage.transform.localScale = new Vector3(0.45f, -0.35f, 0.35f);
            garage.transform.Rotate(new Vector3(0, 0, Random.Range(-35f, 35f)));
        }
      
        for(int i = 0; i < level; ++i)
        {
            GameObject obstacle = Instantiate(_obstaclePrefab, CalculatePosition(OBSTACLE_WIDTH, OBSTACLE_HEIGHT, 0f)._position, Quaternion.identity);
            obstacle.transform.Rotate(new Vector3(0, 0, Random.Range(-50f,50f)));
        }

        m_Player = Instantiate(_playerPrefab, CalculatePosition(PLAYER_WIDTH, PLAYER_HEIGHT, 0f)._position, Quaternion.identity);
    }

    Position CalculatePosition(float width, float height, float overplus)
    {
        int X_Cell = 0;
        int Y_Cell = 0;
        do
        {
            X_Cell = Random.Range(0, MAX_X_CELLS);
            Y_Cell = Random.Range(0, MAX_Y_CELLS);

        } while (_screenMatrix[X_Cell][Y_Cell] == true);

        _screenMatrix[X_Cell][Y_Cell] = true;

        float minX = X_Cell * _stepX + (width * 0.5f) + overplus;
        float maxX = (X_Cell * _stepX) + _stepX - ((height * 0.5f) - overplus);

        float minY = Y_Cell * _stepY;
        float maxY = (Y_Cell * _stepY) + _stepY;

        float X = Random.Range(minX, maxX) - (_screenSize.x * 0.5f);
        float Y = Random.Range(minY, maxY) - (_screenSize.y * 0.5f);
        
        return new Position(new Vector3(X, Y), new Vector2(X_Cell, Y_Cell));
    }

    public GameObject GetPlayer()
    {
        return m_Player;
    }
}
