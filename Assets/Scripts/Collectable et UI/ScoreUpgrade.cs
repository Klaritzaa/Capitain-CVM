using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpgrade : MonoBehaviour
{
    /// <summary>
    /// Valeur de l'énergie regagner au contact
    /// </summary>
    [SerializeField]
    private int _gainPoint = 3;
    [SerializeField]
    private AudioClip _clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {

            if (this.gameObject.tag.Equals("Hat"))
            {
                GameManager.Instance.PlayerData.IncrHat();
            }
            else if (this.gameObject.tag.Equals("Conv")){
                GameManager.Instance.PlayerData.IncrConv();
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
