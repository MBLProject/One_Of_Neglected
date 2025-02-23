using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomController : MonoBehaviour
{
    private float dashSpeed = 10f;
    private float dashDuration = 1f;
    private float damage;
    private bool isDashing;
    private float dashTimer;
    private Vector3 dashDirection;

    public bool HasFinishedDash { get; private set; }

    public void Initialize(float baseDamage)
    {
        damage = baseDamage;
        isDashing = false;
        HasFinishedDash = false;
        dashTimer = 0f;
    }

    public void StartDash(Vector3 direction)
    {
        isDashing = true;
        dashDirection = direction;
        dashTimer = 0f;
    }

    private void Update()
    {
        if (!isDashing) return;

        dashTimer += Time.deltaTime;

        if (dashTimer <= dashDuration)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
        }
        else
        {
            isDashing = false;
            HasFinishedDash = true;
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDashing) return;

        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                //TODO 대적자 가호 여부에 따른 데미지 계산 필요
                player.TakeDamage(damage);
                Debug.Log($"[PhantomController] 플레이어에게 {damage} 데미지!");
            }
        }
    }
}
