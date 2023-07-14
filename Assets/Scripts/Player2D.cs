using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : Character2DProperty
{
    [SerializeField] float MoveSpeed = 3.0f;
    [SerializeField] float JumpFore = 7.5f;
    public bool poisoning = false;

    public LayerMask Land;
    private GroundSensor my_groundSensor;
    private bool my_grounded;
    private float my_delayToIdle = 0.0f;

    public bool IsStop
    {
        set; get;
    }

    void Start()
    {
        // 캐릭터 하단 Ground 충돌 센서
        my_groundSensor = transform.Find("GroundSensor").GetComponent<GroundSensor>();
    }
    private void FixedUpdate()
    {
        // 캐릭터가 땅에 방금 착지했는지 확인
        if (!my_grounded && my_groundSensor.State())
        {
            my_grounded = true;
            myAnim.SetBool("Grounded", my_grounded);
        }
        // 캐릭터가 방금 떨어지기 시작했는지 확인
        if (my_grounded && my_groundSensor.State())
        {
            my_grounded = true;
            myAnim.SetBool("Grounded", my_grounded);
        }
        // 캐릭터 x축 입력
        float inputx = Input.GetAxis("Horizontal");
        // 입력 방향에 따른 캐릭터 방향 전환
        if (inputx > 0) myRenderer.flipX = false;
        else if (inputx < 0) myRenderer.flipX = true;
        // 캐릭터 이동
        myRigid.velocity = new Vector2(inputx * MoveSpeed, myRigid.velocity.y);
        // AirSpeedY 값이 0보다 작으면 Jumping 동작 실행
        myAnim.SetFloat("AirSpeedY", myRigid.velocity.y);

        // Run
        if(Mathf.Abs(inputx) > Mathf.Epsilon)
        {
            my_delayToIdle = 0.05f;
            myAnim.SetBool("IsMoving", true);
        }
        // Jump
        if(Input.GetKeyDown(KeyCode.Space) && my_grounded)
        {
            myAnim.SetTrigger("Jump");
            my_grounded = false;
            myAnim.SetBool("Grounded", my_grounded);
            myRigid.velocity = new Vector2(myRigid.velocity.x, JumpFore);
            // 0.2f 만큼 State 값 false
            my_groundSensor.Disable(0.2f);
        }
        // Idle
        else
        {
            my_delayToIdle -= Time.fixedDeltaTime;
            if (my_delayToIdle < 0) myAnim.SetBool("IsMoving", false);
        }

        // 독 폭탄 피격시 활성화
        if (poisoning) 
        {
            StartCoroutine(Poisoning(2f));
        }
        IEnumerator Poisoning(float delay)
        {     
            MoveSpeed = 1.5f;
            JumpFore = 3f;
            poisoning = false;
            yield return new WaitForSeconds(delay);
            MoveSpeed = 3f;
            JumpFore = 7.5f;
        }
    }
}
