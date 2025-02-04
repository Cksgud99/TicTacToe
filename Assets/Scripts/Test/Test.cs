using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 재귀함수
    void RecursiveFunction(int count)
    {
        if (count <= 0)
        {
            Debug.Log("Count : " + count);
            return;
        }
        
        Debug.Log("Count : " + count);
        RecursiveFunction(count - 1);
    }
}
