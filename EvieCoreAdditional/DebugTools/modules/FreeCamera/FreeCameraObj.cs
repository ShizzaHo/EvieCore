using UnityEngine;

namespace Eviecore
{
    public class FreeCameraObj : MonoBehaviour
    {
        public float moveSpeed = 10f; // �������� �����������
        public float sprintMultiplier = 2f; // ��������� �������� ��� ����
        public float lookSpeed = 2f; // �������� �������� ������
        public float acceleration = 5f; // ��������� ��� ����������� ��������

        private Vector3 velocity; // ������� �������� ��������
        private Vector3 targetVelocity; // �������� �������� ��������
        private float verticalAngle = 0f; // ���� �������� �����/����

        void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        void HandleMovement()
        {
            // ��������� �����
            float moveX = Input.GetAxis("Horizontal"); // A/D ��� ������� �����/������
            float moveZ = Input.GetAxis("Vertical"); // W/S ��� ������� �����/�����
            float moveY = 0;

            if (Input.GetKey(KeyCode.E)) moveY += 1; // ������ (��������, ������� E)
            if (Input.GetKey(KeyCode.Q)) moveY -= 1; // ����� (��������, ������� Q)

            // ������ �������� ��������
            float currentSpeed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);
            targetVelocity = (transform.right * moveX + transform.forward * moveZ + transform.up * moveY).normalized * currentSpeed;

            // ������������ ��� ��������� ��������
            velocity = Vector3.Lerp(velocity, targetVelocity, Time.deltaTime * acceleration);

            // ����������� ������
            transform.position += velocity * Time.deltaTime;
        }

        void HandleRotation()
        {
            // ��������� ����� ����
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // ������� ������ ��� Y (������/�����)
            transform.Rotate(Vector3.up * mouseX * lookSpeed, Space.World);

            // ������� ������ ��� X (�����/����)
            verticalAngle -= mouseY * lookSpeed;
            verticalAngle = Mathf.Clamp(verticalAngle, -90f, 90f); // ����������� ���� �������� �����/����

            // ���������� �������� �����/����
            Vector3 angles = transform.localEulerAngles;
            angles.x = verticalAngle;
            transform.localEulerAngles = angles;
        }
    }
}