using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Track : NetworkBehaviour
{
    public Material mMaterial;
    public GameObject fires;
    public GameObject beacon;
    public GameObject explosion;
    public int TrackNumber;
    public bool isRepaired;

    public void ExplodeTrack()
    {
        Color color = mMaterial.color;
        color.a = Mathf.Clamp(0.5f, 0, 1);
        mMaterial.color = color;
        fires.SetActive(true);
        
        explosion.SetActive(true);
        explosion.GetComponent<ParticleSystem>().Play();
        //explosion.GetComponent<ParticleSystem>().loop = false;
        Invoke("setOffExplosion", 2.0f); // because i dont know how to stop it
    }

    void setOffExplosion()
    {
        explosion.SetActive(false);
    }

    public void MyOrbIsActive()
    {
        beacon.SetActive(true);
    }

    public void TrackRepaired()
    {
        fires.SetActive(false);
        beacon.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        isRepaired = false;
    }

    [ServerCallback]
    void OnTriggerEnter(Collider co)
    {
        //Hit another player
        if (co.tag.Equals("Player") && co.GetComponent<KartController>().TrackAssign == TrackNumber)
        {
            isRepaired = true;
            co.GetComponent<KartController>().TrackAssign = -1;
        }
    }
}
