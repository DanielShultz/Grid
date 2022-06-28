using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class AdditionalFunctions
{
    //Преобразование двумерного массива в список
    public static List<TextMeshProUGUI> ArrayToList(GameObject[,] textObjects)
    {
        List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
        foreach (var item in textObjects)
        {
            texts.Add(item.GetComponent<TextMeshProUGUI>());
        }
        return texts;
    }

    //Блокирование кнопки
    public static IEnumerator DisableButton(float animationTime, Button button)
    {
        button.enabled = false;
        yield return new WaitForSeconds(animationTime);
        button.enabled = true;
    }

    //Авторазмер шрифта на множестве элементов
    public static void TextAutoSize(List<TextMeshProUGUI> textObjects)
    {
        if (textObjects == null || textObjects.Count == 0)
            return;

        int candidateIndex = 0;
        float maxPreferredWidth = 0;

        for (int i = 0; i < textObjects.Count; i++)
        {
            float preferredWidth = textObjects[i].preferredWidth;
            if (preferredWidth > maxPreferredWidth)
            {
                maxPreferredWidth = preferredWidth;
                candidateIndex = i;
            }
        }

        textObjects[candidateIndex].enableAutoSizing = true;
        textObjects[candidateIndex].ForceMeshUpdate();
        float optimumPointSize = textObjects[candidateIndex].fontSize;

        textObjects[candidateIndex].enableAutoSizing = false;

        for (int i = 0; i < textObjects.Count; i++)
            textObjects[i].fontSize = optimumPointSize;
    }
}
