using System;

namespace XRL.World.Parts
{
	// Token: 0x020012F8 RID: 4856
	[Serializable]
	public class Bloodsplatter : IPart
	{
		// Token: 0x0600BE7A RID: 48762 RVA: 0x0045C0F4 File Offset: 0x0045A2F4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BE7B RID: 48763 RVA: 0x0045C0F7 File Offset: 0x0045A2F7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BE7C RID: 48764 RVA: 0x0045C10C File Offset: 0x0045A30C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				foreach (Cell cell in this.ParentObject.CurrentCell.GetLocalAdjacentCells())
				{
					foreach (GameObject gameObject in cell.GetObjectsWithPart("Physics"))
					{
						if (gameObject.HasPart<Render>())
						{
							gameObject.FlingBlood();
						}
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
