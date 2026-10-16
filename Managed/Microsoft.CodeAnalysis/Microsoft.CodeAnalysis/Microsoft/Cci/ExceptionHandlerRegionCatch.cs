using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x02000086 RID: 134
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ExceptionHandlerRegionCatch : ExceptionHandlerRegion
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		public ExceptionHandlerRegionCatch(int tryStartOffset, int tryEndOffset, int handlerStartOffset, int handlerEndOffset, ITypeReference exceptionType)
			: base(tryStartOffset, tryEndOffset, handlerStartOffset, handlerEndOffset)
		{
			this._exceptionType = exceptionType;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000CFD5 File Offset: 0x0000B1D5
		public override ExceptionRegionKind HandlerKind
		{
			get
			{
				return ExceptionRegionKind.Catch;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000CFD8 File Offset: 0x0000B1D8
		public override ITypeReference ExceptionType
		{
			get
			{
				return this._exceptionType;
			}
		}

		// Token: 0x04000176 RID: 374
		private readonly ITypeReference _exceptionType;
	}
}
