using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Devdog.LosPro.Demo
{
    public class MessageOnTrigger : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        [SerializeField]
        private bool _showOnEnter = false;

        [SerializeField]
        private string _messageOnEnter;

        [SerializeField]
        private bool _showOnExit = false;

        [SerializeField]
        private string _messageOnExit;

        protected void OnTriggerEnter(Collider col)
        {
            if (_showOnEnter) // TODO: Add player specific check here...
            {
                _text.text = _messageOnEnter;

                StopAllCoroutines();
                StartCoroutine(_ClearTextInSeconds(5f));
            }
        }

        protected void OnTriggerExit(Collider col)
        {
            if (_showOnExit) // TODO: Add player specific check here...
            {
                _text.text = _messageOnExit;

                StopAllCoroutines();
                StartCoroutine(_ClearTextInSeconds(5f));
            }
        }

        private IEnumerator _ClearTextInSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _text.text = string.Empty;
        }
    }
}