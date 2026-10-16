using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E40 RID: 3648
	[Serializable]
	public class ColorShift : IPart
	{
		// Token: 0x060093EC RID: 37868 RVA: 0x00353E70 File Offset: 0x00352070
		public override bool SameAs(IPart p)
		{
			ColorShift colorShift = p as ColorShift;
			return !(colorShift.ColorString != this.ColorString) && !(colorShift.TileColor != this.TileColor) && !(colorShift.DetailColor != this.DetailColor) && base.SameAs(p);
		}

		// Token: 0x060093ED RID: 37869 RVA: 0x00353ECC File Offset: 0x003520CC
		public void Apply(string ColorString = null, string TileColor = null, string DetailColor = null)
		{
			if (ColorString != null && this.ParentObject.Render != null)
			{
				if (this.ColorString == null)
				{
					this.ColorString = this.ParentObject.Render.ColorString;
				}
				this.ParentObject.Render.ColorString = ColorString;
			}
			if (TileColor != null && this.ParentObject.Render != null)
			{
				if (this.TileColor == null)
				{
					this.TileColor = this.ParentObject.Render.TileColor;
				}
				this.ParentObject.Render.TileColor = TileColor;
			}
			if (DetailColor != null && this.ParentObject.Render != null)
			{
				if (this.DetailColor == null)
				{
					this.DetailColor = this.ParentObject.Render.DetailColor;
				}
				this.ParentObject.Render.DetailColor = DetailColor;
			}
		}

		// Token: 0x060093EE RID: 37870 RVA: 0x00353F98 File Offset: 0x00352198
		public void Unapply()
		{
			if (this.ColorString != null && this.ParentObject.Render != null)
			{
				this.ParentObject.Render.ColorString = this.ColorString;
			}
			if (this.TileColor != null && this.ParentObject.Render != null)
			{
				this.ParentObject.Render.TileColor = this.TileColor;
			}
			if (this.DetailColor != null && this.ParentObject.Render != null)
			{
				this.ParentObject.Render.DetailColor = this.DetailColor;
			}
			this.ParentObject.RemovePart(this);
		}

		// Token: 0x04003D30 RID: 15664
		public string ColorString;

		// Token: 0x04003D31 RID: 15665
		public string TileColor;

		// Token: 0x04003D32 RID: 15666
		public string DetailColor;
	}
}
