using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
{
    private Vector2 StartPosition;

    //Отображение
    public void Show(string alph)
    {
        GetComponent<TextMeshProUGUI>().text = alph[Random.Range(0, alph.Length)].ToString();
        StartPosition = transform.position;
    }

    //Передвижение
    public IEnumerator Move(Vector2 target, float animationTime, int fps)
    {
        //Вычисление количества кадров
        float time = animationTime * fps;

        //Вычисление сдвига для каждого кадра
        Vector3 speed = new Vector2((target.x - StartPosition.x) / time, (target.y - StartPosition.y) / time);

        //Сдвиг на каждом кадре
        for (int i = 0; i < time; i++)
        {
            yield return new WaitForSeconds(1f / fps);
            transform.position += speed;
        }

        //Сохранение новой позиции
        StartPosition = transform.position;
    }
}
