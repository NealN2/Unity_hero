using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCondition : MonoBehaviour
{
    List<GameObject> monsterListInRoom = new List<GameObject>();
    public bool playerInThisRoom = false;
    public bool isClearRoom;

    void Update()
    {
        if (playerInThisRoom)
        {
            if(monsterListInRoom.Count <= 0 && !isClearRoom)
            {
                isClearRoom = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            monsterListInRoom.Add(other.gameObject);
        }

        if (other.CompareTag("Player")) //Player가 Stage에 입장했을 때
        {
            playerInThisRoom = true;
            PlayerTargeting.Instance.MonsterList = new List<GameObject>(monsterListInRoom);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) //Player가 Stage에서 나갔을 때
        {
            playerInThisRoom = false;
        }

        if (other.CompareTag("Monster"))
        {
            monsterListInRoom.Remove(other.gameObject);
        }
    }
}
