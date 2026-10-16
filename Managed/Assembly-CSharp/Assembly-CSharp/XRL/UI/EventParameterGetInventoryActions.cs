using System;
using System.Collections.Generic;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x0200149F RID: 5279
	public class EventParameterGetInventoryActions
	{
		// Token: 0x0600CF66 RID: 53094 RVA: 0x004BC51C File Offset: 0x004BA71C
		public EventParameterGetInventoryActions()
		{
			this.Actions = new Dictionary<string, InventoryAction>();
		}

		// Token: 0x0600CF67 RID: 53095 RVA: 0x004BC52F File Offset: 0x004BA72F
		public EventParameterGetInventoryActions(Dictionary<string, InventoryAction> Actions)
		{
			this.Actions = Actions ?? new Dictionary<string, InventoryAction>();
		}

		// Token: 0x0600CF68 RID: 53096 RVA: 0x004BC548 File Offset: 0x004BA748
		public bool AddAction(string Name, char Key, bool FireOnActor, string Display, string Command, string PreferToHighlight = null, int Default = 0, int Priority = 0, bool Override = false, bool WorksAtDistance = false, bool WorksTelekinetically = false, bool WorksTelepathically = false, bool AsMinEvent = false, GameObject FireOn = null)
		{
			if (!Override && this.Actions.ContainsKey(Name))
			{
				return false;
			}
			InventoryAction inventoryAction = new InventoryAction();
			inventoryAction.Name = Name;
			if (!ControlManager.isKeyMapped(Key, new List<string> { "UINav", "Menus" }))
			{
				inventoryAction.Key = Key;
			}
			inventoryAction.Display = Display;
			inventoryAction.Command = Command;
			inventoryAction.PreferToHighlight = PreferToHighlight;
			inventoryAction.Default = Default;
			inventoryAction.Priority = ((Priority == 0) ? (this.Actions.Count + 1) : Priority);
			inventoryAction.FireOnActor = FireOnActor;
			inventoryAction.WorksAtDistance = WorksAtDistance;
			inventoryAction.WorksTelekinetically = WorksTelekinetically;
			inventoryAction.WorksTelepathically = WorksTelepathically;
			inventoryAction.AsMinEvent = AsMinEvent;
			inventoryAction.FireOn = FireOn;
			this.Actions[Name] = inventoryAction;
			return true;
		}

		// Token: 0x0400508F RID: 20623
		public Dictionary<string, InventoryAction> Actions;
	}
}
