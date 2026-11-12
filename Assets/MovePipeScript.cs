using UnityEngine;                                                             // Imports features used in Unity

public class MovePipeScript : MonoBehaviour                                    // Declares a component MovePipeScript, I attached it to the pipe prefab so the pipes can move.

  {
    public static float currentMoveSpeed = 3f;                                 // Sets the pipe movement speed to 3. Because it is static, the value belongs to the MovePipeScript 
    public float deadZone = -5f;                                               // class and is shared by all pipes. Your PipeSpawnScript also reads this value.
                                                                               // Sets the horizontal position where a pipe is far enough off screen to be removed. can be adjusted
    void Update()                                                              // Unity calls update once per frame while this component is active.
    {
        transform.position += Vector3.left * currentMoveSpeed * Time.deltaTime;// Moves the pipe to the left. Time.deltaTime behaves consistantlyacross different frames.

        if (transform.position.x < deadZone)                                   // Checks whether the pipe has moved past the  cleanup position.
        {
            Destroy(gameObject);                                               // Removes the gameObject pipe from the scene.
        }
    }
}