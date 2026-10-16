using System;

namespace System.Web.UI
{
	/// <summary>Provides a basic utility class that is used to store two related objects. </summary>
	// Token: 0x0200018B RID: 395
	[Serializable]
	public sealed class Pair
	{
		/// <summary>Creates a new, uninitialized instance of the <see cref="T:System.Web.UI.Pair" /> class.</summary>
		// Token: 0x06000F77 RID: 3959 RVA: 0x00002050 File Offset: 0x00000250
		public Pair()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Pair" /> class, using the specified object pair.</summary>
		/// <param name="x">An object. </param>
		/// <param name="y">An object. </param>
		// Token: 0x06000F78 RID: 3960 RVA: 0x00028B49 File Offset: 0x00026D49
		public Pair(object x, object y)
		{
			this.First = x;
			this.Second = y;
		}

		/// <summary>Gets or sets the first object of the object pair.</summary>
		// Token: 0x04001316 RID: 4886
		public object First;

		/// <summary>Gets or sets the second object of the object pair.</summary>
		// Token: 0x04001317 RID: 4887
		public object Second;
	}
}
