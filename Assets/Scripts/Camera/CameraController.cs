using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] private Transform player;

  [SerializeField] private float cameraDistanceY = 2.19f;

  private void Update()
  {
    transform.position = new Vector3(player.position.x, player.position.y + cameraDistanceY, transform.position.z);
  }
}