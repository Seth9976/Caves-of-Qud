using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E80 RID: 3712
	[Serializable]
	public class MamonPart : IPart
	{
		// Token: 0x060095C0 RID: 38336 RVA: 0x003629C9 File Offset: 0x00360BC9
		public override bool Render(RenderEvent E)
		{
			if (!this.Rendered)
			{
				this.Rendered = true;
				The.Game.FinishQuestStep("Raising Indrix", "Find Mamon Souldrinker", -1, true, null);
			}
			return true;
		}

		// Token: 0x04003DED RID: 15853
		[NonSerialized]
		private bool Rendered;
	}
}
