using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static VPSManagerWOA;

public class GameRequest : MonoBehaviour
{
    private string url = "https://yourreponame.github.io/DBTimeMachine.json";
    public TMP_Text question;
    public Text answer;
    public Button findButton;
    public int gameScore = 0;
    public List<GameDataObject> questionsJson;
    public string currentNameOfObject;
    public int fireNumber = 0;

    public void Start()
    {
        StartCoroutine(MakeGameRequest());
    }

    IEnumerator MakeGameRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            questionsJson = JsonConvert.DeserializeObject<List<GameDataObject>>(request.downloadHandler.text);
            question = GameObject.Find("Question").GetComponent<TMP_Text>();
            AdvanceQuestion();
        }
    }
    public void AdvanceQuestion()
    {
        currentNameOfObject = StaticClass.objectName;
        fireNumber = Int32.Parse(Regex.Match(currentNameOfObject, @"\d+").Value);
        question.text = questionsJson.ToArray()[0].game.questions.ToArray()[fireNumber].question.ToString();
        //get the answers from the questionsJson from the current question.text
        List<string> answers = new List<string>();
        foreach (var answer in questionsJson.ToArray()[0].game.questions.ToArray()[fireNumber].answers)
        {
            answers.Add(answer);
        }
        
        foreach (var item in questionsJson)
        {
            MakeAnswerButtons(answers, item.game.questions[fireNumber].correctAnswer);
        }
       
    }

    public void RemoveButton(string buttonName)
    {
        GameObject button = GameObject.Find(buttonName);
        Destroy(button);
    }
    public void MakeAnswerButtons(List<string> answers , int correctAnswerIndex)
    {
        if(answers.Count == 0)
        {
            return;
        }
        if (correctAnswerIndex <= answers.Count && correctAnswerIndex >= 0)
        {
            for (int i = 0; i < answers.Count; i++)
            {
                answer = GameObject.Find("Answer" + i).GetComponent<Text>();
                answer.text = answers[i];
                findButton = GameObject.Find("Button" + i).GetComponent<Button>();
                findButton.onClick.RemoveAllListeners();
                findButton.onClick.AddListener(() => CheckAnswer());
            }
        }
    }
    public void CheckAnswer()
    {
        //get the name of which button is clicked and check if it is the correct answer
        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        GameObject quizScreen = GameObject.Find("QuestionCanvas");
        int buttonNumber = Int32.Parse(Regex.Match(buttonName, @"\d+").Value);
        if (buttonNumber == questionsJson.ToArray()[0].game.questions.ToArray()[fireNumber].correctAnswer)
        {
            gameScore++;
            quizScreen.SetActive(false);
        }
        //check if last question is called from question in questionJSon
        var debug = questionsJson.ToArray()[0].game.questions.ToArray().Last();
        if (questionsJson.ToArray()[0].game.questions.ToArray()[fireNumber].question.ToString() == debug.question)
        {
            question.text = "your score is " + gameScore;
            RemoveButton("Button0");
            RemoveButton("Button1");
            RemoveButton("Button2");
            RemoveButton("Button3");
            return;
        }
        quizScreen.SetActive(false);
        
    }
}
