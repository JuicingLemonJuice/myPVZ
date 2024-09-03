using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    public List<LevelItem> levelDataList = new List<LevelItem>();
}

[Serializable]
public class LevelItem
{
    public int id;
    public int levelID;
    public int progressID;
    public int createTime;
    public int zombieType;
    public int bornPosition;

    public override string ToString()
    {
        return id + ","
            + levelID + ","
            + progressID + ","
            + createTime + ","
            + zombieType + ","
            + bornPosition;
    }
}
