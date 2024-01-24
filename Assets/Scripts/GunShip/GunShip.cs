using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShip : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject gunShipEngine;
    [SerializeField] List<Transform> firePosition = new List<Transform>();

    protected float coolDown;
    protected float maxFireRate;
    protected float currentFireRate;
    protected float fireRateJerk;

    void Start()
    {
    }

    private void Update()
    {

    }

    public void Fire()
    {
        if (InputManager.Instance.fire == true)
        {
            if (currentFireRate < maxFireRate)
            {
                currentFireRate += Time.deltaTime * fireRateJerk;
            }
            if (currentFireRate > maxFireRate)
            {
                currentFireRate = maxFireRate;
            }
        }
        else if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime * 5f * (maxFireRate / 10f);
        }
        else
        {
            currentFireRate = 0f;
        }

        coolDown -= Time.deltaTime * currentFireRate;
        if (coolDown <= 0f)
        {
            for (int i = 0; i < firePosition.Count; i++)
            {
                var bullet = Instantiate(bulletPrefab, firePosition[i].position, firePosition[i].rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = firePosition[i].up * (bulletPrefab.GetComponent<Bullet>().BulletSpeed + gunShipEngine.GetComponent<Rigidbody2D>().velocity.magnitude);
            }
            coolDown = 1f;
        }
    }

    public void Reset()
    {
        coolDown = 1f;
        currentFireRate = 0f;
    }
}
