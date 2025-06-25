using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fundae.UI
{
    public class ButtonBasicUI : MonoBehaviour
    {
        public AudioSource buttonClickSound;
        public Image image;
        public Button button;
        public Text buttonText;

        private bool state;
        private string[] labels = new string[] {"Hide Image", "Show Image"};

        public void ButtonAction()
        {
            StartCoroutine(ButtonInteraction());
        }

        private IEnumerator ButtonInteraction()
        {
            button.interactable = false;
            buttonClickSound.Play();
            image.enabled = !image.enabled;
            buttonText.text = labels[state ? 0 : 1];
            state = !state;
            yield return new WaitForSeconds(0.25f);
            button.interactable = true;
        }
    }
}


