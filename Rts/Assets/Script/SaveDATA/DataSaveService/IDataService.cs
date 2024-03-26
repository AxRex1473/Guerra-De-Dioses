//Esta es una interfaz que ayudará a recolectar los datos del juegos para después guardarlos en el JSON
public interface IDataService 
{
    //bool para saber si guardo el dato o no, "T Data" es el tipo de dato que vaya a guardar
    bool SaveData<T>(string RelativePath, T Data, bool Encrypted);

    T LoadData<T>(string RelativePath,bool Encrypted);
}
