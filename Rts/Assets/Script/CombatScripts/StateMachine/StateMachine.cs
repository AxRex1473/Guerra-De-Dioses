using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public Stack<State> States { get; set; }
    private void Awake()
    {
        States = new Stack<State>();
    }
    private void OnEnable()
    {
        
    }

    private void Update()
    {
        if (GetCurrentState() != null)
        {
            GetCurrentState().Execute();
        }
    }
    public void PushState(System.Action active, System.Action onEnter, System.Action onExit) //Metodo para agregar un nuevo estado a la pila
    {

        if (GetCurrentState() != null)
            GetCurrentState().OnExit();

        State state = new State(active, onEnter, onExit);
        States.Push(state);
        GetCurrentState().OnEnter();

    }
    public void PopState() //Aqui eliminamos el estado actual de la pila 
    {
        GetCurrentState().OnExit();
        GetCurrentState().ActiveAction = null;
        States.Pop();
        GetCurrentState().OnEnter(); //establecemos el nuevo estado en OnEnter 
    }
    private State GetCurrentState()
    {
        return States.Count > 0 ? States.Peek() : null; //Verifica si hay algun "state" en la pila, si esto es falso entonces regresa null
    }
}
