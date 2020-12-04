using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OrbController : MonoBehaviour
{
    // [Header("Reference")]
    // [SerializeField] private Orb instanceOfCE= null;
    [Header("Initial position orbs")]
    //public Transform orbSpawn1;
    //public Transform orbSpawn2;
    //public Transform orbSpawn3;
    //public Transform orbSpawn5;
    //public Transform orbSpawn4;
    // public Transform orbSpawn6;
    // public Transform orbSpawn7;
    // public Transform orbSpawn8;
    // public Transform orbSpawn9;
    // public Transform orbSpawn10;
    // public List<Transform> orbSpawns = new List<Transform>();

    [Header("Prefab orbs")]
    public GameObject orbPrefab;

    [Header("Values")]
    public bool allOrbsCollected = false;
    public List<GameObject> orbes = new List<GameObject>();
    public bool auxAllOrbsCollected = true;
    public Track[] tracks;
    public Orb[] orbs;
    

    public void SetOrbs()
    {
        //orbes = FindObjectsOfType<Orb>();
        orbs = FindObjectsOfType<Orb>();
    }

    // public void CreateOrbs(int randomNumber)
    public void CreateOrbs()
    {
        int index = 0;
        //orbs = FindObjectsOfType<Orb>();
        foreach (Orb orb in orbs)
        {
            //if (randomNumber == 10) randomNumber = 0;
            //GameObject orb = Instantiate(orbPrefab, orbSpawn.position, orbSpawn.rotation);
            // orb.trackAsignationForOrbe = randomNumber;
            // orb.identification = randomNumber;
            orb.trackAsignationForOrbe = index;
            orb.identification = index;
            //NetworkServer.Spawn(orb);
            orbes.Add(orb.GetObject());
            ParticleSystem orbParticle1 = orb.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
            ParticleSystem orbParticle2 = orb.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
            //ParticleSystem orbParticle = orb.GetComponentInChildren(typeof(ParticleSystem)) as ParticleSystem;
            if (index < 5)
            {
                orb.playerID = 1;
                orbParticle1.startColor = new Color(0, 1, 0, .5f);
                orbParticle2.startColor = new Color(0, 1, 0, .5f);
            }
            else
            {
                orb.playerID = 2;
                orbParticle1.startColor = new Color(1, 0, 0, .5f);
                orbParticle2.startColor = new Color(1, 0, 0, .5f);
            }
            index++;
            // randomNumber++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //hasAuthority = true;
    }

    //[Server]
    void destroyOrd(GameObject orb)
    {
        //orbes.Remove(orb);
        //NetworkServer.Destroy(orb);
    }

    public void ActivateTrack(int trackAsignationForOrbe)
    {
        //if (tracks.Length == 0)
        //    tracks = FindObjectsOfType<Track>();
        foreach (Track track in tracks)
            if (track.TrackNumber == trackAsignationForOrbe)
                track.MyOrbIsActive();
    }

    // Update is called once per frame

    // void Update()
    // {
    //     auxAllOrbsCollected = true;
    //     foreach (Orb orb in orbs)
    //     {
    //         if (orb != null && orb.isCollected)
    //         {
    //             print("An orb has been collected");
    //             ActivateTrack(orb.trackAsignationForOrbe);
    //             //destroyOrd(orb);
    //             orb.DestroyGameObject();
    //             auxAllOrbsCollected = false;
    //         }
    //     }
    //     if (auxAllOrbsCollected) allOrbsCollected = true;
    // }

    //revisar si un collected de otros clientes a sido recogido 
/*
    private void HandleChangeOfOrbe(int iden , bool state)
    {
        foreach (Orb orb in orbs)
        {
            if (orb != null && orb.identification == iden && state)
            {
                print("Other player change his orb");
                ActivateTrack(orb.trackAsignationForOrbe);
                //destroyOrd(orb);
                print("Number of Orb");
                print(iden);
                orb.DestroyGameObject();
                auxAllOrbsCollected = false;
            }
        }
    }
*/

/*
    private void onEnable()
    {
        print("onEnableTrack");
        instanceOfCE.EventChangeSomeOrbe += HandleChangeOfOrbe;
    }

    private void onDisable()
    {
        print("onDisableTrack");    
        instanceOfCE.EventChangeSomeOrbe -= HandleChangeOfOrbe;       
    }
*/  


}
