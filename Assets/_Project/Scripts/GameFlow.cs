using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public static event Action<Plant> onRestart = delegate {  };
    public static event Action onFullyGrown = delegate {  };
    public static event Action<bool> onPause = delegate {  };
    
    public void Restart(Plant plant) => onRestart?.Invoke(plant);
    public void FullyGrown() => onFullyGrown?.Invoke();
    public void Pause(bool pause) => onPause?.Invoke(pause);
}
