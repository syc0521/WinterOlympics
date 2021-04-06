//using LitJson;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;


namespace Snow
{
    public class Utils : MonoBehaviour
    {
        //public static SaveData save;
        //public static readonly string filePath = Application.persistentDataPath + "/save.json";
        /// <summary>
        /// 保存存档
        /// </summary>

        /// <summary>
        /// 自定义插值函数
        /// </summary>
        /// <param name="time">系统时间</param>
        /// <param name="timeRangeL">开始时间</param>
        /// <param name="timeRangeR">结束时间</param>
        /// <param name="posRangeL">开始位置</param>
        /// <param name="posRangeR">结束位置</param>
        /// <returns></returns>
        public static float Lerp(float time, float timeRangeL, float timeRangeR, float posRangeL, float posRangeR)
        {
            return Mathf.LerpUnclamped(posRangeL, posRangeR, (time - timeRangeL) / (timeRangeR - timeRangeL));
        }
        /*public static void SavePrefs()
        {
            var jsonStr = JsonMapper.ToJson(save);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(jsonStr);
            sw.Close();
            sw.Dispose();
        }
        /// <summary>
        /// 保存解锁存档
        /// </summary>
        public static void SaveUnlockPrefs()
        {
            var jsonStr = Resources.Load<TextAsset>("save");
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(jsonStr.text);
            sw.Close();
            sw.Dispose();
        }
        /// <summary>
        /// 获取存档
        /// </summary>
        public static void GetSave()
        {
            StreamReader sr = new StreamReader(filePath);
            save = JsonMapper.ToObject<SaveData>(sr.ReadToEnd());
            int sCount = 0;
            foreach (var song in SongManager.songList)
            {
                var current = save.Songs.Find(item => item.path.Equals(song.Path));
                if (current != null)
                {
                    song.GradeLevel = (Grade[])current.grade.Clone();
                    song.Score = (int[])current.score.Clone();
                    sCount += current.grade.Where(item => item.Equals(Grade.S)).Count();
                }
                else
                {
                    save.Songs.Add(new SaveData.SongSave(song.Path));
                }
            }
            sr.Close();
            sr.Dispose();
            NoteController.isAutoPlay = save.SystemSettings.isAutoPlay;
            NoteController.speed = save.SystemSettings.speed;
            NoteController.hitVolume = (float)save.SystemSettings.hitVol;
            MainSceneManager.secret = save.SystemSettings.secret;
            MainSceneManager.ending = save.SystemSettings.ending;
            save.SystemSettings = new SaveData.Settings
            {
                isAutoPlay = save.SystemSettings.isAutoPlay,
                speed = save.SystemSettings.speed,
                hitVol = save.SystemSettings.hitVol,
                ending = save.SystemSettings.ending,
                secret = save.SystemSettings.secret,
                endingSeen = save.SystemSettings.endingSeen,
                tutFinished = save.SystemSettings.tutFinished
            };
            SavePrefs();
            SongManager.songList.Find(item => item.Path.Equals("wwb")).Unlock = save.SystemSettings.secret;
        }
        /// <summary>
        /// 初始化存档
        /// </summary>
        /// <param name="filePath">文件位置</param>
        public static void InitializeSave(string filePath)
        {
            save = new SaveData();
            foreach (var song in SongManager.songList)
            {
                save.Songs.Add(new SaveData.SongSave(song.Path));
            }
            var jsonStr = JsonMapper.ToJson(save);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(jsonStr);
            sw.Close();
            sw.Dispose();
        }
        /// <summary>
        /// 获取歌曲列表
        /// </summary>
        public static void GetList()
        {
            SongManager.songList.Clear();
            TextAsset text = Resources.Load<TextAsset>("SongList");
            var lines = text.text.Split('\n');
            foreach (var item in lines)
            {
                var tmp = item.Split(',');
                var name = tmp[0].Trim('\"');
                var artist = tmp[1].Trim('\"');
                var path = tmp[2];
                var nm = int.Parse(tmp[3]);
                var hd = int.Parse(tmp[4]);
                var song = new Song(name, artist, path, nm, hd);
                SongManager.songList.Add(song);
            }
        }

        /// <summary>
        /// 分数比率转换成等级
        /// </summary>
        /// <param name="rate">分数比率</param>
        /// <returns></returns>
        public static Grade GetGrade(float rate)
        {
            if (rate >= 0.9f)
            {
                return Grade.S;
            }
            else if (rate >= 0.8f)
            {
                return Grade.A;
            }
            else if (rate >= 0.7f)
            {
                return Grade.B;
            }
            else if (rate >= 0.5f)
            {
                return Grade.C;
            }
            else
            {
                return Grade.D;
            }
        }
        /// <summary>
        /// 转换TextMeshPro分数
        /// </summary>
        /// <param name="num">分数</param>
        /// <returns></returns>
        public static string ConvertDigit(int num)
        {
            string tmp = num.ToString();
            string result = "";
            foreach (var c in tmp)
            {
                result += "<sprite=" + c + ">";
            }
            return result;
        }
        /// <summary>
        /// 退出程序
        /// </summary>
        public static void QuitProgram()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
        }
        /// <summary>
        /// 获取存档
        /// </summary>
        public static void GetPrefs()
        {
            if (!File.Exists(filePath))
            {
                InitializeSave(filePath);
            }
            else
            {
                GetSave();
            }
        }
        */
        public static float SinLerp(float start, float end, float x)
        {
            float omega = (2.0f * Mathf.PI) / ((end - start) * 4.0f);
            float phi = - start * omega;
            return Mathf.Sin(omega * x + phi) * (end - start) + start;
        }
    }
}
