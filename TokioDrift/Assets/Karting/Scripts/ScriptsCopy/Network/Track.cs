using System.Collections;
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
    public int identification;

    public int TrackNumber;

    public AudioClip CollectSound;
    public bool isRepaired;
    private GameFlowManager GameFlow ;
    public bool isReady = false;
/*
    public delegate void ChangeSomeTrack(int changeTrack, bool state);

    [SyncEvent]
    public event ChangeSomeTrack EventChangeSomeTrack;

    public GameObject GetObject()
    {
        return gameObject;
    }

    #region Server 
    [Server]
    private void SetChangeTrack(int changeTrack)
    {
        print("SetChangeTrack");
        EventChangeSomeTrack?.Invoke(changeTrack, true); 
    }

    [Command]
    private void CmdSetChangeTrack(int val) => SetChangeTrack(val);
    

    #endregion
*/
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
        GameFlow =  GameObject.Find("GameManager").GetComponent<GameFlowManager>();    
        isRepaired = false;
    }

    IEnumerator StartEffect(int index, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        effects[index].SetActive(true);

    }
    //
    public void OnTriggerEnter(Collider co)
    {
        //Hit another player
        if (co.tag.Equals("Player"))
        {
            //print("UNO-1T");
            if (co.GetComponent<KartController>().TrackAssign == TrackNumber && !isRepaired)
            {
                //print("UNO-2T");    
                isRepaired = true;
                co.GetComponent<KartController>().TrackAssign = -1;
                //print("UNO-3T");
                GameFlow.myScore += 100;
                //co.GetComponent<KartController>().score += 40; // if it is repair, plus
                //print("UNO-4T");
                if (CollectSound)
                    AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);

                StartCoroutine(StartEffect(0, 0f));
                StartCoroutine(StartEffect(1, 0.1f));
                StartCoroutine(StartEffect(2, 0.2f));
                StartCoroutine(StartEffect(3, 0.3f));
                StartCoroutine(StartEffect(4, 0.4f));
                StartCoroutine(StartEffect(5, 0.5f));
                //print("UNO-5T");
            }
            else
            {
                GameFlow.myScore -= 15;
                //co.GetComponent<KartController>().score -= 15;// if not correspond to pass there
            }
        }
    }

/*
    #region Client 
    
    [ClientCallback]
    private void Update()
    {
        if (!hasAuthority) { return;} 
        if (isRepaired) CmdSetChangeTrack(identification);               
    }

    #endregion
    */
}
