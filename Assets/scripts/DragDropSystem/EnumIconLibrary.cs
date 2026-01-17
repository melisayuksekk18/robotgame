using System;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/Enum Icon Library")]
public class EnumIconLibrary : ScriptableObject
{
    [Serializable] public struct Entry { public controller.State key; public Sprite icon; }
    [SerializeField] private Entry[] entries;

    public Sprite Get(controller.State key)
    {
        for (int i = 0; i < entries.Length; i++)
            if (entries[i].key == key) return entries[i].icon;
        return null;
    }
}
