using UnityEngine;                                                              // Gives the script access to unity features, like Monobehavior, vector2, ext.

public class PipeSpawnScript : MonoBehaviour                                    // Declares a unity component named PipeSpawnScript. You can attach to gameObject in your scene.
{
    public GameObject Pipe;                                                     // A reference to the pipe prefab. 

    [Tooltip("Base time between pipe spawns, in seconds.")]                     // Sets the base delay between pipe spawns 3f -means 3 seconds.
    public float spawnRate = 3f;

    [Tooltip("Lower values bring pipe pairs closer together. Try 0.85.")]       // Adds an Inspector slider for adjusting pipe spacing values from 1.2 to 3.2
    [Range(1.2f, 3.2f)]
    public float pipeSpacing = 1.2f;

    [Tooltip("How far above or below the spawner pipes can appear.")]           // Controls how far the random pipe positioncan be above or below the spawners height
    public float heightOffset = 10f;

    [Tooltip("The pipe speed at the start of the game.")]                       // Stores the pipe speed the game starts with. The script uses this to adjust how often the pipes 
    public float startingPipeSpeed = 3f;                                        // spawn as their movement speed changes.

    private float timer;                                                        // Tracks how much time has passed since the previous spawn. private-means other scripts and inspector 
                                                                                // cannot access it directly
    void Start()                                                                // Unity calls start once when this component begins running.
    {                                                                           // This makes the first pipe pair appear right away. 
        spawnPipe();
    }

    void Update()                                                               // Unity calls updaye once per frame while the component is active.
    {
        float currentSpeed = Mathf.Max(MovePipeScript.currentMoveSpeed, 0.01f); // Reads the current pipe speed from MovePipeScript. Mathf.Max makes sure that the value is atlest 0.01f
                                                                                 // avoiding a zero speed in calculation below. Shorten the spawn interval as pipe speed rises, so pipes
                                                                                 // so pipes don't become further apart the more levels are reached.
        float adjustedSpawnRate =
            spawnRate * (startingPipeSpeed / currentSpeed) * pipeSpacing;

        timer += Time.deltaTime;                                                 // Adds the time since the previous frame to timer.Time deltatime makes the timing work consistantly
                                                                                  //across different frame rates.
        if (timer >= adjustedSpawnRate)                                          // Checks whether enough time has passed to spawn another pipe pair.
        {
            spawnPipe();
            timer = 0f;                                                          // Creates the next pipe pair, thenresets the times to start counting toward the following spawn.
        }                                                                                  
    }

    void spawnPipe()                                                             // Defines a helper method that creates onepipe pair,It's called once in start()and again whenever the time reaches the spawn delay.
    {
        float randomY = Random.Range(
            transform.position.y - heightOffset,
            transform.position.y + heightOffset);                                // Chooses a random vertical position between the spawners height minus the offset 
        Instantiate(Pipe,new Vector3(transform.position.x,                       // and it's height plus offset.

        randomY, transform.position.z),transform.rotation);                      // Creates a copy of the pipe prefab at the spawners horizontal and depth positions 
    }                                                                            // using the chosen random height and the spawners rotation
}