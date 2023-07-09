using System.Collections.Generic;
using UnityEngine;

namespace CirclePuzzle
{
    public class CircleController : MonoBehaviour
    {
        [SerializeField] private PieceController piecePrefab;
        [SerializeField] private float radius;
        [SerializeField] private int totalPieceCount;
        [SerializeField] private List<char> charactersList;

        private void Start()
        {
            for(int i = 0; i < totalPieceCount; i++)
            {
                float angle = i * Mathf.PI * 2 / totalPieceCount;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;

                Vector3 pos = transform.position + new Vector3(x, y, 0f);
                PieceController pieceController= Instantiate(piecePrefab, pos, Quaternion.identity);
                pieceController.SetCharacter(charactersList[i]);
                pieceController.gameObject.transform.SetParent(transform);
            }
        }
    }
}