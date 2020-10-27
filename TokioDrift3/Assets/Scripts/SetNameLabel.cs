using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetNameLabel : MonoBehaviour
{
    public GameObject nameGroup;
    public TextMeshProUGUI name;
    public GameObject canvasRect;
    public Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("username"))
            name.text = "Username";
        else
            name.text = PlayerPrefs.GetString("username");
    }

    // Update is called once per frame
    void Update()
    {
        // Offset position above object bbox (in world space)
        float offsetPosY = this.transform.position.y + 1.5f;

        // Final position of marker above GO in world space
        Vector3 offsetPos = new Vector3(this.transform.position.x, offsetPosY, this.transform.position.z);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 canvasPos;
        Vector2 screenPoint = mainCamera.WorldToScreenPoint(offsetPos);

        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect.GetComponent<RectTransform>(), screenPoint, null, out canvasPos);

        // Set
        nameGroup.transform.localPosition = canvasPos;
    }
}
