using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public Transform target;

    void Awake()
    {

    }

    void FixedUpdate()
    {
        TrackTarget();
    }

    void TrackTarget()
    {
        transform.position = new Vector2(target.position.x, target.position.y);
    }
}
