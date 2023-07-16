using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public PlayerStatsController pStats;
    void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        this.text.text = $"Level: {pStats.Level}";
    }
}
