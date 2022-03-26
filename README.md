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

https://github.com/TextusGames/UnitySerializedReferenceUI.git

<img width="464" alt="image" src="https://user-images.githubusercontent.com/34438607/160235251-c6af2ee5-694d-4b38-9ab4-0ddca73f686b.png">

<img width="230" alt="image" src="https://user-images.githubusercontent.com/34438607/160235894-8a1e2bd9-fe93-463d-aa6f-6d4634cc7457.png">




Or add id to your `packages.json` file manually (located inside the project's Packages folder).
```json
"com.textus-games.serialized-reference-ui": "https://github.com/TextusGames/UnitySerializedReferenceUI.git"
```


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
