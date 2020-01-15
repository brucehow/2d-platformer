using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Animator animator;
    private int health;
    private int deathSpeed = 15;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private bool dead;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        if (transform.name.Equals("PigMob")) {
            this.health = 20;
        }
        dead = false;
    }

    private void Update() {
        if (dead) {
            transform.Rotate(0, 0, -100 * Time.deltaTime);
            rb.gravityScale = 500 * Time.deltaTime;
        }
    }

    public void takeDamage(int damage, Collider2D hit) {
        health -= damage;
        if (health <= 0) {
            mobDeath(hit);
        } else {
            hitAnim(hit);
        }
    }

    private void hitAnim(Collider2D hit) {
        if (transform.tag.Equals("PigMob")) {
            animator.Play("pig_hit1");
            hit.GetComponent<PigMob>().setAngry(true);
        }
    }

    private void mobDeath(Collider2D hit) {
        if (transform.tag.Equals("PigMob")) {
            animator.Play("pig_hit2");
            hit.GetComponent<PigMob>().setDead(true);
        }
        
        // Universal death animation
        dead = true;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 15), deathSpeed * Time.deltaTime);
        bc.enabled = false;
    }
}