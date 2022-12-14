using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Cube cube= other.GetComponent<Cube>();
        if (cube != null)
        {
            if (!cube.isMainCube && cube.cubeRigidbody.velocity.magnitude < .1f)
            {
                ScoreManager.instance.GameOver();
            }
        }
    }
}
