using System;
using XRL.Core;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000988 RID: 2440
	[Serializable]
	public abstract class WorldBuilder
	{
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06006716 RID: 26390 RVA: 0x0020083A File Offset: 0x001FEA3A
		public XRLGame game
		{
			get
			{
				return XRLCore.Core.Game;
			}
		}

		// Token: 0x06006717 RID: 26391
		public abstract bool BuildWorld(string worldName);

		// Token: 0x06006718 RID: 26392 RVA: 0x00200848 File Offset: 0x001FEA48
		public void MarkCell(string World, int x, int y, string str)
		{
			ZoneManager zoneManager = The.ZoneManager;
			Zone zone = ((zoneManager != null) ? zoneManager.GetZone(World) : null);
			Cell cell = ((zone != null) ? zone.GetCell(x, y) : null) ?? null;
			GameObject gameObject = ((cell != null) ? cell.GetObjectInCell(0) : null);
			Render render;
			if (gameObject != null && gameObject.TryGetPart<Render>(out render))
			{
				render.RenderString = str;
				render.Tile = null;
			}
		}

		// Token: 0x04002FAD RID: 12205
		public ZoneManager ZM;
	}
}
