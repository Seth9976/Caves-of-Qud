using System;
using System.Linq;

namespace XRL.World.Parts
{
	// Token: 0x02000FE0 RID: 4064
	[Serializable]
	public class PotentialHindrenLookClue : IPart
	{
		// Token: 0x0600A15C RID: 41308 RVA: 0x003AB5C9 File Offset: 0x003A97C9
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A15D RID: 41309 RVA: 0x003AB5CC File Offset: 0x003A97CC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectCreated");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A15E RID: 41310 RVA: 0x003AB5E4 File Offset: 0x003A97E4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectCreated" && KithAndKinGameState.Instance != null && KithAndKinGameState.Instance.lookClues.Any<HindrenClueLook>((HindrenClueLook c) => c.target == this.ParentObject.Blueprint))
			{
				this.ParentObject.AddPart<HindrenClueItem>(new HindrenClueItem(), true, false);
			}
			return base.FireEvent(E);
		}
	}
}
