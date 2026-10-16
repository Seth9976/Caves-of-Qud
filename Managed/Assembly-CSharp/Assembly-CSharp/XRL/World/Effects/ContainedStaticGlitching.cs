using System;
using XRL.Liquids;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000C49 RID: 3145
	[Serializable]
	public class ContainedStaticGlitching : Effect
	{
		// Token: 0x0600807E RID: 32894 RVA: 0x002F4B4D File Offset: 0x002F2D4D
		public ContainedStaticGlitching()
		{
			this.Duration = 1;
			this.DisplayName = "{{entropic|glitching}}";
		}

		// Token: 0x0600807F RID: 32895 RVA: 0x002F4B72 File Offset: 0x002F2D72
		public ContainedStaticGlitching(string Table)
			: this()
		{
			this.Table = Table;
		}

		// Token: 0x06008080 RID: 32896 RVA: 0x002F4B81 File Offset: 0x002F2D81
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x06008081 RID: 32897 RVA: 0x002F4B84 File Offset: 0x002F2D84
		public override int GetEffectType()
		{
			return 512;
		}

		// Token: 0x06008082 RID: 32898 RVA: 0x002F4B8B File Offset: 0x002F2D8B
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008083 RID: 32899 RVA: 0x002F4B8E File Offset: 0x002F2D8E
		public override bool Apply(GameObject Object)
		{
			return Object.HasPart<LiquidVolume>() && !Object.LiquidVolume.IsOpenVolume() && !Object.HasEffect(typeof(ContainedStaticGlitching));
		}

		// Token: 0x06008084 RID: 32900 RVA: 0x002F4BBA File Offset: 0x002F2DBA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<LiquidMixedEvent>.ID;
		}

		// Token: 0x06008085 RID: 32901 RVA: 0x002F4BD8 File Offset: 0x002F2DD8
		public override bool HandleEvent(LiquidMixedEvent E)
		{
			if (base.Object.LiquidVolume.IsMixed())
			{
				GameObject @object = base.Object;
				string table = this.Table;
				base.Object.RemoveEffect(this, true);
				LiquidWarmStatic.GlitchLiquidComponents(@object, table, 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008086 RID: 32902 RVA: 0x002F4C22 File Offset: 0x002F2E22
		public override bool HandleEvent(EndTurnEvent E)
		{
			base.Object.RemoveEffect(this, true);
			return base.HandleEvent(E);
		}

		// Token: 0x040038FD RID: 14589
		public string Table = "RandomLiquid";
	}
}
