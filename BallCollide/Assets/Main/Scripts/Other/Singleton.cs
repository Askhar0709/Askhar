using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object lockObj = new object();
    private static T instance;
    public static T Instance
    {
        get
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        var obj = new GameObject();
                        instance = obj.AddComponent<T>();
                        obj.name = typeof(T).ToString();
                        DontDestroyOnLoad(obj);
                    }
                }
                return instance;
            }
        }
    }
}