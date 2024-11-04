using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// USAGE
// 1. Create a data object that implements IGameStateData
// 2. Create a GameState class that implements GameStateBase
//    public class GameState : GameStateBase<Your IGameStateData class>

public interface IGameStateData 
{
    void Reset();
}

public class GameStateBase<T> where T : IGameStateData, new()
{
    private const string SAVE_KEY = "Save";

    public static T Instance
    {
        get
        {
            if (_data == null)
            {
                if (PlayerPrefs.HasKey(SAVE_KEY))
                {
                    string json = PlayerPrefs.GetString(SAVE_KEY);
                    _data = JsonUtility.FromJson<T>(json);
                }
                else
                {
                    _data = new T();
                }
            }
            return _data;
        }
    }

    private static T _data = default(T);

    public static void ResetSave()
    {
        _data.Reset();

        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(_data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

}
