using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolEffect : MonoBehaviour
{
    public static ObjectPoolEffect Inst;
    [SerializeField]
    private GameObject bombEffPrefab;
    private Queue<GameObject> bombEffQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (null == Inst)
        {
            Inst = this;
            for (int i = 0; i < 5; i++)
            {
                CreateNewEffect();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private GameObject CreateNewEffect() // ��ź ����Ʈ ����
    {
        GameObject newEff = Instantiate(bombEffPrefab);
        newEff.gameObject.SetActive(false);
        newEff.transform.SetParent(Inst.transform);
        Inst.bombEffQueue.Enqueue(newEff);
        return newEff;
    }

    public static GameObject GetEffObject() // ������ ��ź�� ����Ʈ�� ������
    {
        if (Inst.bombEffQueue.Count > 0)
        {
            GameObject Bomb = Inst.bombEffQueue.Dequeue();
            Bomb.gameObject.SetActive(true);
            Bomb.transform.SetParent(Inst.transform);
            return Bomb;
        }
        else // ���� ��ź�� ����Ʈ�� ������
        {
            GameObject newBomb = Inst.CreateNewEffect();
            newBomb.gameObject.SetActive(true);
            newBomb.transform.SetParent(Inst.transform);
            return newBomb;
        }
    }

    public static void ReturnEffObject(GameObject obj) // ����� ����Ʈ�� �ٽ� �־� ��
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Inst.transform);
        Inst.bombEffQueue.Enqueue(obj);
    }
}
