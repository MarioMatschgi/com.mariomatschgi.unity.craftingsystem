using UnityEngine;
using MM.Systems.InventorySystem;

namespace MM.Systems.CraftingSystem
{
	[AddComponentMenu("MM CraftingSystem/Example Player")]
	public class ExamplePlayer : MonoBehaviour, IInteractor, ICraftor
	{
		[SerializeField]
		private int playerId;
		public int interactorId { get { return playerId; } set { playerId = value; } }
		public InteractorInventoryUi inventoryUi { get; set; }

        public CraftingScreenUi craftingScreen { get; set; }


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/MM CraftingSystem/Example Player", false, 10)]
        public static void OnCreate()
        {
            // Create ExamplePlayer
            GameObject _invManagerObj = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Packages/com.mariomatschgi.unity.craftingsystem/Prefabs/ExamplePlayer.prefab",
                typeof(GameObject)));
            _invManagerObj.transform.name = "ExamplePlayer";
        }

        [UnityEditor.MenuItem("GameObject/MM CraftingSystem/Example Player 2D", false, 10)]
        public static void OnCreate2D()
        {
            // Create ExamplePlayer
            GameObject _invManagerObj = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Packages/com.mariomatschgi.unity.craftingsystem/Prefabs/ExamplePlayer2D.prefab",
                typeof(GameObject)));
            _invManagerObj.transform.name = "ExamplePlayer2D";
        }
#endif

		void Awake()
		{

		}

		void Start()
		{

		}

		void Update()
		{
			// Open / Close inventory
			if (Input.GetKeyDown(KeyCode.E))
            {
                inventoryUi.isInventoryOpen = !inventoryUi.isInventoryOpen;

                if (craftingScreen.isCraftingScreenOpen)
                    craftingScreen.isCraftingScreenOpen = false;
            }

			// Update hotbar index
			if (Input.GetKeyDown(KeyCode.Tab))
                // Only update if crafting menue is closed
                if (!craftingScreen.isCraftingScreenOpen)
				    inventoryUi.hotbarRowIdx++;

            // Open / Close craftingScreen
            if (Input.GetKeyDown(KeyCode.C))
            {
                craftingScreen.isCraftingScreenOpen = !craftingScreen.isCraftingScreenOpen;

                if (inventoryUi.isInventoryOpen)
                    inventoryUi.isInventoryOpen = false;
            }
        }

		#endregion

		#region Gameplay Methodes
		/*
         *
         *  Gameplay Methodes
         *
         */

		#endregion

		#region Helper Methodes
		/*
         *
         *  Helper Methodes
         * 
         */

		#endregion
	}
}