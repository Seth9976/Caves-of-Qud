using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000CFF RID: 3327
	[Serializable]
	public class CookingDomainRubber_JumpThrice_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x0600838B RID: 33675 RVA: 0x002FAF76 File Offset: 0x002F9176
		public override string GetDescription()
		{
			return "@they jump@s three times in a row, each time at +2 range.";
		}

		// Token: 0x0600838C RID: 33676 RVA: 0x002FAF7D File Offset: 0x002F917D
		public override string GetNotification()
		{
			return "@they feel@s a overwhelming springiness inside.";
		}

		// Token: 0x0600838D RID: 33677 RVA: 0x002FAF84 File Offset: 0x002F9184
		public override void Apply(GameObject go)
		{
			int num = 0;
			while (num < 3 && Acrobatics_Jump.Jump(go, 2, null, base.GetType().Name))
			{
				num++;
			}
		}
	}
}
