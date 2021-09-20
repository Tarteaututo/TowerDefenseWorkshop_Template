using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Faire une class Timer qui prends une durée, 
///  peut être update dans le temps (== on compte les secondes) 
///  et qui indique quand la durée a été atteinte
///  
/// // Start()
/// // Stop()
/// 
/// // Update()
/// 
/// Bonus
/// // Pause()
/// </summary>

[System.Serializable]
public class Timer
{
    [SerializeField]
    private float _duration = 1;

    [System.NonSerialized]
    private float _currentDuration = 0f;

    [System.NonSerialized]
    private bool _isRunning = false;

    public delegate void TimerEvent();
    public event TimerEvent Stopped = null;
    
    // Constructor
    public Timer(float duration)
    {
        _duration = duration;
    }

    //// == more or less useless in unity context
    //// Destructor
    //~Timer()
    //{
    //}

    public float GetDuration()
    {
        return _duration;
    }

    public bool IsRunning()
    {
        return _isRunning;
    }

    public void Start()
    {
        _currentDuration = _duration;
        _isRunning = true;
    }

    public void Start(float duration)
    {
        _duration = duration;
        _currentDuration = _duration;
        _isRunning = true;
    }

    public void Stop()
    {
        _currentDuration = 0;
        _isRunning = false;
        Stopped?.Invoke();
        //if (Stopped != null) // Strictement égal à Stopped?.Invoke();
        //{
        //    Stopped.Invoke();
        //}
    }

    public bool Update()
    {
        if (_isRunning == false)
        {
            return false;
        }

        _currentDuration -= Time.deltaTime;

        if (_currentDuration <= 0)
        {
            Stop();
            return true;
        }
        return false;
    }
}
