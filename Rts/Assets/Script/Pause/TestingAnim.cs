using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingAnim : MonoBehaviour
{
    [SerializeField] private PauseGame gameUIref;

    //Una vez que termine la animaci�n se ejecuta la Funci�n AnimDone() en el PauseGame.
    public void TriggerEvent()
    {
        gameUIref.AnimDone();
    }

}
