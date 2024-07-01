using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHealth(float hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
        gradient.Evaluate(1f);
    }
    public void SetHealth(float hp)
    {
        slider.value = hp;

        fill.color = gradient.Evaluate(slider.normalizedValue);

    }

        
}
