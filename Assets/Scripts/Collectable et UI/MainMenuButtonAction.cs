using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonAction : MonoBehaviour
{



    /// <summary>
    /// Permet d'afficher un panel transmis en paramètre
    /// </summary>
    /// <param name="PanelAOuvrir">Panel à afficher</param>

    public void AfficherPanel(GameObject PanelAOuvrir)
    {
        PanelAOuvrir.SetActive(true);
    }

    /// <summary>
    /// Permet de ferme aussi le panel actuel
    /// </summary>
    /// <param name="PanelAFermer">Panel à fermer</param>
    public void FermerPanel(GameObject PanelAFermer)
    {
        PanelAFermer.SetActive(false);
    }

    public void ActiverBouton()
    {
   
        Button niv2Button = GameObject.FindGameObjectsWithTag("Level2")[0].GetComponent<Button>();
        Button niv3Button = GameObject.FindGameObjectsWithTag("Level3")[0].GetComponent<Button>();


        if ((GameManager.Instance.PlayerData.AvoirNiveauTermine("Level1"))){
            niv2Button.interactable = true;
        }

        if ((GameManager.Instance.PlayerData.AvoirNiveauTermine("Level2")))
        {
            niv3Button.interactable = true;
        }

    }

    /// <summary>
    /// Permet de charger un niveau
    /// </summary>
    /// <param name="nom">Nom du niveau à charger</param>
    public void ChargerNiveau(string nom)
    {
        /*if ((GameManager.Instance.PlayerData.AvoirNiveauTermine(nom))
            this._button.interactable = true;*/
           

        SceneManager.LoadScene(nom);
    }

    /// <summary>
    /// Permet de fermer l'application
    /// </summary>
    public void Quitter()
    {
        Application.Quit();
    }
}
