using System;
using Unity.Baselib.LowLevel;

namespace Unity.Baselib
{
	// Token: 0x02000016 RID: 22
	internal class BaselibException : Exception
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002238 File Offset: 0x00000438
		internal BaselibException(ErrorState errorState)
			: base(errorState.Explain(Binding.Baselib_ErrorState_ExplainVerbosity.ErrorType_SourceLocation_Explanation))
		{
			this.errorState = errorState;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002254 File Offset: 0x00000454
		public Binding.Baselib_ErrorCode ErrorCode
		{
			get
			{
				return this.errorState.ErrorCode;
			}
		}

		// Token: 0x04000029 RID: 41
		private readonly ErrorState errorState;
	}
}
