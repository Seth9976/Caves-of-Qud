using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x0200124C RID: 4684
	[Serializable]
	public class BeyLahTerrain : IPart
	{
		// Token: 0x0600BA29 RID: 47657 RVA: 0x0043FA6C File Offset: 0x0043DC6C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA2A RID: 47658 RVA: 0x0043FA6F File Offset: 0x0043DC6F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Object.SetIntProperty("ForceMutableSave", 1, false);
			Registrar.Register("BeyLahReveal");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BA2B RID: 47659 RVA: 0x0043FA94 File Offset: 0x0043DC94
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeyLahReveal")
			{
				this.ParentObject.Render.Tile = "Terrain/sw_joppa.bmp";
				this.ParentObject.Render.ColorString = "&W";
				this.ParentObject.Render.DisplayName = "Bey Lah";
				this.ParentObject.Render.DetailColor = "r";
				this.ParentObject.Render.RenderString = "#";
				this.ParentObject.HasProperName = true;
				this.ParentObject.GetPart<Description>().Short = "At the center of a particularly thick copse, the vegetation clears. Flower-bedecked huts huddle in the clearing within, surrounded by phalanxes of tidy watervine rows and carefully-tended lah.";
				TerrainTravel part = this.ParentObject.GetPart<TerrainTravel>();
				if (part != null)
				{
					part.ClearEncounters();
				}
				this.ParentObject.SetStringProperty("OverlayColor", "&W", false);
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

		// Token: 0x04004AD2 RID: 19154
		public string secretId = "$beylah";

		// Token: 0x04004AD3 RID: 19155
		public bool revealed;
	}
}
