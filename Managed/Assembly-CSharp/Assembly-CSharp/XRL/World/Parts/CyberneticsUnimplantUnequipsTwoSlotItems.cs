using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F51 RID: 3921
	[Obsolete]
	[Serializable]
	public class CyberneticsUnimplantUnequipsTwoSlotItems : IPart
	{
		// Token: 0x06009C7F RID: 40063 RVA: 0x0038BF79 File Offset: 0x0038A179
		public override void Attach()
		{
			this.ParentObject.RemovePart(this);
		}
	}
}
