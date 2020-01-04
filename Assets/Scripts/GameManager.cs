using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool m_campaign;

    Timer _timer;
    Player _player;

    GameObject _retry;
    GameObject _exit;
    GameObject _next;

    static int _level = 0;
    bool parked = false;

    [SerializeField]
    AudioClip _nextLevelAudio;
    [SerializeField]
    AudioClip _gameOverAudio;
    AudioSource _audioSource;

    [SerializeField] int m_currentLevel;

    void Start()
    {
        if (!m_campaign)
        {
            SpawnManager _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            if (_spawnManager == null)
            {
                Debug.Log("ObstacleManager is NULL");
            }
            _spawnManager.Spawn(_level);
        }

        _timer = GameObject.Find("Canvas/Timer").GetComponent<Timer>();
        if (_timer == null)
        {
            Debug.Log("Timer is NULL");
        }
        _timer.OutOfTime += _timer_OutOfTime;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Player is NULL");
        }
        _player.Parked += _player_Parked;

        _retry = GameObject.Find("Canvas/Retry");
        if (_retry == null)
        {
            Debug.Log("Retry is NULL");
        }
        _retry.SetActive(false);

        _exit = GameObject.Find("Canvas/Exit");
        if (_exit == null)
        {
            Debug.Log("Exit is NULL");
        }
        _exit.SetActive(false);

        _next = GameObject.Find("Canvas/Next");
        if (_next == null)
        {
            Debug.Log("Next is NULL");
        }
        _next.SetActive(false);

        _audioSource = GetComponent<AudioSource>();
    }

    private void _player_Parked()
    {
        if (_timer.StillRunning())
        {
            _retry.SetActive(true);
            _next.SetActive(true);

            _timer.StopTimer();

            parked = true;

            _audioSource.clip = _nextLevelAudio;
            _audioSource.Play();
        }
    }

    public void NextLevel()
    {
        if (m_campaign)
        {
            int nextLevel = m_currentLevel + 1;
            SceneManager.LoadScene("Level_" + nextLevel);
        }
        else
        {
            _level++;
            SceneManager.LoadScene("Game");
        }
    }

    public void Retry()
    {
        if (m_campaign)
        {
            SceneManager.LoadScene("Level_" + m_currentLevel);
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }

    private void _timer_OutOfTime()
    {
        if (!parked)
        {
            _player.OutOfTime();

            _retry.SetActive(true);
            _exit.SetActive(true);

            _level = 0;

            _audioSource.clip = _gameOverAudio;
            _audioSource.Play();
        }
    }
}
