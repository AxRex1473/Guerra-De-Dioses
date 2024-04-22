using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    // Lista de todas las unidades disponibles en el juego
    public List<GameObject> UnitList = new List<GameObject>();
    // Lista de unidades seleccionadas por el jugador
    public List<GameObject> unitsSelected = new List<GameObject>();

    // Instancia estática de la clase UnitSelections
    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

    private void Awake()
    {
        // Si ya existe una instancia de UnitSelections y no es esta, se destruye esta instancia
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this; // Si no existe, se asigna esta instancia como la única
        }
    }

    // Método para seleccionar una unidad haciendo clic en ella
    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll(); // Deseleccionar todas las unidades previamente seleccionadas
        unitsSelected.Add(unitToAdd); // Agregar la unidad seleccionada a la lista de unidades seleccionadas
        unitToAdd.transform.GetChild(0).gameObject.SetActive(true); // Activar un indicador visual de selección
        unitToAdd.GetComponent<UnitMovement>().enabled = true; // Habilitar el movimiento de la unidad
    }

    // Otros métodos para manejar la selección y deselección de unidades
    public void ShiftClickSelect(GameObject unitToAdd)
    {
        // Implementación similar a ClickSelect, pero permite mantener unidades previamente seleccionadas
    }

    public void DragSelect(GameObject unitToAdd)
    {
        // Implementación similar a ClickSelect, pero para selección mediante arrastre
    }

    public void DeselectAll()
    {
        // Deseleccionar todas las unidades previamente seleccionadas
        foreach (var unit in unitsSelected)
        {
            unit.GetComponent<UnitMovement>().enabled = false; // Deshabilitar el movimiento de la unidad
            unit.transform.GetChild(0).gameObject.SetActive(false); // Desactivar el indicador visual de selección
        }

        unitsSelected.Clear(); // Limpiar la lista de unidades seleccionadas
    }

    public void Deselect(GameObject unitToDeselect)
    {
        // Método opcional para deseleccionar una unidad específica
    }
}
