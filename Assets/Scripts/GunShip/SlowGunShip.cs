using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowGunShip : GunShip
{
    private void Awake()
    {
        maxFireRate = 15f;
        fireRateJerk = 1f;
        coolDown = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Fire();
    }
}
