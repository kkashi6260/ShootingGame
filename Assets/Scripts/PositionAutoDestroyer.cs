using UnityEngine;

public class PositionAutoDestroyer : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    private float desetroyWeight = 2.0f;

    private void LateUpdate()
    {
        if( transform.position.y < stageData.LimitMin.y - desetroyWeight ||
            transform.position.y > stageData.LimitMax.y + desetroyWeight ||
            transform.position.x < stageData.LimitMin.y - desetroyWeight ||
            transform.position.x > stageData.LimitMax.y + desetroyWeight )
        {
            Destroy(gameObject);
        }
    }
}
