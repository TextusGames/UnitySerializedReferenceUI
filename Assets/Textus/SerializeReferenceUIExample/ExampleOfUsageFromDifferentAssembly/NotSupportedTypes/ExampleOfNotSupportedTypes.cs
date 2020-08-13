using System;
using UnityEngine;

public class ExampleOfNotSupportedTypes 
{

}

[Serializable]
public class AnimalMonoBehaviour : MonoBehaviour, IAnimal
{
    public void Feed() => throw new NotImplementedException();
}

[Serializable]
public class AnimalScriptableObject : ScriptableObject, IAnimal
{
    public void Feed() => throw new NotImplementedException();
}

[Serializable]
public class AnimalPrivateConstructor : IAnimal
{
    private AnimalPrivateConstructor(){}
    public void Feed() => throw new NotImplementedException();
}

