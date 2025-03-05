using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonDataMG<T>
{
    const string FILE_PATH = "/Resources/Json";    // jsonファイルのパス
    static string GetFilePath(string fileName) => Application.dataPath + FILE_PATH + "/" + fileName + ".json";

    /// <summary>
    /// jsonとしてデータを保存
    /// </summary>
    /// <param name="fileName"></param> /Resources/Json/以降
    /// <param name="data"></param> 保存するデータ
    public static void Save(string fileName, T data)
    {
        string filepath = GetFilePath(fileName);
        string json = JsonUtility.ToJson(data);                 // jsonとして変換
        StreamWriter wr = new(filepath, false);    // ファイル上書き指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();
    }

    /// <summary>
    /// jsonファイル読み込み
    /// </summary>
    /// <param name="fileName"></param> ファイル名　/Resources/Json/以降
    /// <param name="data"></param>　新規作成時用のデータ
    /// <returns></returns>
    public static T Load(string fileName, E_FileType type)
    {
        string filepath = GetFilePath(fileName);
        //ディレクトリが無ければディレクトリを作成
        if (!Directory.Exists(Application.dataPath + FILE_PATH)) { Directory.CreateDirectory(Application.dataPath + FILE_PATH); }
        // ファイルがないとき、ファイル作成
        if (!File.Exists(filepath)) { CreateFile(filepath, type); }

        StreamReader rd = new(filepath);
        string json = rd.ReadToEnd();
        rd.Close();
        return JsonUtility.FromJson<T>(json);
    }

    public enum E_FileType { /*PlayerData, FishRecord,*/ }
    static void CreateFile(string filepath, E_FileType type)
    {
        string json = "";

        //switch (type) {
        //    case E_FileType.PlayerData:
        //        PlayerData PLdata = new();
        //        json = JsonUtility.ToJson(PLdata);
        //        break;
        //    case E_FileType.FishRecord:
        //        FishRecordData FRdata = new();
        //        json = JsonUtility.ToJson(FRdata);
        //        break;
        //}
        if (json != "") {
            StreamWriter wr = new(filepath, false);    // ファイル上書き指定
            wr.WriteLine(json);                                     // json変換した情報を書き込み
            wr.Close();
            Debug.Log("JsonFile(" + type + ")を作成しました");
        }
    }

}
