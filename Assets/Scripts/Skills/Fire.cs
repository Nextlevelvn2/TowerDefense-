using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fire : MonoBehaviour
{
    [SerializeField] AudioClip exploitionClip;
    [SerializeField] LayerMask enemyLayerMask;
    public Vector3 _target;
    int amountDamage = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            RotateFire(_target);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "waypoint")
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f, enemyLayerMask);
            if(colliders != null)
            {
                foreach(var collider in colliders)
                {
                    if (collider.GetComponent<EnemyHealth>().Health > 0)
                    {
                        collider.GetComponent<EnemyHealth>().TakeDamage(amountDamage);
                    }
                }
            }
            SoundManager.Instance.PlaySound(exploitionClip);
            Destroy(gameObject);
        }
    }
    public void SetTarget(Vector3 target)
    {
        _target = target;
    }
    public void RotateFire(Vector3 target)
    {
        Vector3 vectorTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorTarget.y, vectorTarget.x) * Mathf.Rad2Deg -270f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 50f);
    }
}
