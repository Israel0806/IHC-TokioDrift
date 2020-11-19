using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public Material mMaterial;
    public GameObject fires;
    public GameObject beacon;
    public GameObject explosion;


    public void ExplodeTrack()
    {
        Color color = mMaterial.color;
        color.a = Mathf.Clamp(0.5f, 0, 1);
        mMaterial.color = color;
        fires.SetActive(true);
        
        explosion.SetActive(true);
        explosion.GetComponent<ParticleSystem>().Play();
        //explosion.GetComponent<ParticleSystem>().loop = false;
        Invoke("setOffExplosion", 2.0f);
    }

    void setOffExplosion()
    {
        explosion.SetActive(false);
    }

    void MyOrbIsActive()
    {
        beacon.SetActive(true);
    }

    void TrackRepaired()
    {
        fires.SetActive(false);
        beacon.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
