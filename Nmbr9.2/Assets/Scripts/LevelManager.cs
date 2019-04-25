using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //public TileManager tm;

    [SerializeField]
    private int _tileCount = 20;
    public int TileCount { get { return _tileCount; } private set { _tileCount = value; } }

    private float _tableHeight;
    public float TableHeight { get { return _tableHeight; } private set { _tableHeight = value; } }

    private void Awake()
    {
        _tableHeight = GameObject.FindGameObjectWithTag("Table").transform.position.y; // get the height of the table to act as a base
    }

    public void AddScore(List<TileInfo> tiList)
    {
        int score = 0;

        for (int i = 0; i < _tileCount; i++)
        {
            score += tiList[i].Score;
        }
        Debug.Log("FINAL SCORE IS " + score);
    }
}
