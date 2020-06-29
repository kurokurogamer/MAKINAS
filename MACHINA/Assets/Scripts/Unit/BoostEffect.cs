using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _particles;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimation(bool active)
    {
        _animator.SetBool("Drive", active);
    }


    public void PlayEffect()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Play();
        }
    }
    public void StopEffect()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Stop();
        }
    }
}
