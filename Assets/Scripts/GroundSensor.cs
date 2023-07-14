using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    private int Player_ColCount = 0;
    private float Player_DisableTimer;

    private void OnDisable()
    {
        Player_ColCount = 0;
    }

    public bool State()
    {
        if(Player_DisableTimer > 0) return false;
        return Player_ColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 캐릭터가 땅에 떨어지는 순간부터 증가
        Player_ColCount++;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // 캐릭터가 땅에 계속 붙어있으면 State 값은 flase
        Player_ColCount--;
    }

    private void FixedUpdate()
    {
        Player_DisableTimer -= Time.fixedDeltaTime;
    }
    public void Disable(float duration)
    {
        Player_DisableTimer = duration;
    }
}
