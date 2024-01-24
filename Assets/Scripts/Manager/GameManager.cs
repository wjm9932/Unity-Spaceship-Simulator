using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject averageGunShip;
    public GameObject slowGunShip;
    public GameObject fastGunShip;
    public GameObject gunShip;


    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        Application.targetFrameRate = 144;
        gunShip.GetComponent<GunShipMovement>().SetGunShipType(GunShipType.AverageGunShip);
    }
    void Start()
    {
    }
    void Update()
    {
        if (InputManager.Instance.averageGunShip == true)
        {
            if(averageGunShip.activeSelf == false)
            {
                averageGunShip.SetActive(true);
                gunShip.GetComponent<GunShipMovement>().SetGunShipType(GunShipType.AverageGunShip);

                if(slowGunShip.activeSelf == true)
                {
                    slowGunShip.GetComponent<GunShip>().Reset();
                    slowGunShip.SetActive(false);
                }
                if(fastGunShip.activeSelf == true)
                {
                    fastGunShip.GetComponent<GunShip>().Reset();
                    fastGunShip.SetActive(false);
                }
            }
        }
        if (InputManager.Instance.slowGunShip == true)
        {
            if (slowGunShip.activeSelf == false)
            {
                slowGunShip.SetActive(true);
                gunShip.GetComponent<GunShipMovement>().SetGunShipType(GunShipType.SlowGunShip);

                if (averageGunShip.activeSelf == true)
                {
                    averageGunShip.GetComponent<GunShip>().Reset();
                    averageGunShip.SetActive(false);
                }
                if (fastGunShip.activeSelf == true)
                {
                    fastGunShip.GetComponent<GunShip>().Reset();
                    fastGunShip.SetActive(false);
                }
            }
        }
        if (InputManager.Instance.fastGunShip == true)
        {
            if (fastGunShip.activeSelf == false)
            {
                fastGunShip.SetActive(true);
                gunShip.GetComponent<GunShipMovement>().SetGunShipType(GunShipType.FastGunShip);
                
                if (averageGunShip.activeSelf == true)
                {
                    averageGunShip.GetComponent<GunShip>().Reset();
                    averageGunShip.SetActive(false);
                }
                if (slowGunShip.activeSelf == true)
                {
                    slowGunShip.GetComponent<GunShip>().Reset();
                    slowGunShip.SetActive(false);
                }
            }
        }

        if(InputManager.Instance.exit == true)
        {
            Application.Quit();
        }
    }
}
