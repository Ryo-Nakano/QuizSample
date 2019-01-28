using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Canvas扱う

public class QuizManager : MonoBehaviour {

	string[,] quizData;//string型の2次元配列quizDataを定義→取得したcsvファイルを2次元配列に直してぶち込んでおく！
	string[] quizDataPart; 
	[SerializeField] Text questionTextField;
	[SerializeField] Text[] answerText;

	string questionText;

	void Awake()
	{
		quizData = csvManager.GetCsvData("QuizData");
	}

	void Start () {
		int questionNum = quizData.Length / 5 - 1;

		///データの取得までは完了』
		for (int i = 0; i < quizData.Length / 5; i++)//(問題数 + 1)回分だけ回す
		{
			for (int j = 0; j < 5; i++)//5回回す
			{
				
			}
		}

	}

	void Update () {
		
	}
}
