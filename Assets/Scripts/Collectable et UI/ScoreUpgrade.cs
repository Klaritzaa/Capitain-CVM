using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScoreUpgrade : MonoBehaviour
{
    /// <summary>
    /// Valeur de l'énergie regagner au contact
    /// </summary>
    [SerializeField]
    private int _gainPoint = 3;
    [SerializeField]
    private AudioClip _clip;

    private string _name;

    private void Start()
    {
        _name = SceneManager.GetActiveScene().name.Replace(' ', '_')
            + $"__{(int)this.transform.position.x}_{(int)this.transform.position.y}";

        if(GameManager.Instance.PlayerData.AvoirCollected(_name))
            GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {

            if (this.gameObject.tag.Equals("Hat"))
            {
                GameManager.Instance.PlayerData.IncrHat();
                GameManager.Instance.PlayerData.AjouterCollectable(_name);
            }
            else if (this.gameObject.tag.Equals("Conv")){
                GameManager.Instance.PlayerData.IncrConv();
                GameManager.Instance.PlayerData.AjouterCollectable(_name);
            }

            GameManager.Instance.AudioManager
                .PlayClipAtPoint(_clip, this.transform.position);
            GameManager.Instance
                .PlayerData.IncrScore(this._gainPoint);
            GameObject.Destroy(this.gameObject);

            Debug.Log(this.gameObject.tag);
        }
    }
}
