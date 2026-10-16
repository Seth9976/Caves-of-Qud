using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007CD RID: 1997
	[GameEvent(Base = true)]
	public abstract class IInventoryActionsEvent : MinEvent
	{
		// Token: 0x0600508D RID: 20621 RVA: 0x00187044 File Offset: 0x00185244
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x0018704D File Offset: 0x0018524D
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Actions = null;
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x0018706C File Offset: 0x0018526C
		public bool AddAction(string Name, string Display = null, string Command = null, string PreferToHighlight = null, char Key = ' ', bool FireOnActor = false, int Default = 0, int Priority = 0, bool Override = false, bool WorksAtDistance = false, bool WorksTelekinetically = false, bool WorksTelepathically = false, bool AsMinEvent = true, GameObject FireOn = null, bool ReturnToModernUI = false)
		{
			if (this.Actions == null)
			{
				this.Actions = new Dictionary<string, InventoryAction>();
			}
			else if (!Override && this.Actions.ContainsKey(Name))
			{
				return false;
			}
			InventoryAction inventoryAction = new InventoryAction();
			inventoryAction.Name = Name;
			inventoryAction.Key = Key;
			inventoryAction.Display = Display;
			inventoryAction.Command = Command;
			inventoryAction.PreferToHighlight = PreferToHighlight;
			inventoryAction.Default = Default;
			inventoryAction.Priority = Priority;
			inventoryAction.FireOnActor = FireOnActor;
			inventoryAction.WorksAtDistance = WorksAtDistance;
			inventoryAction.WorksTelekinetically = WorksTelekinetically;
			inventoryAction.WorksTelepathically = WorksTelepathically;
			inventoryAction.AsMinEvent = AsMinEvent;
			inventoryAction.FireOn = FireOn;
			inventoryAction.ReturnToModernUI = ReturnToModernUI;
			this.Actions[Name] = inventoryAction;
			return true;
		}

		// Token: 0x04002811 RID: 10257
		public GameObject Actor;

		// Token: 0x04002812 RID: 10258
		public GameObject Object;

		// Token: 0x04002813 RID: 10259
		public Dictionary<string, InventoryAction> Actions;
	}
}
