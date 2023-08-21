using System.Collections;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    public float shakeForce = 10f;          // ��鸲�� ������ ���� ũ��
    public float shakeDuration = 1.0f;      // ��鸲 ���� �ð�
    public float shakeFrequency = 0.1f;     // ��鸲 ���ļ�

    private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        while (true)
        {
            float elapsedTime = 0f;
            while (elapsedTime < shakeDuration)
            {
                Vector3 randomForce = Random.insideUnitSphere * shakeForce;
                rb.AddForce(randomForce);

                float rotationAmount = Mathf.Sin(elapsedTime * shakeFrequency) * shakeForce * 0.1f;
                transform.rotation = originalRotation * Quaternion.Euler(0f, 0f, rotationAmount);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rb.velocity = Vector3.zero;
            transform.rotation = originalRotation;

            yield return new WaitForSeconds(Random.Range(0.5f, 3f)); // Wait before next shake
        }
    }
}