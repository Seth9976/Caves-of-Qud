using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x020011AE RID: 4526
	[Serializable]
	public class RootKnotInventory : IPart
	{
		// Token: 0x0600B3C7 RID: 46023 RVA: 0x0041948F File Offset: 0x0041768F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B3C8 RID: 46024 RVA: 0x00419492 File Offset: 0x00417692
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ZoneFreezing");
			Registrar.Register(PooledEvent<GetFeelingEvent>.ID, 0, false);
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B3C9 RID: 46025 RVA: 0x004194B4 File Offset: 0x004176B4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ZoneFreezing")
			{
				this.RemoveState();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B3CA RID: 46026 RVA: 0x004194D5 File Offset: 0x004176D5
		public void RemoveState()
		{
			this.ParentObject.Inventory.ClearOnDeath = true;
			this.ParentObject.Inventory.Objects = new List<GameObject>();
			this.ParentObject.Brain.SetPartyLeader(null, 0, false, true);
		}

		// Token: 0x0600B3CB RID: 46027 RVA: 0x00419514 File Offset: 0x00417714
		public void SetState()
		{
			Inventory inventory = this.ParentObject.Inventory;
			inventory.ClearOnDeath = false;
			inventory.Objects = The.Game.RequireSystem<RootKnotSystem>(() => new RootKnotSystem()).inventory;
			if (inventory.Objects.Count == 0)
			{
				inventory.CheckEmptyState();
			}
			else
			{
				inventory.CheckNonEmptyState();
			}
			if (this.ParentObject.Brain.PartyLeader != The.Player)
			{
				this.ParentObject.Brain.SetPartyLeader(The.Player, 0, true, true);
				this.ParentObject.Brain.ClearHostileMemory();
			}
		}

		// Token: 0x0600B3CC RID: 46028 RVA: 0x004195C2 File Offset: 0x004177C2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == ZoneActivatedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x0600B3CD RID: 46029 RVA: 0x004195F0 File Offset: 0x004177F0
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			this.RemoveState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3CE RID: 46030 RVA: 0x004195FF File Offset: 0x004177FF
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.SetState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3CF RID: 46031 RVA: 0x0041960E File Offset: 0x0041780E
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.SetState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3D0 RID: 46032 RVA: 0x0041961D File Offset: 0x0041781D
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.SetState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3D1 RID: 46033 RVA: 0x0041962C File Offset: 0x0041782C
		public override bool HandleEvent(GetFeelingEvent E)
		{
			E.Feeling = 100;
			return false;
		}
	}
}
