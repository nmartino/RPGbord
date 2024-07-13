using System;

public class GameEvents
{
    public static event Action OnStartGame;
    public static event Action OnEndGame;

    public static event Action<Player> OnPlayerInitialized;
    public static event Action<int> OnNewEnemyCount;
    public static event Action OnVictory;
    public static event Action<RewardResource> OnReward;
    public static void RaiseStartGame() => OnStartGame?.Invoke();
    public static void RaiseEndGame() {
        OnEndGame?.Invoke();
        Reset();
    }
    public static void PlayerInitialized(Player player) => OnPlayerInitialized?.Invoke(player);
    public static void RaiseNewEnemyCount(int count) => OnNewEnemyCount?.Invoke(count);
    public static void RaiseVictory() => OnVictory?.Invoke();
    public static void RaiseReward(RewardResource reward) => OnReward?.Invoke(reward);

    public static void Reset() {
        OnStartGame = null;
        OnEndGame = null;
        OnPlayerInitialized = null;
        OnNewEnemyCount = null;
        OnVictory = null;
        OnReward = null;
    }
}