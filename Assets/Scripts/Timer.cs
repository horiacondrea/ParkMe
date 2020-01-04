using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Sprite[] _seconds;

    Image _timeImage;
    int _timeLeft = 5;
    bool _running = true;

    public event Action OutOfTime;

    void Start()
    {
        _timeImage = GetComponent<Image>();
        _timeImage.sprite = _seconds[_timeLeft];

        StartCoroutine(LoseTime());
    }

    public void StopTimer()
    {
        _running = false;
    }

    public bool StillRunning()
    {
        return _running;
    }

    IEnumerator LoseTime()
    {
        while (_running)
        {
            yield return new WaitForSeconds(1);
            _timeLeft--;

            _timeImage.sprite = _seconds[_timeLeft];

            if (_timeLeft == 0)
            {
                _running = false;
                StopCoroutine(LoseTime());

                OutOfTime();
            }
        }
    }
}
