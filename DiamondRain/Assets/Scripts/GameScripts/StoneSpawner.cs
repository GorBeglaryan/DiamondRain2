using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    [SerializeField] private Transform stoneContainer;
    [SerializeField] private ModelService modelService;

    public void Init()
    {
        StartSpawn(10);
    }
    private IEnumerator StoneSpawnerCoroutine(byte count)
    {
        for (byte i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1);
            modelService.LoadModel(stoneContainer, "Stone");
        }
    }
    public void StartSpawn(byte count)
    {
        StartCoroutine(StoneSpawnerCoroutine(count));
    }
    public void DestroyStone()
    {
        if(stoneContainer.childCount > 0)
            Destroy(stoneContainer.GetChild(0).gameObject);
    }
    public void DestroyStones()
    {
        for (byte i = 0; i < stoneContainer.childCount; i++)
        {
            Destroy(stoneContainer.GetChild(i).gameObject);
        }
    }


}
