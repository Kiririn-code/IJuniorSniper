using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    [SerializeField] private int _scoreFactor;

    public int ScoreFactor => _scoreFactor;
}
