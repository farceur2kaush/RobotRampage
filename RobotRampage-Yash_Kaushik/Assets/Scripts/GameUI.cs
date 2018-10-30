using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text armorText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text pickupText;
    [SerializeField]
    private Text waveText;
    [SerializeField]
    private Text enemyText;
    [SerializeField]
    private Text waveClearText;
    [SerializeField]
    private Text newWaveText;
    [SerializeField]
    Player player;

    [SerializeField]
    Sprite redReticle;
    [SerializeField]
    Sprite redReticleB;
    [SerializeField]
    Sprite yellowReticle;
    [SerializeField]
    Sprite blueReticle;
    [SerializeField]
    Image reticle;

    public void UpdateReticle()
    {   // switch statement to change reticle everytime a new gun is selected
        switch (GunEquipper.activeWeaponType)
        {
            case Constants.Pistol:
                reticle.sprite = redReticle;
                break;
            case Constants.Shotgun:
                reticle.sprite = yellowReticle;
                break;
            case Constants.AssaultRifle:
                reticle.sprite = blueReticle;
                break;

            case Constants.Bazooka:
                reticle.sprite = redReticleB;
                break;

            default:
                return;
        }
    }

    // 1 player health and ammo text
    void Start()
    {
        SetArmorText(player.armor);
        SetHealthText(player.health);
    }

    // set text values
    public void SetArmorText(int armor)
    {
        armorText.text = "Armor: " + armor;
    }
    public void SetHealthText(int health)
    {
        healthText.text = "Health: " + health;
    }
    public void SetAmmoText(int ammo)
    {
        ammoText.text = "Ammo: " + ammo;
    }
    public void SetScoreText(int score)
    {
        scoreText.text = "" + score;
    }
    public void SetWaveText(int time)
    {
        waveText.text = "Next Wave: " + time;
    }
    public void SetEnemyText(int enemies)
    {
        enemyText.text = "Enemies: " + enemies;
    }

    // 1 Appear after waves and call the coroutine
    public void ShowWaveClearBonus()
    {
        waveClearText.GetComponent<Text>().enabled = true;
        StartCoroutine("hideWaveClearBonus");
    }
    //  coroutine hide the text after 4 seconds
    IEnumerator hideWaveClearBonus()
    {
        yield return new WaitForSeconds(4);
        waveClearText.GetComponent<Text>().enabled = false;
    }
    // 3    sets pickup text
    public void SetPickUpText(string text)
    {
        pickupText.GetComponent<Text>().enabled = true;
        pickupText.text = text;
        // Restart the Coroutine so it doesn’t end early
        StopCoroutine("hidePickupText");
        StartCoroutine("hidePickupText");
    }
    IEnumerator hidePickupText()
    {
        yield return new WaitForSeconds(4);
        pickupText.GetComponent<Text>().enabled = false;
    }
    // 5    new wave text appears and the coroutine to hide it is called
    public void ShowNewWaveText()
    {
        StartCoroutine("hideNewWaveText");
        newWaveText.GetComponent<Text>().enabled = true;
    }
    // 6    new wave text disappears in 4 seconds
    IEnumerator hideNewWaveText()
    {
        yield return new WaitForSeconds(4);
        newWaveText.GetComponent<Text>().enabled = false;
    }
}
