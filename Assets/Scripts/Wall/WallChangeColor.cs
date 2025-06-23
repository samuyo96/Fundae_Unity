using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fundae.GameObjectFunctionality
{
    public class WallChangeColor : MonoBehaviour
    {
        public Material baseMaterial;
        public float waitTime = 2f;

        private bool isChangingColor;

        public void ChangeColor(Material newMaterial)
        {
            if (!isChangingColor)
            {
                GetComponent<MeshRenderer>().material = newMaterial;
                StartCoroutine(ResetColor());
            }          
        }

        private IEnumerator ResetColor()
        {
            isChangingColor = true;
            yield return new WaitForSeconds(waitTime);
            GetComponent<MeshRenderer>().material = baseMaterial;
            isChangingColor = false;
        }

    }
}


