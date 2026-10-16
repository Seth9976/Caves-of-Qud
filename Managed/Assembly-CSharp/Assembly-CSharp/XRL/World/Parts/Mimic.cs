using System;
using ConsoleLib.Console;

namespace XRL.World.Parts
{
	// Token: 0x02000EDB RID: 3803
	[Serializable]
	public class Mimic : IPart
	{
		// Token: 0x0600982D RID: 38957 RVA: 0x00374664 File Offset: 0x00372864
		public override bool SameAs(IPart p)
		{
			Mimic mimic = p as Mimic;
			return mimic.CopyColor == this.CopyColor && mimic.CopyString == this.CopyString && mimic.CopyBackground == this.CopyBackground && !(mimic.ActiveColorString != this.ActiveColorString) && !(mimic.ActiveDetailColor != this.ActiveDetailColor) && !(mimic.ActiveTile != this.ActiveTile) && !(mimic.ActiveRenderString != this.ActiveRenderString) && mimic.MaxHostilityRecognitionDistanceMimickingColor == this.MaxHostilityRecognitionDistanceMimickingColor && mimic.MaxHostilityRecognitionDistanceMimickingShape == this.MaxHostilityRecognitionDistanceMimickingShape && base.SameAs(p);
		}

		// Token: 0x0600982E RID: 38958 RVA: 0x00374723 File Offset: 0x00372923
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == PooledEvent<GetHostilityRecognitionLimitsEvent>.ID || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600982F RID: 38959 RVA: 0x00374749 File Offset: 0x00372949
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.MimicNearbyObject();
			return base.HandleEvent(E);
		}

		// Token: 0x06009830 RID: 38960 RVA: 0x00374758 File Offset: 0x00372958
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.MimicNearbyObject();
			return base.HandleEvent(E);
		}

		// Token: 0x06009831 RID: 38961 RVA: 0x00374768 File Offset: 0x00372968
		public override bool HandleEvent(GetHostilityRecognitionLimitsEvent E)
		{
			if (this.CopyColor && this.MaxHostilityRecognitionDistanceMimickingColor >= 0 && (!this.ActiveColorString.IsNullOrEmpty() || !this.ActiveDetailColor.IsNullOrEmpty()))
			{
				E.MaxDistance(this.MaxHostilityRecognitionDistanceMimickingColor);
			}
			if (this.CopyString && this.MaxHostilityRecognitionDistanceMimickingShape >= 0 && (!this.ActiveTile.IsNullOrEmpty() || !this.ActiveRenderString.IsNullOrEmpty()))
			{
				E.MaxDistance(this.MaxHostilityRecognitionDistanceMimickingShape);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009832 RID: 38962 RVA: 0x003747EA File Offset: 0x003729EA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009833 RID: 38963 RVA: 0x003747F0 File Offset: 0x003729F0
		public override bool Render(RenderEvent E)
		{
			if (this.CopyColor)
			{
				E.ApplyColors(this.ActiveColorString, this.ActiveDetailColor, Mimic.ICON_COLOR_PRIORITY, Mimic.ICON_COLOR_PRIORITY);
			}
			if (this.CopyString)
			{
				if (!this.ActiveTile.IsNullOrEmpty() && !E.Tile.IsNullOrEmpty())
				{
					E.Tile = this.ActiveTile;
				}
				if (!this.ActiveRenderString.IsNullOrEmpty())
				{
					E.RenderString = this.ActiveRenderString;
				}
			}
			return base.Render(E);
		}

		// Token: 0x06009834 RID: 38964 RVA: 0x00374870 File Offset: 0x00372A70
		public void MimicNearbyObject()
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone == null || !currentZone.Built)
			{
				return;
			}
			Cell randomLocalAdjacentCell = this.ParentObject.CurrentCell.GetRandomLocalAdjacentCell();
			if (randomLocalAdjacentCell == null)
			{
				return;
			}
			int i = 0;
			int count = randomLocalAdjacentCell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = randomLocalAdjacentCell.Objects[i];
				if (gameObject.Render != null && gameObject.Render.RenderLayer > 0 && !gameObject.Render.SameAs(this.ParentObject.Render))
				{
					RenderEvent renderEvent = gameObject.RenderForUI(null, true);
					if (this.CopyColor)
					{
						if (this.CopyBackground)
						{
							if (this.ActiveColorString != renderEvent.ColorString)
							{
								this.ActiveColorString = renderEvent.ColorString;
								this.ActiveDetailColor = renderEvent.DetailColor;
							}
						}
						else
						{
							string text = ColorUtility.StripBackgroundFormatting(renderEvent.ColorString);
							if (this.ActiveColorString != text)
							{
								this.ActiveColorString = text;
								this.ActiveDetailColor = renderEvent.DetailColor;
							}
						}
					}
					if (this.CopyString)
					{
						this.ActiveTile = renderEvent.Tile;
						this.ActiveRenderString = renderEvent.RenderString;
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x04003F63 RID: 16227
		public static readonly int ICON_COLOR_PRIORITY = 150;

		// Token: 0x04003F64 RID: 16228
		public bool CopyColor = true;

		// Token: 0x04003F65 RID: 16229
		public bool CopyString;

		// Token: 0x04003F66 RID: 16230
		public bool CopyBackground = true;

		// Token: 0x04003F67 RID: 16231
		public string ActiveColorString;

		// Token: 0x04003F68 RID: 16232
		public string ActiveDetailColor;

		// Token: 0x04003F69 RID: 16233
		public string ActiveTile;

		// Token: 0x04003F6A RID: 16234
		public string ActiveRenderString;

		// Token: 0x04003F6B RID: 16235
		public int MaxHostilityRecognitionDistanceMimickingColor = 3;

		// Token: 0x04003F6C RID: 16236
		public int MaxHostilityRecognitionDistanceMimickingShape;
	}
}
