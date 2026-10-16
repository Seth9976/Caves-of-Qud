using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010B8 RID: 4280
	[Serializable]
	public class TransfigureOnStatus : IPart
	{
		// Token: 0x0600AA8A RID: 43658 RVA: 0x003E62BC File Offset: 0x003E44BC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID;
		}

		// Token: 0x0600AA8B RID: 43659 RVA: 0x003E62DC File Offset: 0x003E44DC
		public void SaveRender()
		{
			if (this.State != 0)
			{
				return;
			}
			Render render = this.ParentObject.Render;
			this.LastTile = render.Tile;
			this.LastRenderString = render.RenderString;
			this.LastColorString = render.ColorString;
			this.LastTileColor = render.TileColor;
			this.LastDetailColor = render.DetailColor;
		}

		// Token: 0x0600AA8C RID: 43660 RVA: 0x003E633C File Offset: 0x003E453C
		public void CheckState()
		{
			Render render = this.ParentObject.Render;
			if (this.ParentObject.HasEffect(typeof(Broken)))
			{
				if (this.State == 2)
				{
					return;
				}
				this.SaveRender();
				render.Tile = this.BrokenTile.GetRandomSubstring(',', false, null).Coalesce(render.Tile);
				render.RenderString = this.BrokenRenderString.GetRandomSubstring(',', false, null).Coalesce(render.RenderString);
				render.ColorString = this.BrokenColorString.GetRandomSubstring(',', false, null).Coalesce(render.ColorString);
				render.TileColor = this.BrokenTileColor.GetRandomSubstring(',', false, null).Coalesce(render.TileColor);
				render.DetailColor = this.BrokenDetailColor.GetRandomSubstring(',', false, null).Coalesce(render.DetailColor);
				this.State = 2;
				return;
			}
			else
			{
				if (!this.ParentObject.HasEffect(typeof(Rusted)))
				{
					render.Tile = this.LastTile;
					render.RenderString = this.LastRenderString;
					render.ColorString = this.LastColorString;
					render.TileColor = this.LastTileColor;
					render.DetailColor = this.LastDetailColor;
					this.State = 0;
					return;
				}
				if (this.State == 1)
				{
					return;
				}
				this.SaveRender();
				render.Tile = this.RustedTile.GetRandomSubstring(',', false, null).Coalesce(render.Tile);
				render.RenderString = this.RustedRenderString.GetRandomSubstring(',', false, null).Coalesce(render.RenderString);
				render.ColorString = this.RustedColorString.GetRandomSubstring(',', false, null).Coalesce(render.ColorString);
				render.TileColor = this.RustedTileColor.GetRandomSubstring(',', false, null).Coalesce(render.TileColor);
				render.DetailColor = this.RustedDetailColor.GetRandomSubstring(',', false, null).Coalesce(render.DetailColor);
				this.State = 1;
				return;
			}
		}

		// Token: 0x0600AA8D RID: 43661 RVA: 0x003E653C File Offset: 0x003E473C
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			Effect effect = E.Effect;
			if (effect is Broken || effect is Rusted)
			{
				this.CheckState();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA8E RID: 43662 RVA: 0x003E6570 File Offset: 0x003E4770
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			Effect effect = E.Effect;
			if (effect is Broken || effect is Rusted)
			{
				this.CheckState();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004644 RID: 17988
		public string BrokenTile;

		// Token: 0x04004645 RID: 17989
		public string BrokenRenderString;

		// Token: 0x04004646 RID: 17990
		public string BrokenColorString;

		// Token: 0x04004647 RID: 17991
		public string BrokenTileColor;

		// Token: 0x04004648 RID: 17992
		public string BrokenDetailColor;

		// Token: 0x04004649 RID: 17993
		public string RustedTile;

		// Token: 0x0400464A RID: 17994
		public string RustedRenderString;

		// Token: 0x0400464B RID: 17995
		public string RustedColorString;

		// Token: 0x0400464C RID: 17996
		public string RustedTileColor;

		// Token: 0x0400464D RID: 17997
		public string RustedDetailColor;

		// Token: 0x0400464E RID: 17998
		public string LastTile;

		// Token: 0x0400464F RID: 17999
		public string LastRenderString;

		// Token: 0x04004650 RID: 18000
		public string LastColorString;

		// Token: 0x04004651 RID: 18001
		public string LastTileColor;

		// Token: 0x04004652 RID: 18002
		public string LastDetailColor;

		// Token: 0x04004653 RID: 18003
		public int State;
	}
}
