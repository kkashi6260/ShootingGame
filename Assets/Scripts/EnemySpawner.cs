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
    private BGMController  bgmController;
    [SerializeField]
    private GameObject     textBossWarning;
    [SerializeField]
    private GameObject     panelBossHP;
    [SerializeField]
    private GameObject     boss;
    [SerializeField]
    private float          spawnTime;
    [SerializeField]
    private int            maxEnemyCount = 100;

    private void Awake()
    {
        textBossWarning.SetActive(false);
        panelBossHP.SetActive(false);
        boss.SetActive(false);
        StartCoroutine("SpawnEnemy");
    }
    private IEnumerator SpawnEnemy()
    {
        int currentEnemyCount = 0;
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

            currentEnemyCount ++;
            if (currentEnemyCount == maxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

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
    private IEnumerator SpawnBoss()
    {
        bgmController.ChangeBGM(BGMType.Boss);
        textBossWarning.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        textBossWarning.SetActive(false);
        panelBossHP.SetActive(true);
        boss.SetActive(true);
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
