using UnityEngine;

public enum WordType { Subject, Verb, Object };
[CreateAssetMenu(fileName = "NewWord", menuName = "Objects/Word")]
public class WordData : ScriptableObject {
    public string wordText;
    public WordType type; // Enum: Subject, Verb, Object
    public int powerValue; // For the "believability" logic
    public string theme; // e.g., "War", "Love", "Wealth"
}