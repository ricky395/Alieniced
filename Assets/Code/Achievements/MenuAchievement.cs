using UnityEngine;
using System.Collections.Generic;

public static class MenuAchievement {

    static List<string> buttons = new List<string>(); //lista que lleva los nombres de los botones pulsados
    
    	
    public static void AddElement(string element)
    {
        if (!buttons.Contains(element))
        {
            buttons.Add(element);
            CheckAchievement();
        }
    }

    static void CheckAchievement()
    {
        if (buttons.Count == 7)
            GPlayclass.UnlockAchievement("CgkI-Meyi84DEAIQBw");
    }
}
