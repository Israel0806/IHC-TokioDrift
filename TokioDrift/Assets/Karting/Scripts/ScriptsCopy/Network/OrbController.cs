using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OrbController : MonoBehaviour
{
    // [Header("Reference")]
    // [SerializeField] private Orb instanceOfCE= null;
    [Header("Initial position orbs")]
    public Transform orbSpawn1;
    public Transform orbSpawn2;
    public Transform orbSpawn3;
    public Transform orbSpawn5;
    public Transform orbSpawn4;
    public Transform orbSpawn6;
    public Transform orbSpawn7;
    public Transform orbSpawn8;
    public Transform orbSpawn9;
    public Transform orbSpawn10;
    public List<Transform> orbSpawns = new List<Transform>();

    [Header("Prefab orbs")]
    public GameObject orbPrefab;

    [Header("Values")]
    public bool allOrbsCollected = false;
    public List<GameObject> orbes = new List<GameObject>();
    //public int randomNumber = 0;
    public bool auxAllOrbsCollected = true;
    public Track[] tracks;
    public Orb[] orbs;
    //Cuando iniciamos el servidor: 
    //1)creamos los orb sgun el random
    //2)Asignamos su random
    //public override void OnStartServer()
    //{

    //}

    public void SetOrbs()
    {
        //orbes = FindObjectsOfType<Orb>();
        orbs = FindObjectsOfType<Orb>();
    }

    public void CreateOrbs(int randomNumber)
    {
        orbSpawns.Add(orbSpawn1);
        orbSpawns.Add(orbSpawn2);
        orbSpawns.Add(orbSpawn3);
        orbSpawns.Add(orbSpawn4);
        orbSpawns.Add(orbSpawn5);
        orbSpawns.Add(orbSpawn6);
        orbSpawns.Add(orbSpawn7);
        orbSpawns.Add(orbSpawn8);
        orbSpawns.Add(orbSpawn9);
        orbSpawns.Add(orbSpawn10);
        //randomNumber = Random.Range(0, 10);
        foreach (Transform orbSpawn in orbSpawns)
        {
            if (randomNumber == 10) randomNumber = 0;
            GameObject orb = Instantiate(orbPrefab, orbSpawn.position, orbSpawn.rotation);
            orb.GetComponent<Orb>().trackAsignationForOrbe = randomNumber;
            orb.GetComponent<Orb>().identification = randomNumber;
            //NetworkServer.Spawn(orb);
            orbes.Add(orb);
            randomNumber++;
        }
        SetOrbs();
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
        if (tracks.Length == 0)
            tracks = FindObjectsOfType<Track>();
        foreach (Track track in tracks)
            if (track.TrackNumber == trackAsignationForOrbe)
                track.MyOrbIsActive();
    }

    // Update is called once per frame
    void Update()
    {
        auxAllOrbsCollected = true;
        foreach (Orb orb in orbs)
        {
            if (orb != null && orb.isCollected)
            {
                print("An orb has been collected");
                ActivateTrack(orb.trackAsignationForOrbe);
                //destroyOrd(orb);
                orb.DestroyGameObject();
                auxAllOrbsCollected = false;
            }

        }
            
        //revisar si un collected de otros clientes a sido recogido 

        if (auxAllOrbsCollected) allOrbsCollected = true;
    }
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
