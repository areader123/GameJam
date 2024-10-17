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
    public Entity_FX fx { get;private set;}
    #endregion
    [SerializeField]
    public float movementSpeed = 5;
    public float horizonal =0;
    public float vertical =0;
    public bool faceRight{get;private set;} = true;
    public int faceDir = 1;

    public bool isbusy;

    public bool isKoncked;
    [SerializeField]private float konckedSpeed;
    [SerializeField]private float konckbackDuration;
    
    

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<Entity_FX>();
    }


    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {

    }

    public virtual void Damage(Entity_Stat  entity_Stat = null)
    {
        
        //实体受击效果
    }

    public virtual void Die()
    {
        
    }

      protected virtual IEnumerator HitKnockback()
    {
        isKoncked = true;
        animator.speed =0;
        Debug.Log("HitKnockback Start");
        rb.velocity = new Vector2(konckedSpeed * (-faceDir), 0);
        yield return new WaitForSeconds(konckbackDuration);
         rb.velocity =  Vector2.zero;
        animator.speed =1;
        //rb.velocity = new Vector2(0,0);
        Debug.Log("HitKnockback End");
        isKoncked = false;
        
       
    }

    #region Velocity
    public void SetVelocity(float _xVelocity, float _yVelocity,float movementSpeed)
    {
         if (isKoncked)
         {
             return;
         }
        float speed = movementSpeed;
        rb.velocity = new Vector2(_xVelocity, _yVelocity).normalized * speed;
    }
    #endregion

    
     protected void FlipControll(float Input)
        {
            if (Input < 0 && faceRight)
            {
                transform.Rotate(0, 180, 0);
                faceDir *= -1;
                faceRight = !faceRight;
            }
            if (Input > 0 && !faceRight)
            {
                transform.Rotate(0, 180, 0);
                faceDir *= -1;
                faceRight = !faceRight;
            }
        }
     protected virtual void OnDrawGizmos()
     {
        
     }
     public IEnumerator BusyFor(float _seconds)
        {
            isbusy = true;
            yield return new WaitForSeconds(_seconds);
            isbusy = false;
        }
}

}
