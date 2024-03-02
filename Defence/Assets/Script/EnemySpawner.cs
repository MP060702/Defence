using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WaveInfo", menuName = "Scriptable Object/WaveInfo")]

public class WaveInfo : ScriptableObject
{
    public int wavenum = 0;
    public int EnemyCount = 0;
    public int UpgradedEnmeyCount = 0;
}

public class EnemySpawner : MonoBehaviour //MomoBehaviou을 상속받는 클래스 EnmeySpanwer
{   
    
    public Transform SpawnPosition; // Enemy 오브젝트를 생성할 위치를 정하는 Transform 타입의 변수
    public GameObject[] WayPoints;  // 방향을 바꿔줄떄 사용되는 게임오브젝트로서 게임오브젝트 타입의 배열
    public WaveInfo[] WaveInfo;
    public GameObject Enemy;
    public GameObject UpgradedEnemy;
    public Text WaveNum;
    public float SpawnCycleTime = 1f; //실수 타입의 적의 소환 사이클의 딜레이 시간을 담당하는 변수
    private int waveIndex;

    private void Start() // 씬이 시작될때 작동되는 함수
    {
        Activate(); // 함수 Active 호출 
    }

    public void Activate() // 함수 Active 선언 및 정의
    {
        StartCoroutine(SpawnEnemy()); //StartCoruntine 함수를 통한 SpawnEnemy 코루틴을 작동시킨다
    }

    public void DeActivate() // 함수 DeActive 선언 및 정의
    {
        StopCoroutine(SpawnEnemy()); // StopCoruntine 함수를 통해 작동중인 코루틴 SpawnEnemy를 중단시킴
    }

    IEnumerator SpawnEnemy() // SpawnEnemy 코루틴을 정의 및 선언
    {
        while (waveIndex < WaveInfo.Length) // _bCanSpawn 논리형이 True일 때만 작동하는 while 반복문,  게임 내에서 지속적으로 Enemy를 스폰해주기 위해 반복하는 역활을 한다
        {
            WaveInfo currentWave = WaveInfo[waveIndex];
            
            for (int i = 0; i < currentWave.EnemyCount; i++)
            {
                yield return new WaitForSeconds(SpawnCycleTime);
                
                 
                GameObject EnemyInst = Instantiate(Enemy, SpawnPosition.position, Quaternion.identity);

                Enemy EnemyCom = EnemyInst.GetComponent<Enemy>();

                if (EnemyCom)
                {
                    EnemyCom.WayPoints = WayPoints;
                }
            }

            for(int j = 0; j < currentWave.UpgradedEnmeyCount; j++)
            {
                yield return new WaitForSeconds(SpawnCycleTime);


                GameObject EnemyInst = Instantiate(UpgradedEnemy, SpawnPosition.position, Quaternion.identity);

                Enemy EnemyCom = EnemyInst.GetComponent<Enemy>();

                if (EnemyCom)
                {
                    EnemyCom.WayPoints = WayPoints;
                }
            }

            yield return new WaitForSeconds(4f);
            WaveNum.text = $"Wave {waveIndex + 2}";
            WaveNum.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);
            WaveNum.gameObject.SetActive(false);

            waveIndex++;
        }
    }


}