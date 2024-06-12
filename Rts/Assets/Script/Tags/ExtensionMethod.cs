using UnityEngine;

public static class ExtensionMethod
{
    public static bool HasTag(this GameObject gameObject, Tag t)
    {
        if (gameObject.TryGetComponent<Tags>(out var tags))
        {
            return tags.HasTag(t);
        }
        return false;
    } 

    public static bool HasTag(this GameObject gameObject,string TagName)
    {
        //Esto es lo mismo que el de arriba checa si tiene alguna tag y si no retorna false
        return gameObject.TryGetComponent<Tags>(out var tags) && tags.HasTag(TagName);
    }
}
