using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject LetterPrefab;
    public TMP_InputField WidthInput, HeightInput;
    public List<Button> Buttons;
    public float AnimationTime;
    public int AnimationFPS;
    public string Alphabet;

    private GameObject[,] Letters;
    private int Height = 0, Width = 0;

    //Создание
    public void Generate()
    {
        //Размер
        if (HeightInput.text != "")
            Height = int.Parse(HeightInput.text);
        if (WidthInput.text != "")
            Width = int.Parse(WidthInput.text);

        //Чистка объектов
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //Пересоздание массива
        Letters = new GameObject[Height, Width];

        //Размер сетки и буквы
        Grid grid = GetComponent<Grid>();
        grid.cellSize = new Vector3(GetComponent<RectTransform>().rect.width / Width, GetComponent<RectTransform>().rect.height / Height);
        LetterPrefab.GetComponent<RectTransform>().sizeDelta = grid.cellSize;

        //Создание объектов и заполнение массива
        for (int h = 0; h < Height; h++)
        {
            for (int w = 0; w < Width; w++)
            {
                GameObject go = Instantiate(LetterPrefab, transform);
                go.transform.position = grid.CellToWorld(new Vector3Int(w, -h, 0));
                go.GetComponent<Letter>().Show(Alphabet);
                Letters[h, w] = go;
            }
        }

        //Размер шрифта объектов
        AdditionalFunctions.TextAutoSize(AdditionalFunctions.ArrayToList(Letters));
    }


    //Перемешевание
    public void Shuffle()
    {
        //Перемешивание в массиве
        for (int h = 0; h < Height; h++)
        {
            for (int w = 0; w < Width; w++)
            {
                int height = Random.Range(0, Height);
                int width = Random.Range(0, Width);
                GameObject temp = Letters[h, w];
                Letters[h, w] = Letters[height,width];
                Letters[height, width] = temp;
            }
        }

        //Анимация передвижения
        for (int h = 0; h < Height; h++)
        {
            for (int w = 0; w < Width; w++)
            {
                StartCoroutine(Letters[h, w].GetComponent<Letter>().Move(GetComponent<Grid>().CellToWorld(new Vector3Int(w, -h, 0)),AnimationTime,AnimationFPS));
            }
        }

        //Блокирование кнопок до конца анимации
        foreach (var button in Buttons)
        {
            StartCoroutine(AdditionalFunctions.DisableButton(AnimationTime, button));
        }
    }
}
