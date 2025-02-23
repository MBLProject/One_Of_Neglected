using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aug_Warlock : ConditionalAugment
{
    private float teleportDistance = 1f; 
    private bool teleported = false;
    private GameObject teleportEffectPrefab;

    public Aug_Warlock(Player owner) : base(owner)
    {
        aguName = Enums.AugmentName.Warlock;
        teleportEffectPrefab = Resources.Load<GameObject>("Using/Effect/TeleportEffect");
    }

    public override void Activate()
    {
        base.Activate();
        owner.dashDetect += OnDashDetected;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        owner.dashDetect -= OnDashDetected;
    }

    public override bool CheckCondition()
    {
        return true; // 항상 활성화
    }

    public override void OnConditionDetect()
    {
        // 조건 감지시 필요한 로직
    }

    private void OnDashDetected()
    {
        if (owner is Player player)
        {
            teleported = true;

            if (teleportEffectPrefab != null)
            {
                GameObject startEffect = GameObject.Instantiate(teleportEffectPrefab, player.transform.position, Quaternion.identity);
                GameObject.Destroy(startEffect, 1f);
            }

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector2 direction;

            if (horizontalInput != 0 || verticalInput != 0)
            {
                direction = new Vector2(horizontalInput, verticalInput).normalized;
            }
            else
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePosition - (Vector2)player.transform.position).normalized;
            }

            Vector2 targetPosition = (Vector2)player.transform.position + (direction * teleportDistance);
            player.transform.position = targetPosition;
            player.SetCurrentPositionAsTarget();

            if (teleportEffectPrefab != null)
            {
                GameObject endEffect = GameObject.Instantiate(teleportEffectPrefab, targetPosition, Quaternion.identity);
                GameObject.Destroy(endEffect, 0.5f); 
            }

            player.SetDashing(false);
            player.SetSkillInProgress(false, false);
            player.Animator?.SetBool("IsMoving", false);
            player.Animator?.SetTrigger("Idle");

            player.stateHandler.ChangeState(typeof(MagicianIdleState));
        }
    }

    public bool WasTeleported()
    {
        if (teleported)
        {
            teleported = false;
            return true;
        }
        return false;
    }
}
