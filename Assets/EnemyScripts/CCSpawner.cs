using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CCprefab;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] a = GetComponentsInChildren<Transform>();
        for(int i = 0; i < a.Length; i++)
        {
            GameObject.Instantiate(CCprefab, a[i].position, a[i].rotation);
        }
    }
}
