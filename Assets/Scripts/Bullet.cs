using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float bulletSpeed = 30f;
    public Rigidbody2D rb;
    private int damage = 5;

    private void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hit) {
        if (hit.tag.Equals("Player")) return;
        Enemy enemy = hit.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.takeDamage(damage, hit);
        }
        Destroy(this.gameObject);
    }
}
