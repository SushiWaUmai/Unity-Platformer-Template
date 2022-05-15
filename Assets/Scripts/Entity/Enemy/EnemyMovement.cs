using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy Movement Attributes")]

    /// <summary>
    /// The movement speed of the enemy
    /// </summary>
    [SerializeField, Tooltip("The movement speed of the enemy")]
    protected float _speed = 2f;

    /// <summary>
    /// The Ground Layer of the Scene
    /// </summary>
    [SerializeField, Tooltip("The Ground Layer of the Scene")]
    protected LayerMask _groundLayer;

    /// <summary>
    /// The distance of the raycasting range
    /// </summary>
    [SerializeField, Tooltip("The distance of the raycasting range")]
    protected float _groundCastDistance = 1f;

    ///  <summary>
    /// RigidBody2D Component of the Enemy
    /// </summary>
    protected Rigidbody2D _rb;

    /// <summary>
    /// The facing direction of the enemy
    /// </summary>
    protected int _faceDirection = 1;

    /// <summary>
    /// Simple getter for the facing direction of the enemy
    /// </summary>
    public int FaceDirection { get { return _faceDirection; } }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = UpdateVelocity();
    }

    /// <summary>
    /// Updates the velocity of the enemy
    /// </summary>
    /// <returns>target velocity</returns>
    protected virtual Vector2 UpdateVelocity()
    {
        // If there is a wall or a gap in front of the enemy, turn around
        if (IsFacingWall() || IsFacingGap())
            _faceDirection *= -1;

        return new Vector2(_faceDirection * _speed, _rb.velocity.y);
    }

    /// <summary>
    /// Check if there is a wall in front of the enemy
    /// </summary>
    /// <returns>
    /// true if there is a gap in front of the enemy
    /// false if there is no gap in front of the enemy
    /// </returns>
    protected bool IsFacingWall()
    {
        Ray2D wallCheckRay = new Ray2D(transform.position, Vector2.right * _faceDirection);

        // Raycast to check if there is a wall in front of the enemy
        RaycastHit2D wallCheck = Physics2D.Raycast(wallCheckRay.origin, wallCheckRay.direction, _groundCastDistance, _groundLayer);

        return wallCheck.collider != null;
    }

    ///  <summary>
    /// Check if there is a gap in front of the enemy
    /// </summary>
    /// <returns>
    /// true if there is a gap in front of the enemy
    /// false if there is no gap in front of the enemy
    /// </returns>
    protected bool IsFacingGap()
    {
        // Raycast to check if there is a gap in front of the enemy
        Ray2D gapCheckRay = new Ray2D((Vector2)transform.position + Vector2.right * _faceDirection, Vector2.down);
        RaycastHit2D gapCheck = Physics2D.Raycast(gapCheckRay.origin, gapCheckRay.direction, _groundCastDistance, _groundLayer);

        return gapCheck.collider == null;
    }

    // Draw the raycasts in the Scene View
    protected virtual void OnDrawGizmosSelected()
    {
        Ray2D wallCheckRay = new Ray2D(transform.position, Vector2.right * _faceDirection);
        Ray2D gapCheckRay = new Ray2D((Vector2)transform.position + Vector2.right * _faceDirection, Vector2.down);

        Debug.DrawRay(wallCheckRay.origin, wallCheckRay.direction * _groundCastDistance, Color.green);
        Debug.DrawRay(gapCheckRay.origin, gapCheckRay.direction * _groundCastDistance, Color.yellow);
    }
}
