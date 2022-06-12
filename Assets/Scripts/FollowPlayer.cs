using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        if (player.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }
}