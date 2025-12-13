using UnityEngine;

public class CarAI : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform[] waypoints;
    public float speed = 5f;
    public float turnSpeed = 360f;
    public float reachDistance = 0.2f;

    private int currentIndex = 0;
    private bool reachedEnd = false;      // for normal end-of-path stop

    [Header("Collision / Bounce")]
    public float bounceSpeed = 4f;
    public float bounceDuration = 0.3f;

    private float bounceTimer = 0f;
    private Vector3 bounceDirection;
    private bool stoppedAfterBounce = false;  // after collision bounce, never move again

    void Update()
    {
        // If we already bounced and stopped, do nothing forever
        if (stoppedAfterBounce)
            return;

        // Handle bounce movement first
        if (bounceTimer > 0f)
        {
            transform.position += bounceDirection * bounceSpeed * Time.deltaTime;
            bounceTimer -= Time.deltaTime;

            if (bounceTimer <= 0f)
            {
                bounceTimer = 0f;
                // after bounce, stop permanently
                stoppedAfterBounce = true;
            }

            return; // skip any path-following while bouncing
        }

        // Normal path-following (no colli)
        if (waypoints == null || waypoints.Length == 0 || reachedEnd)
            return;

        Transform target = waypoints[currentIndex];

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        // Rotate to face movement direction (sprite facing up)
        if (dir.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                turnSpeed * Time.deltaTime
            );
        }

        // Waypoint reached?
        if (Vector3.Distance(transform.position, target.position) < reachDistance)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length)
            {
                reachedEnd = true; // stopped because reached last waypoint (no colli)
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only react if we hit the player
        if (!collision.collider.CompareTag("Player"))
            return;

        // Direction away from the player
        bounceDirection = (transform.position - collision.transform.position).normalized;
        bounceTimer = bounceDuration;

        // Stop any further path-following, even if bounce ends early
        reachedEnd = true;

        // Tell the player to bounce & lose control briefly 
        Driver playerDriver = collision.collider.GetComponent<Driver>();
        if (playerDriver != null)
        {
            Vector3 dirForPlayer = (collision.transform.position - transform.position).normalized;
            playerDriver.ApplyKnockback(dirForPlayer, bounceDuration);
        }
    }
}
