using UnityEngine;
using UnityEngine.UI;

namespace CirclePuzzle
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private Button infoButton, closeButton, quitButton;
        [SerializeField] private GameObject infoObject;

        public bool DisplayInfo { get; private set; }

        private void Awake()
        {
            infoButton.onClick.AddListener(ShowInfo);
            closeButton.onClick.AddListener(CloseInfo);
            quitButton.onClick.AddListener(Application.Quit);

            DisplayInfo = false;
        }

        private void ShowInfo()
        {
            DisplayInfo = true;
            infoObject.SetActive(true);
            infoButton.gameObject.SetActive(false);
        }

        private void CloseInfo()
        {
            DisplayInfo = false;
            infoObject.SetActive(false);
            infoButton.gameObject.SetActive(true);
        }
    }
}