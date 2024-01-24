using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AverageGunShip : GunShip
{
    private void Awake()
    {
        maxFireRate = 12.5f;
        fireRateJerk = 0.8f;
        coolDown = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        Fire();
    }
}
