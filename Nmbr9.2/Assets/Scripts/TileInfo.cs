using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Attaches to the empty tile gameobject (parent of the individual modules)
// Stores info on wether the tile is currently able to be played

public class TileInfo : MonoBehaviour
{
    float tableHeight; // stored in LevelManager script, retrieved in Awake()
    private float playableHeight;
    private int currentLevel;

    private readonly float tileDepth = 0.25f;

    private TileManager tm;
    private UIManager um;

    private List<FloorSearch> _fsList; // List of FloorSearch scripts in child components
    private List<ModuleInfo> _miList; // List of ModuleInfo scripts in child components used to update levels after being placed

    [SerializeField]
    private int _score;
    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    private void Awake()
    {
        GetChildLists();

        // DO SOMETHING TO THE LINE BELOW; MAYBE HAVE THE VALUE GIVEN FROM TILEMANAGER WHEN GENERATED
        tableHeight = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().TableHeight; // save tableheight from LevelManager
        tm = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        um = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    void OnEnable()
    {
        //TEMP CODE
        Renderer[] ren = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in ren)
        {
            Color trans = new Color(r.material.color.r, r.material.color.g, r.material.color.b, 0.3f);
            r.material.color = trans;
        }
    }

    /// <summary>
    /// Adds the Floor Search scripts from all child modules to a list
    /// </summary>
    void GetChildLists()
    {
        _fsList = new List<FloorSearch>(); // go through each module and add it's FloorSearch script to the list
        _miList = new List<ModuleInfo>();

        foreach (Transform child in transform)
        {
            _fsList.Add(child.GetComponentInChildren<FloorSearch>());
            _miList.Add(child.GetComponentInChildren<ModuleInfo>());
        }
    }

    /// <summary>
    /// searches through list of component modules; returns true if all the modules are above objects with the same tag
    /// </summary>
    /// <returns></returns>
    public bool CheckPlayable()
    {
        bool _isPlayable = true;
        List<string> IDs = new List<string>(); // list of tile IDs used for comparison
        List<int> levels = new List<int>(); // list of floor levels

        foreach (FloorSearch fs in _fsList)
        {
            int currentLevel = fs.FloorLevel();
            IDs.Add(fs.FloorID()); // add current ID to list

            if (currentLevel == -10) { _isPlayable = false; break; } // if there is no module info, return false

            levels.Add(currentLevel); // add current level to list
            if (levels[0] != currentLevel) { _isPlayable = false; break; } // if any modules are on different levels, return false
        }
        if (_isPlayable) // if the tiles are all on the same level
        {
            currentLevel = levels[0];

            if (IDs[0] == "Table" && gameObject != tm.tileList[0]) // if the tile is being placed on the table and it is not the first tile
            {
                foreach (FloorSearch fs in _fsList) // ensure the tile is touching at least one other tile
                {
                    _isPlayable = fs.HasNeighbours(fs.transform);
                    if (_isPlayable) { break; }
                }
            }

            if (IDs[0] != "Table" && !IDs.Distinct().Skip(1).Any()) // if the tile is not being placed on the table and none of the IDs are distinct from each other
            {
                _isPlayable = false; // the tile is not playable
            }
            else // if the tile is playable
            {
                if (IDs[0] == "Table")
                {
                    playableHeight = tableHeight + tileDepth; // if the tile is being placed on the table, place it one tile depth above the table
                }
                else
                {
                    playableHeight = tableHeight + (2 * tileDepth) + (levels[0] * tileDepth); // height to place the tile at = the table, plus the height of a tile, plus the level of the tile
                    return _isPlayable;
                }
            }
        }
        return _isPlayable;
    }

    /// <summary>
    /// puts the current tile onto the play field, updates its score, then disables its movement
    /// </summary>
    public void Place()
    {
        //Debug.Log("Height to play: " + playableHeight);
        transform.position = new Vector3(transform.position.x, playableHeight, transform.position.z);

        for (int i = 0; i < _miList.Count; i++) // run through each ModuleInfo script in child and set the level it's been placed at
        { _miList[i].TileLevel = currentLevel + 1; }

        if ((_score *= currentLevel + 1) > 0) // score is equal to the value of the tile * the level it's placed on
        { um.DisplayScoreOnPlace(currentLevel + 1, _score); }

        gameObject.GetComponent<MovementControl>().enabled = false; // disable movement control of this tile
        gameObject.GetComponent<TouchControls>().enabled = false;

        //TEMP CODE
        Renderer[] ren = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in ren)
        {
            Color trans = new Color(r.material.color.r, r.material.color.g, r.material.color.b, 1);
            r.material.color = trans;
        }

        tm.NextTile();
    }
}