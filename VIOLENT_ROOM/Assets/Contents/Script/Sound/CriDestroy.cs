using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CriWare;

// SEの再生が終了したら自動で消す
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
