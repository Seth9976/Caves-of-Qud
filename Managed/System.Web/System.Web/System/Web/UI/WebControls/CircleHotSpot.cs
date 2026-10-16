using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines a circular hot spot region in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200034B RID: 843
	public sealed class CircleHotSpot : HotSpot
	{
		/// <summary>Returns a string that represents the x- and y-coordinates of a <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object's center and the length of its radius.</summary>
		/// <returns>A string that represents the x- and y-coordinates of a <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object's center and the length of its radius.</returns>
		// Token: 0x06001F24 RID: 7972 RVA: 0x0004CDC4 File Offset: 0x0004AFC4
		public override string GetCoordinates()
		{
			return string.Concat(new string[]
			{
				this.X.ToString(),
				",",
				this.Y.ToString(),
				",",
				this.Radius.ToString()
			});
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x0004CE1F File Offset: 0x0004B01F
		protected internal override string MarkupName
		{
			get
			{
				return "circle";
			}
		}

		/// <summary>Gets or sets the distance from the center to the edge of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object.</summary>
		/// <returns>An integer that represents the distance in pixels from the center to the edge of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 0. </exception>
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0004CE26 File Offset: 0x0004B026
		// (set) Token: 0x06001F27 RID: 7975 RVA: 0x0004CE39 File Offset: 0x0004B039
		[DefaultValue(0)]
		public int Radius
		{
			get
			{
				return base.ViewState.GetInt("Radius", 0);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				base.ViewState["Radius"] = value;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object.</summary>
		/// <returns>The x-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x0004CE5B File Offset: 0x0004B05B
		// (set) Token: 0x06001F29 RID: 7977 RVA: 0x0004CE6E File Offset: 0x0004B06E
		[DefaultValue(0)]
		public int X
		{
			get
			{
				return base.ViewState.GetInt("X", 0);
			}
			set
			{
				base.ViewState["X"] = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object.</summary>
		/// <returns>The y-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x0004CE86 File Offset: 0x0004B086
		// (set) Token: 0x06001F2B RID: 7979 RVA: 0x0004CE99 File Offset: 0x0004B099
		[DefaultValue(0)]
		public int Y
		{
			get
			{
				return base.ViewState.GetInt("Y", 0);
			}
			set
			{
				base.ViewState["Y"] = value;
			}
		}
	}
}
