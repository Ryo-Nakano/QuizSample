using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Canvas扱う
using UnityEngine.EventSystems;//EventSystem扱う
using UnityEngine.SceneManagement;//シーン遷移扱う

public class QuizManager : MonoBehaviour {

	string[,] quizData;//string型の2次元配列quizDataを定義→取得したcsvファイルを2次元配列に直してぶち込んでおく！
	string[] quizDataPart;//1列ずつ格納(保持するのはクイズの回答選択肢のみ)
	[SerializeField] Text questionTextField;//Questionの質問文
	[SerializeField] Text[] choicesText;//回答の選択肢を配列で保持
	[SerializeField] Text scoreText;//最後のスコア表示の為のText

	int nowQuizNum = 1;//現在のQuiz番号を格納しておく為の変数
	int questionNum;//クイズの総問題数を格納しておく為の変数
	int correctCount;//クイズ正解数をカウントする為の変数

	string questionText = "";//問題文のstringを格納しておく為の変数
	private string correctAnswer = "";//正解のtextを格納しておく為の変数
	string selectedText = "";//選択されたtextの内容を格納しておく為の変数

	Animator view2Animator;//View2のAnimatorを格納しておく為の変数
	[SerializeField] GameObject view2;//View2をUnityからアタッチ

	void Awake()
	{
		quizData = csvManager.GetCsvData("QuizData");//クイズのデータをCSVから読み込み
	}

	void Start () 
	{
		view2Animator = view2.GetComponent<Animator>();//Animatorを取得してきて変数に格納
		questionNum = quizData.Length / 5 - 1;//合計問題数を変数questionNumに格納

		QuizGenerator();//クイズ生成
	}

    //それぞれの回答選択肢を押した時に呼ばれる関数
	public void QuizAnswerButton()
	{
		GetTextData();//クリックした場所にあるTextデータを取得
		if(correctAnswer == selectedText)//内容合致してたら
		{
			Debug.Log("正解！");
			correctCount++;
		}
		else
		{
			Debug.Log("残念！正解は『" + correctAnswer + "』");
		}
		nowQuizNum++;
		if(nowQuizNum > questionNum)//現在の問題番号が総問題数を超えたら
		{
			view2Animator.SetBool("running", true);//view2をアニメーションさせる
			scoreText.text = "今回は..." + correctCount + "問正解！";//scoreText表示
			Debug.Log("End！");
		}
		else
		{
			QuizGenerator();//次のクイズ生成
		}
	}

	//クイズの問題文、選択肢を表示する関数https://pb072b29c64.dmc.nico/vod/ht2_nicovideo/nicovideo-so32963148_86a7c12852ba15fad77716bbf396ab61b5c3403bd05cd30000c83b1901f15e49?ht2_nicovideo=83637316.wocjd7_pmaush_36iojf820r1f6た
	void QuizGenerator()
	{
		//問題文を表示
		questionTextField.text = quizData[nowQuizNum, 0];

		//回答選択肢を配列に格納
		quizDataPart = new string[4];//quizDataPartを初期化(要素数4)
		for (int i = 0; i < quizDataPart.Length; i++)
		{
			quizDataPart[i] = quizData[nowQuizNum, i + 1];
		}
		correctAnswer = quizDataPart[0];//正答を変数correctAnswerに格納

        //配列内をランダムに並び替え
		for (int i = 0; i < quizDataPart.Length; i++)
        {
			string tmp = quizDataPart[i];
			int randomIndex = Random.Range(i, quizDataPart.Length);
			quizDataPart[i] = quizDataPart[randomIndex];
			quizDataPart[randomIndex] = tmp;
        }

		//回答選択肢表示
		for (int i = 0; i < quizDataPart.Length; i++)
		{
			choicesText[i].text = quizDataPart[i];
		}
	}

	void Update () 
	{
		
	}

    //クリックした場所にあるTextデータを取得する関数
	void GetTextData()
	{
		PointerEventData pointer = new PointerEventData(EventSystem.current);//マウスの位置に起こるイベントを格納する為の変数pointerを宣言
        pointer.position = Input.mousePosition;//pointerの位置はマウスの位置
        List<RaycastResult> result = new List<RaycastResult>();//Rayが当たった結果を格納しておく為の変数resultを宣言
        EventSystem.current.RaycastAll(pointer, result);
		selectedText = result[0].gameObject.GetComponent<Text>().text;//変数selectedTextにクリックした先にあるText要素の内容を格納
	}

	public void TapToRetryButton()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);//同一シーンの再度読み込み(リセット)
	}
}
