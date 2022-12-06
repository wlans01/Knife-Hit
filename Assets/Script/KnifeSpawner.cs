using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject knifePrefab;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(knifePrefab, transform.position, Quaternion.identity);
        }
    }
}
