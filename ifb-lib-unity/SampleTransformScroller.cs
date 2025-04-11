using UnityEngine;

namespace Assets.ifb_lib_unity
{
    public class SampleTransformScroller : MonoBehaviour
    {
        [SerializeField] private float _speed = 100;
        private void Update()
        {
            if(Input.GetMouseButton(0))
            {
                var axis = Input.GetAxis("Mouse Y");
                transform.Translate(transform.forward * _speed * axis * Time.deltaTime);
            }
        }
    }
}
