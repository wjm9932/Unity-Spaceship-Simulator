using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum GunShipType
{
    AverageGunShip,
    SlowGunShip,
    FastGunShip
}

public class GunShipMovement : MonoBehaviour
{
    private float maxAngularSpeed;
    private float angularAcceleration = 0f;
    private float angularJerk = 3f;

    protected float maxSpeed;
    private float acceleration = 0f;
    private float jerk = 3f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (InputManager.Instance.rotateDirection < 0)
        {
            if(angularAcceleration < 0)
            {
                angularAcceleration += Time.deltaTime * angularJerk * 10f;
                angularAcceleration = ClampMagnitude(angularAcceleration, maxAngularSpeed);
            }
            else
            {
                angularAcceleration += Time.deltaTime * angularJerk;
                angularAcceleration = ClampMagnitude(angularAcceleration, maxAngularSpeed);
            }
            rb.AddTorque(angularAcceleration);
        }
        if(InputManager.Instance.rotateDirection > 0)
        {
            if(angularAcceleration > 0)
            {
                angularAcceleration -= Time.deltaTime * angularJerk * 10f;
                angularAcceleration = ClampMagnitude(angularAcceleration, maxAngularSpeed);
            }
            else
            {
                angularAcceleration -= Time.deltaTime * angularJerk;
                angularAcceleration = ClampMagnitude(angularAcceleration, maxAngularSpeed);
            }
            rb.AddTorque(angularAcceleration);
        }
        if(InputManager.Instance.rotateDirection == 0)
        {
            if(angularAcceleration < 0)
            {
                angularAcceleration += Time.deltaTime * jerk;
                if(angularAcceleration > 0)
                {
                    angularAcceleration = 0f;
                }
            }
            if(angularAcceleration > 0)
            {
                angularAcceleration -= Time.deltaTime * jerk;
                if(angularAcceleration < 0)
                {
                    angularAcceleration = 0f;
                }
            }
        }
        
        if (InputManager.Instance.moveDirection < 0)
        {
            if(acceleration > 0)
            {
                acceleration -= Time.deltaTime * jerk * 10f;
                acceleration = ClampMagnitude(acceleration, maxSpeed);
            }
            else
            {
                acceleration -= Time.deltaTime * jerk;
                acceleration = ClampMagnitude(acceleration, maxSpeed);
            }
            rb.AddForce(new Vector2(gameObject.transform.up.x, gameObject.transform.up.y) * acceleration);
        }
        if(InputManager.Instance.moveDirection > 0)
        {
            if (acceleration < 0)
            {
                acceleration += Time.deltaTime * jerk * 10f;
                acceleration = ClampMagnitude(acceleration, maxSpeed);
            }
            else
            {
                acceleration += Time.deltaTime * jerk;
                acceleration = ClampMagnitude(acceleration, maxSpeed);
            }
            rb.AddForce(new Vector2(gameObject.transform.up.x, gameObject.transform.up.y) * acceleration);
        }
        if(InputManager.Instance.moveDirection == 0)
        {
            if(acceleration > 0)
            {
                acceleration -= Time.deltaTime * jerk;
                if(acceleration < 0)
                {
                    acceleration = 0f;
                }
            }
            if(acceleration < 0)
            {
                acceleration += Time.deltaTime * jerk;
                if(acceleration > 0)
                {
                    acceleration = 0f;
                }
            }
        }
    }


    public void SetGunShipType(GunShipType type)
    {
        // it would be better for Gunship inherit GunShipMovement component I guess, but I would leave it just for now
        switch (type)
        {
            case GunShipType.AverageGunShip:
                maxSpeed = 12f;
                jerk = 3f;

                maxAngularSpeed = 2f;
                angularJerk = 2f;
                break;
            
            case GunShipType.SlowGunShip:
                maxSpeed = 7f;
                jerk = 1f;
                
                maxAngularSpeed = 1f;
                angularJerk = 1f;
                break;
            
            case GunShipType.FastGunShip:
                maxSpeed = 17f;
                jerk = 5f;

                maxAngularSpeed = 3f;
                angularJerk = 3f;
                break;
        }
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
}
