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
            //x 위치는 스테이지의 크기 범위 내에서 임의의 값을 선택
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            // 적 생성위치
            Vector3 positon = new Vector3(positionX, stageData.LimitMax.y+1.0f, 0.0f);
            // 적캐릭터 생성
            GameObject enemyClone = Instantiate(enemyPrefab, positon, Quaternion.identity);
            // 적 체력을 나타내는 Slider UI 생성 및 설정
            SpawnEnemyHPSlider(enemyClone);

            currentEnemyCount ++;
            if (currentEnemyCount == maxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            // spawnTime 만큼 대기
            yield return new WaitForSeconds(spawnTime);
        } 
    }
    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        // 적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        //Slider UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정한다
        //Tip. UI는 캔버스의 자식 오브젝트로 설정되어 있어야 화면에 보인다
        sliderClone.transform.SetParent(canvasTransform);
        // 계층 설정으로 바꾼 크기를 다시 (1, 1, 1)로 설정한다
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
