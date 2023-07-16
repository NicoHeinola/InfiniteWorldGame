using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpMagnet : MonoBehaviour
{
    private GameObject player;
    private PlayerStatsController pStats;

    public string playerTag;
    private bool inRange = false;
    public float pickupRange = 1.5f;
    public float speed = 0.3f;
    public float distanceUntilPicked = 0.2f;
    public int xpAmount = 1;

    private void Start()
    {
        this.player = GameObject.FindGameObjectWithTag(playerTag);
        this.pStats = player.GetComponent<PlayerStatsController>();
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance <= this.pickupRange)
        {
            if (distance <= this.distanceUntilPicked)
            {
                this.pStats.AddXp(this.xpAmount);
                Destroy(gameObject);
            }
            this.inRange = true;
        }
        else
        {
            this.inRange = false;
        }
    }

    private void Update()
    {
        if (this.inRange)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.player.transform.position, speed * Time.deltaTime);
        }
    }
}
