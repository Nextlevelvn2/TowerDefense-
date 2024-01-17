using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathManager : MonoBehaviour
{
    public static PathManager Instance;
    public Transform [] Path;
    public Transform [] Path1;
    public List<Transform[]> PathEnemy = new List<Transform[]>();
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
        Invoke("AddPath", 0.01f);
    }
    void AddPath()
    {
        PathEnemy.Add(Path1);
        PathEnemy.Add(Path);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
