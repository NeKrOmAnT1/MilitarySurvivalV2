using UnityEngine;
using UnityEngine.UI;

public class ResourceBarUI : MonoBehaviour
{
   [SerializeField] private Image _barImage;

    public void SetBarAmount(float valueNormalized) => 
        _barImage.fillAmount = valueNormalized;
}
