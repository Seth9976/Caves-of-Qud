using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines a polygon-shaped hot spot region in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003F0 RID: 1008
	public sealed class PolygonHotSpot : HotSpot
	{
		/// <summary>Returns a string that represents the coordinates of the vertexes of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object.</summary>
		/// <returns>A string that represents the coordinates of the vertexes of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object. The default value is an empty string ("").</returns>
		// Token: 0x06002C7B RID: 11387 RVA: 0x000743B1 File Offset: 0x000725B1
		public override string GetCoordinates()
		{
			return this.Coordinates;
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000743B9 File Offset: 0x000725B9
		protected internal override string MarkupName
		{
			get
			{
				return "poly";
			}
		}

		/// <summary>A string of coordinates that represents the vertexes of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object.</summary>
		/// <returns>A string that represents the coordinates of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object's vertexes.</returns>
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000743C0 File Offset: 0x000725C0
		// (set) Token: 0x06002C7E RID: 11390 RVA: 0x000743ED File Offset: 0x000725ED
		[DefaultValue("")]
		public string Coordinates
		{
			get
			{
				object obj = base.ViewState["Coordinates"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["Coordinates"] = value;
			}
		}
	}
}
