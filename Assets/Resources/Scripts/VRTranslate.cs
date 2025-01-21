using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTranslate : MonoBehaviour
{
    public float translate_gain = 1.0f;
    public float rotation_gain = 1.0f;
    public float curvature_gain = 1.0f;

    public float m_speed = 0.01f;
    public float r_speed = 0.1f;
    GameObject playerCam;
    GameObject playerReal;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.Find("PlayerCam");
        playerReal = GameObject.Find("PlayerReal");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            smartCamDisplace();
            playerReal.transform.position += playerReal.transform.forward * m_speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            smartCamDisplace();
            playerReal.transform.position += -playerCam.transform.forward * m_speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            smartCamDisplace();
            playerReal.transform.eulerAngles -= new Vector3(0.0f, r_speed, 0.0f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            smartCamDisplace();
            playerReal.transform.eulerAngles += new Vector3(0.0f, r_speed, 0.0f);
        }
    }

    void smartCamDisplace()
    {
        translateCam();
        rotateCam();
        curveCam();
    }

    //to complete
    void translateCam()
    {
        
    }

    //to complete
    void rotateCam()
    {
        
    }

    //to complete
    void curveCam()
    {
        
    }

    

}
