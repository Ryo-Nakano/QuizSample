using UnityEngine;
using System.Collections;
using System.IO;//←これはなんだ？
using System.Collections.Generic;
//ここを制限することで、①[可読性上がる(目的が明確化されるから)]②[予測変換少なくなる]


public class csvManager : MonoBehaviour
{
    //CSV読み込んで、string型2次元配列で返す関数
    public static string[,] GetCsvData(string dataPath)
    {

        TextAsset data = (TextAsset)Resources.Load(dataPath);//Resourcesフォルダからデータの取得！
        //Debug.Log (data.ToString ());

        string[] rows = data.ToString().Split("\n"[0]);//先ず横に切って、その1行1行をrowsにぶち込む！(stringに変換！)
        //Debug.Log (rows.Length);

        string[] cols = rows[0].Split(","[0]);//次はどっかのrowを縦に切る！(要素数の取得が目的)

        string[,] csvData = new string[rows.Length, cols.Length];//csvData(2次元配列)の縦と横の大きさ？を指定！

        //要は、rowsが行、colsが列！


        //for文
        for (int i = 0; i < rows.Length; ++i)
        {

            cols = rows[i].Split(","[0]);

            for (int j = 0; j < cols.Length; ++j)
            {
                csvData[i, j] = cols[j];
                Debug.Log ("(" + i + "," + j + ") = " + csvData [i, j]);
            }
        }

        return csvData;
    }


	//csvファイルのデータ書き込み
    public static void WriteData(string dataPath, string[,] newData)
    {
        string stringData = "";
        for (int i = 0; i < newData.GetLength(0); i++)
        {
            for (int j = 0; j < newData.GetLength(1); j++)
            {
                if (j < newData.GetLength(1) - 1)
                {
                    stringData += newData[i, j] + ",";
                }
                else if (j == newData.GetLength(1) - 1 && i < newData.GetLength(0) - 1)
                {
                    stringData += newData[i, j] + "\n";
                }
                else
                {
                    stringData += newData[i, j];
                }
            }
        }
        FileStream fs = new FileStream(GetPath() + dataPath, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(stringData);
        sw.Flush();
        sw.Close();
    }

    public static string GetPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Resources/";
#elif UNITY_ANDROID
        return Application.persistentDataPath + "";
#elif UNITY_IPHONE
        return Application.persistentDataPath + "";
#else
        return Application.dataPath + "";
#endif
    }
}

