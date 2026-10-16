using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E89 RID: 3721
	[Serializable]
	public class QudzuProperties : IPart
	{
		// Token: 0x060095E8 RID: 38376 RVA: 0x00364467 File Offset: 0x00362667
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x060095E9 RID: 38377 RVA: 0x0036447C File Offset: 0x0036267C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				if (this.ParentObject.CurrentCell.IsOccluding())
				{
					this.ParentObject.Render.ColorString = "&r^w";
				}
				else
				{
					this.ParentObject.Render.ColorString = "&r";
				}
			}
			return base.FireEvent(E);
		}
	}
}
