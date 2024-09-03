using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : ScriptableObject
{
    public List<LevelInfoItem> levelInfoList = new List<LevelInfoItem>();

/*    public LevelInfo()
    {
        levelInfoList.Add(new LevelInfoItem());
        levelInfoList[0].LevelID = -1;
    }*/
}

[Serializable]
public class LevelInfoItem
{
    public int id;
    public int levelID;
    public string levelName;
    public float[] progressPercent;

    public LevelInfoItem()
    {
        progressPercent = new float[] {-1, -1, -1};
    }

    public override string ToString()
    {
        return id + ","
            + levelID
            + "," + levelName
            + getFloat();
    }

    private string getFloat()
    {
        string s = "";
        for (int i = 0; i < progressPercent.Length; i++)
        {
            s = "," + progressPercent[i];
        }
        return s;
    }
}
