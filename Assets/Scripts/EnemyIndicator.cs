using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    public GameObject indicator;
    public GameObject player;

    [SerializeField] EdgeCollider2D edgeCollider;
    [SerializeField] Camera cam;
    Vector2[] points = new Vector2[5];

    Renderer rd;
    // Start is called before the first frame update
    void Start()
    {
        edgeCollider = GameObject.Find("Main Camera").transform.Find("CameraBoundaryCollider").GetComponent<EdgeCollider2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("GunshipEngine");
        rd = GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update()
    {
        if (rd.isVisible == false)
        {
            if (indicator.activeSelf == false)
            {
                indicator.SetActive(true);
            }

            Vector2 dir = player.transform.position - transform.position;
            LayerMask layerMask = 1 << LayerMask.NameToLayer("CamBox");
            RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, dir.magnitude, layerMask);

            if (ray.collider != null)
            {
                indicator.transform.position = ray.point;
            }
        }
        else
        {
            if (indicator.activeSelf == true)
            {
                indicator.SetActive(false);
            }
        }
    }

    void Test()
    {
        //points[0] += 
    }
}
