using _Scripts.Domin.Event;
using Domin.Event;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Domin.Enitiy
{
    public class GopherController : MonoBehaviour
    {
        private readonly float _existTime = 0.8f;
        private float _timer;
        private Animator _animator;
        private Collider2D _collider;
        private Camera _camera;
        private static readonly int HasHurt = Animator.StringToHash("hasHurt");
        private static readonly int HasTimeOut = Animator.StringToHash("hasTimeOut");
        public Hole Hole { get; set; }

        private void Update()
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && WasTapped())
            {
                var animator = GetAnimator();
                animator.SetBool(HasHurt, true);
            }

            if (GameSystem.Instance.GameState == GameState.GamePlaying)
            {
                _timer += Time.deltaTime;

                if (_timer >= _existTime)
                {
                    _timer = 0;

                    var animator = GetAnimator();
                    animator.SetBool(HasTimeOut, true);
                }
            }
        }

        private bool WasTapped()
        {
            var collider2D = GetCollider();
            if (!collider2D || !collider2D.enabled)
            {
                return false;
            }

            var worldPosition = GetCamera().ScreenToWorldPoint(Mouse.current.position.ReadValue());
            return collider2D.OverlapPoint(worldPosition);
        }

        private Animator GetAnimator() => _animator ? _animator : (_animator = GetComponent<Animator>());
        private Collider2D GetCollider() => _collider ? _collider : (_collider = GetComponent<Collider2D>());
        private Camera GetCamera() => _camera ? _camera : (_camera = Camera.main);
    }
}