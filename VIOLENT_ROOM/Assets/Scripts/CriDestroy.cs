using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CriWare;

// SE�̍Đ����I�������玩���ŏ���
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
