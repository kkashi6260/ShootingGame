using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData      stageData;
    [SerializeField]
    private GameObject     enemyPrefab;
    [SerializeField]
    private GameObject     enemyHPSliderPrefab;
    [SerializeField]
    private Transform      canvasTransform;
    [SerializeField]
    private float          spawnTime;

    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            //x ��ġ�� ���������� ũ�� ���� ������ ������ ���� ����
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            // �� ������ġ
            Vector3 positon = new Vector3(positionX, stageData.LimitMax.y+1.0f, 0.0f);
            // ��ĳ���� ����
            GameObject enemyClone = Instantiate(enemyPrefab, positon, Quaternion.identity);
            // �� ü���� ��Ÿ���� Slider UI ���� �� ����
            SpawnEnemyHPSlider(enemyClone);

            // spawnTime ��ŭ ���
            yield return new WaitForSeconds(spawnTime);
        } 
    }
    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        // �� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        //Slider UI ������Ʈ�� parent("Canvas" ������Ʈ)�� �ڽ����� �����Ѵ�
        //Tip. UI�� ĵ������ �ڽ� ������Ʈ�� �����Ǿ� �־�� ȭ�鿡 ���δ�
        sliderClone.transform.SetParent(canvasTransform);
        // ���� �������� �ٲ� ũ�⸦ �ٽ� (1, 1, 1)�� �����Ѵ�
        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositonAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHpViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
