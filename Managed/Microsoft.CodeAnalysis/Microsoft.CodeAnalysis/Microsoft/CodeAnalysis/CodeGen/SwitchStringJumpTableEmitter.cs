using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004E4 RID: 1252
	[NullableContext(1)]
	[Nullable(0)]
	internal struct SwitchStringJumpTableEmitter
	{
		// Token: 0x06003275 RID: 12917 RVA: 0x0008F83A File Offset: 0x0008DA3A
		internal SwitchStringJumpTableEmitter(ILBuilder builder, LocalOrParameter key, [Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<ConstantValue, object>[] caseLabels, object fallThroughLabel, [Nullable(2)] LocalDefinition keyHash, SwitchStringJumpTableEmitter.EmitStringCompareAndBranch emitStringCondBranchDelegate, SwitchStringJumpTableEmitter.GetStringHashCode computeStringHashcodeDelegate)
		{
			this._builder = builder;
			this._key = key;
			this._caseLabels = caseLabels;
			this._fallThroughLabel = fallThroughLabel;
			this._keyHash = keyHash;
			this._emitStringCondBranchDelegate = emitStringCondBranchDelegate;
			this._computeStringHashcodeDelegate = computeStringHashcodeDelegate;
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x0008F871 File Offset: 0x0008DA71
		internal void EmitJumpTable()
		{
			if (this._keyHash != null)
			{
				this.EmitHashTableSwitch();
				return;
			}
			this.EmitNonHashTableSwitch(this._caseLabels);
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x0008F890 File Offset: 0x0008DA90
		private void EmitHashTableSwitch()
		{
			Dictionary<uint, List<KeyValuePair<ConstantValue, object>>> dictionary = SwitchStringJumpTableEmitter.ComputeStringHashMap(this._caseLabels, this._computeStringHashcodeDelegate);
			Dictionary<uint, object> dictionary2 = this.EmitHashBucketJumpTable(dictionary);
			foreach (KeyValuePair<uint, List<KeyValuePair<ConstantValue, object>>> keyValuePair in dictionary)
			{
				this._builder.MarkLabel(dictionary2[keyValuePair.Key]);
				List<KeyValuePair<ConstantValue, object>> value = keyValuePair.Value;
				this.EmitNonHashTableSwitch(value.ToArray());
			}
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x0008F920 File Offset: 0x0008DB20
		private Dictionary<uint, object> EmitHashBucketJumpTable([Nullable(new byte[] { 1, 1, 0, 1, 1 })] Dictionary<uint, List<KeyValuePair<ConstantValue, object>>> stringHashMap)
		{
			int count = stringHashMap.Count;
			Dictionary<uint, object> dictionary = new Dictionary<uint, object>(count);
			KeyValuePair<ConstantValue, object>[] array = new KeyValuePair<ConstantValue, object>[count];
			int num = 0;
			foreach (uint num2 in stringHashMap.Keys)
			{
				ConstantValue constantValue = ConstantValue.Create(num2);
				object obj = new object();
				array[num] = new KeyValuePair<ConstantValue, object>(constantValue, obj);
				dictionary[num2] = obj;
				num++;
			}
			SwitchIntegralJumpTableEmitter switchIntegralJumpTableEmitter = new SwitchIntegralJumpTableEmitter(this._builder, array, this._fallThroughLabel, Microsoft.Cci.PrimitiveTypeCode.UInt32, this._keyHash);
			switchIntegralJumpTableEmitter.EmitJumpTable();
			return dictionary;
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x0008F9DC File Offset: 0x0008DBDC
		private void EmitNonHashTableSwitch([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<ConstantValue, object>[] labels)
		{
			foreach (KeyValuePair<ConstantValue, object> keyValuePair in labels)
			{
				this.EmitCondBranchForStringSwitch(keyValuePair.Key, keyValuePair.Value);
			}
			this._builder.EmitBranch(ILOpCode.Br, this._fallThroughLabel, ILOpCode.Nop);
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x0008FA29 File Offset: 0x0008DC29
		private void EmitCondBranchForStringSwitch(ConstantValue stringConstant, object targetLabel)
		{
			this._emitStringCondBranchDelegate(this._key, stringConstant, targetLabel);
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x0008FA40 File Offset: 0x0008DC40
		[return: Nullable(new byte[] { 1, 1, 0, 1, 1 })]
		private static Dictionary<uint, List<KeyValuePair<ConstantValue, object>>> ComputeStringHashMap([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<ConstantValue, object>[] caseLabels, SwitchStringJumpTableEmitter.GetStringHashCode computeStringHashcodeDelegate)
		{
			Dictionary<uint, List<KeyValuePair<ConstantValue, object>>> dictionary = new Dictionary<uint, List<KeyValuePair<ConstantValue, object>>>(caseLabels.Length);
			foreach (KeyValuePair<ConstantValue, object> keyValuePair in caseLabels)
			{
				ConstantValue key = keyValuePair.Key;
				uint num = computeStringHashcodeDelegate((string)key.Value);
				List<KeyValuePair<ConstantValue, object>> list;
				if (!dictionary.TryGetValue(num, ref list))
				{
					list = new List<KeyValuePair<ConstantValue, object>>();
					dictionary.Add(num, list);
				}
				list.Add(keyValuePair);
			}
			return dictionary;
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x0008FAB1 File Offset: 0x0008DCB1
		internal static bool ShouldGenerateHashTableSwitch(CommonPEModuleBuilder module, int labelsCount)
		{
			return module.SupportsPrivateImplClass && SwitchStringJumpTableEmitter.ShouldGenerateHashTableSwitch(labelsCount);
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x0008FAC3 File Offset: 0x0008DCC3
		private static bool ShouldGenerateHashTableSwitch(int labelsCount)
		{
			return labelsCount >= 7;
		}

		// Token: 0x04001374 RID: 4980
		private readonly ILBuilder _builder;

		// Token: 0x04001375 RID: 4981
		private readonly LocalOrParameter _key;

		// Token: 0x04001376 RID: 4982
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		private readonly KeyValuePair<ConstantValue, object>[] _caseLabels;

		// Token: 0x04001377 RID: 4983
		private readonly object _fallThroughLabel;

		// Token: 0x04001378 RID: 4984
		private readonly SwitchStringJumpTableEmitter.EmitStringCompareAndBranch _emitStringCondBranchDelegate;

		// Token: 0x04001379 RID: 4985
		private readonly SwitchStringJumpTableEmitter.GetStringHashCode _computeStringHashcodeDelegate;

		// Token: 0x0400137A RID: 4986
		[Nullable(2)]
		private readonly LocalDefinition _keyHash;

		// Token: 0x02000783 RID: 1923
		// (Invoke) Token: 0x06003EF6 RID: 16118
		[NullableContext(0)]
		public delegate void EmitStringCompareAndBranch(LocalOrParameter key, ConstantValue stringConstant, object targetLabel);

		// Token: 0x02000784 RID: 1924
		// (Invoke) Token: 0x06003EFA RID: 16122
		[NullableContext(0)]
		public delegate uint GetStringHashCode(string key);
	}
}
