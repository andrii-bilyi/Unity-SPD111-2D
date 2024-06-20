using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDisplay : MonoBehaviour
{
    public GameObject lifePrefab;  // Ссылка на префаб изображения жизни
    public Transform livesContainer;  // Ссылка на контейнер для жизней

    private List<GameObject> lifeImages = new List<GameObject>();

    public void UpdateLifeDisplay(int life)
    {
        // Очистка предыдущих изображений
        foreach (GameObject lifeImage in lifeImages)
        {
            Destroy(lifeImage);
        }
        lifeImages.Clear();

        // Создание новых изображений
        for (int i = 0; i < life; i++)
        {
            GameObject lifeImage = Instantiate(lifePrefab, livesContainer);
            lifeImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 20, 0);  // Пример расположения по горизонтали
            lifeImages.Add(lifeImage);
        }
    }
}
