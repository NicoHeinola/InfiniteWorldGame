using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [Serializable]
    public struct SkillPair<T>
    {
        public string name;
        public T skill;
    }

    // Player
    private GameObject player;
    private PlayerStatsController pStats;
    private Animator pAnimator;

    // Skills
    private Dictionary<string, BaseSkill> skills = new Dictionary<string, BaseSkill>();

    // -- types
    public List<SkillPair<BaseSkill>> baseSkillPairs = new List<SkillPair<BaseSkill>>(); // Contains all skills

    // -- updates
    private HashSet<Action> skillUpdates = new HashSet<Action>();
    void Start()
    {
        this.player = this.gameObject;
        this.pAnimator = GetComponent<Animator>();
        this.pStats = GetComponent<PlayerStatsController>();


        // Initializes skill dictionaries
        foreach (SkillPair<BaseSkill> sp in baseSkillPairs)
        {
            BaseSkill skill = Instantiate(sp.skill); // Copy the skill so the prefab won't be changed
            this.skills[sp.name] = skill;
            skill.Initialize(this);
            if (skill.IsActive())
            {
                skill.OnActivate();
            }

        }
    }

    private void Update()
    {
        foreach (Action updateFunc in this.skillUpdates)
        {
            updateFunc();
        }
    }

    public void AddSkillUpdate(Action func)
    {
        this.skillUpdates.Add(func);
    }
    public void RemoveSkillUpdate(Action func)
    {
        this.skillUpdates.Remove(func);
    }
    public GameObject GetPlayer() { return this.player; }
    public PlayerStatsController GetPlayerStats() { return this.pStats; }
    public Animator GetPlayerAnimator() { return this.pAnimator; }
}
