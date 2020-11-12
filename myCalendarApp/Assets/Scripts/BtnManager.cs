using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    public GameObject DaySet;

    public Text dayText;

    public void DayBtn(bool isDown)
    {
        if (isDown)
        {
            gameObject.GetComponentInChildren<Text>().text = dayText.text;
            DaySet.SetActive(true);
        }
    }


}
