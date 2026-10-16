using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Ensures that all finalization code in derived classes is marked as critical.</summary>
	// Token: 0x020007D7 RID: 2007
	public abstract class CriticalFinalizerObject
	{
		// Token: 0x060045BA RID: 17850 RVA: 0x000E5184 File Offset: 0x000E3384
		~CriticalFinalizerObject()
		{
		}
	}
}
