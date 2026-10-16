using System;

namespace System.Web.UI
{
	/// <summary>Provides data for any events that occur when a user clicks an image-based ASP.NET server control, such as the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> or <see cref="T:System.Web.UI.WebControls.ImageButton" /> server controls. This class cannot be inherited.</summary>
	// Token: 0x02000186 RID: 390
	public sealed class ImageClickEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ImageClickEventArgs" /> class using the <paramref name="x" /> and <paramref name="y" /> parameters.</summary>
		/// <param name="x">The x-coordinate where the user clicked an image-based ASP.NET server control. </param>
		/// <param name="y">The y-coordinate where the user clicked an image-based ASP.NET server control. </param>
		// Token: 0x06000F68 RID: 3944 RVA: 0x00028A58 File Offset: 0x00026C58
		public ImageClickEventArgs(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ImageClickEventArgs" /> class using the <paramref name="x" />, <paramref name="y" />, <paramref name="xRaw" />, and <paramref name="yRaw" /> parameters.</summary>
		/// <param name="x">The x-coordinate where a user clicked an image-based ASP.NET server control.</param>
		/// <param name="y">The y-coordinate where a user clicked an image-based ASP.NET server control.</param>
		/// <param name="xRaw">The raw x-coordinate where a user clicked an image-based ASP.NET server control.</param>
		/// <param name="yRaw">The raw y-coordinate where a user clicked an image-based ASP.NET server control.</param>
		// Token: 0x06000F69 RID: 3945 RVA: 0x00028A6E File Offset: 0x00026C6E
		public ImageClickEventArgs(int x, int y, double xRaw, double yRaw)
		{
			this.X = x;
			this.Y = y;
			this.XRaw = xRaw;
			this.YRaw = yRaw;
		}

		/// <summary>An integer that represents the x-coordinate where a user clicked an image-based ASP.NET server control.</summary>
		// Token: 0x04001309 RID: 4873
		public int X;

		/// <summary>An integer that represents the y-coordinate where a user clicked an image-based ASP.NET server control.</summary>
		// Token: 0x0400130A RID: 4874
		public int Y;

		/// <summary>An integer that represents the raw x-coordinate where a user clicked an image-based ASP.NET server control.</summary>
		// Token: 0x0400130B RID: 4875
		public double XRaw;

		/// <summary>An integer that represents the raw y-coordinate where a user clicked an image-based ASP.NET server control.</summary>
		// Token: 0x0400130C RID: 4876
		public double YRaw;
	}
}
