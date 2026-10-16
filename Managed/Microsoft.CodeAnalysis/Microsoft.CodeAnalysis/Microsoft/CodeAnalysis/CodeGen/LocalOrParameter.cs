using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004CD RID: 1229
	[NullableContext(2)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal struct LocalOrParameter
	{
		// Token: 0x0600317D RID: 12669 RVA: 0x0008DAE9 File Offset: 0x0008BCE9
		private LocalOrParameter(LocalDefinition local, int parameterIndex)
		{
			this.Local = local;
			this.ParameterIndex = parameterIndex;
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x0008DAF9 File Offset: 0x0008BCF9
		public static implicit operator LocalOrParameter(LocalDefinition local)
		{
			return new LocalOrParameter(local, -1);
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x0008DB02 File Offset: 0x0008BD02
		public static implicit operator LocalOrParameter(int parameterIndex)
		{
			return new LocalOrParameter(null, parameterIndex);
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x0008DB0C File Offset: 0x0008BD0C
		[NullableContext(1)]
		private string GetDebuggerDisplay()
		{
			if (this.Local == null)
			{
				return this.ParameterIndex.ToString();
			}
			return this.Local.GetDebuggerDisplay();
		}

		// Token: 0x04001316 RID: 4886
		public readonly LocalDefinition Local;

		// Token: 0x04001317 RID: 4887
		public readonly int ParameterIndex;
	}
}
