using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHandler : MonoBehaviour
{

    private Rigidbody2D rb;

    public void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void updateMass(int newMass)
    {
        rb.mass = newMass;
    }

}
