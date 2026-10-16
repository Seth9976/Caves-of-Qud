using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D01 RID: 3329
	public class CookingDomainRubber_OnJump : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008393 RID: 33683 RVA: 0x002FB011 File Offset: 0x002F9211
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature jump@s,";
		}

		// Token: 0x06008394 RID: 33684 RVA: 0x002FB018 File Offset: 0x002F9218
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<JumpedEvent>.ID;
		}

		// Token: 0x06008395 RID: 33685 RVA: 0x002FB02E File Offset: 0x002F922E
		public override bool HandleEvent(JumpedEvent E)
		{
			if (E.Pass == JumpedEvent.PASSES)
			{
				string sourceKey = E.SourceKey;
				if (sourceKey == null || !sourceKey.Contains("CookingDomainRubber"))
				{
					base.Trigger();
				}
			}
			return base.HandleEvent(E);
		}
	}
}
