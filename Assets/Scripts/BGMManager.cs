using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
	private static BGMManager _instance;
	public static BGMManager Instance { get { return _instance; } }
	private void Awake()
	{
		if (_instance != null)
		{
			Destroy(gameObject); return;
		}
		else
		{
			_instance = this;
		}
	}
}
