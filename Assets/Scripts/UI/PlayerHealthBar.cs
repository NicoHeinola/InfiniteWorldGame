using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : Bar
{
    public GameObject player;
    private PlayerStatsController pStats;
    // Start is called before the first frame update
    void Start()
    {
        pStats = player.GetComponent<PlayerStatsController>();
        this.SetMaxValue(pStats.maxHp.GetValue());
        this.SetMinValue(0);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.SetMaxValue(pStats.maxHp.GetValue());
        this.SetValue(pStats.hp);
    }
}
