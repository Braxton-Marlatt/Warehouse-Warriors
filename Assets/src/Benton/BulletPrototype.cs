using UnityEngine;

public class BulletPrototype : MonoBehaviour, IPrototype<GameObject>
{
    public GameObject Clone()
    {
        return Instantiate(this.gameObject);
    }
}
