using System;

namespace XRL.World.Parts
{
	// Token: 0x02001011 RID: 4113
	[Serializable]
	public class DamageContents : IPoweredPart
	{
		// Token: 0x0600A3D4 RID: 41940 RVA: 0x003BD8E4 File Offset: 0x003BBAE4
		public DamageContents()
		{
			this.ChargeUse = 200;
			this.WorksOnInventory = true;
		}

		// Token: 0x0600A3D5 RID: 41941 RVA: 0x003BD91C File Offset: 0x003BBB1C
		public override bool SameAs(IPart p)
		{
			DamageContents damageContents = p as DamageContents;
			return !(damageContents.Damage != this.Damage) && !(damageContents.DamageAttributes != this.DamageAttributes) && !(damageContents.CountLimit != this.CountLimit) && base.SameAs(p);
		}

		// Token: 0x0600A3D6 RID: 41942 RVA: 0x003BD976 File Offset: 0x003BBB76
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A3D7 RID: 41943 RVA: 0x003BD979 File Offset: 0x003BBB79
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckDamageContents(Amount);
		}

		// Token: 0x0600A3D8 RID: 41944 RVA: 0x003BD984 File Offset: 0x003BBB84
		private bool DoDamageContentsItem(GameObject obj)
		{
			if (this.ActiveCountLimit != -1)
			{
				int activeCountLimit = this.ActiveCountLimit;
				this.ActiveCountLimit = activeCountLimit - 1;
				if (activeCountLimit <= 0)
				{
					return false;
				}
			}
			int num = this.Damage.RollCached();
			string damageAttributes = this.DamageAttributes;
			if (obj.TakeDamage(num, "from " + this.ParentObject.t(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", damageAttributes, null, null, null, null, this.ParentObject, this.ParentObject, null, false, false, false, false, false, true, false, false, false, 5, null))
			{
				this.AnyDamage = true;
			}
			return true;
		}

		// Token: 0x0600A3D9 RID: 41945 RVA: 0x003BDA28 File Offset: 0x003BBC28
		public bool DoDamageContents(int Turns = 1)
		{
			if (!string.IsNullOrEmpty(this.CountLimit))
			{
				this.ActiveCountLimit = this.CountLimit.RollCached() * Turns;
			}
			else
			{
				this.ActiveCountLimit = -1;
			}
			for (int i = 0; i < Turns; i++)
			{
				this.AnyDamage = false;
				this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(this.DoDamageContentsItem), true);
				if (!this.AnyDamage)
				{
					break;
				}
				base.ConsumeCharge(Turns, null, null);
			}
			return this.AnyDamage;
		}

		// Token: 0x0600A3DA RID: 41946 RVA: 0x003BDAB0 File Offset: 0x003BBCB0
		public bool CheckDamageContents(int Turns = 1)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.DoDamageContents(Turns);
			}
			return this.AnyDamage;
		}

		// Token: 0x040043CE RID: 17358
		public string Damage = "1d6";

		// Token: 0x040043CF RID: 17359
		public string DamageAttributes = "Crushing";

		// Token: 0x040043D0 RID: 17360
		public string CountLimit;

		// Token: 0x040043D1 RID: 17361
		[NonSerialized]
		private int ActiveCountLimit = -1;

		// Token: 0x040043D2 RID: 17362
		[NonSerialized]
		private bool AnyDamage;
	}
}
