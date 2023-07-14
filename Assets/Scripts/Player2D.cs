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
        // ĳ���� �ϴ� Ground �浹 ����
        my_groundSensor = transform.Find("GroundSensor").GetComponent<GroundSensor>();
    }
    private void FixedUpdate()
    {
        // ĳ���Ͱ� ���� ��� �����ߴ��� Ȯ��
        if (!my_grounded && my_groundSensor.State())
        {
            my_grounded = true;
            myAnim.SetBool("Grounded", my_grounded);
        }
        // ĳ���Ͱ� ��� �������� �����ߴ��� Ȯ��
        if (my_grounded && my_groundSensor.State())
        {
            my_grounded = true;
            myAnim.SetBool("Grounded", my_grounded);
        }
        // ĳ���� x�� �Է�
        float inputx = Input.GetAxis("Horizontal");
        // �Է� ���⿡ ���� ĳ���� ���� ��ȯ
        if (inputx > 0) myRenderer.flipX = false;
        else if (inputx < 0) myRenderer.flipX = true;
        // ĳ���� �̵�
        myRigid.velocity = new Vector2(inputx * MoveSpeed, myRigid.velocity.y);
        // AirSpeedY ���� 0���� ������ Jumping ���� ����
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
            // 0.2f ��ŭ State �� false
            my_groundSensor.Disable(0.2f);
        }
        // Idle
        else
        {
            my_delayToIdle -= Time.fixedDeltaTime;
            if (my_delayToIdle < 0) myAnim.SetBool("IsMoving", false);
        }

        // �� ��ź �ǰݽ� Ȱ��ȭ
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
