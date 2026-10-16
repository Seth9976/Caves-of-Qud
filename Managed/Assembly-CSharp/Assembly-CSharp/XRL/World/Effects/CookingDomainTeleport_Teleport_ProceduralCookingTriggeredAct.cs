using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D11 RID: 3345
	[Serializable]
	public class CookingDomainTeleport_Teleport_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060083E4 RID: 33764 RVA: 0x002FBA40 File Offset: 0x002F9C40
		public override string GetDescription()
		{
			return "@they teleport.";
		}

		// Token: 0x060083E5 RID: 33765 RVA: 0x002FBA47 File Offset: 0x002F9C47
		public override void Apply(GameObject Subject)
		{
			if (Subject.IsPlayer())
			{
				Teleportation.Cast(null, "5-6", null, null, Subject, true, 0);
			}
		}
	}
}
