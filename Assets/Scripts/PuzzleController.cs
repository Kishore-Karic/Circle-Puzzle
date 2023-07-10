using CirclePuzzle.Enum;
using System.Collections.Generic;
using UnityEngine;

namespace CirclePuzzle
{
    public class PuzzleController : MonoBehaviour
    {
        [SerializeField] private List<char> charsList;
        [SerializeField] private int zero;

        public static PuzzleController Instance { get; private set; }

        public bool TakenLeft { get; private set; } 
        public bool TakenRight { get; private set; }
        public bool AtOneEighty { get; private set; }
        public bool AtZero { get; private set; }

        public Dictionary<char, Angles> Answers { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            Answers = new Dictionary<char, Angles>();

            FillAnswer();
        }

        public void TakenLeftSlot(bool val)
        {
            TakenLeft = val;
        }

        public void TakenRightSlot(bool val)
        {
            TakenRight = val;
        }

        public void TakenOneEighty(bool val)
        {
            AtOneEighty = val;
        }

        public void TakenZero(bool val)
        {
            AtZero = val;
        }

        private void FillAnswer()
        {
            for(int i = zero; i < charsList.Count; i++)
            {
                Answers.Add(charsList[i], (Angles)i);
            }
        }
    }
}