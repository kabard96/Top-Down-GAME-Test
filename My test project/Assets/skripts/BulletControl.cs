using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    private void FixedUpdate()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("zombie"))
            {
                hitInfo.collider.GetComponent<ZombieScript>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);






    }
}
