using UnityEngine;

public class GhostHandFollower : MonoBehaviour
{
    public Transform head; //main camera's position

    public Vector3 offset = new Vector3(0.35f, -0.25f, 0.5f); //set the hand's position

    void LateUpdate()
    {
        if (head == null) return;
        //changing position of virtual hand according to position of head
        transform.position =
            head.position //start at head positon
            + head.right * offset.x //move hand horizontally
            + head.up * offset.y //move hand vertically
            + head.forward * offset.z; //move forward or back
    }
}

//make the ghost hand follow head position in world