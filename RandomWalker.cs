using UnityEngine;

public class RandomWalker : MonoBehaviour
{
    private Animator animator;
    public float walkSpeed = 1.0f;
    public float walkDistance = 5.0f;
    private float distanceWalked = 0.0f;
    private Vector3 direction = Vector3.right;
    private float waitTime = 0.0f;
    private float minX = 4f;
    private float maxX = 121f;
    public AudioSource running;
    public AudioSource money;

    void Start()
    {
        animator = GetComponent<Animator>();
        money.volume = 0.1f;
        running.volume = 0.0f;
        
    }

    void Update()
    {
        if (waitTime > 0.0f)
        {
            // If the walker is waiting, decrement the timer
            waitTime -= Time.deltaTime;
            if (waitTime <= 0.0f)
            {
                if (GameObject.FindGameObjectWithTag("Wall") == null)
                {
                    running.volume = 0.6f;
                }
                // When the timer reaches zero, start moving again
                animator.SetFloat("Speed", distanceWalked / walkDistance);
            }
            else
            {
                running.volume = 0;
                // If the timer is still counting down, stop moving
                animator.SetFloat("Speed", 0);
                return;
            }
        }

        // Calculate the amount to move in the current frame
        float moveAmount = walkSpeed * Time.deltaTime;

        // If the game object has walked the full distance in the current direction, change direction
        if (Mathf.Abs(distanceWalked) >= walkDistance)
        {
            direction *= -1.0f;
            distanceWalked = 0.0f;
            walkDistance = Random.Range(2.0f, 20.0f);
            animator.SetFloat("Horizontal", direction.x); // Set the horizontal parameter of the animator to the direction

            // Set the wait time to a random value
            waitTime = Random.Range(1.0f, 3.0f);
        }

        // Move the game object in the current direction
        Vector3 movement = new Vector3(direction.x * moveAmount, 0.0f, 0.0f);

        // Clamp the position within the range of x > 1.3 and x < 29
        Vector3 newPosition = transform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        if(newPosition.x == minX || newPosition.x == maxX){
            distanceWalked = walkDistance;
        }
        transform.position = newPosition;

        // Update the distance walked
        distanceWalked += moveAmount;

        // Set the speed parameter of the animator to the distance walked
        animator.SetFloat("Speed", distanceWalked / walkDistance);
    }

    public void Shush(){
        running.Stop();
        money.volume  = 0;
    }
}

