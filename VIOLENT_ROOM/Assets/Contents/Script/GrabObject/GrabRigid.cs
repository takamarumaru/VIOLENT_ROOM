using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRigid : MonoBehaviour
{
    Rigidbody rigidbody;

    LineRenderer lineRenderer;

    Vector3 prevPos;
    Vector3 velocity;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        //CatchLog.Instance.HandleLog("------------------------------------" + transform.name+ "------------------------------------");
        //CatchLog.Instance.HandleLog("parent  : " + transform.parent.name);
        //CatchLog.Instance.HandleLog(" nowPos : " + transform.position);
        //CatchLog.Instance.HandleLog("prevPos : " + prevPos);

        velocity = transform.position - prevPos;

        prevPos = transform.position;
    }

    private void OnCollisionStay(Collision collision)
    {
        //CatchLog.Instance.HandleLog(collision.gameObject.name+"に接触中");
    }

    private void Update()
    {
        if (lineRenderer)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + velocity * 10);
        }
    }

    public void ResetRigidbody()
    {
        CatchLog.Instance.HandleLog("Grab Object UnSelect");
        //加わる力を初期化
        rigidbody.velocity = Vector3.zero;
        //重力を適用
        rigidbody.useGravity = true;
        //前回のフレームからの移動量から力を追加
        rigidbody.AddForce(velocity*100,ForceMode.Impulse);

    }

    public void SelectObject()
    {
        CatchLog.Instance.HandleLog("Grab Object Select");
        //加わる力を初期化
        rigidbody.velocity = Vector3.zero;
        //重力を反映させないように
        rigidbody.useGravity = false;
    }

    public void ReleaseObject()
    {
        CatchLog.Instance.HandleLog("Grab Object Release");
    }
}
