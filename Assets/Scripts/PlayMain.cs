using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DodgeBomb
{
    public class PlayMain : MonoBehaviour
    {
        public enum STATE
        {
            Create, Start, Play, GameOver
        }
        public STATE myState = STATE.Create;

        [SerializeField] ScoreUI myScore = null;
        [SerializeField] LifeUI myLife = null;

        public Dropper myDropper = null;
        public Player2D myPlayer = null;
        public GameObject StartMenu = null;
        public GameObject RetryMenu = null;
        public void UpdateScore(int n)
        {
            myScore.Score += n;
        }
        public void UpdateLife(int n)
        {
            myLife.Life += n;
            if (myLife.Life == 0)
            {
                ChangeState(STATE.GameOver);
            }
        }
        public static PlayMain Inst = null;
        public Transform myScoreUITransform = null;
        private void Awake()
        {
            Inst = this;
        }
        void ChangeState(STATE s)
        {
            if (myState == s) return;
            myState = s;
            switch(myState)
            {
                case STATE.Create:
                    break;
                case STATE.Start:
                    myPlayer.IsStop = true; // 운직이지 못함
                    // myDropper.SetActive(false); // Dropper 비활성화
                    myScoreUITransform.gameObject.SetActive(false);
                    myLife.Life = 3;
                    StartMenu.SetActive(true);
                    RetryMenu.SetActive(false);
                    break;
                case STATE.Play:
                    myPlayer.IsStop = false;
                    myDropper.SetActive(true);
                    StartMenu.SetActive(false);
                    myScoreUITransform.gameObject.SetActive(true);
                    break;
                case STATE.GameOver:
                    myDropper.SetActive(false);
                    myPlayer.IsStop = true;
                    RetryMenu.SetActive(true);
                    break;
            }
        }
        void StateProcess()
        {
            switch (myState)
            {
                case STATE.Create:
                    break;
                case STATE.Start:
                    if(Input.GetMouseButtonDown(0))
                    {
                        ChangeState(STATE.Play);
                    }
                    break;
                case STATE.Play:
                    if (Input.GetKeyDown(KeyCode.F1)) UpdateLife(1);
                    break;
                case STATE.GameOver:
                    break;
            }
        }
        public void ReStart()
        {
            ChangeState(STATE.Start);
            Debug.Log("yes");
        }

        // Start is called before the first frame update
        void Start()
        {
            ChangeState(STATE.Start);
        }

        // Update is called once per frame
        void Update()
        {
            StateProcess();
        }
    }

}