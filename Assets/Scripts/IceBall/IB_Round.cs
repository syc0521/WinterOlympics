using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class IB_Round : MonoBehaviour
{
    /*
        * �����������λ���ã�
        * 
        *      �������������룬��������ⰴ������
        *      Ĭ��2�˼��̶�ս�����4�����additionalPlayer��ֵ
        *      ���4���ֱ���2�˲ٿ�һ���ֱ�
        *      2���ֱ����̶�֧�֣�4�����֧���ֱ�
        *      �ֱ�����̲�����Ӧ�����ҡ���������ң��ϼ�ȷ�ϣ��¼�ȡ����
        *          ���Ҽ���ˮƽ�ᣬLB1���ܣ�LT2����
        *      �ֱ����ܰ�����start��ʼ��back��ͣ
        *      ���̲���P1��WASD��Eȷ�ϣ�Qȡ����2��3����
        *      ���̲���P2��IJKL��Uȷ�ϣ�Oȡ����8��9����
        *      ���ܰ�����space��ʼ��esc��backspace��ͣ
        *      ��Ϸ�еĲ���˳��4������P1P3ͬ�ӣ�P2P4ͬ�ӣ���
        *      P1P2���ϰ���ѡ���ߣ�P1P2Ͷ��������P3P4���Ʊ���
        *      P3P4���ϰ���ѡ���ߣ�P3P4Ͷ��������P1P2���Ʊ���ѭ��
        *      
    */
        
    //�洢��Ϸ����ʱ��ı���������ÿ�غϽ���
    int time;

    private CinemachineVirtualCamera lookDown;
    GameObject turnText;
        
    //���غ��ܲ�������ң���ҵ������룬���������������������
    int player = 1;
    int horizontal, vertical = 0;
    int additionalPlayer = 0;

    //���Ƶı�������ʼλ�ã�0��0.5��-50������ת��0��0��0)
    //�����Ķ������ԣ�����ʩ��900�����峯����ٶȷ��򱣳�һ��
    GameObject ball;
    float force = 900;
    Vector3 direction;

    //12���Ӵ�ű����ĸ����壬12���ӵı�����Ԥ����
    GameObject team1;
    GameObject team2;
    public GameObject ball1Prefab;
    public GameObject ball2Prefab;

    //������Ϸ���ȵı�����selectThrow/3+1��Ϊ��ǰ�غ���
    public int selectThrow = 0;

    //����Ͷ���׶Σ��ܷ񶪱����������Ƿ�ͣ��
    int throwStage = 0;
    bool canThrow = false;
    bool isStop = true;

    //����ģ�飬buttonX��Y���ӵ�UI�����ϵļ�������ͼ
    //skill�����б�
    //�洢ÿ�ӵļ���
    //��õ�ǰ������ڶ���ļ���
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

    //����ģ�飬��һ��a*b��С������ӳ��ʵ����z*x������
    //��������ÿһ���Ӧʵ�������еĴ�С����
    //topLeft���Ͻǵ����꣨���������ĵ��ᣩ
    //��������ĳ���
    //�������������������Ͻ����ڵĸ���λ��
    Vector3 topLeft = new Vector3(-14, 0, -24);
    static int unit = 4;
    static int MaxLengthZ = 12;
    static int MaxLengthX = 7;
    int lengthZ1 = 0;
    int lengthX1 = 0;
    int lengthZ2 = 0;
    int lengthX2 = 0;

    GameObject p1, p2;


    //�洢����Ԥ����
    //�����Ķ������ݣ��ֱ�Ϊ���������������еĳ��߿���ռ������
    //�洢��������ʱ����������������λ�ã�����Ԥ���������
    //����洢��ǰλ���Ƿ��ܽ���
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
        * ��ʼ����
        *      
        *      ����12��Ѱ��
        *      ��������UI��Ѱ��
        *      ���Ӽ���
        *      ��ʼ����������Ϊ���ܽ���
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
            
        skill.Add("��");
        skill.Add("�޴�");
        skill.Add("�����");
        skill.Add("����");
        skill.Add("����");
        skill.Add("������");

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
        * ����ִ֡�У�
        * 
        *      ����ִ������ģ���Լ��ò�������������¼�
        *      
        */
    private void FixedUpdate()
    {
            
    }

    /*
        * ִ֡�У�
        *      
        *      ��ִ���������
        * 
        */
    void Update()
    {
        if (Input.GetButtonDown("Start") && !canThrow && isStop && isBuild)
        {
            turnText.SetActive(true);
            turnText.GetComponent<Text>().text = "�غ�"+(selectThrow/3+1)+"!";

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

    //Ͷ���ж������
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

    //Ͷ���׶�
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

        //����ĸ�Ͷ���׶�
        if (throwStage < 0)
            throwStage = 0;
        else if (throwStage > 3)
            throwStage = 3;

        //��ͬͶ���׶�ִ�в�ͬ����
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

    //�ı�Ͷ����ˮƽ����
    private void ChangeHorizontal()
    {
        horizontal = (int)Input.GetAxisRaw("P" + player + "Horizontal");
        if (horizontal != 0)
        {
            ball.transform.Translate(horizontal * new Vector3(5f, 0, 0) * Time.deltaTime);
        }
    }

    //�ı�Ͷ���ĽǶ�
    private void ChangeDirection()
    {
        horizontal = (int)Input.GetAxisRaw("P" + player + "Horizontal");
        if (horizontal != 0)
        {
            ball.transform.Rotate(horizontal * new Vector3(0, 15f, 0) * Time.deltaTime);
        }
    }

    //�ı�Ͷ��������
    private void ChangeForce()
    {
        vertical = (int)Input.GetAxisRaw("P" + player + "Vertical");
        if (vertical != 0)
        {
            force += vertical * 80 * Time.deltaTime;
            Debug.Log("��ǰ���ȣ�" + (int)force);
        }
    }

    /* 
        * ���Ʊ�����
        *      
        *      �ж��Ƿ�ͣ���Խ�����һ������������Ͷ�����¼����ٹ�ȥ5�����ٶ�С��0.1
        *      �ٶȸı䣺��õ�ǰ֡���ٶȵķ��򲢸ı䲢��λ�������Ե�ǰ�ٶȵĴ�С
        *      �����ͷ���Ч��
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

    //�����ϰ�����ߵ�ʹ��
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
        * ���ܻ��
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
