using UnityEngine;

/// <summary>
///  Class that handles the Enemy Animation
/// </summary>
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    // The Animator Component of the Enemy, this is needed to change the Animator Parameters
    protected Animator _anim;

    // The SpriteRenderer Component of the Enemy, this is needed to flip the sprite
    protected SpriteRenderer _spriteRenderer;

    [SerializeField, Tooltip("The RigidBody2D Component of the Enemy, this is needed to check the velocity oif the enemy")]
    protected Rigidbody2D _rb;

    [SerializeField, Tooltip("The EnemyMovement Component of the Enemy, this is needed to check the facing direction of the enemys")]
    protected EnemyMovement _movement;

    [SerializeField, Tooltip("The Enemy Component of the Enemy, this is needed to subscribe to the OnDeath event")]
    protected Enemy _enemy;

    [SerializeField, Tooltip("The jump velocity applied to the enemy on death")]
    protected float _jumpHeightOnDeath = 5f;

    [SerializeField] private string _speedParameter = "speed";
    [SerializeField] private string _deathTrigger = "death";

    private void Start()
    {
        // First, get the components of the gameobject
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Subscribe to the OnDeath event of the Enemy, in order to play the death animation
        _enemy.OnDeath += DeathAnimation;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnDeath event of the Enemy
        _enemy.OnDeath -= DeathAnimation;
    }

    private void OnValidate()
    {
        if (!_rb)
            _rb = GetComponentInParent<Rigidbody2D>();
        if (!_enemy)
            _enemy = GetComponentInParent<Enemy>();
        if (!_movement)
            _movement = GetComponentInParent<EnemyMovement>();
    }

    protected virtual void Update()
    {
        // Set the speed parameter of the Animator to the velocity of the RigidBody2D
        _anim.SetFloat(_speedParameter, Mathf.Abs(_rb.velocity.x));

        // Set the facing parameter of the Animator to the facing direction of the EnemyMovement
        _spriteRenderer.flipX = _movement.FaceDirection > 0;
    }

    private void DeathAnimation()
    {
        // Apply the jump velocity to the RigidBody2D
        _rb.velocity = Vector2.up * _jumpHeightOnDeath + Vector2.right * Random.Range(-1f, 1f);

        // Set the death parameter of the Animator to true
        _anim.SetTrigger(_deathTrigger);
    }
}
