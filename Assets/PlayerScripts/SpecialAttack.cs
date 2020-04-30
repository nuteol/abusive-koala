using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttack : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSpecial(int spec)
    {
        slider.value = spec;
    }
    public void AddSpecial(int spec)
    {
        slider.value += spec;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxSpecial(int spec)
    {
        slider.maxValue = spec;
        slider.value = spec;

        fill.color = gradient.Evaluate(1f);
    }
    public float GetSpecAmount()
    {
        return slider.value;
    }
}
