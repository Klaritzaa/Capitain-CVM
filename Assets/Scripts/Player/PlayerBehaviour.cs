using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// Décrit si l'entité est invulnérable
    /// </summary>
    private bool _invulnerable = false;
    /// <summary>
    /// Réfère à l'animator du GO
    /// </summary>
    private Animator _animator;
    /// <summary>
    /// Représente le moment où l'invulnaribilité a commencé
    /// </summary>
    private float _tempsDebutInvulnerabilite;

    private void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (Time.fixedTime > _tempsDebutInvulnerabilite + EnnemyBehaviour.DelaisInvulnerabilite)
            _invulnerable = false;

        /*if (GameManager.Instance.PlayerData.Vie <= 0)
        {
            //GameManager.Instance.PlayerData.resetForGameOver();

            SceneManager.LoadScene("MainMenu");
        }*/
    }

    public void CallEnnemyCollision()
    {
        if (!_invulnerable)
        {
            _animator.SetTrigger("DegatActif");
            GameManager.Instance.PlayerData.DecrEnergie();
            _tempsDebutInvulnerabilite = Time.fixedTime;
            _invulnerable = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tilemap Water"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            GameObject.Destroy(this.gameObject);
            GameManager.Instance.PlayerData.DecrVie();
        }
    }
}
