using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameLoot))]
public class AddNewLoot : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("Remember to add names to ENUM and add effect to Coroutine.");
        GameLoot gameloot = (GameLoot)target;
        
        if (GUILayout.Button("Create Loot"))
        {
            //gameloot.AddLootItemToList();
            gameloot.NewLootItem(gameloot.lootname, gameloot.lootdesc, gameloot.lootsprite, gameloot.loottype, gameloot.lootrarity);
        }
        
    }
}
