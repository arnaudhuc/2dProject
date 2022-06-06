using UnityEngine;

public class RespawnableZone : MonoBehaviour
{
  [SerializeField] private GameObject waypoint;

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.gameObject.CompareTag("Player"))
    {
      PlayerController player = collider.GetComponent<PlayerController>();
      player.MoveTo(waypoint.transform.position);
    }
    else
    {
      Destroy(collider.gameObject);
    }
  }
}
