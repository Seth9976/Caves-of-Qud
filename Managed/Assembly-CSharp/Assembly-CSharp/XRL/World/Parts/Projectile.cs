using System;

namespace XRL.World.Parts
{
	// Token: 0x02001121 RID: 4385
	[Serializable]
	public class Projectile : IPart
	{
		// Token: 0x0600AD82 RID: 44418 RVA: 0x00403C1C File Offset: 0x00401E1C
		public override bool SameAs(IPart p)
		{
			Projectile projectile = p as Projectile;
			return projectile.BasePenetration == this.BasePenetration && projectile.StrengthPenetration == this.StrengthPenetration && projectile.PenetrateCreatures == this.PenetrateCreatures && projectile.PenetrateWalls == this.PenetrateWalls && projectile.Quiet == this.Quiet && !(projectile.BaseDamage != this.BaseDamage) && !(projectile.ColorString != this.ColorString) && !(projectile.Attributes != this.Attributes) && !(projectile.PassByVerb != this.PassByVerb) && projectile.Launcher == this.Launcher && !(projectile.RenderChar != this.RenderChar) && base.SameAs(p);
		}

		// Token: 0x0600AD83 RID: 44419 RVA: 0x00403D00 File Offset: 0x00401F00
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == PooledEvent<GetPowerLoadLevelEvent>.ID && this.Launcher != null);
		}

		// Token: 0x0600AD84 RID: 44420 RVA: 0x00403D24 File Offset: 0x00401F24
		public override bool HandleEvent(GetPowerLoadLevelEvent E)
		{
			if (GameObject.Validate(ref this.Launcher))
			{
				int powerLoadLevel = this.Launcher.GetPowerLoadLevel();
				if (powerLoadLevel > E.Level)
				{
					E.Level = powerLoadLevel;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD85 RID: 44421 RVA: 0x00403D61 File Offset: 0x00401F61
		public bool HasAttribute(string Attribute)
		{
			return !this.Attributes.IsNullOrEmpty() && this.Attributes.HasDelimitedSubstring(',', Attribute, StringComparison.Ordinal);
		}

		// Token: 0x040047C1 RID: 18369
		public int BasePenetration = 1;

		// Token: 0x040047C2 RID: 18370
		public int StrengthPenetration;

		// Token: 0x040047C3 RID: 18371
		public bool PenetrateCreatures;

		// Token: 0x040047C4 RID: 18372
		public bool PenetrateWalls;

		// Token: 0x040047C5 RID: 18373
		public bool Quiet;

		// Token: 0x040047C6 RID: 18374
		public string BaseDamage = "1d4";

		// Token: 0x040047C7 RID: 18375
		public string ColorString;

		// Token: 0x040047C8 RID: 18376
		public string Attributes = "";

		// Token: 0x040047C9 RID: 18377
		public string PassByVerb = "whiz";

		// Token: 0x040047CA RID: 18378
		public string RenderChar;

		// Token: 0x040047CB RID: 18379
		[NonSerialized]
		public GameObject Launcher;
	}
}
