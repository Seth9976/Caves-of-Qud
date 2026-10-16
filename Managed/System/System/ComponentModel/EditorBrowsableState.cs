using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the browsable state of a property or method from within an editor.</summary>
	// Token: 0x02000363 RID: 867
	public enum EditorBrowsableState
	{
		/// <summary>The property or method is always browsable from within an editor.</summary>
		// Token: 0x04000EB0 RID: 3760
		Always,
		/// <summary>The property or method is never browsable from within an editor.</summary>
		// Token: 0x04000EB1 RID: 3761
		Never,
		/// <summary>The property or method is a feature that only advanced users should see. An editor can either show or hide such properties.</summary>
		// Token: 0x04000EB2 RID: 3762
		Advanced
	}
}
