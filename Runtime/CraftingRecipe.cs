using System.Collections.Generic;
using UnityEngine;
using MM.Systems.InventorySystem;

namespace MM.Systems.CraftingSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Crafting recipe", menuName = "ScriptableObjects/MM CraftingSystem/Crafting Recipe", order = 0)]
    public class CraftingRecipe : ScriptableObject
    {
        [Header("Recipe-General")]
        public new string name;
        public string description;

        [Header("Recipe-Data")]
        public List<ItemData> inElements;
        [Space]
        public List<ItemData> outElements;
    }
}
