using DodgeBomb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dropper : MonoBehaviour
{
    public enum STATE
    {
        Create, Active, DeActive
    }
    public STATE myState = STATE.Create;
    public Vector2 MoveRange = new Vector2(-5, 5);
    public float MoveSpeed = 3.0f;
    float MoveDir = 1.0f;
    float MoveDist = 0.0f;

    public void SetActive(bool act)
    {
        if(act)
        {
            ChangeState(STATE.Active);
        }
        else
        {
            ChangeState(STATE.DeActive);
        }
    }
    void ChangeState(STATE s)
    {
        if(myState == s) return;
        myState = s;
        switch(myState)
        {
            case STATE.Create:
                break;
            case STATE.Active:
                if(MoveDir > 0.0f)
                {
                    MoveDist = Mathf.Abs(MoveRange.y - transform.localPosition.x);
                }
                else
                {
                    MoveDist = Mathf.Abs(MoveRange.x - transform.localPosition.x);
                }
                StartCoroutine(Dropping(2.0f));
                break;
            case STATE.DeActive:
                // ObjectPool.ReturnObject(DodgeBomb.Item);
                StopAllCoroutines();
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
                // Dropper 좌우 이동
                float delta = MoveSpeed * Time.deltaTime;
                if(delta >= MoveDist)
                {
                    delta = MoveDist;     
                }
                MoveDist -= delta;
                transform.Translate(Vector3.right * MoveDir * delta);
                if (Mathf.Approximately(MoveDist, 0.0f))
                {
                    MoveDir *= -1.0f;
                    MoveDist = Mathf.Abs(MoveRange.y - MoveRange.x);
                }
                break;
        }
    }
    IEnumerator Dropping(float delay)
    {
        while(true)
        {
            DodgeBomb.Item scp = ObjectPool.GetObject();
            scp.transform.position = transform.position; // 투사체 위치 Dropper로
            int cnt = Random.Range(0, 100);
            if(cnt >= 80)
            {
                scp.Inialize(DodgeBomb.Item.TYPE.Coin, this);
            }
            else if(cnt < 80 && cnt >= 40)
            {
                scp.Inialize(DodgeBomb.Item.TYPE.Bomb, this);
            }
            else if (cnt < 40 && cnt >= 10)
            {
                scp.Inialize(DodgeBomb.Item.TYPE.Poison, this);
            }
            else     
            {
                scp.Inialize(DodgeBomb.Item.TYPE.Heart, this);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    void Update()
    {
        StateProcess();
    }
}
