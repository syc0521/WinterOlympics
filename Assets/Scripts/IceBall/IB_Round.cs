using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IB_Round : MonoBehaviour
{
    /*
     * 上下左右轴输入，其余均虚拟按键输入
     * 默认2人键盘对战，如果4人则改additionalPlayer的值
     * 手柄与键盘操作对应：摇杆上下左右，a确认，b取消，x，y道具
     * 手柄功能按键：start开始，back暂停
     * 键盘操作P1：WASD，E确认，Q取消，1，2道具
     * 键盘操作P2：OKL;，O确认，U取消，8，9道具
     * 功能按键：space开始，esc，backspace暂停
     * 游戏中的操作顺序（4人例，P1P3同队共用左侧键盘，P2P4右侧）：
     * P1P2放障碍，选道具，P1P2投掷冰壶，P3P4控制冰壶
     * P3P4放障碍，选道具，P3P4投掷冰壶，P1P2控制冰壶循环
    */
    int horizontal, vertical = 0;
    int additionalPlayer = 0;
    
    //本回合能操作的玩家
    int player = 1;

    //存储游戏运行时间的变量
    int time;

    //控制的冰壶与额外属性,起始位置-50，0.2，0，旋转0，0，0
    GameObject ball;
    float forceX = 900;
    Vector3 direction;

    //12两队放冰壶的父物体与12两队的冰壶
    GameObject team1;
    GameObject team2;
    public GameObject ball1Prefab;
    public GameObject ball2Prefab;

    //控制游戏进度的变量，selectThrow/3+1即为当前回合数
    public int selectThrow = 0;
  
    //冰壶投掷阶段，能否丢冰壶，冰壶是否停下
    int throwStage = 0;
    bool canThrow = false;
    bool isStop = true;

    // Start is called before the first frame update
    void Start()
    {
        team1 = GameObject.Find("Team1");
        team2 = GameObject.Find("Team2");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start") && !canThrow && isStop)
        {
            SelectThrow();
        }
        else if (canThrow)
        {
            TurnThrow();
        }
        else if (!isStop)
        {
            ControlBall();
        }
    }

    //投掷行动的玩家
    private void SelectThrow()
    {
        if (selectThrow % 3 == 1)
        {
            ball = Instantiate(ball1Prefab, ball1Prefab.transform.position,
                ball1Prefab.transform.rotation, team1.transform);
            player = selectThrow % 3 + ((selectThrow / 3) % 2) * additionalPlayer;
            Debug.Log("Player" + player + " to run!");
            canThrow = true;
        }
        else if (selectThrow % 3 == 2)
        {
            ball = Instantiate(ball2Prefab, ball2Prefab.transform.position,
                ball2Prefab.transform.rotation, team2.transform);
            player = selectThrow % 3 + ((selectThrow / 3) % 2) * additionalPlayer;
            Debug.Log("Player" + player + " to run!");
            canThrow = true;
        }
        else
        {
            BuildRoad();
        }
    }

    //投掷阶段
    private void TurnThrow()
    {
        if (Input.GetButtonDown("P" + player + "Confirm"))
        {
            throwStage++;
        }
        else if (Input.GetButtonDown("P"+player+"Cancel"))
        {
            throwStage--;
            switch (throwStage)
            {
                case -1:
                    ball.transform.position = new Vector3(-50f, 0.2f, 0);
                    break;
                case 0:
                    ball.transform.localEulerAngles = new Vector3(0, 0, 0);
                    break;
                case 1:
                    forceX = 900;
                    break;
            }
        }

        //最多四个投掷阶段
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
                direction = Quaternion.AngleAxis(90, Vector3.up) *
                    ball.transform.forward;
                ball.GetComponent<Rigidbody>().
                    AddForce(direction * forceX);
                forceX = 900;
                throwStage = 0;
                canThrow = false;
                isStop = false;
                time = (int)Time.fixedTime;

                player += additionalPlayer;
                if (player > 4)
                    player -= 4;
                break;
        }
    }

    //改变投掷的水平方向
    private void ChangeHorizontal()
    {
        horizontal = (int)Input.GetAxisRaw("P"+player+"Horizontal");
        if (horizontal != 0)
        {
            ball.transform.Translate(horizontal * new Vector3(0, 0, -5f) * Time.deltaTime);
        }
    }

    //改变投掷的角度
    private void ChangeDirection()
    {
        horizontal = (int)Input.GetAxisRaw("P"+player+"Horizontal");
        if (horizontal != 0)
        {
            ball.transform.Rotate(horizontal * new Vector3(0, 15f, 0) * Time.deltaTime);
        }
    }

    //改变投掷的力度
    private void ChangeForce()
    {
        vertical = (int)Input.GetAxisRaw("P"+player+"Vertical");
        if (vertical != 0)
        {
            forceX += vertical * 100 * Time.deltaTime;
            Debug.Log("当前力度：" + (int)forceX);
        }
    }

    //控制冰壶微调角度
    private void ControlBall()
    {         
        if ((int)Time.fixedTime - time > 5 &&
            ball.GetComponent<Rigidbody>().velocity.magnitude < 0.1)
        {
            isStop = true;
            selectThrow++;
        }
        horizontal = (int)Input.GetAxisRaw("P" + player + "Horizontal");
        if (horizontal != 0)
        {
            direction.z += horizontal * -5f * Time.deltaTime;
            ball.GetComponent<Rigidbody>().velocity = new Vector3(
                ball.GetComponent<Rigidbody>().velocity.x,
                ball.GetComponent<Rigidbody>().velocity.y,
                (float)(ball.GetComponent<Rigidbody>().velocity.z+ horizontal*-30f * Time.deltaTime*
                ball.GetComponent<Rigidbody>().velocity.x*0.01));
        }
    }

    private void BuildRoad()
    {
        Debug.Log("Building!" + selectThrow);
        selectThrow++;
    }
}
