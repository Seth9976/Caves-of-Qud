using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000ED7 RID: 3799
	[Serializable]
	public class LootOnStep : IPart
	{
		// Token: 0x06009816 RID: 38934 RVA: 0x00373EA2 File Offset: 0x003720A2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x06009817 RID: 38935 RVA: 0x00373EB8 File Offset: 0x003720B8
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Object.IsPlayerControlled() && (this.ParentObject.IsAlliedTowards(E.Object) || (!this.RequiresAllied && (!this.NotIfHostile || !this.ParentObject.IsHostileTowards(E.Object)))) && this.ParentObject.PhaseAndFlightMatches(E.Object))
			{
				if (this.Chance.in100())
				{
					GameObject anItem = EncountersAPI.GetAnItem(new Predicate<GameObjectBlueprint>(this.IsSuitableLoot));
					if (anItem != null)
					{
						this.ParentObject.EmitMessage(this.SuccessMessage, anItem, null, true);
						this.SteppedOn(E.Object, E.Forced || E.Dragging != null);
						E.Cell.AddObject(anItem, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
				else
				{
					this.ParentObject.EmitMessage(this.FailMessage, E.Object, null, false);
					this.SteppedOn(E.Object, E.Forced || E.Dragging != null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009818 RID: 38936 RVA: 0x00373FE0 File Offset: 0x003721E0
		public void SteppedOn(GameObject By, bool Accidental = false)
		{
			this.ParentObject.Die(By, null, "You were stepped on.", this.ParentObject.Does("were", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " @@stepped on.", Accidental, null, null, false, false, "", null, null);
		}

		// Token: 0x06009819 RID: 38937 RVA: 0x00374047 File Offset: 0x00372247
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600981A RID: 38938 RVA: 0x0037404A File Offset: 0x0037224A
		private bool IsSuitableLoot(GameObjectBlueprint BP)
		{
			return this.MaxWeight <= 0 || BP.GetPartParameter<int>("Physics", "Weight", 0) <= this.MaxWeight;
		}

		// Token: 0x04003F56 RID: 16214
		public string SuccessMessage = "=subject.T==subject.directionIfAny= =verb:flex= and =verb:splinter= apart, revealing =object.an=.";

		// Token: 0x04003F57 RID: 16215
		public string FailMessage = "=subject.T==subject.directionIfAny= =verb:flex= and =verb:splinter= apart.";

		// Token: 0x04003F58 RID: 16216
		public int MaxWeight = 150;

		// Token: 0x04003F59 RID: 16217
		public int Chance = 100;

		// Token: 0x04003F5A RID: 16218
		public bool RequiresAllied = true;

		// Token: 0x04003F5B RID: 16219
		public bool NotIfHostile = true;
	}
}
