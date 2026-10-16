using System;
using UnityEngine;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A3C RID: 2620
	public class AddPresetAtLocation : ZoneBuilderSandbox
	{
		// Token: 0x06006DBC RID: 28092 RVA: 0x0024F314 File Offset: 0x0024D514
		public bool BuildZone(Zone Z)
		{
			string preset = this.Preset;
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(preset);
			if (gameObject == null)
			{
				Debug.LogError("Bad preset blueprint: " + preset);
			}
			if (gameObject.HasPart<MapChunkPlacement>())
			{
				int width = gameObject.GetPart<MapChunkPlacement>().Width;
				int height = gameObject.GetPart<MapChunkPlacement>().Height;
			}
			if (gameObject.HasPart<MultiMapChunkPlacement>())
			{
				MultiMapChunkPlacement part = gameObject.GetPart<MultiMapChunkPlacement>();
				int mapsWide = part.MapsWide;
				int width2 = part.Width;
				int mapsHigh = part.MapsHigh;
				int height2 = part.Height;
			}
			Z.GetCell(this.X, this.Y).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			new ForceConnections().BuildZone(Z);
			return true;
		}

		// Token: 0x04003233 RID: 12851
		public string Preset;

		// Token: 0x04003234 RID: 12852
		public int X;

		// Token: 0x04003235 RID: 12853
		public int Y;
	}
}
