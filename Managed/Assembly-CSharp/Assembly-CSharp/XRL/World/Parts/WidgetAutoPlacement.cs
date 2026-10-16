using System;

namespace XRL.World.Parts
{
	// Token: 0x020010BE RID: 4286
	[Serializable]
	public class WidgetAutoPlacement : IPart
	{
		// Token: 0x0600AAB1 RID: 43697 RVA: 0x003E6EA2 File Offset: 0x003E50A2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AAB2 RID: 43698 RVA: 0x003E6EB8 File Offset: 0x003E50B8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && !this.Placed)
			{
				this.Placed = true;
				GameObject randomElement = this.ParentObject.CurrentCell.ParentZone.GetObjects((GameObject o) => o.Blueprint == this.Widget).GetRandomElement<GameObject>(null);
				if (randomElement != null)
				{
					this.ParentObject.DirectMoveTo(randomElement.CurrentCell, 0, false, false, false, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004658 RID: 18008
		public bool Placed;

		// Token: 0x04004659 RID: 18009
		public string Widget;
	}
}
