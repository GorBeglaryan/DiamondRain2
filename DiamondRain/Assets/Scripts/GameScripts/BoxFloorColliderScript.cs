using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxFloorColliderScript : MonoBehaviour
{
    [SerializeField] private Transform stoneSpawnPoint;
    public UnityEvent<short> OnBoxEntered { get; } = new UnityEvent<short>();

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        Transform hitTransform = other.transform;
        if (hitTransform == null)
            return;
        StoneValues value = StoneValues.Pear;
        switch (other.gameObject.name)
        {
            case "Marquise(Clone)": value = StoneValues.Marquise; break;
            case "Emerald(Clone)": value = StoneValues.Emerald; break;
            case "Stone(Clone)": value = StoneValues.Stone; break;
            case "Pear(Clone)": value = StoneValues.Pear; break;
            case "Round(Clone)": value = StoneValues.Round; break;
            case "Heart(Clone)": value = StoneValues.Heart; break;
            case "Radiant(Clone)": value = StoneValues.Radiant; break;
            case "Bomb(Clone)": value = StoneValues.Bomb; break;
        }
        if (hitTransform.gameObject.name.StartsWith("Stone"))
        {
            hitTransform.gameObject.SetActive(false);
            hitTransform.position = stoneSpawnPoint.position;
            hitTransform.gameObject.SetActive(true);
        }
        else
        {
            Transform parent = hitTransform.parent;
            Destroy(hitTransform.gameObject);
            parent.gameObject.SetActive(false);
        }
        
        OnBoxEntered?.Invoke((short)value);
    }
}
