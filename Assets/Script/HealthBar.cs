using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform bar;
    public GameManagerScript gameManager;
    private bool isdead;
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<RectTransform>();
        SetSize(Health.totalHealth);
    }

    // Update is called once per frame
    public void Damage (float damage)
    {
        if((Health.totalHealth -= damage) >= 0f)
        {
            Health.totalHealth -= damage;
        }
        else
        {
            Health.totalHealth = 0f;
            gameManager.gameOver();
        }
        SetSize(Health.totalHealth);
    }

    public void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
}
