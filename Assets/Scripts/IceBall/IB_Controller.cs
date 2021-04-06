using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IB_Controller : MonoBehaviour
{
    public GameObject roundPrefab;

    GameObject round;
    
    GameObject team1;
    GameObject team2;

    int score1 = 0;
    int score2 = 0;
   
    //maxSelectThrow/3即为最大回合数
    int maxSelectThrow = 3;

    int roundNum = 1;
    int maxRoundNum = 2;
 
    void Start()
    {
        team1 = GameObject.Find("Team1");
        team2 = GameObject.Find("Team2");
        round = Instantiate(roundPrefab);
    }

    void Update()
    {
       RoundEnd();
    }

    private void RoundEnd()
    {
        if (round.GetComponent<IB_Round>().selectThrow >=
            maxSelectThrow && roundNum<=maxRoundNum)
        {
            ScoreCount();
            Destroy(round);
            round = Instantiate(roundPrefab);
            roundNum++;
        }
    }

    private void ScoreCount()
    {
        List<float> team1dis = new List<float>();
        List<float> team2dis = new List<float>();

        for (int i = maxSelectThrow / 3 - 1; i >=0 ; i--)
        {
            team1dis.Add(DistanceCount(team1.transform.GetChild(i).position));
            team2dis.Add(DistanceCount(team2.transform.GetChild(i).position));
            Destroy(team1.transform.GetChild(i).gameObject);
            Destroy(team2.transform.GetChild(i).gameObject);
        }
        team1dis.Sort((x, y) => x.CompareTo(y));
        team2dis.Sort((x, y) => x.CompareTo(y));

        if (team1dis[0] <= team2dis[0])
        {
            score1++;
            Debug.Log("Team A win!!!");
            for (int i = 1; i < maxSelectThrow / 3; i++)
            {
                if (team1dis[i] <= team2dis[0])
                {
                    score1++;
                }
            }
            Debug.Log(score1);
        }
        else
        {
            score2++;
            Debug.Log("Team B win!!!");
            for (int i = 1; i < maxSelectThrow / 3; i++)
            {
                if (team1dis[i] <= team2dis[0])
                {
                    score2++;
                }
            }
            Debug.Log(score2);
        }
    }

    //距离计算给分数用
    private float DistanceCount(Vector3 ballPos)
    {
        float dis = (ballPos.x - 50) * (ballPos.x - 50) + ballPos.z * ballPos.z;
        return dis;
    }
}
