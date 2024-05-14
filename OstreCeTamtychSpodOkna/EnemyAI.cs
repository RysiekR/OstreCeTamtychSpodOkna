﻿public class EnemyAI
{
    private BattleState battleState;

    public EnemyAI(BattleState state)
    {
        battleState = state;
    }

    public void TakeTurn()
    {
        if (battleState.EnemyPokemon.stats.Hp < battleState.EnemyPokemon.stats.maxHp * 0.3)
        {
            HealSkill healSkill = battleState.EnemyPokemon.ChooseHealSkill();
            if (healSkill != null && healSkill.CanUse)
            {
                healSkill.Use(battleState.PlayerPokemon);
                return;
            }
        }
        OffensiveSkill offensiveSkill = battleState.EnemyPokemon.ChooseOffensiveSkill();
        if (offensiveSkill != null)
        {
            offensiveSkill.Use(battleState.PlayerPokemon);
        }
    }
}
