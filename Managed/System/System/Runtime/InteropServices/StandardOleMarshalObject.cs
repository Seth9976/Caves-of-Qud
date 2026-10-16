using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Replaces the standard common language runtime (CLR) free-threaded marshaler with the standard OLE STA marshaler.</summary>
	// Token: 0x02000189 RID: 393
	[ComVisible(true)]
	[MonoLimitation("The runtime does nothing special apart from what it already does with marshal-by-ref objects")]
	public class StandardOleMarshalObject : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.StandardOleMarshalObject" /> class. </summary>
		// Token: 0x06000A77 RID: 2679 RVA: 0x0002D73C File Offset: 0x0002B93C
		protected StandardOleMarshalObject()
		{
		}
	}
}
