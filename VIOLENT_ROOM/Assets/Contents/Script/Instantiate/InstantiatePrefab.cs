using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    [SerializeField] Transform _prefab;
    [SerializeField] Transform _parent;
    [SerializeField] Transform _refTransform;
    [SerializeField] bool _isTransformOnly = false;

    public void Create()
    {
        if(_isTransformOnly == false)
        {
            Instantiate(_prefab, parent: _parent);
        }
        else
        {
            var obj = Instantiate(_prefab, position: _refTransform.position, rotation: _refTransform.rotation);
            obj.parent = _parent;
            obj.localScale = _prefab.localScale;
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Create();
        //}
    }
}
