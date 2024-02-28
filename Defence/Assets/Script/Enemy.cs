using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour // MonoBehaviour을 상속받는 Enemy 클래스
{
    private GameObject _currentWayPoint; // GameObject 타입의 변수를 선언, 현재의 방향 포인트로서의 역활을 한다
    private int _wayPointCount = 0; // int 타입의 변수를 선언 및 정의,  방향포인트의 개수를 새는 역활을 한다.
    private Vector3 _moveDirection = Vector3.zero; // Vector3 타입의 변수를 선언 및 정의,  해당 클래스를 갖고 있는 오브젝트의 이동방향 역활을 하는 변수
    private int _hp = 5; // int 타입의 변수를 선언 밎 정의, 해당 Enemy의 체력값 역활을 한다

    [HideInInspector] // 인스펙터 창에서 변수들을 보이지 않게 하기 위해 사용
    public GameObject[] WayPoints; // 방향을 바꿔줄떄 사용되는 게임오브젝트로서 게임오브젝트 타입의 배열
    public int MaxHp = 5; // Enemy의 최대 체력값으로서의 역활을 하는 int 타입의 변수
    public float MoveSpeed = 10; // Enemy의 이동속도 값의 역활을 하는 float 타입의 변수
    public int StealCoin = 100; // Enemy를 처치 했을때 얻는 코인의 값 역활을 하는 int 타입의 변수를 선언 밎 정의
    public int Damage = 1; // Enemy가 주는 데미지 값이 역활을 하는 int 타입의 변수 

    private void Start() // 씬이 시작됬을때 작동되는 함수
    {
        _hp = MaxHp; // 체력값에 최대 체력값을 넣어준다
        _currentWayPoint = WayPoints[0]; // 현재 방향 포인트 값에 WayPoints 배열의 0번째 값을 넣어준다
        SetRotationByDirection(); // SetRotationByDirection 함수를 호출한다
    }

    private void Update() // 매 프레임마다 작동되는 함수
    {
        transform.position += _moveDirection * MoveSpeed * Time.deltaTime; // 오브젝트에 위치값에 (이동방향 * 이동속도 * 시간)값을 더해줘 오브젝트가 움직일 수 있도록 해준다
        Vector3 TargetPosition = _currentWayPoint.transform.position; // Vector3 변수에 현재 방향점의 위치값을 넣는다
        TargetPosition.y = transform.position.y; // TargetPostion.y 값에 현재 Enemy 클래스 오브젝트의 위치 y값을 넣는다

        if (Vector3.Distance(transform.position, TargetPosition) <= 0.02f) // Vector3.Distance를 통해 현재 오브젝트의 위치값과, TargetPosition의 위치와의 거리를 계산하여 0.02f보다 작거나 같다면 아래 코드를 작동
        {
            if (_wayPointCount >= WayPoints.Length - 1) // 만약 wayPointCount 방향점 계수 값이, WayPoints 배열의 길이 값에 -1을 더한 값보다 적다 크거나 갔다면 아래 코드를 작동
            {
                GameManager.Inst.playerCharacter.Damaged(Damage);
                Destroy(gameObject); // 자기 자신 오브젝트를 삭제
                return; // 반환한다
            }

            _wayPointCount = Mathf.Clamp(_wayPointCount + 1, 0, WayPoints.Length); //방향점 카운트 값에 Mathf.Clamp 함수를 통해 방향점 값에 1을 더해주고 최소값을 0으로 최댓값을 WayPoints배열의 길이값으로 하여 해당 값 안을 벗어나지 않도록 함
            _currentWayPoint = WayPoints[_wayPointCount]; // 현재 방향점 값에 WayPoints배열의 _WayPointCount번쨰 칸 배열의 값을 넣음

            SetRotationByDirection(); // SetRotationByDirection 함수를 호출한다
        }
    }

    private void SetRotationByDirection() // SetRotationByDirection 함수를 선언 밎 정의,  해당 함수는 
    {
        _moveDirection = _currentWayPoint.transform.position - transform.position; // 이동방향을 현재방향점의 위치값에서 - 자신의 위치값을 뺀것으로 하며 자기 자신을 이동시킨다
        _moveDirection.y = 0; // _moveDirection의 y값을 0으로 해준다
        _moveDirection.Normalize(); // 값을 Normalize 함수를 통해 정규화 시켜준다

        transform.rotation = Quaternion.LookRotation(_moveDirection, Vector3.up); // 현재 오브젝트의 회전값에 LookRotation함수를 통해 매개변수 _moveDirection을 회전시킬 방향 백터로 하고 방향 벡터를 바라보고 있을 떄의 위를 지정한다
    }

    private void OnTriggerEnter(Collider other) // Trigger가 닿았을떄 작동되는 함수 
    {
        if (other.CompareTag("Projectile")) // 만약 "Projectile"태그인 collider에 닿았다면 아래 코드 작동
        {
            _hp = Mathf.Clamp(_hp - 1, 0, MaxHp); // 체력값을 최소값 0과 최댓값 최대체력값에 벗어나지 않는 선에서 체력을 -1 한다

            if (_hp <= 0) // 만약 체력이 0보다 작거나 같다면
            {
                gameObject.SetActive(false); // 게임 오브젝트를 SetActive(false)하여 씬에서 보이지 않도록 하고 작동을 멈춘다
                GameManager.Inst.EnemyDead(StealCoin); // 게임 메니저에서 EnemyDead함수를 작동시키고 매개면수값인 stealCoin값을 얻는다 
                Destroy(gameObject); //해당 오브젝트를 삭제
            }
        }
    }
}