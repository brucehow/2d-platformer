using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Animator animator;
    private int health;

    private void Start() {
        animator = GetComponent<Animator>();
        if (transform.name.Equals("PigMob")) {
            this.health = 20;
        }
    }

    public void takeDamage(int damage, Collider2D hit) {
        health -= damage;
        if (health <= 0) {
            mobDeath();
        }
        hitAnim(hit);
    }

    private void hitAnim(Collider2D hit) {
        if (transform.tag.Equals("PigMob")) {
            animator.Play("pig_hit1");
            hit.GetComponent<PigMob>().setAngry(true);
        }
    }

    private void mobDeath() {
        Destroy(gameObject);
    }
}