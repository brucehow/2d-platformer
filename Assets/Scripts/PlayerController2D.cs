using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    [SerializeField]
    private LayerMask groundCollisionLayer;

    private float runSpeed = 10.0f;
    private float jumpHeight = 25.0f;

    private bool doubleJump = true;
    private bool facingRight = true;

    public GameObject bulletPrefab;
    public Transform firePoint;
    private float shootDelay;
    private float shootSpeed = 0.3f;

    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        shootDelay -= Time.deltaTime;
        if (Input.GetKey("right")) {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            playAnim("mask_run");
            if (!facingRight) {
                transform.Rotate(0, 180, 0);
                facingRight = true;
            }
        } else if (Input.GetKey("left")) {
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            playAnim("mask_run");
            if (facingRight) {
                transform.Rotate(0, 180, 0);
                facingRight = false;
            }
        } else {
            playAnim("mask_idle");
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKeyDown("space")) {
            if (grounded()) {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                animator.Play("mask_jump");
                doubleJump = true;
            } else if (doubleJump) {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                animator.Play("mask_djump");
                doubleJump = false;
            }
        }
        if (Input.GetButton("Fire1")) {
            shoot();
        }
    }

    private void playAnim(string type) {
        if (falling()) {
            animator.Play("mask_fall");
        } else if (!grounded()) {
            if (doubleJump) {
                animator.Play("mask_jump");
            } else {
                animator.Play("mask_djump");
            }
        } else {
            animator.Play(type);
        }
    }
    private void shoot() {
        if (shootDelay < 0) {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            shootDelay = shootSpeed;
        }
        
    }

    private bool grounded() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundCollisionLayer);
        return hit.collider != null;
    }

    private bool falling() {
        return rb.velocity.y < -0.1;
    }
}   
