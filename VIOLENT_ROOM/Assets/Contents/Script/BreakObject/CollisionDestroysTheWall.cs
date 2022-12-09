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

		//�ڐG�n�_���Z�o
		Vector3 hitPos = new();
		foreach (ContactPoint point in collision.contacts)
		{
			hitPos = point.point;
		}
		//���������I�u�W�F�N�g�̏�����ɐڐG�n�_�ɔ������N����
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
