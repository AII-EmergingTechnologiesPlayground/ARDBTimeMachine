using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game
{
    public string gameID { get; set; }
    public List<Question> questions { get; set; }
}

public class Question
{
    public string question { get; set; }
    public List<string> answers { get; set; }
    public int timer { get; set; }
    public string description { get; set; }
    public string link { get; set; }
    public int correctAnswer { get; set; }
}
public class GameDataObject
{
    public Game game { get; set; }
}


