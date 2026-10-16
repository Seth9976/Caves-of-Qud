using System;
using System.Collections.Generic;
using Genkit;
using XRL.EditorFormats.Map;
using XRL.World.Quests;

namespace XRL.World.ZoneParts
{
	// Token: 0x020009C8 RID: 2504
	[Serializable]
	public class ZoneRepair : IZonePart
	{
		// Token: 0x06006B84 RID: 27524 RVA: 0x0022B591 File Offset: 0x00229791
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x06006B85 RID: 27525 RVA: 0x0022B5A7 File Offset: 0x002297A7
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.CheckZoneRepair();
			return base.HandleEvent(E);
		}

		// Token: 0x06006B86 RID: 27526 RVA: 0x0022B5B8 File Offset: 0x002297B8
		public void CheckZoneRepair()
		{
			if (this.LastTurn == -9223372036854775808L && The.Game != null)
			{
				this.LastTurn = The.Game.TimeTicks;
			}
			this.BuildCounter += The.Game.TimeTicks - this.LastTurn;
			this.LastTurn = The.Game.TimeTicks;
			if (this.BuildCounter < this.TurnsPerObject)
			{
				return;
			}
			if (!this.RequiresObjectWithTagOrProperty.IsNullOrEmpty() && !this.ParentZone.HasObjectWithTagOrProperty(this.RequiresObjectWithTagOrProperty))
			{
				return;
			}
			if (!this.RequiresObjectOfFaction.IsNullOrEmpty() && !this.ParentZone.HasObject((GameObject o) => o.IsFactionMember(this.RequiresObjectOfFaction)))
			{
				return;
			}
			if (this.FileName.IsNullOrEmpty())
			{
				return;
			}
			long num = Math.Max(1L, this.BuildCounter / this.TurnsPerObject);
			this.BuildCounter = 0L;
			if (this.Map == null)
			{
				this.Map = MapFile.Resolve(this.FileName, true);
			}
			if (this.Map != null)
			{
				if (this.ToBuild == null)
				{
					this.ToBuild = new List<ValueTuple<Location2D, string>>();
					for (int i = 0; i < this.ParentZone.Height; i++)
					{
						for (int j = 0; j < this.ParentZone.Width; j++)
						{
							foreach (MapFileObjectBlueprint mapFileObjectBlueprint in this.Map.Cells[j, i].Objects)
							{
								GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(mapFileObjectBlueprint.Name);
								if (blueprint != null && !blueprint.IsWall() && !blueprint.HasPart("Brain") && !blueprint.HasTag("Non", false) && !this.ParentZone.GetCell(j, i).HasObject(mapFileObjectBlueprint.Name))
								{
									this.ToBuild.Add(new ValueTuple<Location2D, string>(Location2D.Get(j, i), mapFileObjectBlueprint.Name));
								}
							}
						}
					}
					this.ToBuild.ShuffleInPlace<ValueTuple<Location2D, string>>(null);
				}
				bool flag = false;
				long num2 = 0L;
				while (num2 < num && this.ToBuild.Count > 0)
				{
					ValueTuple<Location2D, string> valueTuple = this.ToBuild[0];
					this.ToBuild.RemoveAt(0);
					this.ParentZone.GetCell(valueTuple.Item1).AddObject(valueTuple.Item2, null, null, null, null);
					flag = true;
					num2 += 1L;
				}
				GritGateScripts.OpenRank2Doors();
				if (this.ToBuild.Count == 0)
				{
					this.ParentZone.RemovePart(this);
				}
				if (flag)
				{
					PrimePowerSystemsEvent.Send(this.ParentZone);
				}
			}
		}

		// Token: 0x04003135 RID: 12597
		public string FileName;

		// Token: 0x04003136 RID: 12598
		public string RequiresObjectWithTagOrProperty;

		// Token: 0x04003137 RID: 12599
		public string RequiresObjectOfFaction;

		// Token: 0x04003138 RID: 12600
		public long TurnsPerObject = 50L;

		// Token: 0x04003139 RID: 12601
		public long BuildCounter;

		// Token: 0x0400313A RID: 12602
		public long LastTurn = long.MinValue;

		// Token: 0x0400313B RID: 12603
		[NonSerialized]
		private MapFile Map;

		// Token: 0x0400313C RID: 12604
		[NonSerialized]
		private List<ValueTuple<Location2D, string>> ToBuild;
	}
}
