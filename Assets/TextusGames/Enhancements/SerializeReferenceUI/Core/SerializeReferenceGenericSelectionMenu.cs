#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
 
public static class SerializeReferenceGenericSelectionMenu
{
    /// Purpose.
    /// This is generic selection menu.
    /// Filtering. 
    /// You can add substring filter here to filter by search string.
    /// As well ass type or interface restrictions.
    /// As well as any custom restriction that is based on input type.
    /// And it will be performed on each Appropriate type found by TypeCache.
    public static void ShowContextMenuForManagedReference(this SerializedProperty property, IEnumerable<Func<Type,bool>> filters = null)
    { 
        var context = new GenericMenu();
        FillContextMenu(filters, context, property);
        context.ShowAsContext();
    }  
    
    
    private static void FillContextMenu(IEnumerable<Func<Type, bool>> enumerableFilters, GenericMenu contextMenu, SerializedProperty property)
    {
        var filters = enumerableFilters.ToList();// Prevents possible multiple enumerations
        
        // Adds "Make Null" menu command
        contextMenu.AddItem(new GUIContent("Null"), false, () => MakeSerializedPropertyNull(property));
        
        // Find real type of managed reference
        var realPropertyType = SerializeReferenceTypeNameUtility.GetRealTypeFromTypename(property.managedReferenceFieldTypename);
        if (realPropertyType == null)
        { 
            Debug.LogError("Can not get type from");
            return;
        }
         
        // Get and filter all appropriate types
        var types = TypeCache.GetTypesDerivedFrom(realPropertyType);
        foreach (var type in types)
        { 
            // Skips unity engine Objects (because they are not serialized by SerializeReference)
            if(type.IsSubclassOf(typeof(UnityEngine.Object)))
                continue;
            // Skip abstract classes because they should not be instantiated
            if(type.IsAbstract)    
                continue;
            // Filter types by provided filters if there is ones
            if (FilterTypeByFilters(filters, type) == false) 
                continue; 
                
            AddItemToContextMenu(type, contextMenu, property); 
        } 
    } 

    private static void MakeSerializedPropertyNull(SerializedProperty serializedProperty)
    {
        serializedProperty.serializedObject.Update();
        serializedProperty.managedReferenceValue = null;
        serializedProperty.serializedObject.ApplyModifiedPropertiesWithoutUndo(); // undo is bugged for now
    }
    
    private static void AddItemToContextMenu(Type type, GenericMenu genericMenuContext, SerializedProperty property)
    {
        var assemblyName =  type.Assembly.ToString().Split('(', ',')[0];
        var entryName = type + "  ( " + assemblyName + " )";
        genericMenuContext.AddItem(new GUIContent(entryName), false, AssignNewInstanceOfType, new AssignInstanceGenericMenuParameter(type, property));
    }
    
    private static void AssignNewInstanceOfType(object objectGenericMenuParameter )
    {
        var parameter = (AssignInstanceGenericMenuParameter) objectGenericMenuParameter;
        var type = parameter.Type;
        var property = parameter.Property;
        var instance = Activator.CreateInstance(type); 
        property.serializedObject.Update();
        property.managedReferenceValue = instance;
        property.serializedObject.ApplyModifiedPropertiesWithoutUndo(); // undo is bugged for now
    }   
    
    private static  bool FilterTypeByFilters (IEnumerable<Func<Type,bool>> filters, Type type) =>
        filters.All(f => f == null || f.Invoke(type));

    
    private readonly struct AssignInstanceGenericMenuParameter
    {
        public AssignInstanceGenericMenuParameter(Type type, SerializedProperty property)
        {
            Type = type;
            Property = property;
        }
        
        public readonly SerializedProperty Property;
        public readonly Type Type;
    }
} 

#endif