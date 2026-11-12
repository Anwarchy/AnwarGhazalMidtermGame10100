using UnityEngine;                                                                // Gives this script access to Unity types.

public class PipeMiddleScript : MonoBehaviour                                     // Lets this script be attached to the pipe's middle object.
{
    public LogicScript logic;                                                     // Holds a reference to the game's LogicScript.

    void Start()                                                                  // Unity calls this when the pipe middle starts.
    {
        if (logic == null)                                                        // Check whether LogicScript was assigned in the Inspector.
        {
            GameObject logicObject = GameObject.FindGameObjectWithTag("logic");   // Find the object tagged "logic".

            if (logicObject != null)                                              // Check whether that object was found.
                logic = logicObject.GetComponent<LogicScript>();                  // Get its LogicScript component.
        }

        if (logic == null)                                                       // Check whether LogicScript is still missing.
            Debug.LogError("PipeMiddleScript could not find LogicScript. Check the Logic Manager's tag and component."); // Report the missing reference.
    }

    private void OnTriggerEnter2D(Collider2D collision)                          // Unity calls this when an object enters the pipe trigger.
    {
        GameObject rabbitObject = collision.attachedRigidbody != null            // Check whether the collider belongs to a Rigidbody2D.
            ? collision.attachedRigidbody.gameObject                             // Use the object that owns the Rigidbody2D.
            : collision.gameObject;                                              // Otherwise, use the collider's own object.

        if (rabbitObject.layer != LayerMask.NameToLayer("Rabbit"))               // Check whether the entering object is on the Rabbit layer.
            return;                                                              // Ignore objects on other layers.

        if (logic == null)                                                       // Check whether LogicScript is available.
        {
            Debug.LogError("PipeMiddleScript cannot add a point because LogicScript is missing."); // Report why scoring cannot happen.
            return;                                                              // Stop before trying to call a method on a missing reference.
        }

        logic.addScore(1);                                                       // Add one point.
    }
}