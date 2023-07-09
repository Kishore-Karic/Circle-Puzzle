using CirclePuzzle.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CirclePuzzle
{
    public class PieceController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pieceText;
        [SerializeField] private int zero, one;
        [SerializeField] private float pointTwo, pointSeven, pointNine;
        [SerializeField] private Vector3 atLeft, atRight, atZero, atOneEighty;
        [SerializeField] private Button button;
        [SerializeField] private Color normal, green;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private CircleController circleController;
        private Angles currentAngle;
        private bool leftSide, moved;
        private Transform leftPoint, rightPoint, centerPoint;
        private char pieceCharacter;

        private void Awake()
        {
            button.onClick.AddListener(MovePiece);
        }

        private void MovePiece()
        {
            if(currentAngle == Angles.zero && !PuzzleController.Instance.TakenRight)
            {
                transform.position = atRight;
                transform.SetParent(null);
                PuzzleController.Instance.TakenRightSlot(true);
                PuzzleController.Instance.TakenZero(false);
                moved = true;
            }
            else if(currentAngle == Angles.oneEighty && !PuzzleController.Instance.TakenLeft)
            {
                transform.position = atLeft;
                transform.SetParent(null);
                PuzzleController.Instance.TakenLeftSlot(true);
                PuzzleController.Instance.TakenOneEighty(false);
                moved = true;
            }
            else if(transform.position == atLeft && !PuzzleController.Instance.AtOneEighty)
            {
                transform.position = atOneEighty;
                moved = false;
                transform.SetParent(circleController.transform);
                PuzzleController.Instance.TakenLeftSlot(false);
            }
            else if(transform.position == atRight && !PuzzleController.Instance.AtZero)
            {
                transform.position = atZero;
                moved = false;
                transform.SetParent(circleController.transform);
                PuzzleController.Instance.TakenRightSlot(false);
            }
        }

        public void SetController(char val, CircleController _circleController, int i, Transform _left, Transform _right, Transform _center)
        {
            pieceCharacter = val;
            pieceText.text = val.ToString();
            circleController = _circleController;

            leftPoint = _left;
            rightPoint = _right;
            centerPoint = _center;
            
            SetAngle();
        }

        /*private void SetAnglesList(int x)
        {
            int tempIndex = zero;

            for(int i = x; i < tempAnglesList.Count; i++)
            {
                anglesList.Add(tempAnglesList[i]);
                tempIndex++;
            }

            for(int i = zero; i < x; i++)
            {
                anglesList.Add(tempAnglesList[i]);
                tempIndex++;
            }

            Debug.Log("Char: " + pieceText.text + " Angle: " + anglesList[zero]);
        }*/

        public void Rotate()
        {
            if (!moved)
            {
                float rotationAtZ = -one * circleController.RotationAtZAxis;
                transform.localEulerAngles = new Vector3(zero, zero, rotationAtZ);

                SetAngle();
            }
        }

        public void SetAngle()
        {
            Vector3 dirToTarget = Vector3.Normalize(transform.position - centerPoint.position);
            float viewDistance = Vector3.Dot(centerPoint.up, dirToTarget);
            
            CheckDotProduct(viewDistance);
        }

        private bool CheckLeastDistance()
        {
            float leftDis = Vector3.Distance(transform.position, leftPoint.position);
            float rightDis = Vector3.Distance(transform.position, rightPoint.position);

            return leftSide = (leftDis < rightDis);
        }

        private void CheckDotProduct(float val)
        {
            CheckLeastDistance();

            if (val == one)
            {
                currentAngle = Angles.ninety;
            }
            else if (val == -one)
            {
                currentAngle = Angles.twoSeventy;
            }
            else if (leftSide)
            {
                if(val > pointSeven && val < pointNine)
                {
                    currentAngle = Angles.oneTwenty;
                }
                else if(val > pointTwo && val < pointSeven)
                {
                    currentAngle = Angles.oneFifty;
                }
                else if(val < -pointTwo && val > -pointSeven)
                {
                    currentAngle = Angles.twoTen;
                }
                else if(val < -pointSeven && val > -pointNine)
                {
                    currentAngle = Angles.twoFourty;
                }
                else
                {
                    currentAngle = Angles.oneEighty;
                    PuzzleController.Instance.TakenOneEighty(true);
                }
            }
            else if (!leftSide)
            {
                if (val > pointSeven && val < pointNine)
                {
                    currentAngle = Angles.sixty;
                }
                else if (val > pointTwo && val < pointSeven)
                {
                    currentAngle = Angles.thirty;
                }
                else if (val < -pointSeven && val > -pointNine)
                {
                    currentAngle = Angles.threeHundred;
                }
                else if (val < -pointTwo && val > -pointSeven)
                {
                    currentAngle = Angles.threeThirty;
                }
                else
                {
                    currentAngle = Angles.zero;
                    PuzzleController.Instance.TakenZero(true);
                }
            }

            Angles tempAngle = Angles.zero;
            PuzzleController.Instance.Answers.TryGetValue(pieceCharacter, out tempAngle);
            if(tempAngle == currentAngle)
            {
                spriteRenderer.color = green;
            }
            else
            {
                spriteRenderer.color = normal;
            }
        }
    }
}