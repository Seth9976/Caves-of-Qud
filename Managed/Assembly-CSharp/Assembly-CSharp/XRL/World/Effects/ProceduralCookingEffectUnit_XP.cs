using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C64 RID: 3172
	[Serializable]
	public class ProceduralCookingEffectUnit_XP : ProceduralCookingEffectUnit
	{
		// Token: 0x060080EC RID: 33004 RVA: 0x002F5646 File Offset: 0x002F3846
		public override string GetDescription()
		{
			return "+XP";
		}

		// Token: 0x060080ED RID: 33005 RVA: 0x002F5650 File Offset: 0x002F3850
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.AwardXP(1000, -1, 0, int.MaxValue, null, null, null, null, null, null);
		}

		// Token: 0x060080EE RID: 33006 RVA: 0x002F5676 File Offset: 0x002F3876
		public override void Remove(GameObject Object, Effect parent)
		{
		}
	}
}
