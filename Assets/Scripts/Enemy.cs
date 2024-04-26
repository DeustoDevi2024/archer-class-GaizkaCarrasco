using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archer
{

    public class Enemy : MonoBehaviour, IScoreProvider
    {

        // Cuántas vidas tiene el enemigo
        [SerializeField]
        private int hitPoints;

        public GameObject sun;
        public GameObject night;
        private AudioSource sound;
        private Animator animator;

        public event IScoreProvider.ScoreAddedHandler OnScoreAdded;

        Coroutine coroutine;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sound = GetComponent<AudioSource>();
        }

        // Método que se llamará cuando el enemigo reciba un impacto
        public void Hit()
        {
            hitPoints -= 1;
            if (hitPoints <= 0)
            {
                Die();
            }
            sound.Play();
        }

        private void Die()
        {
            animator.SetTrigger("Die");
            coroutine = StartCoroutine(DieCoroutine());
        }

        IEnumerator DieCoroutine()
        {
            sun.SetActive(true);
            night.SetActive(false);
            yield return new WaitForSeconds(3);
            sun.SetActive(false);
            night.SetActive(true);
            Destroy(this.gameObject);
        }
    }

}