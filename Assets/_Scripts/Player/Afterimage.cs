using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    public ParticleSystem ps;

    public void FlipParticle(bool isLeft)
    {
        if (ps != null)
        {
            var main = ps.main;
            ps.transform.localScale = new Vector3(isLeft ? -0.5f : 0.5f, 0.5f, 0.5f);
        }
    }
}
