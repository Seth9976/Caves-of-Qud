using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000FF4 RID: 4084
	[Serializable]
	public class AjiConch : IPart
	{
		// Token: 0x0600A2CB RID: 41675 RVA: 0x003B7967 File Offset: 0x003B5B67
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A2CC RID: 41676 RVA: 0x003B796A File Offset: 0x003B5B6A
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, this.ParentObject.Equipped), this.GetCooldown(), 0);
		}

		// Token: 0x0600A2CD RID: 41677 RVA: 0x003B7990 File Offset: 0x003B5B90
		public int GetCooldown()
		{
			return 150;
		}

		// Token: 0x0600A2CE RID: 41678 RVA: 0x003B7998 File Offset: 0x003B5B98
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveItemListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600A2CF RID: 41679 RVA: 0x003B79E9 File Offset: 0x003B5BE9
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject.Equipped);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2D0 RID: 41680 RVA: 0x003B7A18 File Offset: 0x003B5C18
		public override bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			if (E.Actor == this.ParentObject.Equipped && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && E.Distance <= 4 && !this.CommandID.IsNullOrEmpty())
			{
				E.Add("ActivateAjiConch", 1, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2D1 RID: 41681 RVA: 0x003B7A80 File Offset: 0x003B5C80
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(E.Part))
			{
				this.ActivatedAbilityID = E.Actor.AddDynamicCommand(out this.CommandID, "ActivateAjiConch", "Blow Aji Conch", "Items", null, "\a", null, false, false, false, false, false, false, false, false, true, true, false, true, -1, null, null, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2D2 RID: 41682 RVA: 0x003B7AE5 File Offset: 0x003B5CE5
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			E.Actor.UnregisterPartEvent(this, this.CommandID);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2D3 RID: 41683 RVA: 0x003B7B12 File Offset: 0x003B5D12
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.CommandID)
			{
				this.ActivateAjiConch();
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2D4 RID: 41684 RVA: 0x003B7B3C File Offset: 0x003B5D3C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (E.Actor == this.ParentObject.Equipped && E.Actor.IsActivatedAbilityUsable(this.ActivatedAbilityID))
			{
				E.AddAction("Blow", "blow", "ActivateAjiConch", null, 'b', false, 20, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2D5 RID: 41685 RVA: 0x003B7B9A File Offset: 0x003B5D9A
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateAjiConch")
			{
				this.ActivateAjiConch();
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2D6 RID: 41686 RVA: 0x003B7BC1 File Offset: 0x003B5DC1
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A2D7 RID: 41687 RVA: 0x003B7BC4 File Offset: 0x003B5DC4
		private void ActivateAjiConch()
		{
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped == null || !equipped.IsActivatedAbilityUsable(this.ActivatedAbilityID))
			{
				return;
			}
			PopulationResult populationResult = PopulationManager.GenerateOne("DynamicObjectsTable:AjiConch", null, null, null, null, null);
			string text = ((populationResult != null) ? populationResult.Blueprint : null);
			if (text.IsNullOrEmpty())
			{
				return;
			}
			List<Cell> list = equipped.Physics.PickCone(4, 30, AllowVis.Any, null, "Blow Aji Conch");
			if (list == null)
			{
				return;
			}
			GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(text);
			bool flag = blueprint.GetTag("GasGenerationAddSeeping", "").EqualsNoCase("true") || blueprint.GetPartParameter<bool>("Gas", "Seeping", false);
			if (!flag)
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					if (list[i].IsSolid())
					{
						list.RemoveAt(i);
					}
				}
			}
			if (list.Count == 0)
			{
				list.Add(equipped.CurrentCell);
			}
			equipped.CooldownActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(), null, false);
			equipped.PlayWorldSound("Sounds/Interact/sfx_interact_conch_blow", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			IComponent<GameObject>.XDidY(equipped, "blow", "into the conch of the Aji", null, null, null, equipped, null, false, false, null, null, false, true, false, false, false, null);
			Event @event = Event.New("CreatorModifyGas", "Gas", null);
			foreach (Cell cell in list)
			{
				GameObject gameObject = GameObjectFactory.Factory.CreateObject(blueprint, 0, 0, null, null, null, null, null);
				Gas part = gameObject.GetPart<Gas>();
				if (flag)
				{
					part.Seeping = true;
				}
				part.Creator = equipped;
				part.Density = 800 / list.Count;
				part.Level = 5;
				@event.SetParameter("Gas", part);
				equipped.FireEvent(@event);
				cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				The.Core.RenderDelay(25, false, true, false);
			}
		}

		// Token: 0x04004375 RID: 17269
		public const string ABILITY_NAME = "Blow Aji Conch";

		// Token: 0x04004376 RID: 17270
		public const string COMMAND_NAME = "ActivateAjiConch";

		// Token: 0x04004377 RID: 17271
		public const int CONE_LENGTH = 4;

		// Token: 0x04004378 RID: 17272
		public const int CONE_ANGLE = 30;

		// Token: 0x04004379 RID: 17273
		public const int GAS_DENSITY = 800;

		// Token: 0x0400437A RID: 17274
		public const int GAS_LEVEL = 5;

		// Token: 0x0400437B RID: 17275
		public string CommandID;

		// Token: 0x0400437C RID: 17276
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
