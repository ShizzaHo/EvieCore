using UnityEngine;

namespace Eviecore
{
    public class MovementCameraAnimation : MonoBehaviour, EvieCoreUpdate
    {
        public Transform camera;
        private PlayerCamera playerCamera;

        [Header("Movement Tilt Settings")]
        [Tooltip("ћаксимальное отклонение камеры при движении влево/вправ.")]
        public float maxTiltAngle = 5f;

        [Tooltip("—корость возврата к нулевому положению.")]
        public float returnSpeed = 2f;

        private float targetTiltAngle = 0f;  // ÷елевой угол наклона
        private float currentTiltAngle = 0f; // “екущий угол наклона

        private void Start()
        {
            // ѕолучаем ссылку на PlayerCamera, чтобы работать с движением
            playerCamera = GetComponent<PlayerCamera>();
        }

        public void OnUpdate()
        {
            // ѕровер€ем движение игрока по оси X
            float horizontalMovement = Input.GetAxis("Horizontal");

            // ≈сли движение есть, наклон€ем камеру влево/вправ
            if (horizontalMovement != 0f)
            {
                targetTiltAngle = maxTiltAngle * horizontalMovement; // Ќаправление наклона зависит от движени€
            }
            else
            {
                // ≈сли движение прекратилось, плавно возвращаем камеру в исходное положение
                targetTiltAngle = 0f;
            }

            // ѕлавно возвращаем угол наклона камеры к целевому значению
            currentTiltAngle = Mathf.Lerp(currentTiltAngle, targetTiltAngle, returnSpeed * Time.deltaTime);

            // ѕримен€ем наклон к камере
            camera.localRotation = Quaternion.Euler(currentTiltAngle, 0f, 0f); // “олько по оси X
        }

        private void OnDestroy()
        {
            UpdateManager.Instance.Unregister(this);
        }
    }
}