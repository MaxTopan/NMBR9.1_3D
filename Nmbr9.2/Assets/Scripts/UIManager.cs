using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public TileManager tm;
    public Text scoreDisplay;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DisplayScoreOnPlace(int level, int score)
    {
        //TODO: move text to placement area, remove text after it fades away
        //scoreDisplay.text = score.ToString() + "\n" + " (" + level + "*" + score/level + ")";
    }
}
