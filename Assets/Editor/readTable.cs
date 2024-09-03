using UnityEngine;
using UnityEditor;
using OfficeOpenXml;
using System.IO;
using System;
using System.Reflection;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UIElements;

[InitializeOnLoad]
public class Startup
{
    public static bool needRead = true;
    static Startup()
    {
        string path = Application.dataPath + "/Editor/CheckpointManage.xlsx";

        FileInfo fileInfo = new FileInfo(path);
        //LevelData levelData = new LevelData();
        LevelData levelData = (LevelData)ScriptableObject.CreateInstance(typeof(LevelData));
        LevelInfo levelInfo = (LevelInfo)ScriptableObject.CreateInstance(typeof(LevelInfo));
        using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet_zombie = excelPackage.Workbook.Worksheets["zombie"];
            for (int i = worksheet_zombie.Dimension.Start.Row + 2; i <= worksheet_zombie.Dimension.End.Row; i++)
            {
                LevelItem levelItem = new LevelItem();
                Type type = typeof(LevelItem);
                for (int j = worksheet_zombie.Dimension.Start.Column; j <= worksheet_zombie.Dimension.End.Column; j++)
                {
                    if (gettableValue(worksheet_zombie, out string tableValue, i, j))
                    {
                        //FieldInfo variable = type.GetField(worksheet_zombie.GetValue(2, j).ToString());
                        FieldInfo variable = getField(type, worksheet_zombie, 2, j, out int k);
                        variable.SetValue(levelItem, Convert.ChangeType(tableValue, variable.FieldType));
                    }
                }
                levelData.levelDataList.Add(levelItem);
            }

            ExcelWorksheet worksheet_info = excelPackage.Workbook.Worksheets["info"];
            for (int i = worksheet_info.Dimension.Start.Row + 2; i <= worksheet_info.Dimension.End.Row; i++)
            {
                LevelInfoItem levelInfoItem = new LevelInfoItem();
                Type type = typeof(LevelInfoItem);

                for (int j = worksheet_info.Dimension.Start.Column; j <= worksheet_info.Dimension.End.Column; j++)
                {
                    //FieldInfo variable = type.GetField(worksheet_info.GetValue(2, j).ToString());
                    if (gettableValue(worksheet_info, out string tableValue, i, j))
                    {
                        FieldInfo variable = getField(type, worksheet_info, 2, j, out int k);

                        if (typeof(Array).IsAssignableFrom(variable.FieldType))//
                        {
                            //variable.SetValue(levelInfoItem, Array.CreateInstance(variable.FieldType.GetElementType(), 3));
                            Array arr = variable.GetValue(levelInfoItem) as Array;
                            arr.SetValue(Convert.ChangeType(tableValue, variable.FieldType.GetElementType()), j - k);
                            return;
                        }

                        variable.SetValue(levelInfoItem, Convert.ChangeType(tableValue, variable.FieldType));
                    }
                }
                levelInfo.levelInfoList.Add(levelInfoItem);
            }
        }

        if (needRead)
        {
            AssetDatabase.CreateAsset(levelData, "Assets/Resources/LevelData.asset");
            AssetDatabase.CreateAsset(levelInfo, "Assets/Resources/LevelInfo.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        //ÄÃ×Ö¶Î
        FieldInfo getField(Type type, ExcelWorksheet worksheet, int i, int j, out int k)
        {
            FieldInfo variable;

            try
            {
                variable = type.GetField(getField_Str(worksheet, i, j));
            }
            catch
            {
                return getField(type, worksheet, i, --j, out k);
            }

            k = j;
            return variable;
        }

        //ÄÃ×Ö¶ÎÃû
        string getField_Str(ExcelWorksheet worksheet, int i, int j)
        {
            string variable_S;

            try
            {
                variable_S = worksheet.GetValue(i, j).ToString();
            }
            catch
            {
                return getField_Str(worksheet, i, --j);
            }
            return variable_S;
        }

        //ÄÃÖµ
        bool gettableValue(ExcelWorksheet worksheet, out string tableValue, int i, int j)
        {
            try
            {
                tableValue = worksheet.GetValue(i, j).ToString();
            }
            catch
            {
                tableValue = "";
                return false;
            }
            return true;
        }
    }

    static int getInt(string input)
    {
        string value = input.Trim();
        int num = -1;
        int.TryParse(value, out num);
        return num;
    }

}