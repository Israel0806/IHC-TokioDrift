using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class TrackController : MonoBehaviour
{
    [Header("OrbController")]
    public OrbController orbController;

    [Header("Spawns")]
    public Transform[] trackSpawns;

    [Header("Tracks")]
    public Track[] tracks;
    //public int randomNumber = 0;

    [Header("Track Prefab")]
    public GameObject trackPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //tracksDestroyed = new bool[10];
    }

    public void SelectTracks(int randomNumber)
    {
        int randomTrack/* = Random.Range(0, 10)*/;
        //tracks = new Track[10];
        Track[] AllTracks;
        AllTracks = FindObjectsOfType<Track>();

        int[10] tracksSelected; // index of tracks selected
        int tracksSelectedIndex = 0; 
        for (int index = 0; index < 10; ++index) {
            if (randomNumber == 10) randomNumber = 0;
            randomTrack = Random.Range(0, AllTracks.Length); /// elegir un track al azar
            
            /// check if track has already been selected
            for(int i = 0; i < tracksSelectedIndex; ++i )
            {

                if(RandomTrack = tracksSelected[i])
                {
                    randomTrack = Random.Range(0, AllTracks.Length);
                    i = 0;
                }
            }
            /// asign track
            tracksSelected[tracksSelectedIndex++] = randomTrack;

            tracks[index] = AllTracks[randomTrack].GetComponent<Track>();
            tracks[index].TrackNumber = randomNumber;
            randomNumber++;
        }

        //foreach (Transform trackSpawn in trackSpawns)
        //{
        //    if (randomNumber == 10) randomNumber = 0;
        //    Track track = AllTracks[randomTrack];
        //    tracks[index] = track.GetComponent<Track>();
        //    tracks[index].TrackNumber = randomNumber;
        //    index++;
        //    randomNumber++;
        //}
        Invoke("ExplodeTracks", 3f);
    }

    //public void prepareForInstance()
    //{
    //    tracks = new Track[10];
    //}

    public void Explode3fTracks()
    {
        Invoke("ExplodeTracks", 3f);
    }

    public void ExplodeTracks()
    {
        //tracks = FindObjectsOfType<Track>();
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
