using System;

namespace System.CodeDom
{
	/// <summary>Represents a static constructor for a class.</summary>
	// Token: 0x02000335 RID: 821
	[Serializable]
	public class CodeTypeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeConstructor" /> class.</summary>
		// Token: 0x060019F3 RID: 6643 RVA: 0x00060F64 File Offset: 0x0005F164
		public CodeTypeConstructor()
		{
			base.Name = ".cctor";
		}
	}
}
