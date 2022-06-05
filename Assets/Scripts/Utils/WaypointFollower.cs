using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
  [SerializeField] private GameObject[] waypoints;
  private int currentWaypaintIndex = 0;

  [SerializeField] private float speed = 2f;

  private void Update()
  {
    if (Vector2.Distance(waypoints[currentWaypaintIndex].transform.position, transform.position) < .1f)
    {
      //currentWaypaintIndex = currentWaypaintIndex >= waypoints.Length ? 0 : currentWaypaintIndex++;
      currentWaypaintIndex++;
      if (currentWaypaintIndex >= waypoints.Length)
      {
        currentWaypaintIndex = 0;
      }
    }
    transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypaintIndex].transform.position, Time.deltaTime * speed);
  }
}