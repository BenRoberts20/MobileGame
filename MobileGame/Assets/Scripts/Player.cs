using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private Vector2 movement;
    private int Level = 1;
    private int Exp = 0;
    private int ExpNeeded = 15;
    private int MaxHealth = 10;
    private int Health;
    private int Damage = 1;
    private int BonusHealth = 0;
    private int BonusDamage = 0;
    private int Fragments = 0;
    private int FragmentMultiplier = 1;
    private int ExpMultiplier = 1;
    private float speed = 1;
    private int Points = 0;
    public Slider ExpBar;
    public Slider HealthBar;
    public TMP_Text LevelText;
    public TMP_Text FragmentsText;
    public Ships ship;
    public GameObject ResultScreen;
    public Button retryButton;

    void Start()
    {
        GrabData();
        LoadHealth();
        UpdateUI();
    }

    //Uses Accelerometer to move.
    void Update()
    {
        //movement = new Vector2(Input.acceleration.x, Input.acceleration.y) / 10;
        Vector3 movement1 = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position += (movement1 * speed) * Time.deltaTime;
        //transform.Translate(movement * speed);
        PreventPlayerOffScreen();

        
    }

    private void TakeDamage(int DamageToTake, float intensity, float time)
    {
        Health -= DamageToTake;
        UpdateUI();
        CinemachineShake.Instance.ShakeCamera(intensity, time);
        if (Health <= 0)
        {
            LoadResults();
        }
    }

    private void LoadResults()
    {
        ResultScreen.SetActive(true);
        Debug.Log(ResultScreen.transform.Find("Info").GetComponent<TMP_Text>());
    }

    public void RestartGame()
    {
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().RestartWaves();
        LoadHealth();
        UpdateUI();
        ResultScreen.SetActive(false);
    }

    public int GetDamage()
    {
        return Damage;
    }

    public int GetHealth()
    {
        return MaxHealth;
    }
    public int GetLevel()
    {
        return Level;
    }
    public int GetExp()
    {
        return Exp;
    }
    public int GetPoints()
    {
        return Points;
    }

    public void IncreaseHealth(int Amount)
    {
        if (Points > 0)
        {
            BonusHealth += Amount;
            Health += Amount;
            MaxHealth += Amount;
            Points -= 1;
            PlayerPrefs.SetInt("Points", Points);
            PlayerPrefs.SetInt("Health", BonusHealth);
        }
    }

    public void IncreaseDamage(int Amount)
    {
        if (Points > 0)
        {
            BonusDamage += Amount;
            Damage += Amount;
            Points -= 1;
            PlayerPrefs.SetInt("Points", Points);
            PlayerPrefs.SetInt("Damage", BonusDamage);
        }
    }
    public void IncreaseFragmentsMultiplier(int Amount)
    {
        if (Points >= 100)
        {
            FragmentMultiplier += Amount;
            Points -= 100;
            PlayerPrefs.SetInt("Points", Points);
            PlayerPrefs.SetInt("FragmentMultiplier", FragmentMultiplier);
        }
    }

    public void IncreaseExpMultiplier(int Amount)
    {
        if (Points >= 100)
        {
            ExpMultiplier += Amount;
            Points -= 100;
            PlayerPrefs.SetInt("Points", Points);
            PlayerPrefs.SetInt("ExpMultiplier", ExpMultiplier);
        }
    }

    public void IncreaseFragments(int Amount)
    {
        Fragments += (Amount * FragmentMultiplier);
        PlayerPrefs.SetInt("Fragments", Fragments);
        FragmentsText.text = Fragments.ToString();
    }

    public void IncreaseExp(int Amount)
    {
        Exp += (Amount * ExpMultiplier);

        if (Exp > ExpNeeded)
        {
            Exp -= ExpNeeded;
            Level += 1;
            ExpNeeded = 20 * Level;
            Points += 3;
            PlayerPrefs.SetInt("Level", Level);
            PlayerPrefs.SetInt("ExpNeeded", ExpNeeded);
            PlayerPrefs.SetInt("Points", Points);
        }

        PlayerPrefs.SetInt("Exp", Exp);
        UpdateUI();
    }

    public void RemoveFragments(int amount)
    {
        Fragments -= amount;
        PlayerPrefs.SetInt("Fragments", Fragments);
        FragmentsText.text = Fragments.ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            TakeDamage(col.GetComponent<Enemy>().GetImpactDamage(), 7.5f, 0.1f);
        }
        if(col.tag == "EnemyProjectile")
        {
            TakeDamage(col.GetComponent<EnemyProjectiles>().GetEnemy().Damage, 3f, 0.1f);
            Destroy(col.gameObject);
        }
        if (col.tag == "Asteroid")
        {
            TakeDamage(2, 10f, 0.2f);
            StartCoroutine(PlayAnimThenDestroy(col.gameObject, 0.5f));
        }
    }

    private IEnumerator PlayAnimThenDestroy(GameObject obj, float time)
    {
        obj.GetComponent<MoveAsteroid>().canMove = false;
        obj.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

    private void GrabData()
    {
        if (PlayerPrefs.HasKey("Level")) Level = PlayerPrefs.GetInt("Level");
        if (PlayerPrefs.HasKey("Exp")) Exp = PlayerPrefs.GetInt("Exp");
        if (PlayerPrefs.HasKey("ExpNeeded")) ExpNeeded = PlayerPrefs.GetInt("ExpNeeded");
        if (PlayerPrefs.HasKey("Health")) BonusHealth = PlayerPrefs.GetInt("Health");
        if (PlayerPrefs.HasKey("Damage")) BonusDamage = PlayerPrefs.GetInt("Damage");
        if (PlayerPrefs.HasKey("Fragments")) Fragments = PlayerPrefs.GetInt("Fragments");
        if (PlayerPrefs.HasKey("ExpMultiplier")) ExpMultiplier = PlayerPrefs.GetInt("ExpMultiplier");
        if (PlayerPrefs.HasKey("FragmentMultiplier")) FragmentMultiplier = PlayerPrefs.GetInt("FragmentMultiplier");
        if (PlayerPrefs.HasKey("Points")) Points = PlayerPrefs.GetInt("Points");
        if (PlayerPrefs.HasKey("Ship"))
        {
            string s = PlayerPrefs.GetString("Ship");
            if (s == "Dominator") ship = GameObject.Find("ShipsSO").GetComponent<ShipsLibrary>().Dominator;
            if (s == "Starter Ship") ship = GameObject.Find("ShipsSO").GetComponent<ShipsLibrary>().StarterShip;
            if (s == "Speedster") ship = GameObject.Find("ShipsSO").GetComponent<ShipsLibrary>().Speedster;
        }
        Health = MaxHealth;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Level", Level);
        PlayerPrefs.SetInt("Exp", Exp);
        PlayerPrefs.SetInt("ExpNeeded", ExpNeeded);
        PlayerPrefs.SetInt("Health", BonusHealth);
        PlayerPrefs.SetInt("Damage", BonusDamage);
        PlayerPrefs.SetInt("Fragments", Fragments);
        PlayerPrefs.SetInt("ExpMultiplier", ExpMultiplier);
        PlayerPrefs.SetInt("FragmentMultiplier", FragmentMultiplier);
        PlayerPrefs.SetInt("Points", Points);
        PlayerPrefs.SetString("Ship", ship.Name);
    }

    private void UpdateUI()
    {
        LevelText.text = "Level: " + Level;
        FragmentsText.text = Fragments.ToString();
        ExpBar.maxValue = ExpNeeded;
        ExpBar.value = Exp;
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = Health;
    }

    public void LoadShipData(Ships Ship)
    {
        ship = Ship;
        MaxHealth = ship.Health + BonusHealth;
        Damage = ship.Damage + BonusDamage;
        speed = ship.Speed;
        if (ship.animator)
        {
            this.gameObject.GetComponent<Animator>().runtimeAnimatorController = ship.animator;
        }
        else if(ship.animator == null)
        {
            print("this was null");
            this.gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
        }
        this.gameObject.GetComponent<SpriteRenderer>().sprite = ship.sprite;

    }

    private void LoadHealth()
    {
        LoadShipData(ship);
        Health = MaxHealth;
    }

    //Boundaries to prevent player going off screen
    //Using Hard Values currently, but will be changing to the players screen size as the boundary in future.
    private void PreventPlayerOffScreen()
    {
        if (transform.position.y >= 0)
        {
            transform.position = new Vector2(transform.position.x, 0);
        }
        else if (transform.position.y <= -8)
        {
            transform.position = new Vector2(transform.position.x, -8);
        }

        if (transform.position.x >= 2)
        {
            transform.position = new Vector2(2, transform.position.y);
        }
        else if (transform.position.x <= -2)
        {
            transform.position = new Vector2(-2, transform.position.y);
        }

    }

    private void OnDestroy()
    {
        SaveData();
    }
}
