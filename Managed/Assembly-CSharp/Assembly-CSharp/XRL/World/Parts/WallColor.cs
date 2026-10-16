using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F12 RID: 3858
	[Serializable]
	public class WallColor : IPart
	{
		// Token: 0x060099C9 RID: 39369 RVA: 0x0037DB20 File Offset: 0x0037BD20
		public override bool Render(RenderEvent E)
		{
			GameObject parentObject = this.ParentObject;
			GameObject gameObject;
			if (parentObject == null)
			{
				gameObject = null;
			}
			else
			{
				Cell currentCell = parentObject.CurrentCell;
				gameObject = ((currentCell != null) ? currentCell.GetFirstWall() : null);
			}
			GameObject gameObject2 = gameObject;
			if (gameObject2 != null)
			{
				string foregroundColor = gameObject2.Render.GetForegroundColor();
				if (this.AsBackground)
				{
					E.ApplyColors("&" + this.VaryColor(E.GetForegroundColorChar().ToString(), foregroundColor), "^" + foregroundColor, this.VaryColor(E.GetDetailColorChar().ToString(), foregroundColor), this.ColorPriority, this.ColorPriority, this.ColorPriority);
				}
				else
				{
					E.ApplyColors("&" + foregroundColor, this.VaryColor(E.GetDetailColorChar().ToString(), foregroundColor), this.ColorPriority, this.ColorPriority);
				}
			}
			return base.Render(E);
		}

		// Token: 0x060099CA RID: 39370 RVA: 0x0037DBF8 File Offset: 0x0037BDF8
		public string VaryColor(string Color, string RelativeTo)
		{
			if (!(Color == RelativeTo))
			{
				return Color;
			}
			if (Color[0] >= 'A' && Color[0] <= 'Z')
			{
				return Color.ToLower();
			}
			return Color.ToUpper();
		}

		// Token: 0x04004015 RID: 16405
		public bool AsBackground;

		// Token: 0x04004016 RID: 16406
		public int ColorPriority = 100;
	}
}
