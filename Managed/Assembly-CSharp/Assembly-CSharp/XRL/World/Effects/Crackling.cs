using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D1B RID: 3355
	[Serializable]
	public class Crackling : Effect
	{
		// Token: 0x06008412 RID: 33810 RVA: 0x002FBF65 File Offset: 0x002FA165
		public Crackling()
		{
			this.DisplayName = "{{W|crackling}}";
			this.Duration = 9999;
		}

		// Token: 0x06008413 RID: 33811 RVA: 0x002FBF83 File Offset: 0x002FA183
		public override int GetEffectType()
		{
			return 64;
		}

		// Token: 0x06008414 RID: 33812 RVA: 0x002FBF87 File Offset: 0x002FA187
		public override string GetDescription()
		{
			return "{{W|crackling}}";
		}

		// Token: 0x06008415 RID: 33813 RVA: 0x002FBF8E File Offset: 0x002FA18E
		public override string GetDetails()
		{
			return "Electromagnetically unstable.";
		}

		// Token: 0x06008416 RID: 33814 RVA: 0x002FBF98 File Offset: 0x002FA198
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_electricalEffect", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return base.Apply(Object);
		}
	}
}
