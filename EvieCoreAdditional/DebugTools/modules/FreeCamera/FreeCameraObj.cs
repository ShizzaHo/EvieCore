using UnityEngine;

namespace Eviecore
{
    public class FreeCameraObj : MonoBehaviour
    {
        public float moveSpeed = 10f; // Скорость перемещения
        public float sprintMultiplier = 2f; // Множитель скорости при беге
        public float lookSpeed = 2f; // Скорость вращения камеры
        public float acceleration = 5f; // Ускорение для сглаживания движения

        private Vector3 velocity; // Текущая скорость движения
        private Vector3 targetVelocity; // Желаемая скорость движения
        private float verticalAngle = 0f; // Угол поворота вверх/вниз

        void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        void HandleMovement()
        {
            // Получение ввода
            float moveX = Input.GetAxis("Horizontal"); // A/D или стрелки влево/вправо
            float moveZ = Input.GetAxis("Vertical"); // W/S или стрелки вперёд/назад
            float moveY = 0;

            if (Input.GetKey(KeyCode.E)) moveY += 1; // Подъём (например, клавиша E)
            if (Input.GetKey(KeyCode.Q)) moveY -= 1; // Спуск (например, клавиша Q)

            // Расчёт желаемой скорости
            float currentSpeed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);
            targetVelocity = (transform.right * moveX + transform.forward * moveZ + transform.up * moveY).normalized * currentSpeed;

            // Интерполяция для плавности движения
            velocity = Vector3.Lerp(velocity, targetVelocity, Time.deltaTime * acceleration);

            // Перемещение камеры
            transform.position += velocity * Time.deltaTime;
        }

        void HandleRotation()
        {
            // Получение ввода мыши
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Поворот вокруг оси Y (вправо/влево)
            transform.Rotate(Vector3.up * mouseX * lookSpeed, Space.World);

            // Поворот вокруг оси X (вверх/вниз)
            verticalAngle -= mouseY * lookSpeed;
            verticalAngle = Mathf.Clamp(verticalAngle, -90f, 90f); // Ограничение угла поворота вверх/вниз

            // Применение поворота вверх/вниз
            Vector3 angles = transform.localEulerAngles;
            angles.x = verticalAngle;
            transform.localEulerAngles = angles;
        }
    }
}