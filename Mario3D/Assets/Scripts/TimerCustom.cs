
using UnityEngine;

public class TimerCustom
{
    private readonly float _delay;
    private float _timer = DEFAULT;
    private const float DEFAULT = -1;

    public TimerCustom(float _delay) {
        this._delay = _delay;
        Reset();
    }

    public bool IsDone() {
        if (_timer < 0) {
            _timer = Time.realtimeSinceStartup + _delay;
        }

        var timeIsOut = Time.realtimeSinceStartup >= _timer;

        if (timeIsOut)
            Reset();

        return timeIsOut;
    }

    public void Reset() {
        _timer = DEFAULT;
    }
}


 
