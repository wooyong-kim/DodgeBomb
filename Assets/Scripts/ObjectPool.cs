using DodgeBomb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Inst;
    [SerializeField]
    private GameObject bombPrefab;
    private Queue<Item> bombQueue = new Queue<Item>();

    private void Awake()
    {
        if(null == Inst)
        {
            Inst = this;
            for (int i = 0; i < 5; i++)
            {
                CreateNewbomb();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    private Item CreateNewbomb() // ��ź ����
    {
        Item newBomb = Instantiate(bombPrefab).GetComponent<Item>();
        newBomb.gameObject.SetActive(false);
        newBomb.transform.SetParent(Inst.transform);
        Inst.bombQueue.Enqueue(newBomb);
        return newBomb;
    }
    public static Item GetObject() // ������ ��ź�� ������
    {
        if(Inst.bombQueue.Count > 0)
        {
            Item Bomb = Inst.bombQueue.Dequeue();
            Bomb.gameObject.SetActive(true);
            Bomb.transform.SetParent(Inst.transform);
            return Bomb;
        }
        else // ���� ��ź�� ������
        {
            Item newBomb = Inst.CreateNewbomb();
            newBomb.gameObject.SetActive(true);
            newBomb.transform.SetParent(Inst.transform);
            return newBomb;
        }
    }
    public static void ReturnObject(Item obj) // ����� ��ź�� �ٽ� �־� ��
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Inst.transform);
        Inst.bombQueue.Enqueue(obj);
    }
}
