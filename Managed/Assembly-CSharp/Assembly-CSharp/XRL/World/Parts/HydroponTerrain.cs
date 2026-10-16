using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02001272 RID: 4722
	[Serializable]
	public class HydroponTerrain : IPart
	{
		// Token: 0x0600BB0B RID: 47883 RVA: 0x0044526C File Offset: 0x0044346C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB0C RID: 47884 RVA: 0x0044526F File Offset: 0x0044346F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Object.SetIntProperty("ForceMutableSave", 1, false);
			Registrar.Register("HydroponReveal");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BB0D RID: 47885 RVA: 0x00445294 File Offset: 0x00443494
		public override bool FireEvent(Event E)
		{
			if (E.ID == "HydroponReveal")
			{
				this.ParentObject.Render.Tile = "Terrain/sw_joppa.bmp";
				this.ParentObject.Render.ColorString = "&B";
				this.ParentObject.Render.DisplayName = "Hydropon";
				this.ParentObject.Render.DetailColor = "r";
				this.ParentObject.Render.RenderString = "#";
				this.ParentObject.HasProperName = true;
				this.ParentObject.GetPart<Description>().Short = "It's the hydropon.";
				TerrainTravel part = this.ParentObject.GetPart<TerrainTravel>();
				if (part != null)
				{
					part.ClearEncounters();
				}
				this.ParentObject.SetStringProperty("OverlayColor", "&B", false);
				if (this.secretId != null)
				{
					JournalMapNote mapNote = JournalAPI.GetMapNote(this.secretId);
					if (mapNote != null && !mapNote.Revealed)
					{
						mapNote.Reveal(null, false);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B19 RID: 19225
		public string secretId = "$hydropon";

		// Token: 0x04004B1A RID: 19226
		public bool revealed;
	}
}
