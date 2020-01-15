using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMob : MonoBehaviour {

    private Animator animator;

    private float mobSpeed = 4.0f;
    private Transform target;
    private bool angry;
    private bool dead;
    private bool facingRight;
    private BoxCollider2D collider;

    private void Start() {
        dead = false;
        collider = GetComponent<BoxCollider2D>();
        facingRight = false;
        angry = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (angry && !dead) {
            if (transform.position.x - 1 > target.position.x && facingRight) {
                transform.Rotate(0, 180, 0);
                facingRight = !facingRight;
            } else if (transform.position.x + 1 < target.position.x && !facingRight) {
                transform.Rotate(0, 180, 0);
                facingRight = !facingRight;
            }

            if (transform.position.x - collider.bounds.extents.x > target.position.x && !facingRight) {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), mobSpeed * Time.deltaTime);
            } else if (transform.position.x + collider.bounds.extents.x < target.position.x && facingRight) {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), mobSpeed * Time.deltaTime);
            }
        }
    }

    public void setAngry(bool angry) {
        this.angry = angry;
    }

    public void setDead(bool dead) {
        this.dead = dead;
    }
}
