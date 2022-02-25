using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FloatScoreText : MonoBehaviour
{
    [SerializeField] float _floatSpeed = 5f;
    public void SetScoreValue(int _killMultiplier){
        var text = GetComponent<TMP_Text>();
        text.SetText("x"+_killMultiplier);
        if (_killMultiplier < 2)
        {
            text.color = Color.white;
        }
        else if (_killMultiplier < 5)
        {
            text.color = Color.green;
        }
        else if (_killMultiplier < 10)
        {
            text.color = Color.yellow;
        }
        else
        {
            text.color = Color.red;
        }
        Destroy(gameObject, 5f);
    }

    private void Update(){
        transform.position += transform.up * Time.deltaTime * _floatSpeed;
    }
}
