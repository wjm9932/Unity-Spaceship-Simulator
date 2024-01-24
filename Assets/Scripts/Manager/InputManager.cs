using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();
            }

            return instance;
        }
    }
    public bool fire { get; private set; }
    public bool averageGunShip { get; private set; }
    public bool slowGunShip { get; private set; }
    public bool fastGunShip { get; private set; }
    public float moveDirection { get; private set; }
    public float rotateDirection { get; private set; }
    public bool exit { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fire = Input.GetKey(KeyCode.Space);

        averageGunShip = Input.GetKeyDown(KeyCode.Alpha1);
        fastGunShip = Input.GetKeyDown(KeyCode.Alpha2);
        slowGunShip = Input.GetKeyDown(KeyCode.Alpha3);

        moveDirection = Input.GetAxisRaw("Vertical");
        rotateDirection = Input.GetAxisRaw("Horizontal");

        exit = Input.GetKeyDown(KeyCode.Escape);
    }
}
