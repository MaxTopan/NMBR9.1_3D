using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    Vector3 targetPoint;
    TileInfo ti;
    bool playable;

    // Use this for initialization
    void Start()
    {
        playable = ti.CheckPlayable();
    }

    private void Awake()
    {
        ti = gameObject.GetComponent<TileInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.touchCount == 1)
        //{
        //Debug.Log("TOOUUUCCHHH");
        //Touch touch = Input.GetTouch(0);


        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPoint = new Vector3(Mathf.Round(mouse.x), 0, Mathf.Round(mouse.z));

        if (transform.position != targetPoint)
        {
            playable = ti.CheckPlayable();
            transform.position = targetPoint;
        }

        if (playable && Input.GetMouseButtonUp(0))
        {
            ti.Place();
        }
        
        //}
    }
}
