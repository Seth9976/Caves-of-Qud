using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Rules;
using XRL.World;
using XRL.World.AI;

namespace XRL
{
	// Token: 0x02000531 RID: 1329
	[Serializable]
	public class RootKnotSystem : IGameSystem
	{
		// Token: 0x060038EE RID: 14574 RVA: 0x00117A6C File Offset: 0x00115C6C
		public override void Write(SerializationWriter writer)
		{
			writer.WriteGameObjectList(this.inventory);
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x00117A7A File Offset: 0x00115C7A
		public override void Read(SerializationReader reader)
		{
			this.inventory = new List<GameObject>();
			reader.ReadGameObjectList(this.inventory, null);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x00117A94 File Offset: 0x00115C94
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(ZoneBuiltEvent.ID, 0, false);
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x00117AA3 File Offset: 0x00115CA3
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.PlaceSummoner(E.Zone);
			return base.HandleEvent(E);
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x00117AB8 File Offset: 0x00115CB8
		public void PlaceSummoner(Zone Zone)
		{
			if (Zone.IsWorldMap())
			{
				return;
			}
			int num = 3;
			if (Zone.IsCheckpoint())
			{
				num = 40;
			}
			if (this.first)
			{
				num = 100;
			}
			if (Stat.Roll(1, 100) <= num)
			{
				this.first = false;
				if (Zone.ZoneID == "JoppaWorld.11.22.1.1.10")
				{
					Zone.GetCell(24, 21).AddObject("RootPetSummoner", null, null, null, null);
				}
				else
				{
					Cell cell = Zone.GetCellWithEmptyBorder(1);
					if (cell == null)
					{
						cell = Zone.GetEmptyCells(null, null).FirstOrDefault<Cell>();
					}
					if (cell == null)
					{
						cell = Zone.GetCells().FirstOrDefault<Cell>();
					}
					Cell cell2 = cell.GetCellFromDirection("NW", true);
					if (cell2 == null)
					{
						cell2 = cell;
					}
					cell2.AddObject("RootPetSummoner", null, null, null, null);
				}
				foreach (GameObject gameObject in Zone.GetObjects("RootKnot"))
				{
					gameObject.Brain.AddOpinion<OpinionMollify>(The.Player, 100f);
					gameObject.Inventory.Objects = this.inventory;
				}
			}
		}

		// Token: 0x04001CCC RID: 7372
		[NonSerialized]
		public List<GameObject> inventory = new List<GameObject>();

		// Token: 0x04001CCD RID: 7373
		public bool first = true;
	}
}
