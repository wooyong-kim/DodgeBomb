using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DodgeBomb; // namespace DodgeBomb 생략
using Unity.VisualScripting;

namespace DodgeBomb
{
    public class Item : MonoBehaviour
    {
        public Sprite[] Imagelist;
        public SpriteRenderer myRenderer;
        private Player2D player2D;
        private void Awake()
        {
            player2D = GameObject.Find("Player2D").GetComponent<Player2D>();
        }

        public enum STATE
        {
            Create, Active
        }
        public enum TYPE
        {
            Bomb, Coin, Poison, Heart
        }
        public STATE myState = STATE.Create;
        public TYPE myType = TYPE.Bomb;
        public float MoveSpeed = 2.0f;
        int playEffNum = 0;

        Dropper myDropper = null;
        void ChangeState(STATE s)
        {
            if (myState == s) return;
            myState = s;
            switch (myState)
            {
                case STATE.Create:
                    break;
                case STATE.Active:
                    break;
            }
        }
        void StateProcess()
        {
            switch (myState)
            {
                case STATE.Create:
                    break;
                case STATE.Active:
                    // Item 오브젝트 아래로 이동
                    transform.Translate(Vector3.down * Time.deltaTime * MoveSpeed, Space.World);
                    break;
            }
        }
        public void Inialize(TYPE type, Dropper drop)
        {
            myDropper = drop;
            myType = type;
            switch (myType)
            {
                case TYPE.Bomb:
                    playEffNum = 0;
                    break;
                case TYPE.Coin:
                    playEffNum = 1;
                    break;
                case TYPE.Poison:
                    playEffNum = 2;
                    break;
                case TYPE.Heart:
                    playEffNum = 3;
                    break;
            }
            myRenderer.sprite = Imagelist[(int)myType];

            ChangeState(STATE.Active); // 타입이 정해지면 활성화
        }
        public void ItemDestroy()
        {
            ObjectPool.ReturnObject(this); // 폭탄 집어 넣음
            GameObject Effect = ObjectPoolEffect.GetEffObject(); // 폭탄 이펙트 가져옴
            EffObject effobj = Effect.GetComponent<EffObject>();
            Effect.transform.position = transform.position + Vector3.down * MoveSpeed;
            effobj.EffectPlay(playEffNum);
        }


        void Update()
        {
            StateProcess();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                ItemDestroy();               

                switch (myType)
                {
                    case TYPE.Bomb:
                        PlayMain.Inst.UpdateLife(-1);
                        break;
                    case TYPE.Coin:
                        PlayMain.Inst.UpdateScore(10);
                        GameObject obj = Instantiate(Resources.Load("Prefabs/UI/ScoreNumber"), 
                            PlayMain.Inst.myScoreUITransform) as GameObject; // PlayMain.Inst.myScoreUITransform 자식으로 넣어줌
                        obj.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                        obj.GetComponent<ScoreNumber>().Initialize(10);
                        break;
                    case TYPE.Poison:
                        PlayMain.Inst.UpdateLife(-1);
                        player2D.poisoning = true;
                        break;
                    case TYPE.Heart:
                        PlayMain.Inst.UpdateLife(1);
                        break;
                }
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                ItemDestroy();
            }
        }
    }
}
