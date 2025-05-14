using UnityEngine;

namespace Subvrsive.Combat.Pool
{
    public class AutoReturnToPool : MonoBehaviour
    {
        [Tooltip("Time in seconds after which the object will be returned to the pool.")]
        public float returnDelay = 2f;

        [Tooltip("Tag used in the object pool to identify this object.")]
        public string poolTag;

        private void OnEnable()
        {
            CancelInvoke();
            Invoke(nameof(ReturnToPool), returnDelay);
        }

        private void ReturnToPool()
        {
            if (!string.IsNullOrEmpty(poolTag))
            {
                ObjectPool.Instance.ReturnToPool(poolTag, gameObject);
            }
            else
            {
                ObjectPool.Instance.ReturnToPool(gameObject.name, gameObject);
            }
        }

        private void OnDisable()
        {
            CancelInvoke();
        }
    }
}

