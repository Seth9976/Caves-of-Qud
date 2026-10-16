using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Debugging;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004D4 RID: 1236
	internal sealed class MethodBody : IMethodBody
	{
		// Token: 0x060031A5 RID: 12709 RVA: 0x0008DF04 File Offset: 0x0008C104
		[NullableContext(1)]
		public MethodBody([Nullable(0)] ImmutableArray<byte> ilBits, ushort maxStack, IMethodDefinition parent, DebugId methodId, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalDefinition> locals, SequencePointList sequencePoints, DebugDocumentProvider debugDocumentProvider, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ExceptionHandlerRegion> exceptionHandlers, bool areLocalsZeroed, bool hasStackalloc, [Nullable(0)] ImmutableArray<LocalScope> localScopes, bool hasDynamicLocalVariables, IImportScope importScopeOpt, [Nullable(0)] ImmutableArray<LambdaDebugInfo> lambdaDebugInfo, [Nullable(0)] ImmutableArray<ClosureDebugInfo> closureDebugInfo, string stateMachineTypeNameOpt, [Nullable(0)] ImmutableArray<StateMachineHoistedLocalScope> stateMachineHoistedLocalScopes, [Nullable(0)] ImmutableArray<EncHoistedLocalInfo> stateMachineHoistedLocalSlots, [Nullable(new byte[] { 0, 2 })] ImmutableArray<ITypeReference> stateMachineAwaiterSlots, StateMachineMoveNextBodyDebugInfo stateMachineMoveNextDebugInfoOpt, DynamicAnalysisMethodBodyData dynamicAnalysisDataOpt)
		{
			this._ilBits = ilBits;
			this._maxStack = maxStack;
			this._parent = parent;
			this._methodId = methodId;
			this._locals = locals;
			this._exceptionHandlers = exceptionHandlers;
			this._areLocalsZeroed = areLocalsZeroed;
			this.HasStackalloc = hasStackalloc;
			this._localScopes = localScopes;
			this._hasDynamicLocalVariables = hasDynamicLocalVariables;
			this._importScopeOpt = importScopeOpt;
			this._lambdaDebugInfo = lambdaDebugInfo;
			this._closureDebugInfo = closureDebugInfo;
			this._stateMachineTypeNameOpt = stateMachineTypeNameOpt;
			this._stateMachineHoistedLocalScopes = stateMachineHoistedLocalScopes;
			this._stateMachineHoistedLocalSlots = stateMachineHoistedLocalSlots;
			this._stateMachineAwaiterSlots = stateMachineAwaiterSlots;
			this._stateMachineMoveNextDebugInfoOpt = stateMachineMoveNextDebugInfoOpt;
			this._dynamicAnalysisDataOpt = dynamicAnalysisDataOpt;
			this._sequencePoints = MethodBody.GetSequencePoints(sequencePoints, debugDocumentProvider);
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x0008DFBC File Offset: 0x0008C1BC
		private static ImmutableArray<SequencePoint> GetSequencePoints([Nullable(2)] SequencePointList sequencePoints, [Nullable(1)] DebugDocumentProvider debugDocumentProvider)
		{
			if (sequencePoints == null || sequencePoints.IsEmpty)
			{
				return ImmutableArray<SequencePoint>.Empty;
			}
			ArrayBuilder<SequencePoint> instance = ArrayBuilder<SequencePoint>.GetInstance();
			sequencePoints.GetSequencePoints(debugDocumentProvider, instance);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x0008DFEE File Offset: 0x0008C1EE
		[Nullable(1)]
		DynamicAnalysisMethodBodyData IMethodBody.DynamicAnalysisData
		{
			[NullableContext(1)]
			get
			{
				return this._dynamicAnalysisDataOpt;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x0008DFF6 File Offset: 0x0008C1F6
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ExceptionHandlerRegion> IMethodBody.ExceptionRegions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._exceptionHandlers;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x0008DFFE File Offset: 0x0008C1FE
		bool IMethodBody.AreLocalsZeroed
		{
			get
			{
				return this._areLocalsZeroed;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x0008E006 File Offset: 0x0008C206
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalDefinition> IMethodBody.LocalVariables
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._locals;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x0008E00E File Offset: 0x0008C20E
		[Nullable(1)]
		IMethodDefinition IMethodBody.MethodDefinition
		{
			[NullableContext(1)]
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x0008E016 File Offset: 0x0008C216
		[Nullable(1)]
		StateMachineMoveNextBodyDebugInfo IMethodBody.MoveNextBodyInfo
		{
			[NullableContext(1)]
			get
			{
				return this._stateMachineMoveNextDebugInfoOpt;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x0008E01E File Offset: 0x0008C21E
		ushort IMethodBody.MaxStack
		{
			get
			{
				return this._maxStack;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x0008E026 File Offset: 0x0008C226
		public ImmutableArray<byte> IL
		{
			get
			{
				return this._ilBits;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x0008E02E File Offset: 0x0008C22E
		public ImmutableArray<SequencePoint> SequencePoints
		{
			get
			{
				return this._sequencePoints;
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x0008E036 File Offset: 0x0008C236
		ImmutableArray<LocalScope> IMethodBody.LocalScopes
		{
			get
			{
				return this._localScopes;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x0008E03E File Offset: 0x0008C23E
		[Nullable(1)]
		IImportScope IMethodBody.ImportScope
		{
			[NullableContext(1)]
			get
			{
				return this._importScopeOpt;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060031B2 RID: 12722 RVA: 0x0008E046 File Offset: 0x0008C246
		[Nullable(1)]
		string IMethodBody.StateMachineTypeName
		{
			[NullableContext(1)]
			get
			{
				return this._stateMachineTypeNameOpt;
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060031B3 RID: 12723 RVA: 0x0008E04E File Offset: 0x0008C24E
		ImmutableArray<StateMachineHoistedLocalScope> IMethodBody.StateMachineHoistedLocalScopes
		{
			get
			{
				return this._stateMachineHoistedLocalScopes;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x0008E056 File Offset: 0x0008C256
		ImmutableArray<EncHoistedLocalInfo> IMethodBody.StateMachineHoistedLocalSlots
		{
			get
			{
				return this._stateMachineHoistedLocalSlots;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x0008E05E File Offset: 0x0008C25E
		[Nullable(new byte[] { 0, 2 })]
		ImmutableArray<ITypeReference> IMethodBody.StateMachineAwaiterSlots
		{
			[return: Nullable(new byte[] { 0, 2 })]
			get
			{
				return this._stateMachineAwaiterSlots;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x0008E066 File Offset: 0x0008C266
		bool IMethodBody.HasDynamicLocalVariables
		{
			get
			{
				return this._hasDynamicLocalVariables;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x0008E06E File Offset: 0x0008C26E
		public DebugId MethodId
		{
			get
			{
				return this._methodId;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x0008E076 File Offset: 0x0008C276
		public ImmutableArray<LambdaDebugInfo> LambdaDebugInfo
		{
			get
			{
				return this._lambdaDebugInfo;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060031B9 RID: 12729 RVA: 0x0008E07E File Offset: 0x0008C27E
		public ImmutableArray<ClosureDebugInfo> ClosureDebugInfo
		{
			get
			{
				return this._closureDebugInfo;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x0008E086 File Offset: 0x0008C286
		public bool HasStackalloc { get; }

		// Token: 0x04001328 RID: 4904
		[Nullable(1)]
		private readonly IMethodDefinition _parent;

		// Token: 0x04001329 RID: 4905
		private readonly ImmutableArray<byte> _ilBits;

		// Token: 0x0400132A RID: 4906
		private readonly ushort _maxStack;

		// Token: 0x0400132B RID: 4907
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<ILocalDefinition> _locals;

		// Token: 0x0400132C RID: 4908
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<ExceptionHandlerRegion> _exceptionHandlers;

		// Token: 0x0400132D RID: 4909
		private readonly bool _areLocalsZeroed;

		// Token: 0x0400132E RID: 4910
		private readonly ImmutableArray<SequencePoint> _sequencePoints;

		// Token: 0x0400132F RID: 4911
		private readonly ImmutableArray<LocalScope> _localScopes;

		// Token: 0x04001330 RID: 4912
		[Nullable(1)]
		private readonly IImportScope _importScopeOpt;

		// Token: 0x04001331 RID: 4913
		[Nullable(1)]
		private readonly string _stateMachineTypeNameOpt;

		// Token: 0x04001332 RID: 4914
		private readonly ImmutableArray<StateMachineHoistedLocalScope> _stateMachineHoistedLocalScopes;

		// Token: 0x04001333 RID: 4915
		private readonly bool _hasDynamicLocalVariables;

		// Token: 0x04001334 RID: 4916
		[Nullable(1)]
		private readonly StateMachineMoveNextBodyDebugInfo _stateMachineMoveNextDebugInfoOpt;

		// Token: 0x04001335 RID: 4917
		private readonly DebugId _methodId;

		// Token: 0x04001336 RID: 4918
		private readonly ImmutableArray<EncHoistedLocalInfo> _stateMachineHoistedLocalSlots;

		// Token: 0x04001337 RID: 4919
		private readonly ImmutableArray<LambdaDebugInfo> _lambdaDebugInfo;

		// Token: 0x04001338 RID: 4920
		private readonly ImmutableArray<ClosureDebugInfo> _closureDebugInfo;

		// Token: 0x04001339 RID: 4921
		[Nullable(new byte[] { 0, 2 })]
		private readonly ImmutableArray<ITypeReference> _stateMachineAwaiterSlots;

		// Token: 0x0400133A RID: 4922
		[Nullable(1)]
		private readonly DynamicAnalysisMethodBodyData _dynamicAnalysisDataOpt;
	}
}
