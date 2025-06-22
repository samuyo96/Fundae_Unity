using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fundae.Player
{
    public class BasicPlayer : MonoBehaviour
    {
        public float speed;
        public float rotationSpeed;

        public GameObject objectToInstantiate;

        private bool isInstantiated;
        private GameObject objectInstance;

        private void FixedUpdate()
        {
            transform.Rotate(Vector3.up * rotationSpeed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime);
            transform.Translate(Vector3.right * speed * Input.GetAxis("Vertical") * Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                InstantiateObject();
                isInstantiated = !isInstantiated;
            }
        }

        private void InstantiateObject()
        {
            if (isInstantiated) 
            {
                Destroy(objectInstance);
                return;
            }
            objectInstance = Instantiate(objectToInstantiate, transform);
        }
    }
}


