using UnityEngine;

public class MinimapNodePrototype : MonoBehaviour
{
    // The Clone method is the core of the Prototype design pattern.
    // It allows the code to create new instances of objects by copying an existing object (this object) instead of creating them from scratch.
    // This enables flexibility as we don't have to manually instantiate every object in the game; instead, we use a prototype to clone objects.

    // Clone method that returns a new copy of the current object
    public virtual MinimapNodePrototype Clone(Vector3 position, Transform parent)
    {
        GameObject clone = Instantiate(gameObject, parent);
        clone.transform.localPosition = position;
        return clone.GetComponent<MinimapNodePrototype>();
    }
}
