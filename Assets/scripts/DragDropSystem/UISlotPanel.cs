using UnityEngine;

public class UISlotPanel : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private UISlot[] slots;

    [Header("Spawn")]
    [SerializeField] private SlotItemView slotItemPrefab;

    [Header("Optional icon map")]
    [SerializeField] private EnumIconLibrary iconLibrary;

    public void TryDropToSlot(UISlot slot, controller.State value)
    {
        if (!slot.CanPlace()) return;

        if (slot.IsOccupied) slot.Clear();

        var spawned = Instantiate(slotItemPrefab, slot.transform);
        var icon = iconLibrary ? iconLibrary.Get(value) : null;
        spawned.Set(value, icon);

        slot.SetItem(spawned);
    }
}
