using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    [SerializeField] Transform _prefab;
    [SerializeField] Transform _parent;

    public void Create()
    {
        Instantiate(_prefab, parent: _parent);
    }
}
