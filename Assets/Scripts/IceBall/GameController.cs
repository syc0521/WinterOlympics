using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //控制的冰壶
    GameObject ball;

    //AB两队放冰壶的父物体与AB两队的冰壶
    public GameObject teamA;
    public GameObject teamB;
    public GameObject ballA;
    public GameObject ballB;

    //投掷冰壶的力度
    float forceX = 500;

    //控制轮流投掷的变量，selectThrow/3+1即为当前回合数
    int selectThrow = 0;

    int maxSelectThrow = 12;

    //控制投掷阶段的变量
    int throwStage = 0;
    bool isThrow = false;

    //AB两队分数
    int scoreA = 0;
    int scoreB = 0;

    bool endGame = false;

    //初始化
    void Start()
    {

    }

    //帧执行
    void Update()
    {
        if (selectThrow >= maxSelectThrow && Input.GetKeyDown(KeyCode.Space) && !endGame)
        {
            ScoreCount();
            GameEnd();
            endGame = true;
        }

        if (selectThrow % 3 == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            BuildRoad();
        }

        if (selectThrow % 3 != 0 && Input.GetKeyUp(KeyCode.Space))
        {
            SelectThrow();
        }

        if (!isThrow)
        {
            TurnThrow();
        }
    }

    //分别投掷
    private void SelectThrow()
    {
        if (selectThrow % 3 == 1)
        {
            ball = Instantiate(ballA, ballA.transform.position,
                ballA.transform.rotation, teamA.transform);
        }
        else if (selectThrow % 3 == 2)
        {
            ball = Instantiate(ballB, ballB.transform.position,
                ballB.transform.rotation, teamB.transform);
        }
        selectThrow++;
        isThrow = false;
    }

    //投掷阶段
    private void TurnThrow()
    {
        //E确认，Q取消，控制投掷阶段
        if (Input.GetKeyUp(KeyCode.E))
        {
            throwStage++;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            throwStage--;
            switch (throwStage)
            {
                case -1:
                    ball.transform.position = new Vector3(-40f, 0.2f, 0);
                    break;
                case 0:
                    ball.transform.localEulerAngles = new Vector3(0, 0, 0);
                    break;
                case 1:
                    forceX = 500;
                    break;
            }
        }

        //变量范围0,3
        if (throwStage < 0)
            throwStage = 0;
        else if (throwStage > 3)
            throwStage = 3;

        //不同投掷阶段执行不同函数
        switch (throwStage)
        {
            case 0:
                ChangeHorizontal();
                break;
            case 1:
                ChangeDirection();
                break;
            case 2:
                ChangeForce();
                break;
            case 3:
                Vector3 force = Quaternion.AngleAxis(90, Vector3.up) *
                    ball.transform.forward;
                ball.GetComponent<Rigidbody>().
                    AddForce(force * forceX);
                forceX = 500;
                isThrow = true;
                throwStage = 0;
                break;
        }
    }

    //改变投掷的水平方向
    private void ChangeHorizontal()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ball.transform.Translate(new Vector3(0, 0, 4f) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ball.transform.Translate(new Vector3(0, 0, -4f) * Time.deltaTime);
        }
    }

    //改变投掷的角度
    private void ChangeDirection()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ball.transform.Rotate(new Vector3(0, 10f, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ball.transform.Rotate(new Vector3(0, -10f, 0) * Time.deltaTime);
        }
    }

    //改变投掷的力度
    private void ChangeForce()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            forceX += 50 * Time.deltaTime;
            Debug.Log("当前力度：" + (int)forceX);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            forceX -= 50 * Time.deltaTime;
            Debug.Log("当前力度：" + (int)forceX);
        }
    }

    //建造赛道
    private void BuildRoad()
    {
        Debug.Log("Building!" + selectThrow);
        selectThrow++;
    }

    //游戏结束
    private void GameEnd()
    {
        Debug.Log("Team A score:" + scoreA);
        Debug.Log("Team B score:" + scoreB);
        Time.timeScale = 0;
    }

    //分数计算
    private void ScoreCount()
    {
        List<float> teamAdis = new List<float>();
        List<float> teamBdis = new List<float>();
        Debug.Log("a");
        for (int i = 0; i < maxSelectThrow / 3; i++)
        {
            Debug.Log("b");
            teamAdis.Add(DistanceCount(teamA.transform.GetChild(i).position));
            teamBdis.Add(DistanceCount(teamB.transform.GetChild(i).position));
        }
        teamAdis.Sort((x, y) => x.CompareTo(y));
        teamBdis.Sort((x, y) => x.CompareTo(y));

        for (int i = 0; i < maxSelectThrow / 3; i++)
        {
            Debug.Log("teamAdis " + i + ":" + teamAdis[i]);
            Debug.Log("teamBdis " + i + ":" + teamBdis[i]);
        }

        if (teamAdis[0] <= teamBdis[0])
        {
            scoreA++;
            Debug.Log("Team A win!!!");
            for (int i = 1; i < maxSelectThrow / 3; i++)
            {
                if (teamAdis[i] <= teamBdis[0])
                {
                    scoreA++;
                }
            }
        }
        else
        {
            scoreB++;
            Debug.Log("Team B win!!!");
            for (int i = 1; i < maxSelectThrow / 3; i++)
            {
                if (teamBdis[i] <= teamAdis[0])
                {
                    scoreB++;
                }
            }
        }
    }

    //距离计算给分数用
    private float DistanceCount(Vector3 ballPos)
    {
        float dis = (ballPos.x - 50) * (ballPos.x - 50) + ballPos.z * ballPos.z;
        return dis;
    }
}