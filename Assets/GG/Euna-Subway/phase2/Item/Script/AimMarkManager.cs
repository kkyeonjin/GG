using UnityEngine;
using System.Collections.Generic;

public class AimMarkManager : MonoBehaviour
{
    public LayerMask targetLayer; // Ÿ������ ���� ���̾ ������ ����
    private List<Transform> targets; // Ÿ�� ������Ʈ���� Transform�� ������ ����Ʈ

    private Dictionary<Transform, GameObject> targetMarkers; // Ÿ�ٰ� ǥ�� ��ũ�� ������ ��ųʸ�
    public GameObject markerPrefab; // ǥ�� ��ũ �������� ������ ����

    private Camera mainCamera; // ���� ī�޶� ������ ����

    void Start()
    {
        mainCamera = Camera.main; // ���� ī�޶� ������
        targets = new List<Transform>();
    }

    void Update()
    {
        if (GameMgr.Instance.m_LocalPlayer.m_bIsThrow)
        {
            // ��ũ�� ��ǥ���� ���̸� ��
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // ����ĳ��Ʈ�� �����Ͽ� targetLayer�� �ش��ϴ� ������Ʈ�� ã��
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
            {
                // �浹�� ������Ʈ�� Transform�� targets ����Ʈ�� �߰�
                Transform targetTransform = hit.transform;
                if (!targets.Contains(targetTransform))
                {
                    targets.Add(targetTransform);
                    CreateTargetMarker(targetTransform); // ���ο� Ÿ�ٿ� ���� ǥ�� ��ũ ����
                }


                // Ÿ���� ��ġ�� ǥ�� ��ũ�� ������Ʈ
                foreach (Transform target in targets)
                {
                    GameObject marker;
                    if (targetMarkers.TryGetValue(target, out marker))
                    {
                        // Ÿ���� ��ġ�� ���� ��ǥ���� ��ũ�� ��ǥ�� ��ȯ
                        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(target.position);

                        // ǥ�� ��ũ�� ��ġ�� ������Ʈ
                        marker.transform.position = targetScreenPos;
                    }
                }
            }
            else
            {
                // ���̿� �浹�� ������Ʈ�� ������ targets ����Ʈ�� �ʱ�ȭ
                targets.Clear();
            }
        }

    }

    // Ÿ�ٿ� ���� ǥ�� ��ũ�� �����ϴ� �Լ�
    void CreateTargetMarker(Transform target)
    {
        // ǥ�� ��ũ ���������κ��� ���ο� ǥ�� ��ũ ����
        GameObject marker = Instantiate(markerPrefab, Vector3.zero, Quaternion.identity);

        // ������ ǥ�� ��ũ�� Ÿ�ٰ� ����
        targetMarkers[target] = marker;
    }
}