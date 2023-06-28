using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerClickHandler
{
    private Hero player;

    private void Start()
    {
        player = FindObjectOfType<Hero>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        player.OnTouch();
    }
}
