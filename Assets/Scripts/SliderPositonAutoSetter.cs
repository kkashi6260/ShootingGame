using UnityEngine;

public class SliderPositonAutoSetter : MonoBehaviour
{

    [SerializeField]
    private Vector3         distance = Vector3.down * 35.0f;
    private Transform       targetTransform;
    private RectTransform rectTransform; 

    public void Setup(Transform target)
    {
        //Slider UI�� �i�ƴٴ� targett����
        targetTransform = target;
        //RectTransform ������Ʈ ���� ������
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // ���� �ı��Ǿ� �i�ƴٴ� ����� ������� SliderUI�� ����
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = screenPosition + distance;
    }  

}
