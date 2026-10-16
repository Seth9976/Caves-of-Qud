using System;
using XRL.Messages;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013EC RID: 5100
	[Serializable]
	public class HeightenedIntelligence : BaseMutation
	{
		// Token: 0x0600C559 RID: 50521 RVA: 0x0047D9B9 File Offset: 0x0047BBB9
		public HeightenedIntelligence()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C55A RID: 50522 RVA: 0x0047D9CC File Offset: 0x0047BBCC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C55B RID: 50523 RVA: 0x0047D9E2 File Offset: 0x0047BBE2
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C55C RID: 50524 RVA: 0x0047DA05 File Offset: 0x0047BC05
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C55D RID: 50525 RVA: 0x0047DA1A File Offset: 0x0047BC1A
		public int GetEgoPenalty(int Level)
		{
			if (Level >= 9)
			{
				return -3;
			}
			if (Level >= 5)
			{
				return -2;
			}
			return -1;
		}

		// Token: 0x0600C55E RID: 50526 RVA: 0x0047DA2C File Offset: 0x0047BC2C
		public override string GetDescription()
		{
			return "You possess extraordinary analytical prowess but you find difficulty in relating to others.";
		}

		// Token: 0x0600C55F RID: 50527 RVA: 0x0047DA34 File Offset: 0x0047BC34
		public override string GetLevelText(int Level)
		{
			string text = (2 + (Level - 1) / 2).Signed(false) + " Intelligence\n";
			text = text + this.GetEgoPenalty(Level).ToString() + " Ego\n";
			if (Level == this.BaseLevel)
			{
				text += "Small chance to reveal the entire map in a flash of insight";
			}
			else if (Level % 2 == 0)
			{
				text += "Increased chance to reveal the entire map in a flash of insight";
			}
			return text;
		}

		// Token: 0x0600C560 RID: 50528 RVA: 0x0047DAA0 File Offset: 0x0047BCA0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction" && this.ParentObject.IsPlayer() && this.ParentObject.Physics != null && this.ParentObject.Physics.CurrentCell != null && !this.ParentObject.Physics.CurrentCell.ParentZone.IsWorldMap() && Stat.Random(1, 10000) < (2 + base.Level) * this.ParentObject.Statistics["Intelligence"].Modifier)
			{
				MessageQueue.AddPlayerMessage("&CA flash of insight overcomes you!", null, true);
				this.ParentObject.Physics.CurrentCell.ParentZone.ExploreAll();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C561 RID: 50529 RVA: 0x0047DB70 File Offset: 0x0047BD70
		public override bool ChangeLevel(int NewLevel)
		{
			this.ParentObject.Statistics["Intelligence"].BaseValue -= this.Bonus;
			this.Bonus = 2 + (base.Level - 1) / 2;
			this.ParentObject.Statistics["Intelligence"].BaseValue += this.Bonus;
			this.ParentObject.Statistics["Ego"].BaseValue -= this.Penalty;
			this.Penalty = this.GetEgoPenalty(base.Level);
			this.ParentObject.Statistics["Ego"].BaseValue += this.Penalty;
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C562 RID: 50530 RVA: 0x0047DC44 File Offset: 0x0047BE44
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C563 RID: 50531 RVA: 0x0047DC50 File Offset: 0x0047BE50
		public override bool Unmutate(GameObject GO)
		{
			GO.Statistics["Intelligence"].BaseValue -= this.Bonus;
			GO.Statistics["Ego"].BaseValue -= this.Penalty;
			this.Bonus = 0;
			this.Penalty = 0;
			return base.Unmutate(GO);
		}

		// Token: 0x04004DF8 RID: 19960
		public int Bonus;

		// Token: 0x04004DF9 RID: 19961
		public int Penalty;
	}
}
