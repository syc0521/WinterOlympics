using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IB_Round : MonoBehaviour
{
    /*
     * �������������룬��������ⰴ������
     * Ĭ��2�˼��̶�ս�����4�����additionalPlayer��ֵ
     * �ֱ�����̲�����Ӧ��ҡ���������ң�aȷ�ϣ�bȡ����x��y����
     * �ֱ����ܰ�����start��ʼ��back��ͣ
     * ���̲���P1��WASD��Eȷ�ϣ�Qȡ����1��2����
     * ���̲���P2��OKL;��Oȷ�ϣ�Uȡ����8��9����
     * ���ܰ�����space��ʼ��esc��backspace��ͣ
     * ��Ϸ�еĲ���˳��4������P1P3ͬ�ӹ��������̣�P2P4�Ҳࣩ��
     * P1P2���ϰ���ѡ���ߣ�P1P2Ͷ��������P3P4���Ʊ���
     * P3P4���ϰ���ѡ���ߣ�P3P4Ͷ��������P1P2���Ʊ���ѭ��
    */
    int horizontal, vertical = 0;
    int additionalPlayer = 0;
    
    //���غ��ܲ��������
    int player = 1;

    //�洢��Ϸ����ʱ��ı���
    int time;

    //���Ƶı������������,��ʼλ��-50��0.2��0����ת0��0��0
    GameObject ball;
    float forceX = 900;
    Vector3 direction;

    //12���ӷű����ĸ�������12���ӵı���
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

    //Ͷ���׶�
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

    //�ı�Ͷ����ˮƽ����
    private void ChangeHorizontal()
    {
        horizontal = (int)Input.GetAxisRaw("P"+player+"Horizontal");
        if (horizontal != 0)
        {
            ball.transform.Translate(horizontal * new Vector3(0, 0, -5f) * Time.deltaTime);
        }
    }

    //�ı�Ͷ���ĽǶ�
    private void ChangeDirection()
    {
        horizontal = (int)Input.GetAxisRaw("P"+player+"Horizontal");
        if (horizontal != 0)
        {
            ball.transform.Rotate(horizontal * new Vector3(0, 15f, 0) * Time.deltaTime);
        }
    }

    //�ı�Ͷ��������
    private void ChangeForce()
    {
        vertical = (int)Input.GetAxisRaw("P"+player+"Vertical");
        if (vertical != 0)
        {
            forceX += vertical * 100 * Time.deltaTime;
            Debug.Log("��ǰ���ȣ�" + (int)forceX);
        }
    }

    //���Ʊ���΢���Ƕ�
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
