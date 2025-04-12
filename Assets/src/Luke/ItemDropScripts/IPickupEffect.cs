// Description: Interface for pickup effects in the game. Classes implementing this interface should define how the effect is applied to the player.

using UnityEngine;

public interface IPickupEffect
{
    void ApplyEffect(GameObject player);
}
