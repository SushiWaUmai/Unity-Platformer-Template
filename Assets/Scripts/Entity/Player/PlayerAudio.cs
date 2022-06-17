using UnityEngine;
using DYP;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _die;

    [SerializeField] private BasicMovementController2D _playerController;
    [SerializeField] private PlayerLife _playerLife;

    private void OnValidate()
    {
        if (!_playerController)
            _playerController = GetComponent<BasicMovementController2D>();

        if (!_playerLife)
            _playerLife = GetComponent<PlayerLife>();
    }

    private void OnEnable()
    {
        _playerController.OnJump += OnJump;
        _playerLife.OnHit += OnHit;
        _playerLife.OnDeath += OnDie;
    }

    private void OnDisable()
    {
        _playerController.OnJump -= OnJump;
        _playerLife.OnHit -= OnHit;
        _playerLife.OnDeath -= OnDie;
    }

    public void OnJump()
    {
        if (!_jump)
            return;

        AudioManager.Instance.PlaySound(_jump, AudioManager.AudioChannel.SFX);
    }

    public void OnHit(int _)
    {
        if (!_hit)
            return;

        AudioManager.Instance.PlaySound(_hit, AudioManager.AudioChannel.SFX);
    }

    public void OnDie()
    {
        if (!_die)
            return;

        AudioManager.Instance.PlaySound(_die, AudioManager.AudioChannel.SFX);
    }
}

