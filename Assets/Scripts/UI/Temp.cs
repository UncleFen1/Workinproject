using UnityEngine;

namespace UI
{
    public class Temp : MonoBehaviour
    {
        [SerializeField]private CustomButton testButton;
        [SerializeField] private Vector2 sizeOnButton;
        private GameObject tempButton;
        private void OnEnable()
        {
            testButton.OnFocusMouse += ButtonSize;
            testButton.onClick.AddListener(ButtonPanelMM);
            //
        }

        private void ButtonPanelMM()
        {
            //что то
            Debug.Log("++");
            ButtonSize(false, tempButton);
        }

        private void ButtonSize(bool _flag, GameObject _objectButton)
        {
            tempButton = _objectButton;
            if (_flag) { tempButton.transform.localScale = sizeOnButton; }
            else { tempButton.transform.localScale = new Vector3(1, 1, 0); }
        }

    }
}

