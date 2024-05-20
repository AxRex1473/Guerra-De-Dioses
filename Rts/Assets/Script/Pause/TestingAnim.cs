using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingAnim : MonoBehaviour
{
    [SerializeField] private PauseGame gameUIref;

    //Una vez que termine la animación se ejecuta la Función AnimDone() en el PauseGame.
    public void TriggerEvent()
    {
        gameUIref.AnimDone();
    }

}
