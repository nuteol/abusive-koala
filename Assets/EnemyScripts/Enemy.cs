using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{

    public abstract bool TakeDamage(int damage);
    public abstract void Death();
}
