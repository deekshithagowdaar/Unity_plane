using UnityEngine;

public class planecontrol : MonoBehaviour
{
    public float speed = 20f;               // Forward speed of the plane
    public float takeoffSpeed = 5f;         // Vertical speed during takeoff
    public float landingSpeed = 3f;         // Vertical speed during landing
    public float maxAltitude = 20f;         // Maximum altitude during flight
    public float groundLevel = 0.5f;        // Ground level (minimum altitude)
    private float currentAltitude = 0f;     // Current altitude of the plane

    private bool isTakingOff = false;       // Is the plane taking off?
    private bool isLanding = false;         // Is the plane landing?

    void Update()
    {
        // Forward movement
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check for input to start takeoff
        if (Input.GetKeyDown(KeyCode.T) && !isTakingOff && !isLanding)
        {
            isTakingOff = true;
            isLanding = false; // Disable landing during takeoff
        }
        
        // Check for input to start landing
        if (Input.GetKeyDown(KeyCode.L) && !isLanding && !isTakingOff && currentAltitude > groundLevel)
        {
            isLanding = true;
            isTakingOff = false; // Disable takeoff during landing
        }

        // Handle takeoff
        if (isTakingOff)
        {
            TakeOff();
        }

        // Handle landing
        if (isLanding)
        {
            Land();
        }
    }

    private void TakeOff()
    {
        // Increase altitude until reaching maxAltitude
        if (currentAltitude < maxAltitude)
        {
            transform.Translate(Vector3.up * takeoffSpeed * Time.deltaTime);
            currentAltitude += takeoffSpeed * Time.deltaTime;
        }
        else
        {
            isTakingOff = false; // Stop takeoff when max altitude is reached
        }
    }

    private void Land()
    {
        // Decrease altitude gradually until reaching ground level
        if (currentAltitude > groundLevel)
        {
            transform.Translate(Vector3.down * landingSpeed * Time.deltaTime);
            currentAltitude -= landingSpeed * Time.deltaTime;
        }
        else
        {
            // Reset landing state and altitude
            isLanding = false;
            currentAltitude = groundLevel; // Snap to ground level to avoid floating
        }
    }
}
