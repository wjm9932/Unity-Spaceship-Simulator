using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastGunShip : GunShip
{
    // Start is called before the first frame update
    private void Awake()
    {
        maxFireRate = 10f;
        fireRateJerk = 2f;
        coolDown = 1f;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Fire();
    }
}
