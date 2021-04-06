using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snow
{
	public class Obstacle : Note
	{
		public bool firstDestroy = true;
		public GameObject FX;
		public override void Update()
		{
			base.Update();
			if (!NoteController.isAutoPlay)
			{
				if (Time.timeSinceLevelLoad >= Data.Time + moveTime + NoteController.goodTime && firstDestroy)
				{
					firstDestroy = false;
					DestroyMissNote((int)Data.Player);
				}
			}
			else
			{
				AutoPlayMode((int)Data.Player);
			}
		}
		private void DestroyMissNote(int player)
		{
			NoteController.combo[player] = 0;
			if (Data.Index < NoteController.totalCount - 1)
			{
				NoteController.notes[player][Data.Index + 1].CanJudge = true;
			}
			Debug.Log(Data + "Miss");
			NoteController.miss[player]++;
			NoteController.combo[player] = 0;
			ShowJudge(JudgeType.Miss);
			Destroy(gameObject);
		}
		private void AutoPlayMode(int player)
		{
			if (transform.position.y <= endPos.y)
			{
				GenerateHitSound();
				//ShowJudge(JudgeType.Perfect);
				NoteController.combo[player]++;
				NoteController.score[player] += 100;
				ShowJudgeEffect();
				Destroy(gameObject);
			}
		}

		public override void GenerateHitSound()
		{
			Instantiate(FX);
		}
		public IEnumerator DestroyAnim(JudgeType type)
		{
			if (firstDestroy)
			{
				firstDestroy = false;
				ShowJudge(type);
				//yield return new WaitForSeconds(0.3f);
				Destroy(gameObject);
				yield break;
			}

		}

	}

}
