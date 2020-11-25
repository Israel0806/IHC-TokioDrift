using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using KartGame.KartSystems;

public class MoveChicken : MonoBehaviour
{
    public float[] Angles;
    GameObject Chicken;
    public float RayDistance = 1f;
    Animator ChickenAnimator;
    LayerMask TrackLayer;
    LayerMask PlayerLayer;
    public float RunSpeed;
    private bool CanRun = true;
    private bool Hitted = false;
    TextMeshProUGUI score;
    public AudioClip DeathSound;


    // Start is called before the first frame update
    void Start()
    {
        Angles = new float[5];
        Angles[0] = 0;
        Angles[1] = 30;
        Angles[2] = -30;
        Angles[3] = 90;
        Angles[4] = -90;
        RunSpeed = 5f;
        Chicken = gameObject;
        score = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
        TrackLayer = LayerMask.GetMask("Track");
        PlayerLayer = LayerMask.GetMask("Kart");
        ChickenAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!CanRun) return;

        // Move the object forward along its z axis 1 unit/second.
        if(ChickenAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run In Place"))
            transform.Translate(Vector3.forward * Time.deltaTime * RunSpeed);

        Vector3 origin = transform.position;
        //Vector3 originPlayer = new Vector3(transform.position.x, 0.3f, transform.position.z);

        for (int i = 0; i < Angles.Length; i++)
        {
            Vector3 direction = GetDirectionFromAngle(Angles[i], Vector3.up, transform.forward);

            // hit the wall
            RaycastHit hit;
            UnityEngine.Debug.DrawRay(origin, direction * 1, Color.yellow);
            if (Physics.Raycast(origin, direction, out hit, RayDistance, TrackLayer))
                this.transform.Rotate(0f, 180f , 0f, Space.World);
            // hitted by the player
            if (!Hitted && Physics.Raycast(origin, direction, out hit, RayDistance, PlayerLayer)) {


            }
        }
    }

    void OnTriggerEnter(Collider co)
    {
        if (co.tag.Equals("Player"))
        {
            if (DeathSound)
                AudioUtility.CreateSFX(DeathSound, transform.position, AudioUtility.AudioGroups.Collision, 0f);
            if (co.GetComponent<ArcadeKart>().isLocalPlayer)
            {
                float _score = int.Parse(score.text);
                score.text = (_score + 100).ToString();
            }

            Hitted = true;
            CanRun = false;
            this.transform.localScale += new Vector3(3f, 0f, -1f);
            this.transform.Rotate(0f, -180f, 0f, Space.World);
            this.transform.Rotate(-90f, 0f, 0f, Space.World);
            Invoke("KillChicken", 3);
        }
    }

    void KillChicken()
    {
        ChickenAnimator.ResetTrigger("Revive");
        ChickenAnimator.SetTrigger("Die");
        Invoke("ReviveChicken", 2);
    }

    void ReviveChicken()
    {
        Hitted = false;
        CanRun = true;
        this.transform.localScale += new Vector3(-3f, 0f, 1f);
        this.transform.Rotate(90f, 0, 0f, Space.World);
        this.transform.Rotate(0f, 180f, 0f, Space.World);

        ChickenAnimator.ResetTrigger("Die");
        ChickenAnimator.SetTrigger("Revive");
    }

    Vector3 GetDirectionFromAngle(float degrees, Vector3 axis, Vector3 zerothDirection)
    {
        Quaternion rotation = Quaternion.AngleAxis(degrees, axis);
        return (rotation * zerothDirection);
    }
}
