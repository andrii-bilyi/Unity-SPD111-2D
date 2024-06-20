using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BirdScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI passedLabel;
    [SerializeField]
    private GameObject alert;
    [SerializeField]
    private TMPro.TextMeshProUGUI alertLabel;
    [SerializeField]
    private LifeDisplay lifeDisplay; // Ссылка на LifeDisplay
    [SerializeField]
    private PipeScript pipeScript; // Ссылка на PipeScript

    private Rigidbody2D rigidbody; //Посилання на компонент того ж, на якому скрипт
    private int score;
    private int life;
    private bool needClear;    

    void Start()
    {
        Debug.Log("BirdScript Start");
        //пошук компонента
        rigidbody = GetComponent<Rigidbody2D>();
        score = 0;
        life = 3;
        needClear = false;        
        HideAlert();

        // Проверьте инициализацию LifeDisplay
        if (lifeDisplay != null)
        {
            lifeDisplay.UpdateLifeDisplay(life);
        }
        else
        {
            Debug.LogError("LifeDisplay is not assigned in the Inspector");
        }

        if (pipeScript == null)
        {
            Debug.LogError("PipeScript is not assigned in the Inspector");
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(new Vector2(0, 300) * Time.timeScale);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (alert.activeSelf)
            {
                HideAlert();
            }
            else
            {
                ShowAlert("Paused");
            }
           
        }
    }

    /*Подія, що виникає при перетині колайдерів-тригерів*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pipe"))
        {
            Debug.Log("Collision: " + other.gameObject.name);
            needClear = true;
            life--;
            if (lifeDisplay != null)
            {
                lifeDisplay.UpdateLifeDisplay(life); // Обновляем отображение жизней
            }
            else
            {
                Debug.LogError("LifeDisplay is not assigned in the Inspector");
            }
            if(life < 1)
            {
                
                score = 0;
                passedLabel.text = score.ToString("D3");
                life = 3;
                ShowAlert("GAME OVER");
                //needClear = false;
                //HideAlert();
            }
            else
            {
                ShowAlert("OOOPS!");
            }
            
           
        }
        if (other.gameObject.CompareTag("Bonus"))
        {
            Debug.Log("Collision: " + other.gameObject.name);
            //bonusClear = true;
            life++; //тут меняется значение life

            if (lifeDisplay != null)
            {
                lifeDisplay.UpdateLifeDisplay(life); // Обновляем отображение жизней
            }
            else
            {
                Debug.LogError("LifeDisplay is not assigned in the Inspector");
            }

            Destroy(other.gameObject); // Удаляем объект Bonus
        }

    }
    /*Подія, що виникає при роз'єднанні колайдерів-тригерів*/
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pass"))
        {
            Debug.Log("+1");
            score++;
            passedLabel.text = score.ToString("D3");
            CheckScore();
        }
    }

    private void CheckScore()
    {
        if (score % 5 == 0 && score != 0)
        {
            if (pipeScript != null)
            {
                pipeScript.IncreaseSpeed(1f); // Увеличиваем скорость на 1
            }
        }
    }

    private void ShowAlert(string message)
    {
        alert.SetActive(true);
        alertLabel.text = message;
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void HideAlert()
    {
        alert.SetActive(false);
        Time.timeScale = 1f;
        if(needClear)
        {
            foreach (var pipe in GameObject.FindGameObjectsWithTag("Pass"))
            {
                GameObject.Destroy(pipe);
            }
            needClear = false;
        }
    }
}
