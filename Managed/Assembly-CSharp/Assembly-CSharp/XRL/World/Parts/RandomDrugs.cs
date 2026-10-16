using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EF4 RID: 3828
	[Serializable]
	public class RandomDrugs : IPart
	{
		// Token: 0x060098EF RID: 39151 RVA: 0x0037821A File Offset: 0x0037641A
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060098F0 RID: 39152 RVA: 0x00378220 File Offset: 0x00376420
		public void AddRandomMed()
		{
			int num = Stat.Random(1, 8);
			if (num == 1)
			{
				this.ParentObject.ApplyEffect(new Blaze_Tonic(1000), null);
			}
			if (num == 2)
			{
				this.ParentObject.ApplyEffect(new HulkHoney_Tonic(1000), null);
			}
			if (num == 3)
			{
				this.ParentObject.ApplyEffect(new Rubbergum_Tonic(1000), null);
			}
			if (num == 4)
			{
				this.ParentObject.ApplyEffect(new Salve_Tonic(30), null);
			}
			if (num == 5)
			{
				this.ParentObject.ApplyEffect(new ShadeOil_Tonic(1000), null);
			}
			if (num == 6)
			{
				this.ParentObject.ApplyEffect(new Skulk_Tonic(1000), null);
			}
			if (num == 7)
			{
				this.ParentObject.ApplyEffect(new SphynxSalt_Tonic(1000), null);
			}
			if (num == 8)
			{
				this.ParentObject.ApplyEffect(new Ubernostrum_Tonic(30), null);
			}
		}

		// Token: 0x060098F1 RID: 39153 RVA: 0x00378305 File Offset: 0x00376505
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x060098F2 RID: 39154 RVA: 0x0037831C File Offset: 0x0037651C
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			if (this.bFirst)
			{
				this.bFirst = false;
				if (this.Shrooms)
				{
					this.ParentObject.ApplyEffect(new Hoarshroom_Tonic(1000), null);
					this.ParentObject.ApplyEffect(new Hoarshroom_Tonic(1000), null);
					this.ParentObject.ApplyEffect(new Blaze_Tonic(1000), null);
					this.ParentObject.ApplyEffect(new SphynxSalt_Tonic(1000), null);
				}
				else
				{
					int num = Stat.Roll(this.Number, null);
					for (int i = 0; i < num; i++)
					{
						this.AddRandomMed();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003FB5 RID: 16309
		public bool Shrooms;

		// Token: 0x04003FB6 RID: 16310
		public bool bFirst = true;

		// Token: 0x04003FB7 RID: 16311
		public string Number = "1d2+1";
	}
}
