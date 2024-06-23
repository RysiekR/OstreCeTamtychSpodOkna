public class BattleState
{
    public Pokemon PlayerPokemon { get; set; }
    public Pokemon EnemyPokemon { get; set; }
    public bool IsPlayerTurn { get; set; }
    public Action OnPlayerTurnStart { get; set; }
    public Action OnEnemyTurnStart { get; set; }
    

    public BattleState(Pokemon playerPokemon, Pokemon enemyPokemon)
    {
        PlayerPokemon = playerPokemon;
        EnemyPokemon = enemyPokemon;
        IsPlayerTurn = true;
    }

    public void StartBattle()
    {
        IsPlayerTurn = true;
        OnPlayerTurnStart?.Invoke();
    }
    public void NextTurn()
    {
        IsPlayerTurn = !IsPlayerTurn;
        if (IsPlayerTurn)
        {
            OnPlayerTurnStart?.Invoke();
        }
        else
        {
            OnEnemyTurnStart?.Invoke();
        }
    }
}