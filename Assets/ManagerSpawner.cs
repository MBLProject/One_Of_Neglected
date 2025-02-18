using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject UI_Manager_Prefab;
    private void Awake()
    {
        try
        {
            GameObject gameObject = FindAnyObjectByType<UI_Manager>().gameObject;
        }
        catch
        {
            Debug.Log("UI매니저없음");
            Instantiate(UI_Manager_Prefab);
        }

    }
}
