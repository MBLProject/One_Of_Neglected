using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Needle : Skill
{
    public Needle(float defaultCooldown) : base(Enums.SkillName.Needle, defaultCooldown) { }

    protected override async UniTask StartSkill()
    {
        isSkillActive = true;

        while (isSkillActive)
        {
            if (!GameManager.Instance.isPaused)
            {
                Debug.Log($"Fire! : {skillName}");
                for (int i = 0; i < shotCount; ++i)
                {
                    for (int j = 0; j < projectileCount; ++j)
                        SpawnNeedles();
                }
                await DelayFloat(defaultCooldown);
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }

    private void SpawnNeedles()
    {
        var enemies = FindNearbyEnemies();
        int needleCount = CalculateNeedleCount();

        foreach (var enemy in enemies)
        {
            for (int i = 0; i < needleCount; i++)
            {
                SpawnNeedleAtEnemy(enemy);
            }
        }
    }

    private GameObject[] FindNearbyEnemies()
    {
        return new GameObject[0];
    }

    private int CalculateNeedleCount()
    {
        return level * 2;
    }

    private void SpawnNeedleAtEnemy(GameObject enemy)
    {

    }
}
