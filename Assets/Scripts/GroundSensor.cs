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
        // ĳ���Ͱ� ���� �������� �������� ����
        Player_ColCount++;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // ĳ���Ͱ� ���� ��� �پ������� State ���� flase
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
