using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000489 RID: 1161
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class EncVariableSlotAllocator : VariableSlotAllocator
	{
		// Token: 0x06002DAE RID: 11694 RVA: 0x0008198C File Offset: 0x0007FB8C
		public EncVariableSlotAllocator(SymbolMatcher symbolMap, [Nullable(new byte[] { 2, 1, 2 })] Func<SyntaxNode, SyntaxNode> syntaxMap, IMethodSymbolInternal previousTopLevelMethod, DebugId methodId, [Nullable(0)] ImmutableArray<EncLocalInfo> previousLocals, [Nullable(new byte[] { 2, 0 })] IReadOnlyDictionary<int, KeyValuePair<DebugId, int>> lambdaMap, [Nullable(2)] IReadOnlyDictionary<int, DebugId> closureMap, [Nullable(2)] string stateMachineTypeName, int hoistedLocalSlotCount, [Nullable(2)] IReadOnlyDictionary<EncHoistedLocalInfo, int> hoistedLocalSlots, int awaiterCount, [Nullable(new byte[] { 2, 1 })] IReadOnlyDictionary<ITypeReference, int> awaiterMap, LambdaSyntaxFacts lambdaSyntaxFacts)
		{
			this._symbolMap = symbolMap;
			this._syntaxMap = syntaxMap;
			this._previousLocals = previousLocals;
			this._previousTopLevelMethod = previousTopLevelMethod;
			this._methodId = methodId;
			this._hoistedLocalSlots = hoistedLocalSlots;
			this._hoistedLocalSlotCount = hoistedLocalSlotCount;
			this._stateMachineTypeName = stateMachineTypeName;
			this._awaiterCount = awaiterCount;
			this._awaiterMap = awaiterMap;
			this._lambdaMap = lambdaMap;
			this._closureMap = closureMap;
			this._lambdaSyntaxFacts = lambdaSyntaxFacts;
			Dictionary<EncLocalInfo, int> dictionary = new Dictionary<EncLocalInfo, int>();
			for (int i = 0; i < previousLocals.Length; i++)
			{
				EncLocalInfo encLocalInfo = previousLocals[i];
				if (!encLocalInfo.IsUnused)
				{
					dictionary.Add(encLocalInfo, i);
				}
			}
			this._previousLocalSlots = dictionary;
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x00081A3C File Offset: 0x0007FC3C
		public override DebugId? MethodId
		{
			get
			{
				return new DebugId?(this._methodId);
			}
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x00081A49 File Offset: 0x0007FC49
		private int CalculateSyntaxOffsetInPreviousMethod(SyntaxNode node)
		{
			return this._previousTopLevelMethod.CalculateLocalSyntaxOffset(this._lambdaSyntaxFacts.GetDeclaratorPosition(node), node.SyntaxTree);
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x00081A68 File Offset: 0x0007FC68
		public override void AddPreviousLocals(ArrayBuilder<ILocalDefinition> builder)
		{
			builder.AddRange(Enumerable.Select<EncLocalInfo, SignatureOnlyLocalDefinition>(this._previousLocals, (EncLocalInfo info, int index) => new SignatureOnlyLocalDefinition(info.Signature, index)));
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x00081AA0 File Offset: 0x0007FCA0
		private bool TryGetPreviousLocalId(SyntaxNode currentDeclarator, LocalDebugId currentId, out LocalDebugId previousId)
		{
			if (this._syntaxMap == null)
			{
				previousId = currentId;
				return true;
			}
			SyntaxNode syntaxNode = this._syntaxMap.Invoke(currentDeclarator);
			if (syntaxNode == null)
			{
				previousId = default(LocalDebugId);
				return false;
			}
			int num = this.CalculateSyntaxOffsetInPreviousMethod(syntaxNode);
			previousId = new LocalDebugId(num, currentId.Ordinal);
			return true;
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00081AF4 File Offset: 0x0007FCF4
		[return: Nullable(2)]
		public override LocalDefinition GetPreviousLocal(ITypeReference currentType, ILocalSymbolInternal currentLocalSymbol, [Nullable(2)] string name, SynthesizedLocalKind kind, LocalDebugId id, LocalVariableAttributes pdbAttributes, LocalSlotConstraints constraints, [Nullable(0)] ImmutableArray<bool> dynamicTransformFlags, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> tupleElementNames)
		{
			if (id.IsNone)
			{
				return null;
			}
			LocalDebugId localDebugId;
			if (!this.TryGetPreviousLocalId(currentLocalSymbol.GetDeclaratorSyntax(), id, out localDebugId))
			{
				return null;
			}
			ITypeReference typeReference = this._symbolMap.MapReference(currentType);
			if (typeReference == null)
			{
				return null;
			}
			EncLocalInfo encLocalInfo = new EncLocalInfo(new LocalSlotDebugInfo(kind, localDebugId), typeReference, constraints, null);
			int num;
			if (!this._previousLocalSlots.TryGetValue(encLocalInfo, ref num))
			{
				return null;
			}
			return new LocalDefinition(currentLocalSymbol, name, currentType, num, kind, id, pdbAttributes, constraints, dynamicTransformFlags, tupleElementNames);
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x00081B6C File Offset: 0x0007FD6C
		[Nullable(2)]
		public override string PreviousStateMachineTypeName
		{
			[NullableContext(2)]
			get
			{
				return this._stateMachineTypeName;
			}
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x00081B74 File Offset: 0x0007FD74
		public override bool TryGetPreviousHoistedLocalSlotIndex(SyntaxNode currentDeclarator, ITypeReference currentType, SynthesizedLocalKind synthesizedKind, LocalDebugId currentId, DiagnosticBag diagnostics, out int slotIndex)
		{
			if (this._hoistedLocalSlots == null)
			{
				slotIndex = -1;
				return false;
			}
			LocalDebugId localDebugId;
			if (!this.TryGetPreviousLocalId(currentDeclarator, currentId, out localDebugId))
			{
				slotIndex = -1;
				return false;
			}
			ITypeReference typeReference = this._symbolMap.MapReference(currentType);
			if (typeReference == null)
			{
				slotIndex = -1;
				return false;
			}
			EncHoistedLocalInfo encHoistedLocalInfo = new EncHoistedLocalInfo(new LocalSlotDebugInfo(synthesizedKind, localDebugId), typeReference);
			return this._hoistedLocalSlots.TryGetValue(encHoistedLocalInfo, ref slotIndex);
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x00081BD5 File Offset: 0x0007FDD5
		public override int PreviousHoistedLocalSlotCount
		{
			get
			{
				return this._hoistedLocalSlotCount;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x00081BDD File Offset: 0x0007FDDD
		public override int PreviousAwaiterSlotCount
		{
			get
			{
				return this._awaiterCount;
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x00081BE8 File Offset: 0x0007FDE8
		public override bool TryGetPreviousAwaiterSlotIndex(ITypeReference currentType, DiagnosticBag diagnostics, out int slotIndex)
		{
			if (this._awaiterMap == null)
			{
				slotIndex = -1;
				return false;
			}
			ITypeReference typeReference = this._symbolMap.MapReference(currentType);
			return this._awaiterMap.TryGetValue(typeReference, ref slotIndex);
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x00081C1C File Offset: 0x0007FE1C
		private bool TryGetPreviousSyntaxOffset(SyntaxNode currentSyntax, out int previousSyntaxOffset)
		{
			Func<SyntaxNode, SyntaxNode> syntaxMap = this._syntaxMap;
			SyntaxNode syntaxNode = ((syntaxMap != null) ? syntaxMap.Invoke(currentSyntax) : null);
			if (syntaxNode == null)
			{
				previousSyntaxOffset = 0;
				return false;
			}
			previousSyntaxOffset = this.CalculateSyntaxOffsetInPreviousMethod(syntaxNode);
			return true;
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x00081C50 File Offset: 0x0007FE50
		private bool TryGetPreviousLambdaSyntaxOffset(SyntaxNode lambdaOrLambdaBodySyntax, bool isLambdaBody, out int previousSyntaxOffset)
		{
			SyntaxNode syntaxNode = (isLambdaBody ? this._lambdaSyntaxFacts.GetLambda(lambdaOrLambdaBodySyntax) : lambdaOrLambdaBodySyntax);
			Func<SyntaxNode, SyntaxNode> syntaxMap = this._syntaxMap;
			SyntaxNode syntaxNode2 = ((syntaxMap != null) ? syntaxMap.Invoke(syntaxNode) : null);
			if (syntaxNode2 == null)
			{
				previousSyntaxOffset = 0;
				return false;
			}
			SyntaxNode syntaxNode3;
			if (isLambdaBody)
			{
				syntaxNode3 = this._lambdaSyntaxFacts.TryGetCorrespondingLambdaBody(syntaxNode2, lambdaOrLambdaBodySyntax);
				if (syntaxNode3 == null)
				{
					previousSyntaxOffset = 0;
					return false;
				}
			}
			else
			{
				syntaxNode3 = syntaxNode2;
			}
			previousSyntaxOffset = this.CalculateSyntaxOffsetInPreviousMethod(syntaxNode3);
			return true;
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x00081CB4 File Offset: 0x0007FEB4
		public override bool TryGetPreviousClosure(SyntaxNode scopeSyntax, out DebugId closureId)
		{
			int num;
			if (this._closureMap != null && this.TryGetPreviousSyntaxOffset(scopeSyntax, out num) && this._closureMap.TryGetValue(num, ref closureId))
			{
				return true;
			}
			closureId = default(DebugId);
			return false;
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x00081CF0 File Offset: 0x0007FEF0
		public override bool TryGetPreviousLambda(SyntaxNode lambdaOrLambdaBodySyntax, bool isLambdaBody, out DebugId lambdaId)
		{
			int num;
			KeyValuePair<DebugId, int> keyValuePair;
			if (this._lambdaMap != null && this.TryGetPreviousLambdaSyntaxOffset(lambdaOrLambdaBodySyntax, isLambdaBody, out num) && this._lambdaMap.TryGetValue(num, ref keyValuePair))
			{
				lambdaId = keyValuePair.Key;
				return true;
			}
			lambdaId = default(DebugId);
			return false;
		}

		// Token: 0x04001234 RID: 4660
		private readonly SymbolMatcher _symbolMap;

		// Token: 0x04001235 RID: 4661
		[Nullable(new byte[] { 2, 1, 2 })]
		private readonly Func<SyntaxNode, SyntaxNode> _syntaxMap;

		// Token: 0x04001236 RID: 4662
		private readonly IMethodSymbolInternal _previousTopLevelMethod;

		// Token: 0x04001237 RID: 4663
		private readonly DebugId _methodId;

		// Token: 0x04001238 RID: 4664
		private readonly IReadOnlyDictionary<EncLocalInfo, int> _previousLocalSlots;

		// Token: 0x04001239 RID: 4665
		[Nullable(0)]
		private readonly ImmutableArray<EncLocalInfo> _previousLocals;

		// Token: 0x0400123A RID: 4666
		[Nullable(2)]
		private readonly string _stateMachineTypeName;

		// Token: 0x0400123B RID: 4667
		private readonly int _hoistedLocalSlotCount;

		// Token: 0x0400123C RID: 4668
		[Nullable(2)]
		private readonly IReadOnlyDictionary<EncHoistedLocalInfo, int> _hoistedLocalSlots;

		// Token: 0x0400123D RID: 4669
		private readonly int _awaiterCount;

		// Token: 0x0400123E RID: 4670
		[Nullable(new byte[] { 2, 1 })]
		private readonly IReadOnlyDictionary<ITypeReference, int> _awaiterMap;

		// Token: 0x0400123F RID: 4671
		[Nullable(new byte[] { 2, 0 })]
		private readonly IReadOnlyDictionary<int, KeyValuePair<DebugId, int>> _lambdaMap;

		// Token: 0x04001240 RID: 4672
		[Nullable(2)]
		private readonly IReadOnlyDictionary<int, DebugId> _closureMap;

		// Token: 0x04001241 RID: 4673
		private readonly LambdaSyntaxFacts _lambdaSyntaxFacts;
	}
}
