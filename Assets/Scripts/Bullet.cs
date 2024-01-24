using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 lastPosition;

    public float range;
    public float speed;
    private float distanceTravel = 0f;

    public float BulletSpeed
    {
        get { return speed; }
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var distanceDelta = (transform.position - lastPosition).magnitude;
        distanceTravel += distanceDelta;
        lastPosition = transform.position;

        if(distanceTravel >= range)
        {
            Destroy(gameObject);
        }
    }
}
