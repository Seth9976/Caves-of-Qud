using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A3D RID: 2621
	public class InsertPresetFromPopulation : ZoneBuilderSandbox
	{
		// Token: 0x06006DBE RID: 28094 RVA: 0x0024F3D0 File Offset: 0x0024D5D0
		public bool BuildZone(Zone Z)
		{
			string blueprint = PopulationManager.RollOneFrom(this.Population, null, null).Blueprint;
			int num = 0;
			int num2 = 0;
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(blueprint);
			if (gameObject == null)
			{
				Debug.LogError("Bad preset blueprint: " + blueprint);
			}
			if (gameObject.HasPart<MapChunkPlacement>())
			{
				num = gameObject.GetPart<MapChunkPlacement>().Width;
				num2 = gameObject.GetPart<MapChunkPlacement>().Height;
			}
			if (gameObject.HasPart<MultiMapChunkPlacement>())
			{
				MultiMapChunkPlacement part = gameObject.GetPart<MultiMapChunkPlacement>();
				num = part.MapsWide * part.Width;
				num2 = part.MapsHigh * part.Height;
			}
			using (List<Cell>.Enumerator enumerator = Z.GetCells().Shuffle<Cell>(null).GetEnumerator())
			{
				IL_0158:
				while (enumerator.MoveNext())
				{
					Cell cell = enumerator.Current;
					if (cell.X < Z.Width - num && cell.Y < Z.Height - num2)
					{
						for (int i = 0; i < cell.X + num; i++)
						{
							for (int j = 0; j < cell.Y + num2; j++)
							{
								if (Z.GetCell(i, j).HasObjectWithPart("StairsUp") || Z.GetCell(i, j).HasObjectWithPart("StairsDown"))
								{
									goto IL_0158;
								}
							}
						}
					}
					GameObject gameObject2 = cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					if (!this.Important)
					{
						break;
					}
					if (gameObject2 == null)
					{
						break;
					}
					gameObject2.SetImportant(true, false, false);
					break;
				}
			}
			new ForceConnections().BuildZone(Z);
			return true;
		}

		// Token: 0x04003236 RID: 12854
		public string Population;

		// Token: 0x04003237 RID: 12855
		public bool Important;
	}
}
