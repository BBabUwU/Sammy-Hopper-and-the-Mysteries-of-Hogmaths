using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float _speed = 5f;
    public float _attackRange = 3f;
    private Transform _player;
    private Rigidbody2D _bossRb;
    Boss _boss;
    public float _throwCoolDown = 3f;
    private float _nextFireTime = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _bossRb = animator.GetComponent<Rigidbody2D>();
        _boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss.LookAtPlayer();
        Vector2 _target = new Vector2(_player.position.x, _bossRb.position.y);
        Vector2 _newPos = Vector2.MoveTowards(_bossRb.position, _target, _speed * Time.fixedDeltaTime);
        _bossRb.MovePosition(_newPos);

        float _distanceFromPlayer = Vector2.Distance(_player.position, _bossRb.position);


        float _randomNumber = Random.Range(1f, 100f);

        if (_distanceFromPlayer > _attackRange && _randomNumber <= 2f && Time.time > _nextFireTime)
        {
            animator.SetTrigger("ThrowRock");
            _nextFireTime = Time.time + _throwCoolDown;
        }

        if (_distanceFromPlayer <= _attackRange)
        {
            // Attack
            animator.SetTrigger("Attack");
        }

        if (_boss._isDefeated)
        {
            animator.SetTrigger("Dead");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("ThrowRock");
        animator.ResetTrigger("Dead");
    }
}
