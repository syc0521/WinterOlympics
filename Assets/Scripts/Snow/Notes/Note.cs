using UnityEngine;

namespace Snow
{
    public abstract class Note : MonoBehaviour
    {
        public NoteData Data { set; get; }
        public Player Player { get; set; }
        [HideInInspector]
        public Vector3 startPos, endPos, judgePos;
        public GameObject[] judge;
        [HideInInspector]
        public float moveTime;
        public GameObject hitEffect;
        private void Awake()
        {
            moveTime = NoteController.NoteSpeed;
        }
        public virtual void Update()
        {
            Move();
        }
        public virtual void Move()
        {
            if (Time.timeSinceLevelLoad < Data.Time + moveTime)
            {
                var time = Time.timeSinceLevelLoad;
                var endTime = Data.Time + moveTime;
                var fixedEndPos = endPos.y + 0.1f;
                transform.position = new Vector3(transform.position.x,
                     Utils.Lerp(time, Data.Time, endTime, startPos.y, fixedEndPos));
            }
            else
            {
                transform.position = endPos;
            }
        }

        public abstract void GenerateHitSound();

        public virtual void ShowJudge(JudgeType type)
        {
            /*switch (type)
            {
                case JudgeType.Perfect:
                    Instantiate(judge[0]);
                    break;
                case JudgeType.EarlyGood:
                    Instantiate(judge[1]);
                    break;
                case JudgeType.LateGood:
                    Instantiate(judge[1]);
                    break;
                case JudgeType.Miss:
                    Instantiate(judge[2]);
                    break;
            }*/
        }
        public virtual void ShowJudgeEffect()
        {
            //Instantiate(hitEffect, endPos, Quaternion.identity);
        }
    }
}