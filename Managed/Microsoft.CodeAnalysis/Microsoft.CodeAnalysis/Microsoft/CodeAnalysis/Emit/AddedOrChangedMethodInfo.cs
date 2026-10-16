using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000480 RID: 1152
	internal readonly struct AddedOrChangedMethodInfo
	{
		// Token: 0x06002D20 RID: 11552 RVA: 0x0007EDFC File Offset: 0x0007CFFC
		public AddedOrChangedMethodInfo(DebugId methodId, ImmutableArray<EncLocalInfo> locals, ImmutableArray<LambdaDebugInfo> lambdaDebugInfo, ImmutableArray<ClosureDebugInfo> closureDebugInfo, [Nullable(2)] string stateMachineTypeName, ImmutableArray<EncHoistedLocalInfo> stateMachineHoistedLocalSlotsOpt, [Nullable(new byte[] { 0, 2 })] ImmutableArray<ITypeReference> stateMachineAwaiterSlotsOpt)
		{
			this.MethodId = methodId;
			this.Locals = locals;
			this.LambdaDebugInfo = lambdaDebugInfo;
			this.ClosureDebugInfo = closureDebugInfo;
			this.StateMachineTypeName = stateMachineTypeName;
			this.StateMachineHoistedLocalSlotsOpt = stateMachineHoistedLocalSlotsOpt;
			this.StateMachineAwaiterSlotsOpt = stateMachineAwaiterSlotsOpt;
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x0007EE34 File Offset: 0x0007D034
		[NullableContext(1)]
		public AddedOrChangedMethodInfo MapTypes(SymbolMatcher map)
		{
			ImmutableArray<EncLocalInfo> immutableArray = ImmutableArray.CreateRange<EncLocalInfo, SymbolMatcher, EncLocalInfo>(this.Locals, new Func<EncLocalInfo, SymbolMatcher, EncLocalInfo>(AddedOrChangedMethodInfo.MapLocalInfo), map);
			ImmutableArray<EncHoistedLocalInfo> immutableArray2 = (this.StateMachineHoistedLocalSlotsOpt.IsDefault ? default(ImmutableArray<EncHoistedLocalInfo>) : ImmutableArray.CreateRange<EncHoistedLocalInfo, SymbolMatcher, EncHoistedLocalInfo>(this.StateMachineHoistedLocalSlotsOpt, new Func<EncHoistedLocalInfo, SymbolMatcher, EncHoistedLocalInfo>(AddedOrChangedMethodInfo.MapHoistedLocalSlot), map));
			ImmutableArray<ITypeReference> immutableArray3;
			if (!this.StateMachineAwaiterSlotsOpt.IsDefault)
			{
				immutableArray3 = ImmutableArray.CreateRange<ITypeReference, SymbolMatcher, ITypeReference>(this.StateMachineAwaiterSlotsOpt, delegate(ITypeReference typeRef, SymbolMatcher map)
				{
					if (typeRef != null)
					{
						return map.MapReference(typeRef);
					}
					return null;
				}, map);
			}
			else
			{
				immutableArray3 = default(ImmutableArray<ITypeReference>);
			}
			ImmutableArray<ITypeReference> immutableArray4 = immutableArray3;
			return new AddedOrChangedMethodInfo(this.MethodId, immutableArray, this.LambdaDebugInfo, this.ClosureDebugInfo, this.StateMachineTypeName, immutableArray2, immutableArray4);
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x0007EEF8 File Offset: 0x0007D0F8
		[NullableContext(1)]
		private static EncLocalInfo MapLocalInfo(EncLocalInfo info, SymbolMatcher map)
		{
			if (info.Type == null)
			{
				return info;
			}
			ITypeReference typeReference = map.MapReference(info.Type);
			return new EncLocalInfo(info.SlotInfo, typeReference, info.Constraints, info.Signature);
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x0007EF34 File Offset: 0x0007D134
		[NullableContext(1)]
		private static EncHoistedLocalInfo MapHoistedLocalSlot(EncHoistedLocalInfo info, SymbolMatcher map)
		{
			if (info.Type == null)
			{
				return info;
			}
			ITypeReference typeReference = map.MapReference(info.Type);
			return new EncHoistedLocalInfo(info.SlotInfo, typeReference);
		}

		// Token: 0x040011E8 RID: 4584
		public readonly DebugId MethodId;

		// Token: 0x040011E9 RID: 4585
		public readonly ImmutableArray<EncLocalInfo> Locals;

		// Token: 0x040011EA RID: 4586
		public readonly ImmutableArray<LambdaDebugInfo> LambdaDebugInfo;

		// Token: 0x040011EB RID: 4587
		public readonly ImmutableArray<ClosureDebugInfo> ClosureDebugInfo;

		// Token: 0x040011EC RID: 4588
		[Nullable(2)]
		public readonly string StateMachineTypeName;

		// Token: 0x040011ED RID: 4589
		public readonly ImmutableArray<EncHoistedLocalInfo> StateMachineHoistedLocalSlotsOpt;

		// Token: 0x040011EE RID: 4590
		[Nullable(new byte[] { 0, 2 })]
		public readonly ImmutableArray<ITypeReference> StateMachineAwaiterSlotsOpt;
	}
}
