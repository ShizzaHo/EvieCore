using UnityEngine;

namespace Eviecore
{
    public class MovementCameraAnimation : MonoBehaviour, EvieCoreUpdate
    {
        public Transform camera;
        private PlayerCamera playerCamera;

        [Header("Movement Tilt Settings")]
        [Tooltip("������������ ���������� ������ ��� �������� �����/�����.")]
        public float maxTiltAngle = 5f;

        [Tooltip("�������� �������� � �������� ���������.")]
        public float returnSpeed = 2f;

        private float targetTiltAngle = 0f;  // ������� ���� �������
        private float currentTiltAngle = 0f; // ������� ���� �������

        private void Start()
        {
            // �������� ������ �� PlayerCamera, ����� �������� � ���������
            playerCamera = GetComponent<PlayerCamera>();
        }

        public void OnUpdate()
        {
            // ��������� �������� ������ �� ��� X
            float horizontalMovement = Input.GetAxis("Horizontal");

            // ���� �������� ����, ��������� ������ �����/�����
            if (horizontalMovement != 0f)
            {
                targetTiltAngle = maxTiltAngle * horizontalMovement; // ����������� ������� ������� �� ��������
            }
            else
            {
                // ���� �������� ������������, ������ ���������� ������ � �������� ���������
                targetTiltAngle = 0f;
            }

            // ������ ���������� ���� ������� ������ � �������� ��������
            currentTiltAngle = Mathf.Lerp(currentTiltAngle, targetTiltAngle, returnSpeed * Time.deltaTime);

            // ��������� ������ � ������
            camera.localRotation = Quaternion.Euler(currentTiltAngle, 0f, 0f); // ������ �� ��� X
        }

        private void OnDestroy()
        {
            UpdateManager.Instance.Unregister(this);
        }
    }
}