using System;

[Serializable]
public struct AnimalStruct : IAnimal
{
    public string name;
    public void Feed() => throw new NotImplementedException();
}
