using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDeNiveau : MonoBehaviour
{

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Félicitation, le niveau est terminé.");
            Debug.Log(SceneManager.GetActiveScene().name);

            GameManager.Instance.PlayerData.AjouterNiveauTermine(SceneManager.GetActiveScene().name);
            GameManager.Instance.SaveData();
           
            SceneManager.LoadScene("MainMenu");
            
        }
    }
}
