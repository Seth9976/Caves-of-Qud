using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E17 RID: 3607
	[Serializable]
	public class MarkovBookshelf : IPart
	{
		// Token: 0x060092EF RID: 37615 RVA: 0x0034F78A File Offset: 0x0034D98A
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060092F0 RID: 37616 RVA: 0x0034F78D File Offset: 0x0034D98D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x060092F1 RID: 37617 RVA: 0x0034F7A4 File Offset: 0x0034D9A4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && !this.Opened)
			{
				List<PopulationResult> list = PopulationManager.Generate(this.BookTable, null, null, null, null, null);
				for (int i = 0; i < list.Count; i++)
				{
					for (int j = 0; j < list[i].Number; j++)
					{
						GameObject gameObject = GameObject.Create("MarkovBook");
						gameObject.GetPart<MarkovBook>().SetContents(Stat.Random(0, 2147483646), list[i].Blueprint);
						this.ParentObject.Inventory.AddObject(gameObject, null, false, false, true, null);
					}
				}
				this.Opened = true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003CD1 RID: 15569
		public bool Opened;

		// Token: 0x04003CD2 RID: 15570
		public string BookTable = "StiltCorpus";
	}
}
