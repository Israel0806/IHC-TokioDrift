﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Track : MonoBehaviour
{
    public Material mMaterial;
    public GameObject fires;
    public GameObject beacon;
    public GameObject explosion;
    public GameObject[] effects;

    public int TrackNumber;

    public AudioClip CollectSound;
    public bool isRepaired;

    public GameObject GetObject()
    {
        return gameObject;
    }

    public void ExplodeTrack()
    {
        if(mMaterial)
        {
            Color color = mMaterial.color;
            color.a = Mathf.Clamp(0.5f, 0, 1);
            mMaterial.color = color;
        }
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

    IEnumerator StartEffect(int index, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        effects[index].SetActive(true);

    }

    public void OnTriggerEnter(Collider co)
    {
        //Hit another player
        if (co.tag.Equals("Player") && co.GetComponent<KartController>().TrackAssign == TrackNumber)
        {
            isRepaired = true;
            co.GetComponent<KartController>().TrackAssign = -1;

            if (CollectSound)
                AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            
            StartCoroutine(StartEffect(0, 0f));
            StartCoroutine(StartEffect(1, 0.1f));
            StartCoroutine(StartEffect(2, 0.2f));
            StartCoroutine(StartEffect(3, 0.3f));
            StartCoroutine(StartEffect(4, 0.4f));
            StartCoroutine(StartEffect(5, 0.5f));
        }
    }
}
