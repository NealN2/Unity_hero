using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance  //singleton
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StageManager>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("StageManager");
                    instance = instanceContainer.AddComponent<StageManager>();
                }
            }
            return instance;
        }
    }
    private static StageManager instance;


    public GameObject Player;  //Player 가져오기
    public GameObject Camera;

    //Stage 배열을 직렬화해 인스펙터창에서 다룰 수 있게 함   /  NormalMonsterStage의 StartPos배열
    [System.Serializable]
    public class NoramlStageGroup
    {
        public List<Transform> NormalStartPos = new List<Transform>();
    }
    public NoramlStageGroup[] NoramlStageGroups;

    //Angle,Boss,LastBoss Stage의 StartPos들
    public List<Transform> BossStartPos = new List<Transform>();
    public List<Transform> SantaStartPos = new List<Transform>();
    public Transform LastBossStartPos;

    public int curStage;     //현재 Stage
    int lastBossStage = 20;  //LastBoss Stage
    int tempIndex;

    public bool stageChanging = false;


    public void EnterNextStage()
    {
        
        curStage++;

        if (curStage > lastBossStage) { return; }

        if(curStage % 5 != 0)
        {
            int groupIndex = curStage / 10; //1~10 Stage인지 10~20 Stage인지 판별
            //groupIdx 안의 normalstartpos 리스트 중에 randomIndex의 위치의 값을 가져오기 위해 Random.Range 사용
            int randomIndex = Random.Range(0, NoramlStageGroups[groupIndex].NormalStartPos.Count);

            if(randomIndex == tempIndex)
            {
                if ((randomIndex + 1) == NoramlStageGroups[groupIndex].NormalStartPos.Count)
                    randomIndex--;

                else
                    randomIndex++;
            }

            //Player의 Pos를 NormalStartPos[randomIndex]의 Pos로 이동
            Player.transform.position = NoramlStageGroups[groupIndex].NormalStartPos[randomIndex].transform.position;

            tempIndex = randomIndex; // 같은 Stage가 바로 반복되는 걸 막기 위해 임시Index에 randomIndex값을 저장
        }

        else
        {
            if(curStage % 10 == 5) //Angel Stage
            {
                int groupIndex = curStage / 10;

                if (groupIndex == 0)
                    Player.transform.position = SantaStartPos[0].transform.position;

                else
                    Player.transform.position = SantaStartPos[1].transform.position;

                Debug.Log("Enter Santa Stage");
            }

            else if(curStage == lastBossStage) //LastBoss
            {
                Player.transform.position = LastBossStartPos.transform.position;
                Debug.Log("Enter LastBoss Stage");
            }

            else //Middle Boss
            {
                int randomIndex = Random.Range(0, BossStartPos.Count);

                Player.transform.position = BossStartPos[randomIndex].transform.position;
                
                //같은 Boss가 반복되는 걸 막기 위해 randomIndex에 있는 값을 삭제
                BossStartPos.RemoveAt(randomIndex);

                Debug.Log("Enter Middle Boss Stage");
            }
        }

        Camera.GetComponent<CameraMove>().CamNextStage();
    }
}
