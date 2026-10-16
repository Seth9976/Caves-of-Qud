using System;

namespace XRL.World.Parts
{
	// Token: 0x020010B3 RID: 4275
	[Serializable]
	public class Thurible : IPart
	{
		// Token: 0x0600AA52 RID: 43602 RVA: 0x003E4BFF File Offset: 0x003E2DFF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEarlyEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<IdleQueryEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600AA53 RID: 43603 RVA: 0x003E4C38 File Offset: 0x003E2E38
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Incense", "incense", Thurible.COMMAND_NAME, null, 'i', false, 10, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA54 RID: 43604 RVA: 0x003E4C70 File Offset: 0x003E2E70
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == Thurible.COMMAND_NAME && this.SmokeThurible(E.Actor, !E.Auto))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA55 RID: 43605 RVA: 0x003E4CA8 File Offset: 0x003E2EA8
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return false;
		}

		// Token: 0x0600AA56 RID: 43606 RVA: 0x003E4CAB File Offset: 0x003E2EAB
		public override bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			return !this.SmokeThurible(E.Actor, false);
		}

		// Token: 0x0600AA57 RID: 43607 RVA: 0x003E4CC0 File Offset: 0x003E2EC0
		public override bool HandleEvent(IdleQueryEvent E)
		{
			return (!E.Actor.HasPart<Brain>() || E.Actor.HasPart<Robot>() || E.Actor.DistanceTo(this.ParentObject) > 1 || !10.in100() || !this.SmokeThurible(E.Actor, false)) && base.HandleEvent(E);
		}

		// Token: 0x0600AA58 RID: 43608 RVA: 0x003E4D1C File Offset: 0x003E2F1C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AA59 RID: 43609 RVA: 0x003E4D1F File Offset: 0x003E2F1F
		public bool CanPuff()
		{
			return true;
		}

		// Token: 0x0600AA5A RID: 43610 RVA: 0x003E4D24 File Offset: 0x003E2F24
		public bool SmokeThurible(GameObject Actor, bool FromDialog)
		{
			this.CanPuff();
			if (!BeforeConsumeEvent.Check(Actor, Actor, this.ParentObject, false, false, false, false, false, true))
			{
				return false;
			}
			base.PlayWorldSound("sfx_interact_hookah_smoke", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			IComponent<GameObject>.XDidYToZ(Actor, "incense", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, FromDialog, false, null);
			Actor.UseEnergy(1000, "Item", null, null, false);
			for (int i = 2; i < 16; i++)
			{
				this.ParentObject.SmokePuff();
			}
			Event @event = Event.New("Smoked", "Actor", Actor, "Object", this.ParentObject);
			this.ParentObject.FireEvent(@event);
			Actor.FireEvent(@event);
			AfterConsumeEvent.Send(Actor, Actor, this.ParentObject, false, false, false, false, false, true);
			return true;
		}

		// Token: 0x04004628 RID: 17960
		public static readonly string COMMAND_NAME = "SmokeThurible";
	}
}
