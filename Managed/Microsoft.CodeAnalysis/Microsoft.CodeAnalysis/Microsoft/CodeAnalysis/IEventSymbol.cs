using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200023F RID: 575
	[NullableContext(2)]
	public interface IEventSymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001624 RID: 5668
		[Nullable(1)]
		ITypeSymbol Type
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001625 RID: 5669
		NullableAnnotation NullableAnnotation { get; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001626 RID: 5670
		bool IsWindowsRuntimeEvent { get; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001627 RID: 5671
		IMethodSymbol AddMethod { get; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001628 RID: 5672
		IMethodSymbol RemoveMethod { get; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001629 RID: 5673
		IMethodSymbol RaiseMethod { get; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600162A RID: 5674
		[Nullable(1)]
		IEventSymbol OriginalDefinition
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600162B RID: 5675
		IEventSymbol OverriddenEvent { get; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600162C RID: 5676
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IEventSymbol> ExplicitInterfaceImplementations
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
