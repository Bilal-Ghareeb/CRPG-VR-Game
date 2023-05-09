using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private float healthPoints = 100;
    private float maxHealthPoints = 100;
    private int damage = 10;

    public void init(int damage, float maxHealth) {
        this.damage = damage;
        maxHealthPoints = maxHealth;
        healthPoints = maxHealth;
    }

    public float getHealth() {
        return healthPoints;
    }
    public void reset() {
        healthPoints = maxHealthPoints;
    }

    public void takeDamage(int damage) {
        healthPoints -= damage;
    }

    public bool isDead() {
        return healthPoints <= 0;
    }
}
