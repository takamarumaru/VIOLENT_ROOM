using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroysTheWall : MonoBehaviour
{
	public LayerMask HitLayers;
	public LayerMask ShardLayers;
	public float breakPower;
	public float powerRatio;
	public float rangeRatio;

	private void OnCollisionEnter(Collision collision)
    {
		if (((1 << collision.gameObject.layer) & HitLayers) == 0) return;

		//接触地点を算出
		Vector3 hitPos = new();
		foreach (ContactPoint point in collision.contacts)
		{
			hitPos = point.point;
		}
		//当たったオブジェクトの情報を基に接触地点に爆発を起こす
		var collisionRigidbody = collision.rigidbody;
		float collisionPower = 0.0f;
		if (collisionRigidbody)
		{
			collisionPower = collisionRigidbody.mass * collisionRigidbody.velocity.magnitude;
		}
		if(collisionPower >= breakPower)
		Explode(hitPos,collisionPower);
	}

	void Explode(Vector3 position,float power)
	{
		Debug.Log(power);
		foreach (var shard in Physics.OverlapSphere(position, power * rangeRatio, ShardLayers))
		{
			var rigidbody = shard.GetComponent<Rigidbody>();
			if (rigidbody == null) return;

			rigidbody.isKinematic = false;
			rigidbody.AddExplosionForce(power* powerRatio, position, power * rangeRatio);

			shard.gameObject.AddComponent<GK.DestroyIn>().Time = 3.0f;
		}
	}
}
