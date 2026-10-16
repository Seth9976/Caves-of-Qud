using System;

namespace XRL.World.Parts
{
	// Token: 0x020012DB RID: 4827
	[Serializable]
	public abstract class IIconColorPart : IPart
	{
		// Token: 0x0600BE14 RID: 48660 RVA: 0x0045A8A0 File Offset: 0x00458AA0
		public override bool SameAs(IPart p)
		{
			IIconColorPart iconColorPart = p as IIconColorPart;
			return !(iconColorPart.TextForeground != this.TextForeground) && !(iconColorPart.TileForeground != this.TileForeground) && !(iconColorPart.TileDetail != this.TileDetail) && !(iconColorPart.Background != this.Background) && iconColorPart.TextForegroundPriority == this.TextForegroundPriority && iconColorPart.TileForegroundPriority == this.TileForegroundPriority && iconColorPart.TileDetailPriority == this.TileDetailPriority && iconColorPart.BackgroundPriority == this.BackgroundPriority && base.SameAs(p);
		}

		// Token: 0x0600BE15 RID: 48661 RVA: 0x0045A94F File Offset: 0x00458B4F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID;
		}

		// Token: 0x0600BE16 RID: 48662 RVA: 0x0045A968 File Offset: 0x00458B68
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "TextForeground", this.TextForeground);
			E.AddEntry(this, "TileForeground", this.TileForeground);
			E.AddEntry(this, "TileDetail", this.TileDetail);
			E.AddEntry(this, "Background", this.Background);
			E.AddEntry(this, "TextForegroundPriority", this.TextForegroundPriority);
			E.AddEntry(this, "TileForegroundPriority", this.TileForegroundPriority);
			E.AddEntry(this, "TileDetailPriority", this.TileDetailPriority);
			E.AddEntry(this, "BackgroundPriority", this.BackgroundPriority);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE17 RID: 48663 RVA: 0x0045AA0C File Offset: 0x00458C0C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BE18 RID: 48664 RVA: 0x0045AA10 File Offset: 0x00458C10
		public override bool Render(RenderEvent E)
		{
			E.ApplyColors(this.TextForeground, this.TileForeground, this.TileDetail, this.Background, this.TextForegroundPriority, this.TileForegroundPriority, this.TileDetailPriority, this.BackgroundPriority);
			return base.Render(E);
		}

		// Token: 0x0600BE19 RID: 48665 RVA: 0x0045AA5C File Offset: 0x00458C5C
		public void Configure(string TextForeground = null, string TileForeground = null, string TileDetail = null, string Background = null, string Foreground = null, int? TextForegroundPriority = null, int? TileForegroundPriority = null, int? TileDetailPriority = null, int? BackgroundPriority = null, int? Priority = null)
		{
			this.TextForeground = TextForeground ?? Foreground ?? this.TextForeground;
			this.TileForeground = TileForeground ?? Foreground ?? this.TileForeground;
			this.TileDetail = TileDetail ?? this.TileDetail;
			this.Background = Background ?? this.Background;
			this.TextForegroundPriority = TextForegroundPriority ?? Priority ?? this.TextForegroundPriority;
			this.TileForegroundPriority = TileForegroundPriority ?? Priority ?? this.TileForegroundPriority;
			this.TileDetailPriority = TileDetailPriority ?? Priority ?? this.TileDetailPriority;
			this.BackgroundPriority = BackgroundPriority ?? Priority ?? this.BackgroundPriority;
		}

		// Token: 0x04004C8B RID: 19595
		public string TextForeground;

		// Token: 0x04004C8C RID: 19596
		public string TileForeground;

		// Token: 0x04004C8D RID: 19597
		public string TileDetail;

		// Token: 0x04004C8E RID: 19598
		public string Background;

		// Token: 0x04004C8F RID: 19599
		public int TextForegroundPriority;

		// Token: 0x04004C90 RID: 19600
		public int TileForegroundPriority;

		// Token: 0x04004C91 RID: 19601
		public int TileDetailPriority;

		// Token: 0x04004C92 RID: 19602
		public int BackgroundPriority;
	}
}
