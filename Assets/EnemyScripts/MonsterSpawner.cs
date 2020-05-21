using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject monsterPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] a = GetComponentsInChildren<Transform>();
        for(int i = 0; i < a.Length; i++)
        {
            GameObject.Instantiate(monsterPrefab, a[i].position, a[i].rotation);
        }
    }
}
