using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _fill;

    /// <summary>
    /// Set HealthBar value
    /// </summary>
    /// <param name="value">min = 0; max = 100</param>
    public void SetFloat(float value)
    {
        _fill.fillAmount = value / 100f;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}