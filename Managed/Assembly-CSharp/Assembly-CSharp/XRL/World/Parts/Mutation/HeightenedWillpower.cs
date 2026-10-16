using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013ED RID: 5101
	[Serializable]
	public class HeightenedWillpower : BaseMutation
	{
		// Token: 0x0600C564 RID: 50532 RVA: 0x0047DCB6 File Offset: 0x0047BEB6
		public HeightenedWillpower()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C565 RID: 50533 RVA: 0x0047DCC9 File Offset: 0x0047BEC9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C566 RID: 50534 RVA: 0x0047DCDF File Offset: 0x0047BEDF
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("salt", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C567 RID: 50535 RVA: 0x0047DD02 File Offset: 0x0047BF02
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C568 RID: 50536 RVA: 0x0047DD0C File Offset: 0x0047BF0C
		public override string GetDescription()
		{
			return "You are possessed of an indefatigable focus which every so often manifests itself as stubbornness.";
		}

		// Token: 0x0600C569 RID: 50537 RVA: 0x0047DD14 File Offset: 0x0047BF14
		public override string GetLevelText(int Level)
		{
			string text = (2 + (Level - 1) / 2).Signed(false) + " Willpower\n";
			if (Level == this.BaseLevel)
			{
				text += "Small chance that you stubbornly refuse to flee from a fight [unimplemented]\n";
			}
			else if (Level % 2 != 0)
			{
				text += "Increased chance that you stubbornly refuse to flee from a flight\n";
			}
			if (Level == this.BaseLevel)
			{
				text += "Small chance when you are injured to ignore all damage for the next 5 rounds [unimplemented]";
			}
			else if (Level % 2 == 0)
			{
				text += "Increased chance when you are injured to ignore all damage for the next 5 rounds [unimplemented]";
			}
			return text;
		}

		// Token: 0x0600C56A RID: 50538 RVA: 0x0047DD8C File Offset: 0x0047BF8C
		public override bool ChangeLevel(int NewLevel)
		{
			this.ParentObject.Statistics["Willpower"].BaseValue -= this.Bonus;
			this.Bonus = 2 + (base.Level - 1) / 2;
			this.ParentObject.Statistics["Willpower"].BaseValue += this.Bonus;
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C56B RID: 50539 RVA: 0x0047DE00 File Offset: 0x0047C000
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C56C RID: 50540 RVA: 0x0047DE0A File Offset: 0x0047C00A
		public override bool Unmutate(GameObject GO)
		{
			GO.Statistics["Willpower"].BaseValue -= this.Bonus;
			return base.Unmutate(GO);
		}

		// Token: 0x04004DFA RID: 19962
		public int Bonus;
	}
}
