using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreNumber : MonoBehaviour
{
    public void Initialize(int n)
    {
        StartCoroutine(Activating(n));
    }

    IEnumerator Activating(int n)
    {
        TMPro.TMP_Text Label = GetComponentInChildren<TMPro.TMP_Text>();
        Label.text = n.ToString();
        yield return new WaitForSeconds(0.5f);

        Color col = Label.color;

        while(col.a > 0.0f)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 50.0f); // ���� �ö�
            col.a -= Time.deltaTime;
            Label.color = col; // ���� �����ϰ� �ٲ�
            yield return null;
        }
        Destroy(gameObject);
    }
}
