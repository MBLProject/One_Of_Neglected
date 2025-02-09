using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private StateHandler<Monster> stateHandler;

    public float moveSpeed = 3f;
    public float attackDamage = 10f;

    [SerializeField] private Rigidbody2D rb;
    private Transform playerTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        stateHandler = new StateHandler<Monster>(this);
        stateHandler.RegisterState(new MonsterMoveState(stateHandler));
        stateHandler.RegisterState(new MonsterAttackState(stateHandler));

        // 생성되자마자 이동 상태 시작
        stateHandler.ChangeState(typeof(MonsterMoveState));
    }

    private void Update()
    {
        stateHandler.Update();
    }

    public void MoveTowardsPlayer()
    {
        if (playerTransform == null) return;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // 2D 스프라이트 방향 전환 (선택적)
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(direction.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stateHandler.ChangeState(typeof(MonsterAttackState));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stateHandler.ChangeState(typeof(MonsterMoveState));
        }
    }
}
