using UnityEngine;
using System;
using System.Collections.Generic;

public static class EventHandler
{
    public static event Action OnGameLose; 
    public static event Action OnGameWin;
    public static event Action<int> OnWinCountdown;
    public static event Action<List<GameObject>> OnBlockChange;

    public static void WhenGameLose() => OnGameLose?.Invoke();
    public static void WhenGameWin() => OnGameWin?.Invoke();
    public static void WhenWinCountdown(int num) => OnWinCountdown?.Invoke(num);
    public static void WhenBlockChange(List<GameObject> blockPrefabList) => OnBlockChange?.Invoke(blockPrefabList);
}