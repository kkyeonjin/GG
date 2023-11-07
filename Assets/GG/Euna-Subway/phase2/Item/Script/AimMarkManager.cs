using UnityEngine;
using System.Collections.Generic;

public class AimMarkManager : MonoBehaviour
{
    public LayerMask targetLayer; // 타겟으로 삼을 레이어를 설정할 변수
    private List<Transform> targets; // 타겟 오브젝트들의 Transform을 저장할 리스트

    private Dictionary<Transform, GameObject> targetMarkers; // 타겟과 표적 마크를 연결할 딕셔너리
    public GameObject markerPrefab; // 표적 마크 프리팹을 설정할 변수

    private Camera mainCamera; // 메인 카메라를 저장할 변수

    void Start()
    {
        mainCamera = Camera.main; // 메인 카메라를 가져옴
        targets = new List<Transform>();
    }

    void Update()
    {
        if (GameMgr.Instance.m_LocalPlayer.m_bIsThrow)
        {
            // 스크린 좌표에서 레이를 쏨
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // 레이캐스트를 수행하여 targetLayer에 해당하는 오브젝트를 찾음
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
            {
                // 충돌한 오브젝트의 Transform을 targets 리스트에 추가
                Transform targetTransform = hit.transform;
                if (!targets.Contains(targetTransform))
                {
                    targets.Add(targetTransform);
                    CreateTargetMarker(targetTransform); // 새로운 타겟에 대한 표적 마크 생성
                }


                // 타겟의 위치에 표적 마크를 업데이트
                foreach (Transform target in targets)
                {
                    GameObject marker;
                    if (targetMarkers.TryGetValue(target, out marker))
                    {
                        // 타겟의 위치를 월드 좌표에서 스크린 좌표로 변환
                        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(target.position);

                        // 표적 마크의 위치를 업데이트
                        marker.transform.position = targetScreenPos;
                    }
                }
            }
            else
            {
                // 레이에 충돌한 오브젝트가 없으면 targets 리스트를 초기화
                targets.Clear();
            }
        }

    }

    // 타겟에 대한 표적 마크를 생성하는 함수
    void CreateTargetMarker(Transform target)
    {
        // 표적 마크 프리팹으로부터 새로운 표적 마크 생성
        GameObject marker = Instantiate(markerPrefab, Vector3.zero, Quaternion.identity);

        // 생성된 표적 마크를 타겟과 연결
        targetMarkers[target] = marker;
    }
}