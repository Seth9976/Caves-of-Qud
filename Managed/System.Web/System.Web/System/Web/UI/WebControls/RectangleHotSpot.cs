using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines a rectangular hot spot region in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003F6 RID: 1014
	public sealed class RectangleHotSpot : HotSpot
	{
		/// <summary>Returns a string that represents the x -and y-coordinates of a <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object's top left corner and the x- and y-coordinates of its bottom right corner.</summary>
		/// <returns>A string that represents the x- and y-coordinates of a <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object's top left corner and the x- and y-coordinates of its bottom right corner.</returns>
		// Token: 0x06002CC9 RID: 11465 RVA: 0x00074C78 File Offset: 0x00072E78
		public override string GetCoordinates()
		{
			return string.Concat(new string[]
			{
				this.Left.ToString(),
				",",
				this.Top.ToString(),
				",",
				this.Right.ToString(),
				",",
				this.Bottom.ToString()
			});
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x00074CEC File Offset: 0x00072EEC
		protected internal override string MarkupName
		{
			get
			{
				return "rect";
			}
		}

		/// <summary>Gets or sets the x-coordinate of the left side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
		/// <returns>The x-coordinate of the left side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x00074CF4 File Offset: 0x00072EF4
		// (set) Token: 0x06002CCC RID: 11468 RVA: 0x00074D1D File Offset: 0x00072F1D
		[DefaultValue(0)]
		public int Left
		{
			get
			{
				object obj = base.ViewState["Left"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Left"] = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the top side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
		/// <returns>The y-coordinate of the top side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x00074D38 File Offset: 0x00072F38
		// (set) Token: 0x06002CCE RID: 11470 RVA: 0x00074D61 File Offset: 0x00072F61
		[DefaultValue(0)]
		public int Top
		{
			get
			{
				object obj = base.ViewState["Top"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Top"] = value;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the right side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
		/// <returns>The x-coordinate of the right side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x00074D7C File Offset: 0x00072F7C
		// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x00074DA5 File Offset: 0x00072FA5
		[DefaultValue(0)]
		public int Right
		{
			get
			{
				object obj = base.ViewState["Right"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Right"] = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the bottom side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
		/// <returns>The y-coordinate of the bottom side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x00074DC0 File Offset: 0x00072FC0
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x00074DE9 File Offset: 0x00072FE9
		[DefaultValue(0)]
		public int Bottom
		{
			get
			{
				object obj = base.ViewState["Bottom"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Bottom"] = value;
			}
		}
	}
}
