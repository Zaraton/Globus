using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpButton : MonoBehaviour
{
    public Image ClockHand;
    public Toggle ToggleScript;

    public Image Image;
    public Sprite ImgOn, ImgOff;

    public float lowSpeed = 1.0f;
    public float highSpeed = 100.0f;

    bool is100 = false;

    float currentSpeed;
    float angle = 10.0f;

    SpeedUpButton()
    {
        currentSpeed = lowSpeed;
    }

    void Update()
    {
        if (ClockHand != null)
            ClockHand.rectTransform.Rotate(-Vector3.forward * Time.deltaTime * angle * currentSpeed); // поворачиваем стрелку

        if (ToggleScript.isOn ^ is100) // если эти переменные разные, состояние изменилось -> запускаем ToggleClock()
            ToggleClock();
    }

    void ToggleClock()
    {
        is100 = !is100;

        if (is100) // время ускорено
        {
            Image.sprite = ImgOn;
            currentSpeed = highSpeed;
        }
        else // время нормальное
        {
            Image.sprite = ImgOff;
            currentSpeed = lowSpeed;
        }
    }

}
