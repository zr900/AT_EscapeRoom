using UnityEngine;

public class GhostHandFollower : MonoBehaviour
{
    public Transform head;

    public Vector3 offset = new Vector3(0.35f, -0.25f, 0.5f);

    void LateUpdate()
    {
        if (head == null) return;

        transform.position =
            head.position
            + head.right * offset.x
            + head.up * offset.y
            + head.forward * offset.z;
    }
}
