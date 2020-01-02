using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    //singleton
    public static PlayerTargeting Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerTargeting>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerTargeting");
                    instance = instanceContainer.AddComponent<PlayerTargeting>();
                }
            }
            return instance;
        }
    }
    private static PlayerTargeting instance;


    public bool getATarget = false;
    float currentDist = 0;       //현재 거리
    float closetDist = 100f;     //가까운 거리
    float TargetDist = 100f;     //타켓 거리
    int closeDistIndex = 0;      //가장 가까운 인덱스
    int TargetIndex = -1;        //타겟팅할 인덱스
    public LayerMask layerMask;

    public List<GameObject> MonsterList = new List<GameObject>(); //몬스터를 담는 List

    public GameObject Boomerang; //발사체
    public Transform AttackPoint;
    
    void Update()
    {
        if(MonsterList.Count != 0)
        {
            currentDist = 0f;
            closeDistIndex = 0;
            TargetIndex = -1;

            for(int i = 0; i < MonsterList.Count; i++)
            {
                currentDist = Vector3.Distance(transform.position, MonsterList[i].transform.position);

                RaycastHit rayHit;
                bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.position - transform.position, out rayHit, 20f, layerMask);

                if(isHit && rayHit.transform.CompareTag("Monster"))
                {
                    if(TargetDist >= currentDist)
                    {
                        TargetIndex = i;
                        TargetDist = currentDist;
                    }
                }

                if (closetDist >= currentDist)
                {
                    closeDistIndex = i;
                    closetDist = currentDist;
                }

            }

            if(TargetIndex == -1)
            {
                TargetIndex = closeDistIndex;
            }

            //초기화
            closetDist = 100f;
            TargetDist = 100f;
            getATarget = true;
        }

        if(getATarget && !JoyStick.Instance.isPlayerMoving)
        {
            

            //transform.LookAt(new Vector3(MonsterList[TargetIndex].transform.position.x,
             //                transform.position.y, MonsterList[TargetIndex].transform.position.z));

            if (PlayerMove.Instance.anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                PlayerMove.Instance.anim.SetBool("Idle", false);
                PlayerMove.Instance.anim.SetBool("Attack", true);
            }
        }

        else if (JoyStick.Instance.isPlayerMoving)
        {
            if (!PlayerMove.Instance.anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            {
                PlayerMove.Instance.anim.SetBool("Idle", false);
                PlayerMove.Instance.anim.SetBool("Walk", true);
                PlayerMove.Instance.anim.SetBool("Attack", false);
            }
        }

    }

    void Attack()
    {
        Instantiate(Boomerang, AttackPoint.position, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        if (getATarget)
        {
            for(int i = 0; i < MonsterList.Count; i++)
            {
                RaycastHit rayHit;
                bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.position - transform.position, out rayHit, 30f, layerMask);

                if (isHit && rayHit.transform.CompareTag("Monster"))
                    Gizmos.color = Color.green;

                else
                    Gizmos.color = Color.red;

                Gizmos.DrawRay(transform.position, MonsterList[i].transform.position - transform.position);
            }
        }
    }
}
