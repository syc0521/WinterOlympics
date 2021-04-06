using System;

namespace Snow
{
    public enum NoteType { Obstacle = 1, Jump = 2, Reward = 3 }
    public enum Position { Left = 1, Middle = 2, Right = 3 }
    public enum Player { P1 = 0, P2 = 1 }

    public class NoteData : IComparable<NoteData>
    {
        public NoteData(int type, int time, int position)
        {
            Type = (NoteType)type;
            Time = time / 1000.0f;
            Position = (Position)position;
        }
        public NoteType Type { set; get; }
        public float Time { get; set; }
        public Position Position { get; set; }
        public bool CanJudge { set; get; }
        public Player Player { get; set; }
        public int Index { get; set; }
        public int CompareTo(NoteData other)
        {
            return (int)(Time * 1000.0f - other.Time * 1000.0f);
        }

        public override string ToString()
        {
            float dt = Time - UnityEngine.Time.timeSinceLevelLoad + NoteController.NoteSpeed;
            return "time=" + (Time * 1000f) + ", deltaTime=" + dt + ", judge=";
        }
    }


}