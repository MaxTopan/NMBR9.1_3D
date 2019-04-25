using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleInfo : MonoBehaviour
{
    [SerializeField]
    private string _tileID = "DEFAULT ID";
    public string TileID
    {
        get
        {
            return _tileID;
        }

        set
        {
            _tileID = value;
        }
    }

    [SerializeField]
    private int _tileLevel;
    public int TileLevel
    {
        get
        {
            return _tileLevel;
        }
        set
        {
            _tileLevel = value;
        }
    }
}
