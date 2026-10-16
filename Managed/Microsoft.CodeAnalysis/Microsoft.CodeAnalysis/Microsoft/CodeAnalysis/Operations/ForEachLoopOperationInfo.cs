using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000464 RID: 1124
	[NullableContext(1)]
	[Nullable(0)]
	internal class ForEachLoopOperationInfo
	{
		// Token: 0x06002C25 RID: 11301 RVA: 0x0007CD4C File Offset: 0x0007AF4C
		public ForEachLoopOperationInfo(ITypeSymbol elementType, IMethodSymbol getEnumeratorMethod, IPropertySymbol currentProperty, IMethodSymbol moveNextMethod, bool isAsynchronous, bool needsDispose, bool knownToImplementIDisposable, [Nullable(2)] IMethodSymbol patternDisposeMethod, IConvertibleConversion currentConversion, IConvertibleConversion elementConversion, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> getEnumeratorArguments = default(ImmutableArray<IArgumentOperation>), [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> moveNextArguments = default(ImmutableArray<IArgumentOperation>), [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> currentArguments = default(ImmutableArray<IArgumentOperation>), [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> disposeArguments = default(ImmutableArray<IArgumentOperation>))
		{
			this.ElementType = elementType;
			this.GetEnumeratorMethod = getEnumeratorMethod;
			this.CurrentProperty = currentProperty;
			this.MoveNextMethod = moveNextMethod;
			this.IsAsynchronous = isAsynchronous;
			this.KnownToImplementIDisposable = knownToImplementIDisposable;
			this.NeedsDispose = needsDispose;
			this.PatternDisposeMethod = patternDisposeMethod;
			this.CurrentConversion = currentConversion;
			this.ElementConversion = elementConversion;
			this.GetEnumeratorArguments = getEnumeratorArguments;
			this.MoveNextArguments = moveNextArguments;
			this.CurrentArguments = currentArguments;
			this.DisposeArguments = disposeArguments;
		}

		// Token: 0x04001175 RID: 4469
		public readonly ITypeSymbol ElementType;

		// Token: 0x04001176 RID: 4470
		public readonly IMethodSymbol GetEnumeratorMethod;

		// Token: 0x04001177 RID: 4471
		public readonly IPropertySymbol CurrentProperty;

		// Token: 0x04001178 RID: 4472
		public readonly IMethodSymbol MoveNextMethod;

		// Token: 0x04001179 RID: 4473
		public readonly bool IsAsynchronous;

		// Token: 0x0400117A RID: 4474
		public readonly bool NeedsDispose;

		// Token: 0x0400117B RID: 4475
		public readonly bool KnownToImplementIDisposable;

		// Token: 0x0400117C RID: 4476
		[Nullable(2)]
		public readonly IMethodSymbol PatternDisposeMethod;

		// Token: 0x0400117D RID: 4477
		public readonly IConvertibleConversion CurrentConversion;

		// Token: 0x0400117E RID: 4478
		public readonly IConvertibleConversion ElementConversion;

		// Token: 0x0400117F RID: 4479
		[Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<IArgumentOperation> GetEnumeratorArguments;

		// Token: 0x04001180 RID: 4480
		[Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<IArgumentOperation> MoveNextArguments;

		// Token: 0x04001181 RID: 4481
		[Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<IArgumentOperation> CurrentArguments;

		// Token: 0x04001182 RID: 4482
		[Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<IArgumentOperation> DisposeArguments;
	}
}
