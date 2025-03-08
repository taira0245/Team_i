using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Jsonの読込・保存処理クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public static class JsonDataMG<T>
{
    const string FILE_PATH = "/Resources/Json/PlayerData.json";    // jsonファイルのパス
    static string GetFilePath() => Application.dataPath + FILE_PATH;

    /// <summary>
    /// jsonとしてデータを保存
    /// </summary>
    /// <param name="fileName"></param> /Resources/Json/以降
    /// <param name="data"></param> 保存するデータ
    public static void Save(T data)
    {
        string filepath = GetFilePath();
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
    public static T Load()
    {
        string filepath = GetFilePath();
        //ディレクトリが無ければディレクトリを作成
        if (!Directory.Exists(Application.dataPath + FILE_PATH)) { Directory.CreateDirectory(Application.dataPath + "/Resources/Json/"); }
        // ファイルがないとき、ファイル作成
        if (!File.Exists(filepath)) { CreateFile(); }

        StreamReader rd = new(filepath);
        string json = rd.ReadToEnd();
        rd.Close();
        return JsonUtility.FromJson<T>(json);
    }


    /// <summary>
    /// データファイルの作成
    /// </summary>
    /// <param name="filepath"></param>
    /// <param name="type"></param>
    static void CreateFile()
    {
        Debug.Log("Jsonファイルが作成されました。");

        string filepath = GetFilePath();
        JsonScoreData PLdata = new();
        Debug.Log("PLData : " + PLdata);
        Debug.Log("PLData.Score : " + PLdata.LatestScore);

        //ファイルへの書込み
        string json = JsonUtility.ToJson(PLdata);                 // jsonとして変換
        StreamWriter wr = new(filepath, false);    // ファイル上書き指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();

        Debug.Log("jsonData : " + json);
    }

}