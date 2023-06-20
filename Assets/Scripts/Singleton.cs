
using UnityEngine;

public class Singleton<T>  where T : new()
{
    public static T instance { get; private set; }

    public static T GetInstance()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }


}
