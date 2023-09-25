using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private ModelService modelService;
    [SerializeField] private Transform bombContainer;


    public UnityEvent OnSpawnBomb { get; } = new UnityEvent();

    private void Start()
    {
        OnSpawnBomb.AddListener(SpawnBomb);
    }
    private void SpawnBomb()
    {
        if (bombContainer.childCount > 0)
            return;
        modelService.LoadModel(bombContainer, "Bomb");
        bombContainer.gameObject.SetActive(true);
    }

}
