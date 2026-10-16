using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013CD RID: 5069
	[Serializable]
	public class ColdBlooded : BaseMutation
	{
		// Token: 0x0600C3D6 RID: 50134 RVA: 0x00476895 File Offset: 0x00474A95
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600C3D7 RID: 50135 RVA: 0x00476898 File Offset: 0x00474A98
		// (set) Token: 0x0600C3D8 RID: 50136 RVA: 0x004768A0 File Offset: 0x00474AA0
		public int SpeedBonus
		{
			get
			{
				return this._SpeedBonus;
			}
			set
			{
				if (this._SpeedBonus == value)
				{
					return;
				}
				if (this._SpeedBonus > 0)
				{
					this.ParentObject.Statistics["Speed"].Bonus -= this._SpeedBonus;
				}
				if (this._SpeedBonus < 0)
				{
					this.ParentObject.Statistics["Speed"].Penalty -= -this._SpeedBonus;
				}
				this._SpeedBonus = value;
				if (this._SpeedBonus > 0)
				{
					this.ParentObject.Statistics["Speed"].Bonus += this._SpeedBonus;
				}
				if (this._SpeedBonus < 0)
				{
					this.ParentObject.Statistics["Speed"].Penalty += -this._SpeedBonus;
				}
			}
		}

		// Token: 0x0600C3D9 RID: 50137 RVA: 0x00476980 File Offset: 0x00474B80
		public override string GetDescription()
		{
			return "Your vitality depends on your temperature; at higher temperatures, you are more lively. At lower temperatures, you are more torpid.\n\nYour base quickness score is reduced by 10.\nYour quickness increases as your temperature increases and decreases as your temperature decreases.\n+100 reputation with {{w|unshelled reptiles}}";
		}

		// Token: 0x0600C3DA RID: 50138 RVA: 0x00476987 File Offset: 0x00474B87
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C3DB RID: 50139 RVA: 0x0047698E File Offset: 0x00474B8E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<EnvironmentalUpdateEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C3DC RID: 50140 RVA: 0x004769AC File Offset: 0x00474BAC
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("ice", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C3DD RID: 50141 RVA: 0x004769D4 File Offset: 0x00474BD4
		public override bool HandleEvent(EnvironmentalUpdateEvent E)
		{
			int temperature = this.ParentObject.Physics.Temperature;
			if (temperature == 25)
			{
				this.SpeedBonus = -10;
			}
			else if (temperature < 25)
			{
				double num = 90.0;
				int num2 = Math.Min(25 - temperature + 25, 3000);
				for (int i = 26; i <= num2; i++)
				{
					num -= 1250.0 / (double)((i + 25) * (i + 25));
				}
				this.SpeedBonus = (int)(num - 100.0);
			}
			else
			{
				double num3 = 90.0;
				for (int j = 26; j <= temperature; j++)
				{
					num3 += 1250.0 / (double)((j + 25) * (j + 25));
				}
				this.SpeedBonus = (int)(num3 - 100.0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C3DE RID: 50142 RVA: 0x00476AAE File Offset: 0x00474CAE
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C3DF RID: 50143 RVA: 0x00476AB7 File Offset: 0x00474CB7
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C3E0 RID: 50144 RVA: 0x00476AC1 File Offset: 0x00474CC1
		public override bool Unmutate(GameObject GO)
		{
			this.SpeedBonus = 0;
			return base.Unmutate(GO);
		}

		// Token: 0x04004DC2 RID: 19906
		public int _SpeedBonus;
	}
}
