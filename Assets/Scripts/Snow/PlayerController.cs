using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snow
{
    public class PlayerController : MonoBehaviour
    {
        private float axisValue = 0f;
        private float positionY;
        public Transform leftPos, rightPos, middlePos;
        public Player player;
        private void Start()
        {
            positionY = transform.position.y;
        }

        private void Update()
        {
            if (player == Player.P1)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    ChangeAxisValue(false);
                    transform.position = new Vector3(Mathf.Lerp(middlePos.position.x, leftPos.position.x, -axisValue),
                        positionY);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    ChangeAxisValue(true);
                    transform.position = new Vector3(Mathf.Lerp(middlePos.position.x, rightPos.position.x, axisValue),
                        positionY);
                }
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    transform.position = new Vector3(middlePos.position.x, positionY);
                    axisValue = 0;
                    //StartCoroutine(RestorePlayer());
                }
            }
            else if (player == Player.P2)
            {
                if (Input.GetKey(KeyCode.J))
                {
                    ChangeAxisValue(false);
                    transform.position = new Vector3(Mathf.Lerp(-middlePos.position.x, -rightPos.position.x, -axisValue),
                        positionY);
                }
                if (Input.GetKey(KeyCode.L))
                {
                    ChangeAxisValue(true);
                    transform.position = new Vector3(Mathf.Lerp(-middlePos.position.x, -leftPos.position.x, axisValue),
                        positionY);
                }
                if (Input.GetKeyUp(KeyCode.J) || Input.GetKeyUp(KeyCode.L))
                {
                    transform.position = new Vector3(-middlePos.position.x, positionY);
                    axisValue = 0;
                    //StartCoroutine(RestorePlayer());
                }
            }
        }

        private void ChangeAxisValue(bool isRight)
        {
            axisValue += (isRight ? 1 : -1) * 30.0f * Time.deltaTime;
            if (axisValue >= 1f)
            {
                axisValue = 1f;
            }
            if (axisValue <= -1f)
            {
                axisValue = -1f;
            }
        }

        private IEnumerator RestorePlayer()
        {
            while (!Mathf.Approximately(axisValue, 0f))
            {
                axisValue += (axisValue > 0f ? -1 : 1) * 2.0f * Time.deltaTime;
                if (Mathf.Approximately(axisValue, 0f))
                {
                    yield break;
                }
                Debug.Log("Restore");
                yield return null;
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!NoteController.isAutoPlay)
            {
                Reward reward = other.gameObject.GetComponent<Reward>();
                if (reward != null)
                {
                    reward.Data.CanJudge = false;
                    reward.GenerateHitSound();
                    StartCoroutine(ModifyNote(reward.Data, (int)player));
                    NoteController.bell[(int)player]++;
                    Debug.Log(NoteController.bell[0]);
                    Destroy(other.gameObject);
                }
            }
        }

        private IEnumerator ModifyNote(NoteData note, int player)
        {
            yield return new WaitForSeconds(0.02f);
            if (note.Index < NoteController.totalCount - 1)
            {
                NoteController.notes[player][note.Index + 1].CanJudge = true;
            }
        }
    }

}
