using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MyData", menuName ="Data/DataBase")]
public class Scores_Object : ScriptableObject
{
    public List<int> _Score;
    public List<int> _9Scores;
    readonly public List<int> _Chance = new List<int>() { 1, 5, 10, 0, 50, 0, 100, 250, 500, 1000, 2000 };
    public int startPoints;
    public int points;
    public int timeFrame;

}
