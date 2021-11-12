using UnityEngine;
using UnityEngine.UI; 

public class PlayerHPViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerHP     playerHP;
    private Slider      sliderHp;

    private void Awake()
    {
        sliderHp = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderHp.value = playerHP.CurrentHp / playerHP.MaxHP;
    }
}
