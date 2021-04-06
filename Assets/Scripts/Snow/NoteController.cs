using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snow
{
    public enum JudgeType
    {
        Perfect = 0, EarlyGood = 1, LateGood = 2, Miss = -1, Default = -2
    };
    public enum Diff { Normal = 0, Hard = 1 }
    public class NoteController : MonoBehaviour
    {
        public static List<NoteData>[] notes = { new List<NoteData>() , new List<NoteData>() };
        public static List<Note>[] noteObjs = { new List<Note>() , new List<Note>() };
        public static readonly float perfectTime = 0.078f;
        public static readonly float goodTime = 0.16f;
        public static float NoteSpeed => 3.2f - 0.3f * (speed / 2.0f);
        public static int[] bell = { 0, 0 };
        public static int[] combo = { 0, 0 };
        public static float[] score = { 0, 0 };
        public static int[] perfect = { 0, 0 }, good = { 0, 0 }, miss = { 0, 0 };
        public static float hitVolume;
        public static int totalCount, noteCount, rewardCount;
        public static bool isAutoPlay = false;
        public static int speed = 13;
        public static bool isPaused = false;
        public GameObject pauseCanvas;
        [HideInInspector]
        public AudioSource source;
        [HideInInspector]
        public int bpm;
        public static int maxCombo;
        public string path;
        public Diff diff;
        public GameObject obstacleObj, jumpObj, rewardObj;
        public Transform start_L, start_M, start_R, end_L, end_M, end_R;
        public Transform reward_L, reward_R;
        private Vector3 P2_L_Start, P2_M_Start, P2_R_Start;
        private Vector3 P2_L_End, P2_M_End, P2_R_End;

        void Start()
        {
            P2_L_Start = ReversePos(start_R.position);
            P2_M_Start = ReversePos(start_M.position);
            P2_R_Start = ReversePos(start_L.position);
            P2_L_End = ReversePos(end_R.position);
            P2_M_End = ReversePos(end_M.position);
            P2_R_End = ReversePos(end_L.position);
            source = GetComponent<AudioSource>();
            StartCoroutine(GetSong());
            GetMap();
            for (int i = 0; i < notes.Length; i++)
            {
                foreach (NoteData note in notes[i])
                {
                    StartCoroutine(CreateNote(note, i));
                }
            }
        }

        void Update()
        {
            PauseGame();
            for (int i = 0; i < score.Length; i++)
            {
                score[i] = GetScore(perfect[i], good[i], bell[i]);
            }
        }

        private void GetMap()
        {
            TextAsset text = Resources.Load<TextAsset>("Songs/" + path + "/" + (int)diff);
            var lines = text.text.Split('\n');
            bpm = int.Parse(lines[0].Substring(5));
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i] != "")
                {
                    var line = lines[i].Split(',');
                    for (int j = 0; j < 2; j++)
                    {
                        NoteData note = new NoteData(int.Parse(line[0]), int.Parse(line[1]), int.Parse(line[2]))
                        {
                            Index = i - 1,
                            CanJudge = false,
                            Player = (Player)j
                        };
                        notes[j].Add(note);
                    }
                }
            }
            notes[0].Sort();
            notes[1].Sort();
            totalCount = notes[0].Count;
            noteCount = notes[0].FindAll(item => item.Type != NoteType.Reward).Count;
            rewardCount = totalCount - noteCount;
            notes[0][0].CanJudge = true;
            notes[1][0].CanJudge = true;
        }
        private IEnumerator GetSong()
        {
            var clip = Resources.Load<AudioClip>("Songs/" + path + "/song");
            source.clip = clip;
            source.playOnAwake = false;
            yield return new WaitForSeconds(NoteSpeed);
            source.Play();
            yield return new WaitForSeconds(clip.length);
            SceneManager.LoadScene("Result");
            if (isAutoPlay)
            {
                //LoadingManager.nextScene = "Select";
                //SceneManager.LoadScene("Loading");
            }
            else
            {
                //SceneManager.LoadScene("Result");
            }
        }

        private void PauseGame()
        {
            if (!isPaused)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isPaused = true;
                    source.Pause();
                    pauseCanvas.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }
        private IEnumerator CreateNote(NoteData note, int player)
        {
            yield return new WaitForSeconds(note.Time);
            Note noteObj = null;
            switch (note.Type)
            {
                case NoteType.Obstacle:
                    noteObj = CreateObstacle(note);
                    break;
                case NoteType.Jump:
                    noteObj = CreateJump(note);
                    break;
                case NoteType.Reward:
                    noteObj = CreateReward(note);
                    break;
            };
            noteObj.Data = note;
            //noteObj.judgePos = judgePos.position;
            noteObjs[player].Add(noteObj);
        }

        private Note CreateObstacle(NoteData note)
        {
            Obstacle obstacle;
            var player = note.Player;
            if (player == Player.P1)
            {
                if (note.Position == Position.Left)
                {
                    obstacle = Instantiate(obstacleObj, start_L.position, Quaternion.identity).GetComponent<Obstacle>();
                    obstacle.startPos = start_L.position;
                    obstacle.endPos = end_L.position;
                }
                else
                {
                    obstacle = Instantiate(obstacleObj, start_R.position, Quaternion.identity).GetComponent<Obstacle>();
                    obstacle.startPos = start_R.position;
                    obstacle.endPos = end_R.position;
                }
            }
            else
            {
                if (note.Position == Position.Left)
                {
                    obstacle = Instantiate(obstacleObj, P2_L_Start, Quaternion.identity).GetComponent<Obstacle>();
                    obstacle.startPos = P2_L_Start;
                    obstacle.endPos = P2_L_End;
                }
                else
                {
                    obstacle = Instantiate(obstacleObj, P2_R_Start, Quaternion.identity).GetComponent<Obstacle>();
                    obstacle.startPos = P2_R_Start;
                    obstacle.endPos = P2_R_End;
                }
            }
            return obstacle;
        }
        private Note CreateJump(NoteData note)
        {
            Jump jump;
            var player = note.Player;
            if (player == Player.P1)
            {
                jump = Instantiate(jumpObj, start_M.position, Quaternion.identity).GetComponent<Jump>();
                jump.startPos = start_M.position;
                jump.endPos = end_M.position;
            }
            else
            {
                jump = Instantiate(jumpObj, P2_M_Start, Quaternion.identity).GetComponent<Jump>();
                jump.startPos = P2_M_Start;
                jump.endPos = P2_M_End;
            }
                
            return jump;
        }
        private Note CreateReward(NoteData note)
        {
            Reward reward;
            var player = note.Player;
            Vector3 startPos, endPos;
            if (player == Player.P1)
            {
                if (note.Position == Position.Left)
                {
                    startPos = new Vector3(reward_L.position.x, start_M.position.y);
                    endPos = new Vector3(reward_L.position.x, end_M.position.y);
                }
                else if (note.Position == Position.Middle)
                {
                    startPos = start_M.position;
                    endPos = end_M.position;
                }
                else
                {
                    startPos = new Vector3(reward_R.position.x, start_M.position.y);
                    endPos = new Vector3(reward_R.position.x, end_M.position.y);
                }
            }
            else
            {
                if (note.Position == Position.Left)
                {
                    startPos = new Vector3(ReversePos(reward_R.position).x, ReversePos(start_M.position).y);
                    endPos = new Vector3(ReversePos(reward_R.position).x, ReversePos(end_M.position).y);
                }
                else if (note.Position == Position.Middle)
                {
                    startPos = ReversePos(start_M.position);
                    endPos = ReversePos(end_M.position);
                }
                else
                {
                    startPos = new Vector3(-reward_L.position.x, start_M.position.y);
                    endPos = new Vector3(-reward_L.position.x, end_M.position.y);
                }
            }
            reward = Instantiate(rewardObj, startPos, Quaternion.identity).GetComponent<Reward>();
            reward.startPos = startPos;
            reward.endPos = endPos;

            return reward;
        }

        private Vector3 ReversePos(Vector3 vector) => new Vector3(-vector.x, vector.y);

        public float GetScore(int perfect, int good, int bell)
        {
            return (perfect + good * 0.6f) / noteCount * 90.0f + bell / (float)rewardCount * 10.0f;
        }
    }
}

