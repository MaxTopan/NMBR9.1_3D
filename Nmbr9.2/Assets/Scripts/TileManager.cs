using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To manage which tile is currently active, switch to the next one once the previous has been placed

public class TileManager : MonoBehaviour
{
    public GameObject[] tileArr = new GameObject[10];
    public List<GameObject> tileList;

    private List<TileInfo> tiList = new List<TileInfo>();

    public LevelManager lm;

    private int tileCount;
    private List<int> order;

    private void Awake()
    {
        tileCount = lm.TileCount;
        order = GenerateTileList(tileCount);
        GenerateTiles(order);

        NextTile();
    }

    private List<int> GenerateTileList(int tiles)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < tiles; i++)
        {
            if (i >= 10)
            {
                result.Add((i % 10)); // if i is more than one digit, return the unit (ie 10 is 0, 11 is 1 etc.)
            }
            else
            {
                result.Add(i);
            }
        }
        ShuffleScript.Shuffle(result);
        return result;
    }

    private void GenerateTiles(List<int> _order)
    {
        GameObject currentGo;
        List<ModuleInfo> currentMods;
        tileList = new List<GameObject>();

        for (int i = 0; i < _order.Count; i++) // run through list of tiles, instantiate each one
        {
            currentGo = Instantiate(tileArr[_order[i]], transform.position, transform.rotation);
            currentMods = new List<ModuleInfo>(currentGo.GetComponentsInChildren<ModuleInfo>());

            foreach (ModuleInfo mi in currentMods)
            {
                if (_order.GetRange(0, i).Contains(order[i])) // if there is already one of that tile
                { mi.TileID = _order[i].ToString() + "a" + i; } // set tileID to distinguish between duplicate tiles
                else
                { mi.TileID = _order[i].ToString(); } // set tileID to match the tile's number
            }

            tiList.Add(currentGo.GetComponent<TileInfo>());
            tiList[i].Score = _order[i]; // Initialise tile score

            //mcList.Add(currentGo.GetComponent<MovementControl>());
            currentGo.SetActive(false);

            tileList.Add(currentGo);
        }
    }


    private int currentInt = 0;
    public int CurrentInt { get { return currentInt; } }
    /// <summary>
    /// Moves player control to the next tile in the list
    /// </summary>
    public void NextTile()
    {
        if (currentInt != lm.TileCount) { tileList[currentInt++].SetActive(true); } // update and enable new tile
        else { lm.AddScore(tiList); } // if it's the last tile, call AddScore from LevelManager
    }
}
