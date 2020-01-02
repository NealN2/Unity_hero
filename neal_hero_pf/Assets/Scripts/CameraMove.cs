using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Player;

    public float offSetY = 45f;
    public float offSetZ = -40f;

    Vector3 CameraPos;

    private void Start()
    {
        CameraPos.x = Player.transform.position.x;
    }

    void LateUpdate()
    {
        //CameraPos.x = transform.position.x;
        CameraPos.y = Player.transform.position.y + offSetY;
        CameraPos.z = Player.transform.position.z + offSetZ;

        transform.position = CameraPos; 
    }

    public void CamNextStage()
    {
        Debug.Log("CamNextStage!");
        CameraPos.x = Player.transform.position.x;
    }

}
