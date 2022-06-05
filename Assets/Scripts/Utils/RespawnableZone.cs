using UnityEngine;

public class RespawnableZone : MonoBehaviour
{
  [SerializeField] private GameObject waypoints;

  private Rigidbody2D rb;

  // Start is called before the first frame update
  private void Start()
  {
  }

  // Update is called once per frame
  private void Update()
  {
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      Debug.Log(collision.gameObject.name);
      collision.gameObject.SendMessage("MoveTo", waypoints.transform.position, SendMessageOptions.DontRequireReceiver);
    }
    else
    {
      Destroy(collision.gameObject);
    }
  }
}