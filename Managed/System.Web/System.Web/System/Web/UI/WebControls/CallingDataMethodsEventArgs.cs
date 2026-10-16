using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ModelDataSource.CallingDataMethods" /> event.</summary>
	// Token: 0x02000281 RID: 641
	public class CallingDataMethodsEventArgs : EventArgs
	{
		/// <summary>The type that contains the data methods to call, when the data methods are static methods.</summary>
		/// <returns>The type that contains the static data methods to call, or null if the data methods are not static methods.</returns>
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x000432FB File Offset: 0x000414FB
		// (set) Token: 0x06001A30 RID: 6704 RVA: 0x00043303 File Offset: 0x00041503
		public Type DataMethodsType { get; set; }

		/// <summary>An object that contains the data methods to call, when the data methods are not static methods on a type.</summary>
		/// <returns>The instance that contains the data methods to call, or null if the data methods are static methods.</returns>
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x0004330C File Offset: 0x0004150C
		// (set) Token: 0x06001A32 RID: 6706 RVA: 0x00043314 File Offset: 0x00041514
		public object DataMethodsObject { get; set; }
	}
}
