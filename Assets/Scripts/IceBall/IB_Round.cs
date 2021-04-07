using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class IB_Round : MonoBehaviour
{
    /*
        * 基本操作与键位设置：
        * 
        *      上下左右轴输入，其余均虚拟按键输入
        *      默认2人键盘对战，如果4人则改additionalPlayer的值
        *      如果4人手柄则2人操控一个手柄
        *      2人手柄键盘都支持，4人则仅支持手柄
        *      手柄与键盘操作对应：左侧摇杆上下左右，上键确认，下键取消，
        *          左右键副水平轴，LB1技能，LT2技能
        *      手柄功能按键：start开始，back暂停
        *      键盘操作P1：WASD，E确认，Q取消，2，3道具
        *      键盘操作P2：IJKL，U确认，O取消，8，9道具
        *      功能按键：space开始，esc，backspace暂停
        *      游戏中的操作顺序（4人例，P1P3同队，P2P4同队）：
        *      P1P2放障碍，选道具，P1P2投掷冰壶，P3P4控制冰壶
        *      P3P4放障碍，选道具，P3P4投掷冰壶，P1P2控制冰壶循环
        *      
    */
        
    //存储游戏运行时间的变量，控制每回合进程
    int time;

    private CinemachineVirtualCamera lookDown;
    GameObject turnText;
        
    //本回合能操作的玩家，玩家的轴输入，额外玩家数量控制总人数
    int player = 1;
    int horizontal, vertical = 0;
    int additionalPlayer = 0;

    //控制的冰壶，起始位置（0，0.5，-50），旋转（0，0，0)
    //冰壶的额外属性：基础施力900，物体朝向或速度方向保持一致
    GameObject ball;
    float force = 900;
    Vector3 direction;

    //12两队存放冰壶的父物体，12两队的冰壶的预制体
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

    //技能模块，buttonX，Y链接到UI画布上的技能缩略图
    //skill技能列表
    //存储每队的技能
    //获得当前玩家所在队伍的技能
    GameObject buttonX;
    GameObject buttonY;
    List<string> skill = new List<string>();
    int team1SkillX, team1SkillY, team2SkillX, team2SkillY = 0;
    int playerSkillX, playerSkillY = 0;

    GameObject buildStart;
    public Sprite[] skills;
    GameObject t1skx, t1sky, t2skx, t2sky;
    bool tsk = false;
    GameObject skx, sky;    
    GameObject throwStart;
    GameObject turnPlayer;

    //建筑模块，用一个a*b大小的区域映射实际中z*x的区域
    //虚拟区域每一格对应实际区域中的大小比例
    //topLeft左上角的坐标（非物体中心的轴）
    //虚拟区域的长宽
    //物体在虚拟区域中左上角所在的格子位置
    Vector3 topLeft = new Vector3(-14, 0, -24);
    static int unit = 4;
    static int MaxLengthZ = 12;
    static int MaxLengthX = 7;
    int lengthZ1 = 0;
    int lengthX1 = 0;
    int lengthZ2 = 0;
    int lengthX2 = 0;

    GameObject p1, p2;


    //存储建筑预制体
    //建筑的额外数据，分别为物体在虚拟区域中的长高宽所占格子数
    //存储建筑的临时变量，建筑的生成位置，建筑预制体的索引
    //数组存储当前位置是否能建造
    bool isBuild = true;
    int selectBuild1 = 0;
    int selectBuild2 = 0;
    int team1Build = 0;
    int team2Build = 0;

    public GameObject[] buildingPrefab = new GameObject[8];
    float[,] buildingData = new float[8, 3]
    {    
        {2,0,2},
        {2,0,2},
        {1,0,1},
        {2,0,2},
        {1,0,1},
        {2,0,2},
        {1,0,1},
        {1,0,1}
     };
    GameObject buildings;
    GameObject building1;
    GameObject building2;
    Vector3 buildingPos1;
    Vector3 buildingPos2;
    bool[,] canBuild = new bool[MaxLengthX, MaxLengthZ];

    /* 
        * 初始化：
        *      
        *      队伍12的寻找
        *      技能链接UI的寻找
        *      增加技能
        *      初始化建筑区域为都能建造
        *      
        */

    void Start()
    {
        lookDown = GameObject.Find("LookDown").GetComponent<CinemachineVirtualCamera>();

        turnText = GameObject.Find("TurnText");
        turnText.SetActive(false);

        t1skx = GameObject.Find("t1skx");
        t1sky = GameObject.Find("t1sky");
        t2skx = GameObject.Find("t2skx");
        t2sky = GameObject.Find("t2sky");

        skx = GameObject.Find("skx");
        sky = GameObject.Find("sky");
        turnPlayer = GameObject.Find("turnplayer");
        throwStart = GameObject.Find("ThrowStart");
        throwStart.SetActive(false);

        p1 = GameObject.Find("p1");
        p2 = GameObject.Find("p2");

        team1 = GameObject.Find("Team1");
        team2 = GameObject.Find("Team2");

        buildings = GameObject.Find("Buildings");

        buttonX = GameObject.Find("ButtonX");
        buttonY = GameObject.Find("ButtonY");
            
        skill.Add("跳");
        skill.Add("巨大化");
        skill.Add("冲击波");
        skill.Add("氮气");
        skill.Add("防滑");
        skill.Add("碰碰车");

        for (int i = 0; i < MaxLengthX; i++)
        {
            for (int j = 0; j < MaxLengthZ; j++)
            {
                canBuild[i, j] = true;
            }
        }

        buildStart = GameObject.Find("BuildStart");
        buildStart.SetActive(false);
    }

    /* 
        * 物理帧执行：
        * 
        *      用于执行物理模拟以及用不到输入监听的事件
        *      
        */
    private void FixedUpdate()
    {
            
    }

    /*
        * 帧执行：
        *      
        *      仅执行输入监听
        * 
        */
    void Update()
    {
        if (Input.GetButtonDown("Start") && !canThrow && isStop && isBuild)
        {
            turnText.SetActive(true);
            turnText.GetComponent<Text>().text = "回合"+(selectThrow/3+1)+"!";

            buildStart.SetActive(true);

            Skills();
            isBuild = false;
            if ((selectThrow / 3 + 1) % 2 == 0)
                player = 1;
            else
                player = 1 + additionalPlayer;
            lookDown.Priority = 20;
        }

        if ( !canThrow && isStop)
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

            tsk = true;

            playerSkillX = team1SkillX;
            playerSkillY = team1SkillY;

            skx.GetComponent<Image>().sprite = skills[playerSkillX];
            sky.GetComponent<Image>().sprite = skills[playerSkillY];

            turnPlayer.GetComponent<Text>().text = "Player"+player+"'s turn!";

            throwStart.SetActive(true);
        }
        else if (selectThrow % 3 == 2)
        {
            ball = Instantiate(ball2Prefab, ball2Prefab.transform.position,
                ball2Prefab.transform.rotation, team2.transform);
            player = selectThrow % 3 + ((selectThrow / 3) % 2) * additionalPlayer;
            Debug.Log("Player" + player + " to run!");
            canThrow = true;

            tsk = true;

            playerSkillX = team2SkillX;
            playerSkillY = team2SkillY;

            skx.GetComponent<Image>().sprite = skills[playerSkillX];
            sky.GetComponent<Image>().sprite = skills[playerSkillY];

            turnPlayer.GetComponent<Text>().text = "Player" + player + "'s turn!";

            throwStart.SetActive(true);
        }
        else if (!isBuild)
        {
            SelectBuild();
        }
    }

    //投掷阶段
    private void TurnThrow()
    {
        if (Input.GetButtonDown("P" + player + "Confirm"))
        {
            throwStage++;
        }
        else if (Input.GetButtonDown("P" + player + "Cancel"))
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
                    force = 900;
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
                direction = ball.transform.forward;
                ball.GetComponent<Rigidbody>().
                    AddForce(direction * force);
                force = 900;
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
        horizontal = (int)Input.GetAxisRaw("P" + player + "Horizontal");
        if (horizontal != 0)
        {
            ball.transform.Translate(horizontal * new Vector3(5f, 0, 0) * Time.deltaTime);
        }
    }

    //改变投掷的角度
    private void ChangeDirection()
    {
        horizontal = (int)Input.GetAxisRaw("P" + player + "Horizontal");
        if (horizontal != 0)
        {
            ball.transform.Rotate(horizontal * new Vector3(0, 15f, 0) * Time.deltaTime);
        }
    }

    //改变投掷的力度
    private void ChangeForce()
    {
        vertical = (int)Input.GetAxisRaw("P" + player + "Vertical");
        if (vertical != 0)
        {
            force += vertical * 80 * Time.deltaTime;
            Debug.Log("当前力度：" + (int)force);
        }
    }

    /* 
        * 控制冰壶：
        *      
        *      判断是否停下以进行下一步操作条件：投掷后事件至少过去5秒且速度小于0.1
        *      速度改变：获得当前帧的速度的方向并改变并单位化，乘以当前速度的大小
        *      技能释放与效果
        * 
        */
    private void ControlBall()
    {
        if ((int)Time.fixedTime - time > 5 &&
            ball.GetComponent<Rigidbody>().velocity.magnitude < 0.1)
        {
            isStop = true;
            canThrow = false;
            selectThrow++;
            throwStart.SetActive(false);
        }

        horizontal = (int)Input.GetAxisRaw("P" + player + "Horizontal");

        if (horizontal != 0)
        {
            direction = ball.GetComponent<Rigidbody>().velocity.normalized;
            direction = new Vector3(direction.x + 0.5f * horizontal * Time.deltaTime,
                direction.y, direction.z).normalized;

            ball.GetComponent<Rigidbody>().velocity =
                ball.GetComponent<Rigidbody>().velocity.magnitude *
                direction;
        }

        if (Input.GetButtonDown("P" + player + "X") && tsk)
        {
            switch (playerSkillX)
            {
                case 0:
                    ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, 0));
                    break;
                case 1:
                    ball.transform.localScale = new Vector3(4, 0.4f, 4);
                    break;
                case 2:
                    break;
                case 3:
                    ball.GetComponent<Rigidbody>().velocity = new Vector3(
                        1.2f * ball.GetComponent<Rigidbody>().velocity.x,
                        ball.GetComponent<Rigidbody>().velocity.y,
                        1.2f * ball.GetComponent<Rigidbody>().velocity.z);
                    break;
                case 4:
                    ball.GetComponent<Rigidbody>().drag = 5;
                    break;
                case 5:
                    ball.GetComponent<Rigidbody>().mass = 0.2f;
                    break;
            }
            tsk = false;
        }

        if (Input.GetButtonDown("P" + player + "Y") && tsk)
        {
            switch (playerSkillY)
            {
                case 0:
                    ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, 0));
                    break;
                case 1:
                    ball.transform.localScale = new Vector3(4, 0.4f, 4);
                    break;
                case 2:
                    break;
                case 3:
                    ball.GetComponent<Rigidbody>().velocity = new Vector3(
                        1.2f * ball.GetComponent<Rigidbody>().velocity.x,
                        ball.GetComponent<Rigidbody>().velocity.y,
                        1.2f * ball.GetComponent<Rigidbody>().velocity.z);
                    break;
                case 4:
                    ball.GetComponent<Rigidbody>().drag = 5;
                    break;
                case 5:
                    ball.GetComponent<Rigidbody>().mass = 0.2f;
                    break;
            }
            tsk = false;
        }
    }


    private void SelectBuild()
    {
        switch (selectBuild1)
        {
            case 0:
                if (Input.GetButtonDown("P"+player+"Left"))
                {
                    team1Build--;
                    p1.transform.position = new Vector3(
                        p1.transform.position.x - UnityEngine.Screen.width / 8,
                        p1.transform.position.y,
                        p1.transform.position.z);
                }
                else if (Input.GetButtonDown("P" + player + "Right"))
                {
                    team1Build++;
                    p1.transform.position = new Vector3(
                        p1.transform.position.x + UnityEngine.Screen.width / 8,
                        p1.transform.position.y,
                        p1.transform.position.z);
                }
                if (team1Build > buildingPrefab.Length-1)
                {
                    team1Build = buildingPrefab.Length-1;
                    p1.transform.position = new Vector3(
                        p1.transform.position.x - UnityEngine.Screen.width / 8,
                        p1.transform.position.y,
                        p1.transform.position.z);
                }
                else if (team1Build < 0)
                {
                    team1Build = 0;
                    p1.transform.position = new Vector3(
                        p1.transform.position.x + UnityEngine.Screen.width / 8,
                        p1.transform.position.y,
                        p1.transform.position.z);
                }                  

                if (Input.GetButtonDown("P"+player+"Confirm"))
                {
                    selectBuild1++;
                    turnText.SetActive(false);
                }
                else if (Input.GetButtonDown("P"+player+"Cancel"))
                {
                    team1Build = 0;
                }
                break;

            case 1:

                building1 = Instantiate(buildingPrefab[team1Build],
                    new Vector3(topLeft.x + buildingData[team1Build, 0] * unit / 2,
                        topLeft.y + buildingData[team1Build, 1] * unit / 2,
                        topLeft.z + buildingData[team1Build, 2] * unit / 2),
                        buildingPrefab[team1Build].transform.rotation,
                        buildings.transform);
                buildingPos1 = building1.transform.position;

                lengthZ1 = 0;
                lengthX1 = 0;
                selectBuild1++;
                break;

            case 2:
                BuildRoad1(player);
                break;

            default:
                break;
        }

        switch (selectBuild2)
        {
            case 0:
                if (Input.GetButtonDown("P" + (player +1) + "Left"))
                {
                    team2Build--;
                    p2.transform.position = new Vector3(
                        p2.transform.position.x - UnityEngine.Screen.width / 8,
                        p2.transform.position.y,
                        p2.transform.position.z);
                }
                else if (Input.GetButtonDown("P" + (player +1) + "Right"))
                {
                    team2Build++;
                    p2.transform.position = new Vector3(
                        p2.transform.position.x + UnityEngine.Screen.width / 8,
                        p2.transform.position.y,
                        p2.transform.position.z);
                }
                if (team2Build > buildingPrefab.Length-1)
                {
                    team2Build = buildingPrefab.Length-1;
                    p2.transform.position = new Vector3(
                        p2.transform.position.x - UnityEngine.Screen.width/8,
                        p2.transform.position.y,
                        p2.transform.position.z);
                }
                else if (team2Build < 0)
                {
                    team2Build = 0;
                    p2.transform.position = new Vector3(
                        p2.transform.position.x + UnityEngine.Screen.width / 8,
                        p2.transform.position.y,
                        p2.transform.position.z);
                }
                    
                if (Input.GetButtonDown("P" + (player +1)+ "Confirm"))
                {
                    selectBuild2++;
                    turnText.SetActive(false);
                }
                else if (Input.GetButtonDown("P" + (player +1)+ "Cancel"))
                {
                    team2Build = 0;
                }
                break;

            case 1:
                building2 = Instantiate(buildingPrefab[team2Build],
                    new Vector3(topLeft.x + buildingData[team2Build, 0] * unit / 2.0f,
                        topLeft.y + buildingData[team2Build, 1] * unit / 2.0f,
                        topLeft.z + buildingData[team2Build, 2] * unit / 2.0f),
                        buildingPrefab[team2Build].transform.rotation,
                        buildings.transform);
                buildingPos2 = building2.transform.position;
                lengthZ2 = 0;
                lengthX2 = 0;
                selectBuild2++;
                break;

            case 2:
                BuildRoad2(player+1);
                break;

            default:
                break;
        }

        if (selectBuild1==3 && selectBuild2==3)
        {
            selectThrow++;
            isBuild = true;
            selectBuild1 = 0;
            selectBuild2 = 0;

            buildStart.SetActive(false);

            lookDown.Priority = 10;
        }
    }

    //建造障碍与道具的使用
    private void BuildRoad1(int p)
    {
        if (Input.GetButtonDown("P"+p+"Left"))
        {
            lengthZ1 -= 1;
        }
        else if (Input.GetButtonDown("P" + p + "Right"))
        {
            lengthZ1 += 1;
        }

        if (Input.GetButtonDown("P" + p + "Up"))
        {
            lengthX1 -= 1;
        }
        else if (Input.GetButtonDown("P" + p + "Down"))
        {
            lengthX1 += 1;
        }

        if (lengthZ1 < 0)
            lengthZ1 = 0;
        if (lengthZ1 > MaxLengthZ - (int)buildingData[team1Build, 2])
            lengthZ1 = MaxLengthZ - (int)buildingData[team1Build, 2];


        if (lengthX1 < 0)
            lengthX1 = 0;
        if (lengthX1 > MaxLengthX - (int)buildingData[team1Build, 0])
            lengthX1 = MaxLengthX - (int)buildingData[team1Build, 0];


        building1.transform.position = new Vector3(
            buildingPos1.x + lengthX1 * unit,
            buildingPos1.y,
            buildingPos1.z + lengthZ1 * unit);

        if (Input.GetButtonDown("P"+p+"Confirm"))
        {
            bool canbuild = true;
            for (int i = lengthZ1; i < lengthZ1 + (int)buildingData[team1Build, 2]; i++)
            {
                for (int j = lengthX1; j < lengthX1 + (int)buildingData[team1Build, 0]; j++)
                {
                    if (canBuild[j,i] == false)
                    {
                        canbuild = false;
                        break;
                    }
                }
                if (!canbuild)
                {
                    break;
                }
            }

            if (canbuild)
            {
                for (int i = lengthZ1; i < lengthZ1 + (int)buildingData[team1Build, 2]; i++)
                    for (int j = lengthX1; j < lengthX1 + (int)buildingData[team1Build, 0]; j++)
                        canBuild[j, i] = false;
                selectBuild1++;
            }             
        }
    }

    private void BuildRoad2(int p)
    {
        if (Input.GetButtonDown("P" + p + "Left"))
        {
            lengthZ2 -= 1;
        }
        else if (Input.GetButtonDown("P" + p + "Right"))
        {
            lengthZ2 += 1;
        }

        if (Input.GetButtonDown("P" + p + "Up"))
        {
            lengthX2 -= 1;
        }
        else if (Input.GetButtonDown("P" + p + "Down"))
        {
            lengthX2 += 1;
        }

        if (lengthZ2 < 0)
            lengthZ2 = 0;
        if (lengthZ2 > MaxLengthZ - (int)buildingData[team2Build, 2])
            lengthZ2 = MaxLengthZ - (int)buildingData[team2Build, 2];
        if (lengthX2 < 0)
            lengthX2 = 0;
        if (lengthX2 > MaxLengthX - (int)buildingData[team2Build, 0])
            lengthX2 = MaxLengthX - (int)buildingData[team2Build, 0];

        building2.transform.position = new Vector3(
            buildingPos2.x + lengthX2 * unit,
            buildingPos2.y,
            buildingPos2.z + lengthZ2 * unit);

        if (Input.GetButtonDown("P" + p + "Confirm"))
        {
            bool canbuild = true;
            for (int i = lengthZ2; i < lengthZ2 + (int)buildingData[team2Build, 0]; i++)
            {
                for (int j = lengthX2; j < lengthX2 + (int)buildingData[team2Build, 2]; j++)
                {
                    if (canBuild[j, i] == false)
                    {
                        canbuild = false;
                        break;
                    }
                }
                if (!canbuild)
                {
                    break;
                }
            }

            if (canbuild)
            {
                for (int i = lengthZ2; i < lengthZ2 + (int)buildingData[team2Build, 0]; i++)
                    for (int j = lengthX2; j < lengthX2 + (int)buildingData[team2Build, 2]; j++)
                        canBuild[j, i] = false;
                selectBuild2++;
            }
        }
    }

    /*
        * 
        * 技能获得
        * 
        */
    private void Skills()
    {
        Debug.Log("Building!" + selectThrow);

        team1SkillX = Random.Range(0, skill.Count);
        team1SkillY = Random.Range(0, skill.Count);
        team2SkillX = Random.Range(0, skill.Count);
        team2SkillY = Random.Range(0, skill.Count);
        Debug.Log("Team1Skill:" + " " + skill[team1SkillX] + " " + skill[team1SkillY]);
        Debug.Log("Team2Skill:" + " " + skill[team2SkillX] + " " + skill[team2SkillY]);

        t1skx.GetComponent<Image>().sprite = skills[team1SkillX];
        t1sky.GetComponent<Image>().sprite = skills[team1SkillY];
        t2skx.GetComponent<Image>().sprite = skills[team2SkillX];
        t2sky.GetComponent<Image>().sprite = skills[team2SkillY];
    }
}
