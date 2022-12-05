using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CriWare;

// SE‚ÌÄ¶‚ªI—¹‚µ‚½‚ç©“®‚ÅÁ‚·
public class CriDestroy : MonoBehaviour
{
    CriAtomSource _source;

    private void Awake()
    {
        _source = transform.GetComponent<CriAtomSource>();
    }

    void Update()
    {
        if (_source.status == CriAtomSource.Status.PlayEnd)
        {
            Destroy(gameObject);
        }
    }
}
