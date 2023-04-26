using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public static event Action onRestart = delegate {  };
    public static event Action onFullyGrown = delegate {  };
    
    public void Restart() => onRestart?.Invoke();
    public void FullyGrown() => onFullyGrown?.Invoke();
}
