using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffObject : MonoBehaviour
{
    public GameObject[] effObject;
    public void EffectPlay (int playEffNum)
    {
        effObject[playEffNum].gameObject.SetActive(true);
        StartCoroutine(EffectStop(playEffNum, 1f));
    }

    IEnumerator EffectStop(int playEffNum, float delay)
    {
        yield return new WaitForSeconds(delay);
        effObject[playEffNum].gameObject.SetActive(false);
        ObjectPoolEffect.ReturnEffObject(this.gameObject);
    }
}
