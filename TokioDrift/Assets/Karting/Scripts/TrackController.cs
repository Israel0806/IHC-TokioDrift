using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class TrackController : NetworkBehaviour
{
    [Header("OrbController")]
    public OrbController orbController;

    [Header("Spawns")]
    public Transform[] trackSpawns;

    [Header("Tracks")]
    public Track[] tracks;
    public int randomNumber = 0;

    [Header("Track Prefab")]
    public GameObject trackPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //tracksDestroyed = new bool[10];
    }

    [Server]
    void CreateTracks()
    {
        int index = 0;
        randomNumber = Random.Range(0, 10);
        tracks = new Track[10];
        foreach (Transform trackSpawn in trackSpawns)
        {
            if (randomNumber == 10) randomNumber = 0;
            GameObject track = Instantiate(trackPrefab, trackSpawn.position, trackSpawn.rotation);
            NetworkServer.Spawn(track);
            tracks[index] = track.GetComponent<Track>();
            tracks[index].TrackNumber = randomNumber;
            index++;
            randomNumber++;
        }
    }

    public void prepareForInstance()
    {
        tracks = new Track[10];
    }

    public void Explode3Tracks()
    {
        Invoke("ExplodeTracks", 3f);
    }

    public void ExplodeTracks()
    {
        tracks = FindObjectsOfType<Track>();
        foreach (Track track in tracks)
            track.ExplodeTrack();
    }



    // Update is called once per frame
    void Update()
    {
        foreach (Track track in tracks)
            if (track != null && track.isRepaired)
                track.TrackRepaired();

    }
}
