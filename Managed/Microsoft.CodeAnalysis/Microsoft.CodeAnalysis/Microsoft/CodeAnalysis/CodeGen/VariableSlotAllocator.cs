using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004E7 RID: 1255
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class VariableSlotAllocator
	{
		// Token: 0x06003288 RID: 12936
		public abstract void AddPreviousLocals(ArrayBuilder<ILocalDefinition> builder);

		// Token: 0x06003289 RID: 12937
		[return: Nullable(2)]
		public abstract LocalDefinition GetPreviousLocal(ITypeReference type, ILocalSymbolInternal symbol, [Nullable(2)] string name, SynthesizedLocalKind kind, LocalDebugId id, LocalVariableAttributes pdbAttributes, LocalSlotConstraints constraints, [Nullable(0)] ImmutableArray<bool> dynamicTransformFlags, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> tupleElementNames);

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x0600328A RID: 12938
		[Nullable(2)]
		public abstract string PreviousStateMachineTypeName
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x0600328B RID: 12939
		public abstract bool TryGetPreviousHoistedLocalSlotIndex(SyntaxNode currentDeclarator, ITypeReference currentType, SynthesizedLocalKind synthesizedKind, LocalDebugId currentId, DiagnosticBag diagnostics, out int slotIndex);

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x0600328C RID: 12940
		public abstract int PreviousHoistedLocalSlotCount { get; }

		// Token: 0x0600328D RID: 12941
		public abstract bool TryGetPreviousAwaiterSlotIndex(ITypeReference currentType, DiagnosticBag diagnostics, out int slotIndex);

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x0600328E RID: 12942
		public abstract int PreviousAwaiterSlotCount { get; }

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x0600328F RID: 12943
		public abstract DebugId? MethodId { get; }

		// Token: 0x06003290 RID: 12944
		public abstract bool TryGetPreviousClosure(SyntaxNode closureSyntax, out DebugId closureId);

		// Token: 0x06003291 RID: 12945
		public abstract bool TryGetPreviousLambda(SyntaxNode lambdaOrLambdaBodySyntax, bool isLambdaBody, out DebugId lambdaId);
	}
}
