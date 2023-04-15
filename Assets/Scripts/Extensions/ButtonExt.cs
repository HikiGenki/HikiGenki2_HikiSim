using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public static class ButtonExt { 

    public static void SetupVerticalNavigation(this List<Button> buttons) {
        for(int i = 0; i < buttons.Count; i++) {
            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit;
            //Up
            if(i != 0) {
                nav.selectOnUp = buttons[i-1];
            }
            //Down
            if(i != buttons.Count - 1) {
                nav.selectOnDown = buttons[i + 1];
            }

            buttons[i].navigation = nav;
        }
    }

    public static void SetupHorizontalNavigation(this List<Button> buttons) {
        for(int i = 0; i < buttons.Count; i++) {
            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit;
            //Left
            if(i != 0) {
                nav.selectOnLeft = buttons[i - 1];
            }
            //Right
            if(i != buttons.Count - 1) {
                nav.selectOnRight = buttons[i + 1];
            }

            buttons[i].navigation = nav;
        }
    }

    public static void SetDownNavigation(this Button button, Selectable onDownSelectable) {
        Navigation nav = button.navigation;
        nav.selectOnDown = onDownSelectable;
        button.navigation = nav;
    }

    public static void SetUpNavigation(this Button button, Selectable onUpSelectable) {
        Navigation nav = button.navigation;
        nav.selectOnUp = onUpSelectable;
        button.navigation = nav;
    }

    public static void SetLeftNavigation(this Button button, Selectable onLeftSelectable) {
        Navigation nav = button.navigation;
        nav.selectOnLeft = onLeftSelectable;
        button.navigation = nav;
    }
    public static void SetRightNavigation(this Button button, Selectable onRightSelectable) {
        Navigation nav = button.navigation;
        nav.selectOnRight = onRightSelectable;
        button.navigation = nav;
    }

    public static EventTrigger GetOrAddEventTrigger(this Button button) {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger) {
            return trigger;
        } else {
            return button.gameObject.AddComponent<EventTrigger>();
        }
    }

    public static void DoOnSelect(this EventTrigger trigger, UnityAction<BaseEventData> action) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Select;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }
    public static void DoOnDeselect(this EventTrigger trigger, UnityAction<BaseEventData> action) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Deselect;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }
}