using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    public Text announcementText; // Reference to the Text component
    private float messageDisplayTime = 2f; // Duration for which the message is displayed
    private float messageTimer; // Timer to track message duration

    // Method to display a message
    public void DisplayMessage(string message)
    {
        announcementText.text = message; // Update the text
        messageTimer = messageDisplayTime; // Reset the timer whenever a new message is displayed
    }

    void Update()
    {
        // Count down the message display time
        if (messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0)
            {
                announcementText.text = ""; // Clear the message after the timer ends
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Display "Ouch" if the car hits something
        DisplayMessage("Ouchhh!!!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Display messages based on what the car collides with
        if (other.CompareTag("Package"))
        {
            DisplayMessage("Package Picked Up!!");
            Destroy(other.gameObject); // Destroy the package object
        }
        else if (other.CompareTag("Customer"))
        {
            DisplayMessage("Package Delivered!!");
            // Add more logic here for delivering the package
        }
    }
}
