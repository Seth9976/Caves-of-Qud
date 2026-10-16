using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012FB RID: 4859
	[Serializable]
	public class Firestarter : IPart
	{
		// Token: 0x0600BE85 RID: 48773 RVA: 0x0045C32A File Offset: 0x0045A52A
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BE86 RID: 48774 RVA: 0x0045C32D File Offset: 0x0045A52D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BE87 RID: 48775 RVA: 0x0045C344 File Offset: 0x0045A544
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				foreach (Cell cell in this.ParentObject.CurrentCell.ParentZone.GetCell(Stat.Random(0, 79), Stat.Random(0, 24)).GetLocalAdjacentCells())
				{
					if (Stat.Random(1, 100) <= 25)
					{
						foreach (GameObject gameObject in cell.GetObjectsWithPart("Physics"))
						{
							if (gameObject.HasPart<Render>() && !gameObject.IsCombat)
							{
								gameObject.Physics.Temperature = gameObject.Physics.FlameTemperature + 25;
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
