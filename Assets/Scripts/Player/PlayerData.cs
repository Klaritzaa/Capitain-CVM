﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Représente les données de jeu
/// </summary>
[System.Serializable]
public class PlayerData
{
    /// <summary>
    /// Niveau sélectionné par l'utilisateur pour le vol. général
    /// </summary>
    [Range(-80, 0)]
    private float _volumeGeneral = 0;
    public float VolumeGeneral { get { return _volumeGeneral; } set { _volumeGeneral = value; } }

    /// <summary>
    /// Niveau sélectionné par l'utilisateur pour le vol. de la musique
    /// </summary>
    [Range(-80, 0)]
    private float _volumeMusique = 0;
    public float VolumeMusique { get { return _volumeMusique; } set { _volumeMusique = value; } }

    /// <summary>
    /// Niveau sélectionné par l'utilisateur pour le vol. de la musique
    /// </summary>
    [Range(-80, 0)]
    private float _volumeEffet = 0;
    public float VolumeEffet { get { return _volumeEffet; } set { _volumeEffet = value; } }

    /// <summary>
    /// Représente le nombre de points de vie du personnage
    /// </summary>
    private int _vie;
    /// <summary>
    /// Représente le nombre d'énergie (entre 0 et 4)
    /// </summary>
    private int _energie;
    /// <summary>
    /// Représente le score obtenu
    /// </summary>
    private int _score;
    /// <summary>
    /// Représente le nombre de chapeaux récoltés
    /// </summary>
    private int _hatCount;
    /// <summary>
    /// Représente le nombre de convention récoltées
    /// </summary>
    private int _convCount;
    /// <summary>
    /// Liste des coffres ouverts dans le jeu
    /// </summary>
    private List<string> _chestOpenList;
    /// <summary>
    /// Représente le maximum d'énergie du personnage
    /// </summary>
    public const int MAX_ENERGIE = 4;
    /// <summary>
    /// Permet d'identifier les actions sur le UI à réaliser
    /// lors de la perte d'énergie
    /// </summary>
    public System.Action UIPerteEnergie;
    /// <summary>
    /// Permet d'identifier les actions sur le UI à réaliser
    /// lors de la perte d'énergie
    /// </summary>
    public System.Action UIPerteVie;
    /// <summary>
    /// Permet d'identifier les actions à réaliser lors d'un gameover
    /// </summary>
    public System.Action Gameover;
    /// <summary>
    /// Permet d'identifier les niveaux terminés
    /// </summary>
    private List<string> _listLevelDone;
    /// <summary>
    /// Liste des collectables ramassés dans leu
    /// </summary>
    private List<string> _listCollectable;

    public int Energie { get { return this._energie; } }
    public int Vie { get { return this._vie; } }
    public int Score { get { return this._score; } }
    public string[] ListeCoffreOuvert { get { return this._chestOpenList.ToArray(); } }
    public string[] ListeLevelsDone { get { return this._listLevelDone.ToArray(); } }
    public int HatCount { get { return this._hatCount; } }
    public int ConventionCount { get { return this._convCount; } }
    public string[] CollectableList { get { return this._listCollectable.ToArray(); } }

    public PlayerData()
    {
        this._vie = 0;
        this._energie = 0;
        this._score = 0;
        this._volumeGeneral = 0;
        this._volumeMusique = 0;
        this._volumeEffet = 0;
        this.UIPerteEnergie = null;
        this.UIPerteVie = null;
        this.Gameover = null;
        this._chestOpenList = new List<string>();
        this._listLevelDone = new List<string>();
        this._listCollectable = new List<string>();
        this._hatCount = 0;
        this._convCount = 0;
    }

    public PlayerData(int vie = 1, int energie = 2, int score = 0,
        float volumeGeneral = 0, float volumeMusique = 0, float volumeEffet = 0,
        System.Action uiPerteEnergie = null, System.Action uiPerteVie = null,
        System.Action gameOver = null, List<string> ListLevelDone=null, List<string> ChestList = null, List<string> CollectableList = null,int hatCount=0,int convCount=0)
    {
        this._vie = vie;
        this._energie = energie;
        this._score = score;
        this._volumeGeneral = volumeGeneral;
        this._volumeMusique = volumeMusique;
        this._volumeEffet = volumeEffet;
        this.UIPerteEnergie += uiPerteEnergie;
        this.UIPerteVie += uiPerteVie;
        this.Gameover += gameOver;

        this._chestOpenList = new List<string>();
        if (ChestList != null)
                    this._chestOpenList = ChestList;

        this._listLevelDone = new List<string>();
        if (ListLevelDone != null)
            this._listLevelDone = ListLevelDone;

        this._listCollectable = new List<string>();
        if (CollectableList != null)
            this._listCollectable = CollectableList;

        this._hatCount = hatCount;

        this._convCount = convCount;
    }

