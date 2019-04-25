using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 This script is to be attached to the tile prefab (parent of the individual modules)
 Currently has way too much crap in it; refactor anything that's not movement to another script
 */


public class MovementControl : MonoBehaviour
{
    private KeyCode left = KeyCode.A, right = KeyCode.D, up = KeyCode.W, down = KeyCode.S;
    TileInfo ti; // used to access list of floor search modules

    private bool isPlayable = false; // bool to determine if the tile is currently in a playable position

    // Use this for initialization
    void Awake()
    {
        ti = gameObject.GetComponent<TileInfo>();
    }

    void Start()
    {
        isPlayable = ti.CheckPlayable();
    }

    // Update is called once per frame
    void Update()
    {
        #region Up Down Left Right
        if (Input.GetKeyDown(right)) // right
        {
            transform.position += Vector3.right;
            isPlayable = ti.CheckPlayable();
            //Debug.Log("Currently playable: " + isPlayable);
        }
        if (Input.GetKeyDown(left))
        {
            transform.position += Vector3.left;
            isPlayable = ti.CheckPlayable();
            //Debug.Log("Currently playable: " + isPlayable);
        }

        if (Input.GetKeyDown(up))
        {
            transform.position += Vector3.forward;
            isPlayable = ti.CheckPlayable();
            //Debug.Log("Currently playable: " + isPlayable);
        }
        if (Input.GetKeyDown(down))
        {
            transform.position += Vector3.back;
            isPlayable = ti.CheckPlayable();
            //Debug.Log("Currently playable: " + isPlayable);
        }
        #endregion

        #region Rotations
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(0.0f, 90.0f, 0.0f);
            isPlayable = ti.CheckPlayable();
            //Debug.Log("Currently playable: " + isPlayable);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(0.0f, -90.0f, 0.0f);
            isPlayable = ti.CheckPlayable();
            //Debug.Log("Currently playable: " + isPlayable);
        }
        #endregion


        if (isPlayable)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ti.Place();
            }
        }
    }
}