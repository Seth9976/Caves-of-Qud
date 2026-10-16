using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Debugging;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x0200009C RID: 156
	internal interface IMethodBody
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600041F RID: 1055
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ExceptionHandlerRegion> ExceptionRegions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000420 RID: 1056
		bool AreLocalsZeroed { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000421 RID: 1057
		bool HasStackalloc { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000422 RID: 1058
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalDefinition> LocalVariables
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000423 RID: 1059
		[Nullable(1)]
		IMethodDefinition MethodDefinition
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000424 RID: 1060
		[Nullable(1)]
		StateMachineMoveNextBodyDebugInfo MoveNextBodyInfo
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000425 RID: 1061
		ushort MaxStack { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000426 RID: 1062
		ImmutableArray<byte> IL { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000427 RID: 1063
		ImmutableArray<SequencePoint> SequencePoints { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000428 RID: 1064
		bool HasDynamicLocalVariables { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000429 RID: 1065
		ImmutableArray<LocalScope> LocalScopes { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600042A RID: 1066
		[Nullable(1)]
		IImportScope ImportScope
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600042B RID: 1067
		DebugId MethodId { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600042C RID: 1068
		ImmutableArray<StateMachineHoistedLocalScope> StateMachineHoistedLocalScopes { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600042D RID: 1069
		[Nullable(1)]
		string StateMachineTypeName
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600042E RID: 1070
		ImmutableArray<EncHoistedLocalInfo> StateMachineHoistedLocalSlots { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600042F RID: 1071
		[Nullable(new byte[] { 0, 2 })]
		ImmutableArray<ITypeReference> StateMachineAwaiterSlots
		{
			[return: Nullable(new byte[] { 0, 2 })]
			get;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000430 RID: 1072
		ImmutableArray<ClosureDebugInfo> ClosureDebugInfo { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000431 RID: 1073
		ImmutableArray<LambdaDebugInfo> LambdaDebugInfo { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000432 RID: 1074
		[Nullable(1)]
		DynamicAnalysisMethodBodyData DynamicAnalysisData
		{
			[NullableContext(1)]
			get;
		}
	}
}
