using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidSettings : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        Application.targetFrameRate = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
