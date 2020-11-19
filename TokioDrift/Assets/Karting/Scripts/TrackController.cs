using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public bool[] tracksDestroyed;
    public Track[] tracks;
    // Start is called before the first frame update
    void Start()
    {
        tracksDestroyed = new bool[10];
    }

    public void choose5Tracks()
    {

    }

    public void Wait3ExplodeTracks()
    {
        Invoke("ExplodeTracks", 3f);
    }

    public void ExplodeTracks()
    {
        tracks = FindObjectsOfType<Track>();
        foreach (Track track in tracks)
        {
            track.ExplodeTrack();
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
