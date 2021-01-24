using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// Apply force to projectile
public class Projectile : MonoBehaviour
{
    public int force = 20;
    public float lifetime = 10;
    
    public void Launch()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
        Destroy(gameObject, lifetime); // can be improve with pooling
    }
}
