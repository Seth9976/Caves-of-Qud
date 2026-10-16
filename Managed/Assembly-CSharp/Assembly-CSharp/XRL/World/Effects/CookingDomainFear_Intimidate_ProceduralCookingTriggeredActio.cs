using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000CA1 RID: 3233
	[Serializable]
	public class CookingDomainFear_Intimidate_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060081C4 RID: 33220 RVA: 0x002F6BFF File Offset: 0x002F4DFF
		public override string GetDescription()
		{
			return "@they intimidate@s creatures around @them.";
		}

		// Token: 0x060081C5 RID: 33221 RVA: 0x002F6C06 File Offset: 0x002F4E06
		public override string GetNotification()
		{
			return "@they intimidate@s everyone around @them.";
		}

		// Token: 0x060081C6 RID: 33222 RVA: 0x002F6C0D File Offset: 0x002F4E0D
		public override void Apply(GameObject go)
		{
			if (go.IsPlayer() && go.GetCurrentCell() != null)
			{
				Persuasion_Intimidate.ApplyIntimidate(go.GetCurrentCell(), go, true);
			}
		}
	}
}
