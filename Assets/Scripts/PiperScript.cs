using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    
    void Start()
    {
        
    }
   
    void Update()
    {
        this.transform.Translate(speed * Time.deltaTime * Vector3.left);
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
        Debug.Log("Speed increased to: " + speed);
    }
    /* [SerializeField] - атрибут, що зазначає те, що значення для поля буде визначено через "Редактор".
     * Translate - переміщення
     * Time.deltaTime - час, що пройшов від попереднього фрейму (виклику методу) 
     * корекція на час (множення на Time.deltaTime) створює FPS-незалежність
     */
}
