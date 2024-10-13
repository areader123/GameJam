using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{
    public class Entity : MonoBehaviour
{
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    #endregion
    [SerializeField]
    public float movementSpeed = 5;
    public float horizonal =0;
    public float vertical =0;
    public float last_face_reigon;
    
    

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    protected virtual void Start()
    {
        last_face_reigon =1;
    }

    protected virtual void Update()
    {

    }

    public virtual void Damage()
    {
        
        //实体受击效果
    }

    // protected virtual IEnumerator HitKnockback()
    // {

    // }

    #region Velocity
    public void SetVelocity(float _xVelocity, float _yVelocity,float movementSpeed)
    {
        // if (isKoncked)
        // {
        //     return;
        // }
        float speed = movementSpeed;
        rb.velocity = new Vector2(_xVelocity, _yVelocity) * speed;
    }
    #endregion


     public virtual void OnDrawGizmos()
     {
        
     }
}

}
