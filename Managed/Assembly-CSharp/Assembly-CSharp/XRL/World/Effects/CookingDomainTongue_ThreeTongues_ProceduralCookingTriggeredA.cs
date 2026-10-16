using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D16 RID: 3350
	[Serializable]
	public class CookingDomainTongue_ThreeTongues_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060083F6 RID: 33782 RVA: 0x002FBCCC File Offset: 0x002F9ECC
		public override string GetDescription()
		{
			return "@they shoot@s out up to three sticky tongues per Sticky Tongue at rank 10.";
		}

		// Token: 0x060083F7 RID: 33783 RVA: 0x002FBCD3 File Offset: 0x002F9ED3
		public override string GetNotification()
		{
			return "@they shoot@s out a trio of sticky tongues.";
		}

		// Token: 0x060083F8 RID: 33784 RVA: 0x002FBCDA File Offset: 0x002F9EDA
		public override void Apply(GameObject go)
		{
			go.EmitMessage("A trio of tongues vegetate from =subject.t's= =bodypart:Face=!", null, null, false);
			StickyTongue.HarpoonNearest(go, StickyTongue.GetRange(10), "&M", 3, true, false);
		}
	}
}
