using UnityEngine;

public class SliderPositonAutoSetter : MonoBehaviour
{

    [SerializeField]
    private Vector3         distance = Vector3.down * 35.0f;
    private Transform       targetTransform;
    private RectTransform rectTransform; 

    public void Setup(Transform target)
    {
        //Slider UI가 쫒아다닐 targett설정
        targetTransform = target;
        //RectTransform 컴포넌트 정보 얻어오기
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // 적이 파괴되어 쫒아다닐 대상이 사라지면 SliderUI도 삭제
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = screenPosition + distance;
    }  

}
