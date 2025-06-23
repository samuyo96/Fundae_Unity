using UnityEngine;

namespace Fundae.Player
{
    using GameObjectFunctionality;

    public class BasicPlayerShooting : BasicPlayer
    {
        public Material changeMaterial;

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                SecondAction();
            }
        }

        protected override void InstantiateObject()
        {
            if (isInstantiated) 
            {
                Destroy(objectInstance);
                return;
            }
            objectInstance = Instantiate(objectToInstantiate, transform.position + Vector3.right, transform.rotation);
            Rigidbody rb = objectInstance.GetComponent<Rigidbody>();
            Vector3 direccion = transform.TransformDirection(Vector3.right);
            rb.AddForce(direccion * 10f, ForceMode.Impulse);
            Debug.DrawRay(transform.position, direccion * 2f, Color.red, 2f);
        }

        private void SecondAction()
        {
            Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.right));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Default")))
            {
                if (hit.collider.gameObject.TryGetComponent(out WallChangeColor wallChangeColor))
                { 
                    wallChangeColor.ChangeColor(changeMaterial);
                }               
            }
        }
    }
}


