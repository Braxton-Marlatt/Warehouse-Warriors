using UnityEngine;

public class EnemyDeathHandlerSub : EnemyDeathHandler
{
    protected override void HandleEnemyDeath(EnemyHealth enemyHealth, Enemy enemy)
    {
        FindObjectOfType<CoinManager>().AddCoin(); // Add coins to the CoinManager
        base.HandleEnemyDeath(enemyHealth, enemy);
    }
}