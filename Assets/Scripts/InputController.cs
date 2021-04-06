using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Snow;
public class InputController : MonoBehaviour
{
    public static InputMaster controls;
    private readonly float perfectTime = NoteController.perfectTime;
    private readonly float goodTime = NoteController.goodTime;
    //public GameObject judge;

    private void OnEnable()
    {
        controls = new InputMaster();
        controls.PlayController.Enable();
        if (!NoteController.isAutoPlay)
        {
            controls.PlayController.P1_L.started += DestroyP1RightObstacle;
            controls.PlayController.P1_R.started += DestroyP1LeftObstacle;
            controls.PlayController.P1_Jump.started += DestroyP1Jump;
            controls.PlayController.P2_L.started += DestroyP2RightObstacle;
            controls.PlayController.P2_R.started += DestroyP2LeftObstacle;
            controls.PlayController.P2_Jump.started += DestroyP2Jump;
        }
    }
    private void OnDisable()
    {
        controls.PlayController.Disable();
        if (!NoteController.isAutoPlay)
        {
            controls.PlayController.P1_L.started -= DestroyP1RightObstacle;
            controls.PlayController.P1_R.started -= DestroyP1LeftObstacle;
            controls.PlayController.P1_Jump.started -= DestroyP1Jump;
            controls.PlayController.P2_L.started -= DestroyP2RightObstacle;
            controls.PlayController.P2_R.started -= DestroyP2LeftObstacle;
            controls.PlayController.P2_Jump.started -= DestroyP2Jump;
        }
    }
    private void DestroyP1LeftObstacle(InputAction.CallbackContext obj)
    {
        var time = Time.timeSinceLevelLoad - NoteController.NoteSpeed;
        var note = NoteController.notes[0].Find(item => item.Time > time - goodTime 
                                                  && item.Time < time + goodTime && item.Type.Equals(NoteType.Obstacle)
                                                  && item.Position == Position.Left && item.CanJudge);
        if (note != null)
        {
            var noteObj = NoteController.noteObjs[0][note.Index];
            JudgeType judgeType = JudgeTap(note);
            TapJudgeFinished(judgeType, 0);
            noteObj.GenerateHitSound();
            StartCoroutine(ModifyNote(note,0));
            note.CanJudge = false;
            noteObj.ShowJudge(judgeType);
            noteObj.ShowJudgeEffect();
            Destroy(noteObj.gameObject);
        }
    }
    private void DestroyP1RightObstacle(InputAction.CallbackContext obj)
    {
        var time = Time.timeSinceLevelLoad - NoteController.NoteSpeed;
        var note = NoteController.notes[0].Find(item => item.Time > time - goodTime
                                                  && item.Time < time + goodTime && item.Type.Equals(NoteType.Obstacle)
                                                  && item.Position == Position.Right && item.CanJudge);
        if (note != null)
        {
            var noteObj = NoteController.noteObjs[0][note.Index];
            JudgeType judgeType = JudgeTap(note);
            TapJudgeFinished(judgeType, 0);
            noteObj.GenerateHitSound();
            StartCoroutine(ModifyNote(note,0));
            note.CanJudge = false;
            noteObj.ShowJudge(judgeType);
            noteObj.ShowJudgeEffect();
            Destroy(noteObj.gameObject);
        }
    }
    private void DestroyP1Jump(InputAction.CallbackContext obj)
    {
        var time = Time.timeSinceLevelLoad - NoteController.NoteSpeed;
        var note = NoteController.notes[0].Find(item => item.Time > time - goodTime
                                                  && item.Time < time + goodTime && item.Type.Equals(NoteType.Jump)
                                                  && item.CanJudge);
        if (note != null)
        {
            var noteObj = NoteController.noteObjs[0][note.Index];
            JudgeType judgeType = JudgeTap(note);
            TapJudgeFinished(judgeType,0);
            noteObj.GenerateHitSound();
            StartCoroutine(ModifyNote(note,0));
            note.CanJudge = false;
            noteObj.ShowJudge(judgeType);
            noteObj.ShowJudgeEffect();
            Destroy(noteObj.gameObject);
        }
    }
    private void DestroyP2LeftObstacle(InputAction.CallbackContext obj)
    {
        var time = Time.timeSinceLevelLoad - NoteController.NoteSpeed;
        var note = NoteController.notes[1].Find(item => item.Time > time - goodTime
                                                  && item.Time < time + goodTime && item.Type.Equals(NoteType.Obstacle)
                                                  && item.Position == Position.Left && item.CanJudge);
        if (note != null)
        {
            var noteObj = NoteController.noteObjs[1][note.Index];
            JudgeType judgeType = JudgeTap(note);
            TapJudgeFinished(judgeType,1);
            noteObj.GenerateHitSound();
            StartCoroutine(ModifyNote(note,1));
            note.CanJudge = false;
            noteObj.ShowJudge(judgeType);
            noteObj.ShowJudgeEffect();
            Destroy(noteObj.gameObject);
        }
    }
    private void DestroyP2RightObstacle(InputAction.CallbackContext obj)
    {
        var time = Time.timeSinceLevelLoad - NoteController.NoteSpeed;
        var note = NoteController.notes[1].Find(item => item.Time > time - goodTime
                                                  && item.Time < time + goodTime && item.Type.Equals(NoteType.Obstacle)
                                                  && item.Position == Position.Right && item.CanJudge);
        if (note != null)
        {
            var noteObj = NoteController.noteObjs[1][note.Index];
            JudgeType judgeType = JudgeTap(note);
            TapJudgeFinished(judgeType,1);
            noteObj.GenerateHitSound();
            StartCoroutine(ModifyNote(note,1));
            note.CanJudge = false;
            noteObj.ShowJudge(judgeType);
            noteObj.ShowJudgeEffect();
            Destroy(noteObj.gameObject);
        }
    }
    private void DestroyP2Jump(InputAction.CallbackContext obj)
    {
        var time = Time.timeSinceLevelLoad - NoteController.NoteSpeed;
        var note = NoteController.notes[1].Find(item => item.Time > time - goodTime
                                                  && item.Time < time + goodTime && item.Type.Equals(NoteType.Jump)
                                                  && item.CanJudge);
        if (note != null)
        {
            var noteObj = NoteController.noteObjs[1][note.Index];
            JudgeType judgeType = JudgeTap(note);
            TapJudgeFinished(judgeType,1);
            noteObj.GenerateHitSound();
            StartCoroutine(ModifyNote(note,1));
            note.CanJudge = false;
            noteObj.ShowJudge(judgeType);
            noteObj.ShowJudgeEffect();
            Destroy(noteObj.gameObject);
        }
    }
    private JudgeType JudgeTap(NoteData note)
    {
        float sceneTime = Time.timeSinceLevelLoad;
        float exactTime = note.Time + NoteController.NoteSpeed + 0.025f;
        if (note.CanJudge)
        {
            if (sceneTime <= exactTime + perfectTime && sceneTime > exactTime - perfectTime)
            {
                Debug.Log(note + "perfect");
                return JudgeType.Perfect;
            }
            else if (sceneTime < exactTime + goodTime - 0.02f && sceneTime > exactTime + perfectTime)
            {
                Debug.Log(note + "Lgood");
                return JudgeType.LateGood;
            }
            else if (sceneTime > exactTime - goodTime && sceneTime < exactTime - perfectTime)
            {
                Debug.Log(note + "Egood");
                return JudgeType.EarlyGood;
            }
            else if (sceneTime < exactTime - goodTime)
            {
                return JudgeType.Default;
            }
            else if (sceneTime > exactTime + goodTime)
            {
                return JudgeType.Miss;
            }
        }
        return JudgeType.Default;
    }

    private IEnumerator ModifyNote(NoteData note, int player)
    {
        yield return new WaitForSeconds(0.02f);
        if (note.Index < NoteController.totalCount - 1)
        {
            NoteController.notes[player][note.Index + 1].CanJudge = true;
        }
    }

    public static void TapJudgeFinished(JudgeType judgeType, int player)
    {
        switch (judgeType)
        {
            case JudgeType.Perfect:
                NoteController.combo[player]++;
                NoteController.perfect[player]++;
                break;
            case JudgeType.EarlyGood:
                NoteController.combo[player]++;
                NoteController.good[player]++;
                break;
            case JudgeType.LateGood:
                NoteController.combo[player]++;
                NoteController.good[player]++;
                break;
            case JudgeType.Miss:
                NoteController.miss[player]++;
                NoteController.combo[player] = 0;
                break;
        }
    }
}
