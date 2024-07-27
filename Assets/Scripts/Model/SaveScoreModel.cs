using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SaveScoreModel 
{
    public SaveScoreModel(int score, string username)
    {
        this.score = score;
        this.username = username;
    }

    public int score { get; set; }
    public string username { get;set; }
}
