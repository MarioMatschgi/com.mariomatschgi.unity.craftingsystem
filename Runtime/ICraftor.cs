using MM.Systems.InventorySystem;

namespace MM.Systems.CraftingSystem
{
	public interface ICraftor : IInteractor
	{
		CraftingPanelUi craftingScreen { get; set; }
	}
}