using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides a basic utility class that is used to store three related objects.</summary>
	// Token: 0x02000238 RID: 568
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class Triplet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Triplet" /> class. </summary>
		// Token: 0x06001774 RID: 6004 RVA: 0x00002050 File Offset: 0x00000250
		public Triplet()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Triplet" /> class and sets the first two objects. </summary>
		/// <param name="x">Object assigned to <see cref="F:System.Web.UI.Triplet.First" />.</param>
		/// <param name="y">Object assigned to <see cref="F:System.Web.UI.Triplet.Second" />.</param>
		// Token: 0x06001775 RID: 6005 RVA: 0x0003DC80 File Offset: 0x0003BE80
		public Triplet(object x, object y)
		{
			this.First = x;
			this.Second = y;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Triplet" /> class with the provided three objects. </summary>
		/// <param name="x">Object assigned to <see cref="F:System.Web.UI.Triplet.First" />.</param>
		/// <param name="y">Object assigned to <see cref="F:System.Web.UI.Triplet.Second" />.</param>
		/// <param name="z">Object assigned to <see cref="F:System.Web.UI.Triplet.Third" />.</param>
		// Token: 0x06001776 RID: 6006 RVA: 0x0003DC96 File Offset: 0x0003BE96
		public Triplet(object x, object y, object z)
		{
			this.First = x;
			this.Second = y;
			this.Third = z;
		}

		/// <summary>Gets or sets the first object of the triplet.</summary>
		// Token: 0x040015E6 RID: 5606
		public object First;

		/// <summary>Gets or sets the second object of the triplet.</summary>
		// Token: 0x040015E7 RID: 5607
		public object Second;

		/// <summary>Gets or sets the third object of the triplet.</summary>
		// Token: 0x040015E8 RID: 5608
		public object Third;
	}
}
