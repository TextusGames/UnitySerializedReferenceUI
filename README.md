# UnitySerializedReferenceUI
The UI for Unity's SerealizedReference attribute. It allows to change the instance type of field right in editor.

Project is provided under Mit license which you can find in inner main folder ("SerializedReferenceUI")

Known limitations.
- Custom property drawer has no effect. Becouse property is drawn from custom attribute drawer custom property drawer is not applied (unity default behaviour).
- Can not serialized types derived from UnityEngine.Object (monbehaviour, scriptableObject) (unity's limitation).
- Can not create instance of class that does not have empty constructor(only have private for example).

Known Issues of serialized reference: 
- Renaming used type can produce data loss and throws unknown managed type exception. This is unity's bug.
- SerializeReference itself is not working properly with Prefab Instances;

Future plans:
Possibly
- copy / paste.
- menu with searchbar.

## Installation
Use the + inside the Package Manager window and add this URL:
```json
"com.textus-games.serialized-reference-ui": "https://github.com/popcron/UnitySerializedReferenceUI.git"
```
Or add id to your `packages.json` file manually (located inside the project's Packages folder).

## Example
![Woah UI woah woah!!!](https://cdn.discordapp.com/attachments/784916261871550494/847185548632260628/unknown.png)
```cs
[Serializable]
public class Slot
{
    [SerializeReference, SerializeReferenceButton]
    public Item item;
}

[Serializable]
public class Item {}
public class Metal : Item {}
public class Wood : Item {}
```

For more examples, this package contains two Samples that can be imported into your project.