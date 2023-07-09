using TMPro;
using UnityEngine;

namespace CirclePuzzle
{
    public class PieceController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pieceText;

        public void SetCharacter(char val)
        {
            pieceText.text = val.ToString();
        }
    }
}