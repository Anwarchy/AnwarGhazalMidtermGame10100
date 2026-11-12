using UnityEngine.SceneManagement;                                                   // Lets this script reload the current scene.
using UnityEngine;                                                                   // Gives this script access to Unity types.
using UnityEngine.UI;                                                                // Gives this script access to the legacy UI Text component.

public class LogicScript : MonoBehaviour                                             // Lets this script be attached to a GameObject.
{
    public int playerScore;                                                          // Stores the player's current score.
    public Text scoreText;                                                           // Displays the score on screen.
    public GameObject gameOverScreen;                                                // Holds the game-over screen object.
    public AudioSource musicSource;                                                  // Plays the background and game-over music.
    public AudioClip gameplayMusic;                                                  // Holds the background music loop.
    public AudioClip gameOverMusic;                                                  // Holds the game-over music loop.

    public float startingPipeSpeed = 5f;                                             // Sets the pipe speed when a game starts.
    public float speedIncreasePerLevel = 1f;                                         // Sets how much the pipe speed increases each level.
    public int pointsPerLevel = 5;                                                   // Sets how many points are needed to reach the next level.
    public AudioSource levelUpSource;                                                // Plays the level-up sound.
    public AudioClip levelUpSound;                                                   // Holds the level-up sound clip.

    void Start()                                                                     // Unity calls this when the Logic Manager starts.
    {
        playerScore = 0;                                                             // Resets the score when the scene starts.
        MovePipeScript.currentMoveSpeed = startingPipeSpeed;                         // Resets pipe speed when a new game starts.

        if (scoreText != null)                                                       // Check that the score text is assigned.
            scoreText.text = playerScore.ToString();                                 // Display the starting score.

        if (musicSource != null && gameplayMusic != null)                            // Check that the source and gameplay clip are assigned.
        {
            musicSource.clip = gameplayMusic;                                        // Set the Audio Source to the gameplay music.
            musicSource.loop = true;                                                 // Repeat the gameplay music.
            musicSource.Play();                                                      // Start the gameplay music.
        }
    }

    public void addScore(int scoreToAdd)                                             // Adds points and checks whether the player reached a new level.
    {
        if (pointsPerLevel <= 0)                                                     // Make sure the level threshold is valid.
        {
            Debug.LogError("Points Per Level must be greater than zero.");           // Explain the invalid setting.
            return;                                                                  // Stop before dividing by zero.
        }

        int previousLevel = playerScore / pointsPerLevel;                            // Record the current level before adding points.
        playerScore += scoreToAdd;                                                   // Update the stored score.

        if (scoreText != null)                                                       // Check that the score text is assigned.
            scoreText.text = playerScore.ToString();                                 // Show the updated score.

        int newLevel = playerScore / pointsPerLevel;                                 // Calculate the level after adding points.

        if (newLevel > previousLevel)                                                // Check whether the score crossed a level threshold.
        {
            MovePipeScript.currentMoveSpeed = startingPipeSpeed + newLevel * speedIncreasePerLevel; // Increase pipe speed for the new level.

            if (levelUpSource != null && levelUpSound != null)                       // Check that the level-up audio is assigned.
                levelUpSource.PlayOneShot(levelUpSound);                             // Play the level-up sound once.
        }
    }

    public void restartGame()                                                        // This is the method the Play Again button calls.
    {
        Time.timeScale = 1f;                                                         // Make sure the game is no longer paused.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);            // Reload the current scene.
    }

    public void gameOver()                                                           // Called when Rabbit collides with something.
    {
        if (gameOverScreen != null)                                                  // Check that the game-over screen is assigned.
            gameOverScreen.SetActive(true);                                          // Show the game-over screen.

        if (musicSource != null && gameOverMusic != null)                            // Check that the source and game-over clip are assigned.
        {
            musicSource.Stop();                                                      // Stop the gameplay music.
            musicSource.clip = gameOverMusic;                                        // Set the Audio Source to the game-over music.
            musicSource.loop = true;                                                 // Repeat the game-over music.
            musicSource.Play();                                                      // Start the game-over music.
        }
    }
}