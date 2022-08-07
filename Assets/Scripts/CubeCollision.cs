using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private Cube _cube;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        if (otherCube != null && _cube.cubeID > otherCube.cubeID)
        {
            if (_cube.cubeNumber == otherCube.cubeNumber)
            {
                Vector3 contactPoint = collision.contacts[0].point;

                if (otherCube.cubeNumber < CubeSpawner.instance.maxCubeNumber)
                {
                    Cube newCube = CubeSpawner.instance.Spawn(_cube.cubeNumber * 2, contactPoint + Vector3.up * 1.6f);

                    float pushForce = 2.5f;
                    newCube.cubeRigidbody.AddForce(new Vector3(0, 0.3f, 1f) * pushForce, ForceMode.Impulse);

                    float randomValue = Random.Range(-20f, 20f);
                    Vector3 randomDirection = Vector3.one * randomValue;
                    newCube.cubeRigidbody.AddTorque(randomDirection);
                }
                else
                {
                    ScoreManager.instance.AddScore(10);
                }
                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                float explosionForce = 400f;
                float explosionRadius = 1.5f;
                foreach (Collider c in surroundedCubes)
                {
                    if (c.attachedRigidbody != null)
                    {
                        c.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                    }
                }
                FX.Instance.PlayCubeFX(contactPoint, _cube.cubeColor);

                CubeSpawner.instance.DestroyCube(_cube);
                CubeSpawner.instance.DestroyCube(otherCube);
                ScoreManager.instance.AddScore(1);
            }
        }
    }
}
