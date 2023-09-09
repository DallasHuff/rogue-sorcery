using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private FloatReference Variable, Max;
    [SerializeField] private Slider slider;

    public void Start()
    {
        slider.maxValue = Max.Value;
        slider.value = Max.Value;
    }

    public void Update()
    {
        slider.maxValue = Max.Value;
        slider.value = Variable.Value;
    }
}