    /// <summary>
    /// Diminue l'énergie du personnage
    /// </summary>
    /// <param name="perte">Niveau de perte (par défaut 1)</param>
    public void DecrEnergie(int perte = 1)
    {
        this._energie -= perte;
        this.UIPerteEnergie();
        if (this._energie <= 0)
        {
            this.DecrVie();
        }
    }

    /// <summary>
    /// Permet d'augmenter le nombre de chapeaux collectés
    /// </summary>
    public void IncrHat(int gain = 1)
    {
        this._hatCount += gain;

    }

    /// <summary>
    /// Permet d'augmenter le nombre de conventions collectés
    /// </summary>
    public void IncrConv(int gain = 1)
    {
        this._convCount += gain;
    }

    /// <summary>
    /// Permet de réduire la vie d'un personnage
    /// </summary>
    public void DecrVie()
    {
        this._vie--;
        this.UIPerteVie();
        if (this._vie <= 0)
            this.Gameover();
        else
        {
            this.IncrEnergie(MAX_ENERGIE);
            GameManager.Instance.RechargerNiveau();
        }
    }

    /// <summary>
    /// Permet d'augmenter l'énergie jusqu'à MAX_ENERGIE
    /// </summary>
    /// <param name="gain">Gain d'augmentation</param>
    public void IncrEnergie(int gain)
    {
        this._energie += gain;
        if (this._energie > MAX_ENERGIE)
        {
            this._energie = 1;
            this.IncrVie();
        }
        
        this.UIPerteEnergie();
    }

    /// <summary>
    /// Permet d'augmenter la vie
    /// </summary>
    /// <param name="gain">Gain d'augmentation</param>
    public void IncrVie(int gain = 1)
    {
        this._vie += gain;
        this.UIPerteVie();
    }

    /// <summary>
    /// Augmente le score du joueur
    /// </summary>
    /// <param name="gain">Point gagné</param>
    public void IncrScore(int gain = 1)
    {
        this._score += gain;
        Debug.Log("gain de point");
    }

    /// <summary>
    /// Ajoute le nom du coffre à la liste
    /// </summary>
    /// <param name="nom">Nom du coffre à ajouter</param>
    public void AjouterCoffreOuvert(string nom)
    {
        this._chestOpenList.Add(nom);
    }

    /// <summary>
    /// Ajoute le niveau terminé à la liste
    /// </summary>
    /// <param name="nom">Nom du niveau</param>
    public void AjouterNiveauTermine(string nom)
    {
        this._listLevelDone.Add(nom);
    }

    /// <summary>
    /// Permet de vérifier si le niveau est terminé
    /// </summary>
    public bool AvoirNiveauTermine(string nom)
    {
        return this._listLevelDone.Contains(nom);
    }

    /// <summary>
    /// Permet d'ajouter un nom de collectable à liste de collectable ramassés
    /// </summary>
    public void AjouterCollectable(string nom)
    {
        this._listCollectable.Add(nom);
    }

    /// <summary>
    /// Permet de terminé si le collectable a déjà été ramassé
    /// </summary>
    public bool AvoirCollected(string nom)
    {
        return this._listCollectable.Contains(nom);
    }
    /// <summary>
    /// Détermine si le coffre est contenu dans la liste
    /// des coffres ouverts
    /// </summary>
    /// <param name="nom">Nom du coffre à vérifier</param>
    /// <returns>true si le coffre est ouvert, false sinon</returns>
    public bool AvoirOuvertureCoffre(string nom)
    {
        return this._chestOpenList.Contains(nom);
    }
    /// <summary>
    /// Reset si le gameover dans un niveau
    /// </summary>
    /*public void resetForGameOver(int vie = 1, int energie = 2, int score = 0, List<string> ListLevelDone = null, List<string> ChestList = null, List<string> CollectableList = null, int hatCount = 0, int convCount = 0)
    {
        this._vie = vie;
        this._energie = energie;
        this._listLevelDone = ListLevelDone;
        this._chestOpenList = ChestList;
        this._listCollectable = CollectableList;
        this._hatCount = hatCount;
        this._convCount = convCount;
    }*/
}
