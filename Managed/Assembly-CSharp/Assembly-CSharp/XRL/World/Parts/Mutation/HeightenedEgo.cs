using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013EB RID: 5099
	[Serializable]
	public class HeightenedEgo : BaseMutation
	{
		// Token: 0x0600C54F RID: 50511 RVA: 0x0047D830 File Offset: 0x0047BA30
		public HeightenedEgo()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C550 RID: 50512 RVA: 0x0047D843 File Offset: 0x0047BA43
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C551 RID: 50513 RVA: 0x0047D859 File Offset: 0x0047BA59
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C552 RID: 50514 RVA: 0x0047D87C File Offset: 0x0047BA7C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C553 RID: 50515 RVA: 0x0047D886 File Offset: 0x0047BA86
		public override string GetDescription()
		{
			return "You possess a towering vision of self that you project onto the minds of nearby creatures.";
		}

		// Token: 0x0600C554 RID: 50516 RVA: 0x0047D88D File Offset: 0x0047BA8D
		public int GetAlertRadius(int Level)
		{
			if (Level < 5)
			{
				return 5;
			}
			if (Level < 9)
			{
				return 10;
			}
			return 15;
		}

		// Token: 0x0600C555 RID: 50517 RVA: 0x0047D8A0 File Offset: 0x0047BAA0
		public override string GetLevelText(int Level)
		{
			string text = (2 + (Level - 1) / 2).Signed(false) + " Ego\n";
			text = text + "Creatures within a radius of " + this.GetAlertRadius(Level).ToString() + " are alerted to your presence [unimplemented]\n";
			if (Level == this.BaseLevel)
			{
				text += "Small chance to frighten an adjacent enemy [unimplemented]";
			}
			else if (Level % 2 == 0)
			{
				text += "Increased chance to frighten an adjacent enemy [unimplemented]";
			}
			return text;
		}

		// Token: 0x0600C556 RID: 50518 RVA: 0x0047D910 File Offset: 0x0047BB10
		public override bool ChangeLevel(int NewLevel)
		{
			this.ParentObject.Statistics["Ego"].BaseValue -= this.Bonus;
			this.Bonus = 2 + (base.Level - 1) / 2;
			this.ParentObject.Statistics["Ego"].BaseValue += this.Bonus;
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C557 RID: 50519 RVA: 0x0047D984 File Offset: 0x0047BB84
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C558 RID: 50520 RVA: 0x0047D98E File Offset: 0x0047BB8E
		public override bool Unmutate(GameObject GO)
		{
			GO.Statistics["Ego"].BaseValue -= this.Bonus;
			return base.Unmutate(GO);
		}

		// Token: 0x04004DF7 RID: 19959
		public int Bonus;
	}
}
