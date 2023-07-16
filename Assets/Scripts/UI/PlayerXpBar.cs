using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXpBar : Bar
{
    public GameObject player;
    private PlayerStatsController pStats;
    // Start is called before the first frame update
    void Start()
    {
        pStats = player.GetComponent<PlayerStatsController>();
        this.SetMaxValue(pStats.GetNextXp());
        this.SetMinValue(0);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.SetMaxValue(pStats.GetNextXp());
        this.SetValue(pStats.GetXp());
    }
}
