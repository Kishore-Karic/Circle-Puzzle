using System;
using System.Collections.Generic;
using UnityEngine;

namespace CirclePuzzle
{
    public class CircleController : MonoBehaviour
    {
        [SerializeField] private PieceController piecePrefab;
        [SerializeField] private float innerCircleRadius, outerCircleRadius;
        [SerializeField] private int totalPieceCount, rotateSpeed, zero, two, thirty, sixty, ninety, oneTwenty, oneFifty, oneEighty, twoTen, twoFourty, twoSeventy, threeHundred, threeThirty;
        [SerializeField] private int fifteen, fourtyFive, eightyFive, oneHundredFive, oneThirtyFive, oneSixtyFive, oneNinetyFive, twoTwentyFive, twoFiftyFive, twoEightyFive, threeFifteen, threeFourtyFive;
        [SerializeField] private List<char> innerCircleCharacters;
        [SerializeField] private Transform leftPoint, rightPoint, centerPoint;
        [SerializeField] private List<GameObject> outerCircleText;

        public float RotationAtZAxis { get; private set; }

        private float horizontalRotation;
        private bool mouseDrag;
        private event Action OnPiecesRotate;

        private void Start()
        {
            OuterCircleImplement();
            InnerCircleImplement();
        }

        private void InnerCircleImplement()
        {
            for (int i = zero; i < totalPieceCount; i++)
            {
                float angle = i * Mathf.PI * two / totalPieceCount;
                float x = Mathf.Cos(angle) * innerCircleRadius;
                float y = Mathf.Sin(angle) * innerCircleRadius;

                Vector3 pos = transform.position + new Vector3(x, y, zero);
                PieceController pieceController = Instantiate(piecePrefab, pos, Quaternion.identity);
                pieceController.SetController(innerCircleCharacters[i], this, i, leftPoint, rightPoint, centerPoint);
                OnPiecesRotate += pieceController.Rotate;
                pieceController.gameObject.transform.SetParent(transform);
            }
        }
        
        private void OuterCircleImplement()
        {
            for (int i = zero; i < totalPieceCount; i++)
            {
                float angle = i * Mathf.PI * two / totalPieceCount;
                float x = Mathf.Cos(angle) * outerCircleRadius;
                float y = Mathf.Sin(angle) * outerCircleRadius;

                outerCircleText[i].transform.position = transform.position + new Vector3(x, y, zero);
            }
        }

        private void OnMouseDrag()
        {
            mouseDrag = true;
        }

        void Update()
        {
            horizontalRotation = Input.GetAxisRaw("Mouse X");

            if (Input.GetMouseButtonUp(zero))
            {
                mouseDrag = false;
                SetCirclePosition(transform.localEulerAngles.z);
            }

            if (horizontalRotation != zero && mouseDrag)
            {
                transform.Rotate(Vector3.forward * horizontalRotation * rotateSpeed);
            }
        }

        void SetCirclePosition(float val)
        {
            if (val >= eightyFive && val < oneHundredFive)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, ninety);
            }
            else if (val >= fourtyFive && val < eightyFive)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, sixty);
            }
            else if (val >= fifteen && val < fourtyFive)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, thirty);
            }
            else if (val >= threeFourtyFive || val < fifteen)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, zero);
            }
            else if (val >= threeFifteen && val < threeFourtyFive)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -thirty);
            }
            else if (val >= twoEightyFive && val < threeFifteen)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -sixty);
            }
            else if (val >= twoFiftyFive && val < twoEightyFive)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -ninety);
            }
            else if (val >= twoTwentyFive && val < twoFiftyFive)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -oneTwenty);
            }
            else if (val >= oneNinetyFive && val < twoTwentyFive)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -oneFifty);
            }
            else if (val >= oneThirtyFive && val < oneSixtyFive)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, oneFifty);
            }
            else if (val >= oneHundredFive && val < oneThirtyFive)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, oneTwenty);
            }
            else if (val >= oneSixtyFive && val < oneNinetyFive)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, oneEighty);
            }

            //PuzzleController.Instance.ResetAngleSlot();
            PuzzleController.Instance.TakenZero(false);
            PuzzleController.Instance.TakenOneEighty(false);
            RotationAtZAxis = transform.localEulerAngles.z;
            OnPiecesRotate?.Invoke();
        }
    }
}