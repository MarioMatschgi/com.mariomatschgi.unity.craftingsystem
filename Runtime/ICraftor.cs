using MM.Systems.InventorySystem;

namespace MM.Systems.CraftingSystem
{
	public interface ICraftor : IInteractor
	{
		CraftingScreenUi craftingScreen { get; set; }
	}
}