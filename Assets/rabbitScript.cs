using UnityEngine;                                                                 // Gives script access to Unity types like Rigidbody and Vector2

public class rabbitScript : MonoBehaviour                                          // Lets the script be attached to GameObject Rabbit 
{
    public Rigidbody2D myRigidbody;
    public float rabHop = 9f;                                                      // This is where we gague the hop strenth UP in the inspector
    public float rabDrop = 20f;                                                    // This is where we gague the hop strenth Down
    public float maxFallSpeed = 18f;                                               // This is where we gague the Down hop speed
    public SpriteRenderer rabbitSprite;                                            // The renderer that controls Rabbit's color
    public LogicScript logic;                                                      // So Rabbit script and logic script can talk to eachother
    public bool RabbitIsAlive = true;                                              // Yes/No statement checking if Rabbit is alive or dead

    public AudioSource soundEffectsSource;                                         // This is the Audio Source for Sound Effects Baby! 
    public AudioClip impactSound;                                                  // Sound clip of rabbits collision with pipes

    void Start()                                                                   // Unity calls this once when the Rabbit object starts.
    {
        if (myRigidbody == null)                                                   // Check whether a Rigidbody2D was assigned in the Inspector.
            myRigidbody = GetComponent<Rigidbody2D>();                             // Find the Rigidbody2D attached to this GameObject.

        if (logic == null)                                                         // Check whether the LogicScript was assigned in the Inspector.
        {
            GameObject logicObject = GameObject.FindGameObjectWithTag("logic");    // Find the GameObject tagged "logic".
            if (logicObject != null)                                               // Make sure a GameObject with that tag was found.
                logic = logicObject.GetComponent<LogicScript>();                   // Get its LogicScript component.
        }

        if (rabbitSprite == null)                                                  // Check whether Rabbit's SpriteRenderer was assigned in the Inspector.
            rabbitSprite = GetComponent<SpriteRenderer>();                         // Find the SpriteRenderer attached to this GameObject.
    }
    void Update()
    {
        if (!RabbitIsAlive || myRigidbody == null)                                 // Stop here if Rabbit is dead or is missing a RigidBody2d
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            myRigidbody.velocity = Vector2.up * rabHop;                            // When the down arrow is held accelerate the downward speed
    }

    void FixedUpdate()
    {
        if (!RabbitIsAlive || myRigidbody == null)                                 // Stop here if Rabbit is dead or is missing a RigidBody2d                               
            return;

                                                                                   // Hold the Down Arrow to accelerate the rabbit downward.
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float newYVelocity =
                myRigidbody.velocity.y - rabDrop * Time.fixedDeltaTime;            // Subtract from vertical velocity to accelerate Downward

            myRigidbody.velocity = new Vector2(
                myRigidbody.velocity.x,
                Mathf.Max(newYVelocity, -maxFallSpeed));                             // Keep the horizontal velocity unchanged and cap the downward speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)                         // Ignore further collisions after the rabbit has already died
    {
        if (!RabbitIsAlive)
            return;

        RabbitIsAlive = false;                                                     // Marks the Rabbit Dead
        if (rabbitSprite != null)
            rabbitSprite.color = Color.white;
        if (soundEffectsSource != null && impactSound != null)                     // Play the impact sound, if both audio references are assigned
            soundEffectsSource.PlayOneShot(impactSound);

        if (logic != null)
            logic.gameOver();                                                      // Tell the game logic to Start game over behavior, if assigned
    }
}