using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSearch : MonoBehaviour
{
    private readonly string tableTag = "Table";
    RaycastHit hit;

    /// <summary>
    /// Returns the ID of the tile below this module. Null if there isn't one
    /// </summary>
    /// <returns></returns>
    public string FloorID()
    {
        ModuleInfo mi;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (mi = hit.transform.GetComponent<ModuleInfo>()) // if the gameobject has a ModuleInfo script attached
            {
                //Debug.Log("Module ID: " + mi.TileID);
                return mi.TileID;
            }
            else
            {
                //Debug.Log("No Module Info");
                return null;
            }
        }
        return null;
    }

    /// <summary>
    /// Returns the tile level of the object beneath the module, or -1 if there's no module below
    /// </summary>
    /// <returns></returns>
    public int FloorLevel()
    {
        ModuleInfo mi;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (mi = hit.transform.GetComponent<ModuleInfo>()) // if the gameobject has a ModuleInfo script attached
            {
                //Debug.Log("Module Level: " + mi.TileLevel);
                return mi.TileLevel;
            }
            else
            {
                //Debug.Log("No Module Info -10");
                return -10;
            }
        }
        //Debug.Log("No Module Info -1");
        return -10;
    }

    /// <summary>
    /// searches for tiles one space in each cardinal direction, returns true if any are found
    /// </summary>
    /// <param name="tr">the position to search around</param>
    /// <returns></returns>
    public bool HasNeighbours(Transform tr)
    {
        //Debug.DrawRay(tr.position + Vector3.forward, Vector3.down, Color.cyan, 3.0f);
        if (Physics.Raycast(tr.position + Vector3.forward, Vector3.down, out hit)) // search forward
        {
            if (hit.transform.tag != tableTag)
            {
                //Debug.Log("FORWARD");
                return true;
            }
        }
        if (Physics.Raycast(tr.position + Vector3.back, Vector3.down, out hit)) // search back
        {
            if (hit.transform.tag != tableTag)
            {
                //Debug.Log("BACK");
                return true;
            }
        }
        if (Physics.Raycast(tr.position + Vector3.left, Vector3.down, out hit)) // search left
        {
            if (hit.transform.tag != tableTag)
            {
                //Debug.Log("LEFT");
                return true;
            }
        }
        if (Physics.Raycast(tr.position + Vector3.right, Vector3.down, out hit)) // search right
        {
            if (hit.transform.tag != tableTag)
            {
                //Debug.Log("RIGHT");
                return true;
            }
        }
        return false;
    }
}