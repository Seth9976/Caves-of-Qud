using System;
using XRL.Messages;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013D7 RID: 5079
	[Serializable]
	public class PackRat : BaseMutation
	{
		// Token: 0x0600C443 RID: 50243 RVA: 0x00477C5B File Offset: 0x00475E5B
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C444 RID: 50244 RVA: 0x00477C5E File Offset: 0x00475E5E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("BeginDrop");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C445 RID: 50245 RVA: 0x00477C89 File Offset: 0x00475E89
		public override string GetDescription()
		{
			return "You compulsively lug around everything you can.\n\nYou must maintain at least 90% of your carry capacity.\n\nYou cannot drop items if dropping them would reduce your weight beneath this requirement.\n\nYou suffer one point of damage each round you do not maintain this requirement.\n\nYou can only drop one set of items every 10 rounds.";
		}

		// Token: 0x0600C446 RID: 50246 RVA: 0x00477C90 File Offset: 0x00475E90
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C447 RID: 50247 RVA: 0x00477C98 File Offset: 0x00475E98
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && !this.Entered)
			{
				Inventory inventory = this.ParentObject.Inventory;
				int maxCarriedWeight = this.ParentObject.GetMaxCarriedWeight();
				while (this.ParentObject.GetCarriedWeight() < (int)((double)maxCarriedWeight * 0.9))
				{
					inventory.AddObject(PopulationManager.CreateOneFrom("Junk 1", null, 0, 0, null, null, null, null, null), null, false, false, true, null);
				}
				this.Entered = true;
				this.ParentObject.UnregisterPartEvent(this, "EnteredCell");
			}
			if (E.ID == "BeginTakeAction")
			{
				int maxCarriedWeight2 = this.ParentObject.GetMaxCarriedWeight();
				if (this.ParentObject.GetCarriedWeight() < (int)((double)maxCarriedWeight2 * 0.9) && this.ParentObject.IsPlayer())
				{
					MessageQueue.AddPlayerMessage("&RYou must collect more junk! (minimum: " + ((int)((double)maxCarriedWeight2 * 0.9)).ToString() + " lbs.)", null, true);
					Statistic statistic = this.ParentObject.Statistics["Hitpoints"];
					int penalty = statistic.Penalty;
					statistic.Penalty = penalty + 1;
				}
				if (this.DropCooldown > 0)
				{
					this.DropCooldown--;
				}
				return true;
			}
			if (!(E.ID == "BeginDrop"))
			{
				return true;
			}
			if (E.HasFlag("Forced") || E.HasFlag("ForEquip"))
			{
				return true;
			}
			Inventory inventory2 = this.ParentObject.Inventory;
			Body body = this.ParentObject.Body;
			int maxCarriedWeight3 = this.ParentObject.GetMaxCarriedWeight();
			int carriedWeight = this.ParentObject.GetCarriedWeight();
			GameObject gameObject = E.GetParameter("Object") as GameObject;
			if (carriedWeight - gameObject.Weight < (int)((double)maxCarriedWeight3 * 0.9))
			{
				if (this.ParentObject.IsPlayer())
				{
					Popup.Show("That wouldn't leave you with NEARLY enough junk! You can't drop that!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			if (this.DropCooldown == 0)
			{
				this.DropCooldown = 10;
				return true;
			}
			if (this.ParentObject.IsPlayer())
			{
				Popup.Show("You must wait " + this.DropCooldown.Things("more turn", "more turns") + " to work up the willpower to drop something!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return false;
		}

		// Token: 0x0600C448 RID: 50248 RVA: 0x00477EDE File Offset: 0x004760DE
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C449 RID: 50249 RVA: 0x00477EE7 File Offset: 0x004760E7
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C44A RID: 50250 RVA: 0x00477EF1 File Offset: 0x004760F1
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}

		// Token: 0x04004DD1 RID: 19921
		public int DropCooldown;

		// Token: 0x04004DD2 RID: 19922
		public bool Entered;
	}
}
