using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    private PlayerStatsController pStats;
    // Start is called before the first frame update
    void Start()
    {
        this.pStats = GetComponent<PlayerStatsController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
