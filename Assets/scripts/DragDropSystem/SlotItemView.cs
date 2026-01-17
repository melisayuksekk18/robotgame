using UnityEngine;
using UnityEngine.UI;

public class SlotItemView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private controller.State value;

    public controller.State Value => value;

    public void Set(controller.State v, Sprite sprite = null)
    {
        value = v;
        if (icon && sprite) icon.sprite = sprite;
    }
}
