using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float health = 1f;
    private RectTransform healthBarTransform;
    private Animator[] dogAnimators;
    private float colorMin;
    private float currentColor;

    private void Awake()
    {
        colorMin = 166 / 255f;
        healthBarTransform = GetComponent<RectTransform>();
        dogAnimators = FindObjectsByType<Animator>(0);
    }
    private void Update()
    {


        if (health <= 0)
        {
            health = 0;
        }

        healthBarTransform.localScale = new Vector3(health, 1, 1);
        if (health < 1.0f && health != 0)
            health += 0.01f * Time.deltaTime;

        currentColor = health * colorMin;

        foreach (var animator in dogAnimators)
        {
            animator.GetComponent<RawImage>().color = new Color(colorMin, currentColor, currentColor, 1);


        }

    }
    public void SetMaxHealth(float maxHp)
	{
        healthBarTransform.localScale = new Vector3(maxHp, 1, 1);
    }

    public void SetHealth(float damage)
	{
        health -= damage;
    }



}
