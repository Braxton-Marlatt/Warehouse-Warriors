using UnityEngine;

public class EnemyDeathHandlerSub : EnemyDeathHandler
{

    // In C#, the virtual keyword marks a method in a base class as overridable, allowing subclasses to provide their own implementation.
    protected override void HandleEnemyDeath(EnemyHealth enemyHealth, Enemy enemy)
    {
        FindObjectOfType<CoinManager>().AddCoin();
        base.HandleEnemyDeath(enemyHealth, enemy);
    }
}