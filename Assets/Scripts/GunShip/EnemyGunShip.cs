using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random;

public class EnemyGunShip : MonoBehaviour
{
    public GameObject destinationPrefab;
    GameObject currentDestination;
    Vector2 targetPos;
    Vector2 UNDEFINED = new Vector2(-100, -100);

    private float maxAngularAcceleration = 50f;
    private float maxAngularVelocity = 50f;
    private float angularAcceleration = 0f;
    private float angularVelocity = 0f;
    private float angularJerk = 30f;

    private float maxAcceleration = 10f;
    private float acceleration = 0f;
    private float jerk = 3f;

    private float undefinedTime = 0f;

    private Rigidbody2D rb;

    private bool isStop;
    private bool previousStop = true;
    private bool isFacing = false;
    // Start is called before the first frame update
    void Start()
    {
        SetNextTarget();
        rb = GetComponent<Rigidbody2D>();
        maxAcceleration *= rb.drag;
        jerk *= rb.drag;
    }
    private void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsCoordinateVaild(targetPos) == true)
        {
            float rotate = 0f;
            float angleDiff = GetAngular(targetPos);
            if (Mathf.Abs(angleDiff) > 0.5f)
            {
                float turnDirection = 1f;
                if (angleDiff < 0f)
                {
                    turnDirection = -1f;
                }

                angularAcceleration += Time.deltaTime * angularJerk * turnDirection;
                angularAcceleration = ClampMagnitude(angularAcceleration, maxAngularAcceleration);

                angularVelocity += Time.deltaTime * angularAcceleration;
                angularVelocity = ClampMagnitude(angularVelocity, maxAngularVelocity);

                rotate = Time.deltaTime * angularVelocity;
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotate);
            }
            else
            {
                isFacing = true;
                angularAcceleration = 0f;
                angularVelocity = 0f;
            }

            Vector2 localAcceleration = new Vector2(transform.up.x, transform.up.y) * acceleration;
            float distanceToStop = GetDistanceToStop(Time.deltaTime, rb.velocity, acceleration, maxAcceleration, jerk);

            Vector2 targetDir = targetPos - new Vector2(transform.position.x, transform.position.y);
            float distanceToTarget = targetDir.magnitude;

            if (isStop == true)
            {
                if (isFacing == false)
                {
                    if (previousStop == true)
                    {
                        acceleration -= Time.deltaTime * jerk * 0.2f;
                        acceleration = ClampMagnitude(acceleration, maxAcceleration);
                    }
                    else
                    {
                        acceleration -= Time.deltaTime * jerk * 0.5f;
                        acceleration = ClampMagnitude(acceleration, maxAcceleration);
                    }
                }
                else if (distanceToStop * 1.1f >= distanceToTarget)
                {
                    acceleration -= Time.deltaTime * jerk * 2f;
                    acceleration = ClampMagnitude(acceleration, maxAcceleration);
                }
                else if (distanceToStop * 1.2f < distanceToTarget)
                {
                    acceleration += Time.deltaTime * jerk;
                    acceleration = ClampMagnitude(acceleration, maxAcceleration);
                }
            }
            else
            {
                if (isFacing == false)
                {
                    if (previousStop == true)
                    {
                        acceleration -= Time.deltaTime * jerk * 0.2f;
                        acceleration = ClampMagnitude(acceleration, maxAcceleration);
                    }
                    else
                    {
                        acceleration -= Time.deltaTime * jerk * 0.5f;
                        acceleration = ClampMagnitude(acceleration, maxAcceleration);
                    }
                }
                else
                {
                    acceleration += Time.deltaTime * jerk;
                    acceleration = ClampMagnitude(acceleration, maxAcceleration);
                }
            }
            localAcceleration = localAcceleration.normalized * Mathf.Abs(acceleration);
            rb.AddForce(localAcceleration);

            if (distanceToTarget <= 1f)
            {
                if (isStop == false)
                {
                    previousStop = false;
                    SetNextTarget();
                }
                else
                {
                    previousStop = true;
                    acceleration = 0f;
                    targetPos = UNDEFINED;
                    Destroy(currentDestination);
                }
                isFacing = false;
            }
        }
        else
        {
            undefinedTime += Time.deltaTime;
        }

        if (undefinedTime > 5f)
        {
            undefinedTime = 0f;
            SetNextTarget();
        }
    }

    void SetNextTarget()
    {

        if (Random.Range(0, 10) % 10 > 4)
        {
            isStop = false;
            SetDestination();
        }
        else
        {
            isStop = true;
            SetDestination();
        }
    }

    float GetAngular(Vector2 target)
    {
        Vector2 faceDir = new Vector2(transform.up.x, transform.up.y);
        Vector2 targetDir = target - new Vector2(transform.position.x, transform.position.y);
        return Mathf.Atan2(faceDir.x * targetDir.y - faceDir.y * targetDir.x, targetDir.x * faceDir.x + targetDir.y * faceDir.y) * Mathf.Rad2Deg;
    }

    float ClampMagnitude(float currentValue, float maxValue)
    {
        if (currentValue > maxValue)
        {
            return maxValue;
        }
        else if (-maxValue > currentValue)
        {
            return -maxValue;
        }
        else
        {
            return currentValue;
        }
    }
    float GetDistanceToStop(float dt, Vector2 velocity,float acceleration, float maxAcceleration, float jerk)
    {
        if (acceleration < 0)
        {
            return 0;
        }
        else
        {
            float distance = 0f;
            float currentVelocity = velocity.magnitude;
            float currentAcceleration = acceleration;

            while (currentVelocity > 0f)
            {
                if (currentAcceleration > -maxAcceleration)
                {
                    currentAcceleration -= dt * jerk * 2f;
                }
                if (currentAcceleration < -maxAcceleration)
                {
                    currentAcceleration = -maxAcceleration;
                }
                if (currentVelocity > 0f)
                {
                    currentVelocity += dt * currentAcceleration;
                }
                if (currentVelocity < 0f)
                {
                    currentVelocity = 0f;
                }

                distance += dt * currentVelocity;
            }
            return distance;
        }
    }

    Vector2 GetRandomCoordinate()
    {
        int x = Random.Range(-70, 71);
        int y = Random.Range(-50, 51);

        return new Vector2(x, y);
    }

    bool IsCoordinateVaild(Vector2 vec)
    {
        if (vec == UNDEFINED)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void SetDestination()
    {
        if (currentDestination != null)
        {
            Destroy(currentDestination);
        }
        targetPos = GetRandomCoordinate();
        currentDestination = Instantiate(destinationPrefab, new Vector3(targetPos.x, targetPos.y, 0), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
            Destroy(currentDestination);
            Destroy(collision.gameObject);
        }
    }
}

