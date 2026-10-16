using System;
using System.Collections.Generic;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000ED0 RID: 3792
	[Serializable]
	public class Leader : IPart
	{
		// Token: 0x060097DC RID: 38876 RVA: 0x003721F8 File Offset: 0x003703F8
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060097DD RID: 38877 RVA: 0x003721FB File Offset: 0x003703FB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x060097DE RID: 38878 RVA: 0x00372211 File Offset: 0x00370411
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.SetupLeader(true);
			return base.HandleEvent(E);
		}

		// Token: 0x060097DF RID: 38879 RVA: 0x00372221 File Offset: 0x00370421
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x060097E0 RID: 38880 RVA: 0x00372236 File Offset: 0x00370436
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				this.SetupLeader(false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x060097E1 RID: 38881 RVA: 0x00372258 File Offset: 0x00370458
		private void SetupLeader(bool builtKnown = false)
		{
			try
			{
				if (!this.bCreated)
				{
					if (builtKnown || this.ParentObject.CurrentCell.ParentZone.Built)
					{
						this.bCreated = true;
						Physics physics = this.ParentObject.Physics;
						List<string> list = new List<string>();
						foreach (PopulationResult populationResult in PopulationManager.Generate(this.popTable, null, null, null, null, null))
						{
							for (int i = 0; i < populationResult.Number; i++)
							{
								list.Add(populationResult.Blueprint);
							}
						}
						List<Cell> list2 = new List<Cell>();
						this.ParentObject.CurrentCell.GetAdjacentCells(this.SpawnRadius, list2, true, true);
						List<Cell> list3 = new List<Cell>();
						foreach (Cell cell in list2)
						{
							if (cell.IsEmpty())
							{
								list3.Add(cell);
							}
						}
						for (int j = 0; j < list.Count; j++)
						{
							GameObject gameObject = GameObject.Create(list[j]);
							gameObject.SetAlliedLeader<AllyPack>(this.ParentObject, 0, false);
							Cell cell2 = ((list3 != null) ? list3.GetRandomElement<Cell>(null) : null);
							if (cell2 != null)
							{
								cell2.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								gameObject.MakeActive(false);
								list3.Remove(cell2);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04003F2F RID: 16175
		public bool bCreated;

		// Token: 0x04003F30 RID: 16176
		public string popTable = "";

		// Token: 0x04003F31 RID: 16177
		public int SpawnRadius = 2;
	}
}
