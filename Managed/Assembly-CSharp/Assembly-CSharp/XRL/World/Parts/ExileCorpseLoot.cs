using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F6B RID: 3947
	[Serializable]
	public class ExileCorpseLoot : IPart
	{
		// Token: 0x06009D1F RID: 40223 RVA: 0x0038E5BA File Offset: 0x0038C7BA
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009D20 RID: 40224 RVA: 0x0038E5BD File Offset: 0x0038C7BD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009D21 RID: 40225 RVA: 0x0038E5D4 File Offset: 0x0038C7D4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				if (this.bCreated)
				{
					return true;
				}
				this.bCreated = true;
				Cell currentCell = this.ParentObject.CurrentCell;
				currentCell.AddObject("Godshroom Cap", null, null, null, null);
				currentCell.AddObject("Godshroom Cap", null, null, null, null);
				currentCell.AddObject("Scroll Bound by Kelp", null, null, null, null);
				currentCell.AddObject("CyberneticsCreditWedge3", null, null, null, null);
				this.ParentObject.UnregisterPartEvent(this, "EnteredCell");
			}
			return base.FireEvent(E);
		}

		// Token: 0x040040ED RID: 16621
		public bool bCreated;
	}
}
