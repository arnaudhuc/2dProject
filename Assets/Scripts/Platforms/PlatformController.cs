using UnityEngine;

public class PlatformController : MonoBehaviour
{
  private SpriteRenderer sprite;
  private Transform transform;
  private Vector3 initialPosition;

  [SerializeField] private float speed = 2f;

  private bool canGoUp = false;
  private bool isTriggered = false;

  // Start is called before the first frame update
  private void Start()
  {
    sprite = GetComponent<SpriteRenderer>();
    sprite.color = Color.red;
    transform = GetComponent<Transform>();
    initialPosition = transform.position;
  }

  // Update is called once per frame
  private void Update()
  {
    canGoUp = !isTriggered && (transform.position != initialPosition);
    Debug.Log("Can go up : " + canGoUp);
    Debug.Log("isTriggered" + isTriggered);
    Debug.Log("initialPosition" + initialPosition);
    Debug.Log("transform.position" + transform.position);
    if (canGoUp)
    {
      GoToInitialPosition();
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    sprite.color = Color.blue;
    Rigidbody2D platformRigidBody = gameObject.AddComponent<Rigidbody2D>();
    platformRigidBody.freezeRotation = true;
    platformRigidBody.gravityScale = 0.1f;
    canGoUp = false;
    isTriggered = true;
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    isTriggered = false;
    sprite.color = Color.red;
    Rigidbody2D platformRigidBody = gameObject.GetComponent<Rigidbody2D>();
    Destroy(platformRigidBody);
    canGoUp = true;
  }

  private void GoToInitialPosition()
  {
    transform.position = Vector3.MoveTowards(transform.position, initialPosition, Time.deltaTime * speed);
  }
}