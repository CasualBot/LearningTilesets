using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpForce = 1.0f;
    [SerializeField] bool jumping = false;
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        var movementX = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed_f", movementX);
        transform.Translate(Vector3.right * Time.deltaTime * movementX * speed);
        if (!jumping)
        {
            var jump = Input.GetAxis("Jump");
            rb.AddForce(Vector3.up * jumpForce * jump, ForceMode2D.Impulse);
        }

        if(Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jumping_b", true);

        }
        if (Input.GetButtonDown("Horizontal"))
        {
            animator.SetBool("Moving_bool", true);
        }
        else
        {
            animator.SetBool("Moving_bool", false);
        }



        if(movementX > 0 )
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        } else if(movementX < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                jumping = false;
                animator.SetBool("Jumping_b", false);
                break;
            case "Obstacle":
                jumping = false;
                animator.SetBool("Jumping_b", false);

                //Debug.LogFormat("Collided with obstacle: {0}", collision.gameObject.name);
                break;
            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag) {
            case "Ground":
                jumping = true;
                animator.SetBool("Jumping_b", true);
                break;
            case "Obstacle":
                jumping = true;
                animator.SetBool("Jumping_b", true);
                //Debug.LogFormat("No longer colliding with obstacle: {0}", collision.gameObject.name);
                break;
            default:
                break;
        }

    }
}
