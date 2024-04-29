using System.Collections;
using UnityEngine;

public class Timer
{

    public float _timer { get; private set; } = 1f;
    public float _timerLength { get; private set; } = 1f;

    public bool TimerFinished => _timer.Equals(_timerLength);

    public void SetTime(float timerLength)
    {
        _timerLength = timerLength;
    }

    public IEnumerator StartTimer()
    {
        _timer = 0;

        while (_timer < _timerLength)
        {
            yield return new WaitForEndOfFrame();
            _timer += Time.deltaTime;
            _timer = Mathf.Clamp(_timer, 0, _timerLength);
            //Debug.Log("Timer: " + _timer + "  ::  " + _timerLength);
        }
    }
}
