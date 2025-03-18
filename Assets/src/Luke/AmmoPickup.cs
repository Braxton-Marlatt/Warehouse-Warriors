using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ammo picked up by player!");

            //refill ammo here

            if (1 != null)
            { 
                //refill ammo here

                Debug.Log("Ammo picked up");
            }
            Destroy(gameObject);

        }
    }
}
