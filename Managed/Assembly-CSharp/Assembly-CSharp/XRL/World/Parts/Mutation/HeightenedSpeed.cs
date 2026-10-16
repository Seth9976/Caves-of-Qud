using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001447 RID: 5191
	[Serializable]
	public class HeightenedSpeed : BaseMutation
	{
		// Token: 0x0600CAB2 RID: 51890 RVA: 0x0049B699 File Offset: 0x00499899
		public HeightenedSpeed()
		{
			base.Type = "Physical";
		}

		// Token: 0x0600CAB3 RID: 51891 RVA: 0x0049B6AC File Offset: 0x004998AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CAB4 RID: 51892 RVA: 0x0049B6C2 File Offset: 0x004998C2
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CAB5 RID: 51893 RVA: 0x0049B6E5 File Offset: 0x004998E5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CAB6 RID: 51894 RVA: 0x0049B6EF File Offset: 0x004998EF
		public int GetSpeedBonus(int Level)
		{
			return 13 + 2 * Level;
		}

		// Token: 0x0600CAB7 RID: 51895 RVA: 0x0049B6F7 File Offset: 0x004998F7
		public override string GetDescription()
		{
			return "You are gifted with tremendous speed.";
		}

		// Token: 0x0600CAB8 RID: 51896 RVA: 0x0049B700 File Offset: 0x00499900
		public override string GetLevelText(int Level)
		{
			return "+{{rules|" + this.GetSpeedBonus(Level).ToString() + "}} Quickness";
		}

		// Token: 0x0600CAB9 RID: 51897 RVA: 0x0049B72B File Offset: 0x0049992B
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x0600CABA RID: 51898 RVA: 0x0049B734 File Offset: 0x00499934
		public override bool ChangeLevel(int NewLevel)
		{
			this.ParentObject.Statistics["Speed"].BaseValue -= this.Bonus;
			this.Bonus = this.GetSpeedBonus(base.Level);
			this.ParentObject.Statistics["Speed"].BaseValue += this.Bonus;
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CABB RID: 51899 RVA: 0x0049B7A8 File Offset: 0x004999A8
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CABC RID: 51900 RVA: 0x0049B7B2 File Offset: 0x004999B2
		public override bool Unmutate(GameObject GO)
		{
			GO.Statistics["Speed"].BaseValue -= this.Bonus;
			this.Bonus = 0;
			return base.Unmutate(GO);
		}

		// Token: 0x04004EF6 RID: 20214
		public int Bonus;

		// Token: 0x04004EF7 RID: 20215
		public int Penalty;
	}
}
