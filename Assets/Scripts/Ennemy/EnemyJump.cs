using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyJump : MonoBehaviour
{
    /// <summary>
    /// Vitesse de l'objet en patrouille
    /// </summary>
    [SerializeField]
    private float jumpForceX = 2f;
    /// <summary>
    /// Liste de GO représentant les points à atteindre
    /// </summary>
    [SerializeField]
    private float jumpForceY = 4f;
    /// <summary>
    /// Référence vers la cible actuelle de l'objet
    /// </summary>
    //private Transform _cible = null;
    /// <summary>
    /// Permet de connaître la position actuelle de la cible dans le tableau
    /// </summary>
   // private int _indexPoint;
    /// <summary>
    /// Seuil où l'objet change de cible de déplacement
    /// </summary>
    //private float _distanceSeuil = 0.3f;
    /// <summary>
    /// Référence vers le sprite Renderer
    /// </summary>
    /// 
    private SpriteRenderer _sr;

   


    private Animator _animator;

    private Rigidbody2D _rb;

    private bool _ausol = false;
    private bool _saute = false;
    private bool _tombe = false;

    public float idleTime = 2f;
    public float currentIdleTime = 0;
    public bool isIdle = true;

   
    public float _PosY = 0;
   
 
    // Start is called before the first frame update
    void Start()
    {
      
        _PosY = transform.position.y;
        _rb = GetComponent<Rigidbody2D>();
        _sr = this.GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (isIdle)
        {
            currentIdleTime += Time.deltaTime;

            if (currentIdleTime >= idleTime)
            {
    
                currentIdleTime = 0;
                _sr.flipX = !_sr.flipX;
                Saute();
            }
        }

        if(_ausol && !_saute)
        {
            _tombe = false;
            _saute = false;
            isIdle = true;
        }
        else if(transform.position.y > _PosY && !_ausol && !isIdle)
        {
            _saute = true;
            _tombe = false;

        }
        else if(transform.position.y < _PosY && !_ausol && !isIdle)
        {
            _saute = false;
            _tombe = true;
        }

        _PosY = transform.position.y;
    }

    /// <summary>
    /// Détermine si le sprite est au sol
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (collision.gameObject.tag.Equals("Tilemap"))
        {
            _ausol = true;
            Debug.Log("Au sol");
            isIdle = true;
        }
    }

    /// <summary>
    /// Change la direction de déplacement lors du saut et effectue le saut
    /// avec l'animation
    /// </summary>
    void Saute()
    {
        _saute = true;
        isIdle = false;

        int direction = 0;

        
        if (_sr.flipX)
            direction = 1;
        else
            direction = -1;

        
        _rb.velocity = new Vector3(jumpForceX * direction, jumpForceY);

        _animator.SetTrigger("JumpActif");
        Debug.Log("Jump");
    }

   
}
