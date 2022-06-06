using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Rigidbody2D rb;
  private BoxCollider2D coll;
  private Animator animator;
  private SpriteRenderer sprite;
  private Transform transform;

  [SerializeField] private AudioSource jumpSoundEffect;

  [SerializeField] private LayerMask jumpableGround;

  [SerializeField] private float mooveSpeed = 7f, jumpForce = 9f;

  private float dirX = 0f;

  private enum MovementState
  {
    idle,
    running,
    jumping,
    falling
  }

  // Start is called before the first frame update
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    coll = GetComponent<BoxCollider2D>();
    animator = GetComponent<Animator>();
    sprite = GetComponent<SpriteRenderer>();
    transform = GetComponent<Transform>();
  }

  // Update is called once per frame
  private void Update()
  {
    dirX = Input.GetAxisRaw("Horizontal");
    rb.velocity = new Vector2(dirX * mooveSpeed, rb.velocity.y);

    if (Input.GetButtonDown("Jump") && IsGrounded())
    {
      /*jumpSoundEffect.Play();*/
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    UpdateAnimationState();
  }

  private void OnTriggerEnter2D(Collider2D collision)
   {
     if (collision.gameObject.CompareTag("EndFlag"))
     {
       rb.bodyType = RigidbodyType2D.Static;
       animator.enabled = false;
     }
   }


  private void UpdateAnimationState()
  {
    MovementState state;

    state = dirX != 0f ? MovementState.running : MovementState.idle;
    if (dirX < 0f)
    {
      sprite.flipX = true;
    }
    if (dirX > 0f)
    {
      sprite.flipX = false;
    }

    if (rb.velocity.y > .1f) //Jumping
    {
      state = MovementState.jumping;
    }
    else if (rb.velocity.y < -.1f) // Falling
    {
      state = MovementState.falling;
    }

    animator.SetInteger("movementState", (int)state); // cast to int
  }

  private bool IsGrounded() => Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

  public void MoveTo(Vector3 position)
  {
    transform.position = position;
  }
}
