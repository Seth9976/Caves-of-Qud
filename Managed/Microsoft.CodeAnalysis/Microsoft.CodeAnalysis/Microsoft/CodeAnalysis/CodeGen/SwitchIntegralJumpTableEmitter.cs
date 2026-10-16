using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004E3 RID: 1251
	[NullableContext(1)]
	[Nullable(0)]
	internal struct SwitchIntegralJumpTableEmitter
	{
		// Token: 0x06003265 RID: 12901 RVA: 0x0008F1A4 File Offset: 0x0008D3A4
		internal SwitchIntegralJumpTableEmitter(ILBuilder builder, [Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<ConstantValue, object>[] caseLabels, object fallThroughLabel, Microsoft.Cci.PrimitiveTypeCode keyTypeCode, LocalOrParameter key)
		{
			this._builder = builder;
			this._key = key;
			this._keyTypeCode = keyTypeCode;
			this._fallThroughLabel = fallThroughLabel;
			Array.Sort<KeyValuePair<ConstantValue, object>>(caseLabels, new Comparison<KeyValuePair<ConstantValue, object>>(SwitchIntegralJumpTableEmitter.CompareIntegralSwitchLabels));
			this._sortedCaseLabels = ImmutableArray.Create<KeyValuePair<ConstantValue, object>>(caseLabels);
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x0008F1E4 File Offset: 0x0008D3E4
		internal void EmitJumpTable()
		{
			ImmutableArray<KeyValuePair<ConstantValue, object>> sortedCaseLabels = this._sortedCaseLabels;
			int num = sortedCaseLabels.Length - 1;
			int num2;
			if (sortedCaseLabels[0].Key != ConstantValue.Null)
			{
				num2 = 0;
			}
			else
			{
				num2 = 1;
			}
			if (num2 <= num)
			{
				ImmutableArray<SwitchIntegralJumpTableEmitter.SwitchBucket> immutableArray = this.GenerateSwitchBuckets(num2, num);
				this.EmitSwitchBuckets(immutableArray, 0, immutableArray.Length - 1);
				return;
			}
			this._builder.EmitBranch(ILOpCode.Br, this._fallThroughLabel, ILOpCode.Nop);
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x0008F25C File Offset: 0x0008D45C
		private static int CompareIntegralSwitchLabels([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<ConstantValue, object> first, [Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<ConstantValue, object> second)
		{
			ConstantValue key = first.Key;
			ConstantValue key2 = second.Key;
			return SwitchConstantValueHelper.CompareSwitchCaseLabelConstants(key, key2);
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x0008F280 File Offset: 0x0008D480
		[NullableContext(0)]
		private ImmutableArray<SwitchIntegralJumpTableEmitter.SwitchBucket> GenerateSwitchBuckets(int startLabelIndex, int endLabelIndex)
		{
			ArrayBuilder<SwitchIntegralJumpTableEmitter.SwitchBucket> instance = ArrayBuilder<SwitchIntegralJumpTableEmitter.SwitchBucket>.GetInstance();
			for (int i = startLabelIndex; i <= endLabelIndex; i++)
			{
				SwitchIntegralJumpTableEmitter.SwitchBucket switchBucket = this.CreateNextBucket(i, endLabelIndex);
				while (!instance.IsEmpty<SwitchIntegralJumpTableEmitter.SwitchBucket>())
				{
					SwitchIntegralJumpTableEmitter.SwitchBucket switchBucket2 = instance.Peek<SwitchIntegralJumpTableEmitter.SwitchBucket>();
					if (!switchBucket.TryMergeWith(switchBucket2))
					{
						break;
					}
					instance.Pop<SwitchIntegralJumpTableEmitter.SwitchBucket>();
				}
				instance.Push(switchBucket);
			}
			ArrayBuilder<SwitchIntegralJumpTableEmitter.SwitchBucket> instance2 = ArrayBuilder<SwitchIntegralJumpTableEmitter.SwitchBucket>.GetInstance();
			foreach (SwitchIntegralJumpTableEmitter.SwitchBucket switchBucket3 in instance)
			{
				int degenerateBucketSplit = switchBucket3.DegenerateBucketSplit;
				if (degenerateBucketSplit != -1)
				{
					if (degenerateBucketSplit != 0)
					{
						instance2.Add(new SwitchIntegralJumpTableEmitter.SwitchBucket(this._sortedCaseLabels, switchBucket3.StartLabelIndex, degenerateBucketSplit - 1, true));
						instance2.Add(new SwitchIntegralJumpTableEmitter.SwitchBucket(this._sortedCaseLabels, degenerateBucketSplit, switchBucket3.EndLabelIndex, true));
					}
					else
					{
						instance2.Add(new SwitchIntegralJumpTableEmitter.SwitchBucket(this._sortedCaseLabels, switchBucket3.StartLabelIndex, switchBucket3.EndLabelIndex, true));
					}
				}
				else
				{
					instance2.Add(switchBucket3);
				}
			}
			instance.Free();
			return instance2.ToImmutableAndFree();
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x0008F37F File Offset: 0x0008D57F
		private SwitchIntegralJumpTableEmitter.SwitchBucket CreateNextBucket(int startLabelIndex, int endLabelIndex)
		{
			return new SwitchIntegralJumpTableEmitter.SwitchBucket(this._sortedCaseLabels, startLabelIndex);
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x0008F390 File Offset: 0x0008D590
		[NullableContext(0)]
		private void EmitSwitchBucketsLinearLeaf(ImmutableArray<SwitchIntegralJumpTableEmitter.SwitchBucket> switchBuckets, int low, int high)
		{
			for (int i = low; i < high; i++)
			{
				object obj = new object();
				this.EmitSwitchBucket(switchBuckets[i], obj);
				this._builder.MarkLabel(obj);
			}
			this.EmitSwitchBucket(switchBuckets[high], this._fallThroughLabel);
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x0008F3E0 File Offset: 0x0008D5E0
		[NullableContext(0)]
		private void EmitSwitchBuckets(ImmutableArray<SwitchIntegralJumpTableEmitter.SwitchBucket> switchBuckets, int low, int high)
		{
			if (high - low < 3)
			{
				this.EmitSwitchBucketsLinearLeaf(switchBuckets, low, high);
				return;
			}
			int num = (low + high + 1) / 2;
			object obj = new object();
			ConstantValue endConstant = switchBuckets[num - 1].EndConstant;
			this.EmitCondBranchForSwitch(this._keyTypeCode.IsUnsigned() ? ILOpCode.Bgt_un : ILOpCode.Bgt, endConstant, obj);
			this.EmitSwitchBuckets(switchBuckets, low, num - 1);
			this._builder.MarkLabel(obj);
			this.EmitSwitchBuckets(switchBuckets, num, high);
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x0008F45C File Offset: 0x0008D65C
		private void EmitSwitchBucket(SwitchIntegralJumpTableEmitter.SwitchBucket switchBucket, object bucketFallThroughLabel)
		{
			if (switchBucket.LabelsCount == 1U)
			{
				KeyValuePair<ConstantValue, object> keyValuePair = switchBucket[0];
				ConstantValue key = keyValuePair.Key;
				object value = keyValuePair.Value;
				this.EmitEqBranchForSwitch(key, value);
			}
			else if (switchBucket.IsDegenerate)
			{
				this.EmitRangeCheckedBranch(switchBucket.StartConstant, switchBucket.EndConstant, switchBucket[0].Value);
			}
			else
			{
				this.EmitNormalizedSwitchKey(switchBucket.StartConstant, switchBucket.EndConstant, bucketFallThroughLabel);
				object[] array = this.CreateBucketLabels(switchBucket);
				this._builder.EmitSwitch(array);
			}
			this._builder.EmitBranch(ILOpCode.Br, bucketFallThroughLabel, ILOpCode.Nop);
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x0008F500 File Offset: 0x0008D700
		private object[] CreateBucketLabels(SwitchIntegralJumpTableEmitter.SwitchBucket switchBucket)
		{
			ConstantValue startConstant = switchBucket.StartConstant;
			bool isNegativeNumeric = startConstant.IsNegativeNumeric;
			int num = 0;
			ulong num2 = 0UL;
			ulong bucketSize = switchBucket.BucketSize;
			object[] array = new object[bucketSize];
			for (ulong num3 = 0UL; num3 < bucketSize; num3 += 1UL)
			{
				if (num3 == num2)
				{
					array[(int)(checked((IntPtr)num3))] = switchBucket[num].Value;
					num++;
					if ((long)num >= (long)((ulong)switchBucket.LabelsCount))
					{
						break;
					}
					ConstantValue key = switchBucket[num].Key;
					if (isNegativeNumeric)
					{
						num2 = (ulong)(key.Int64Value - startConstant.Int64Value);
					}
					else
					{
						num2 = key.UInt64Value - startConstant.UInt64Value;
					}
				}
				else
				{
					array[(int)(checked((IntPtr)num3))] = this._fallThroughLabel;
				}
			}
			return array;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x0008F5B9 File Offset: 0x0008D7B9
		private void EmitCondBranchForSwitch(ILOpCode branchCode, ConstantValue constant, object targetLabel)
		{
			this._builder.EmitLoad(this._key);
			this._builder.EmitConstantValue(constant);
			this._builder.EmitBranch(branchCode, targetLabel, SwitchIntegralJumpTableEmitter.GetReverseBranchCode(branchCode));
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x0008F5EC File Offset: 0x0008D7EC
		private void EmitEqBranchForSwitch(ConstantValue constant, object targetLabel)
		{
			this._builder.EmitLoad(this._key);
			if (constant.IsDefaultValue)
			{
				this._builder.EmitBranch(ILOpCode.Brfalse, targetLabel, ILOpCode.Nop);
				return;
			}
			this._builder.EmitConstantValue(constant);
			this._builder.EmitBranch(ILOpCode.Beq, targetLabel, ILOpCode.Nop);
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x0008F640 File Offset: 0x0008D840
		private void EmitRangeCheckedBranch(ConstantValue startConstant, ConstantValue endConstant, object targetLabel)
		{
			this._builder.EmitLoad(this._key);
			if (!startConstant.IsDefaultValue)
			{
				this._builder.EmitConstantValue(startConstant);
				this._builder.EmitOpCode(ILOpCode.Sub);
			}
			if (this._keyTypeCode.Is64BitIntegral())
			{
				this._builder.EmitLongConstant(endConstant.Int64Value - startConstant.Int64Value);
			}
			else
			{
				this._builder.EmitIntConstant(SwitchIntegralJumpTableEmitter.<EmitRangeCheckedBranch>g__Int32Value|17_0(endConstant) - SwitchIntegralJumpTableEmitter.<EmitRangeCheckedBranch>g__Int32Value|17_0(startConstant));
			}
			this._builder.EmitBranch(ILOpCode.Ble_un, targetLabel, ILOpCode.Bgt_un);
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x0008F6D0 File Offset: 0x0008D8D0
		private static ILOpCode GetReverseBranchCode(ILOpCode branchCode)
		{
			switch (branchCode)
			{
			case ILOpCode.Beq:
				return ILOpCode.Bne_un;
			case ILOpCode.Bge:
			case ILOpCode.Ble:
				break;
			case ILOpCode.Bgt:
				return ILOpCode.Ble;
			case ILOpCode.Blt:
				return ILOpCode.Bge;
			default:
				if (branchCode == ILOpCode.Bgt_un)
				{
					return ILOpCode.Ble_un;
				}
				if (branchCode == ILOpCode.Blt_un)
				{
					return ILOpCode.Bge_un;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(branchCode);
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x0008F720 File Offset: 0x0008D920
		private void EmitNormalizedSwitchKey(ConstantValue startConstant, ConstantValue endConstant, object bucketFallThroughLabel)
		{
			this._builder.EmitLoad(this._key);
			if (!startConstant.IsDefaultValue)
			{
				this._builder.EmitConstantValue(startConstant);
				this._builder.EmitOpCode(ILOpCode.Sub);
			}
			this.EmitRangeCheckIfNeeded(startConstant, endConstant, bucketFallThroughLabel);
			this._builder.EmitNumericConversion(this._keyTypeCode, Microsoft.Cci.PrimitiveTypeCode.UInt32, false);
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x0008F77C File Offset: 0x0008D97C
		private void EmitRangeCheckIfNeeded(ConstantValue startConstant, ConstantValue endConstant, object bucketFallThroughLabel)
		{
			if (this._keyTypeCode.Is64BitIntegral())
			{
				object obj = new object();
				this._builder.EmitOpCode(ILOpCode.Dup);
				this._builder.EmitLongConstant(endConstant.Int64Value - startConstant.Int64Value);
				this._builder.EmitBranch(ILOpCode.Ble_un, obj, ILOpCode.Bgt_un);
				this._builder.EmitOpCode(ILOpCode.Pop);
				this._builder.EmitBranch(ILOpCode.Br, bucketFallThroughLabel, ILOpCode.Nop);
				this._builder.AdjustStack(1);
				this._builder.MarkLabel(obj);
			}
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x0008F808 File Offset: 0x0008DA08
		[CompilerGenerated]
		internal static int <EmitRangeCheckedBranch>g__Int32Value|17_0(ConstantValue value)
		{
			ConstantValueTypeDiscriminator discriminator = value.Discriminator;
			if (discriminator == ConstantValueTypeDiscriminator.Byte)
			{
				return (int)value.ByteValue;
			}
			if (discriminator != ConstantValueTypeDiscriminator.UInt16)
			{
				return value.Int32Value;
			}
			return (int)value.UInt16Value;
		}

		// Token: 0x0400136E RID: 4974
		private readonly ILBuilder _builder;

		// Token: 0x0400136F RID: 4975
		private readonly LocalOrParameter _key;

		// Token: 0x04001370 RID: 4976
		private readonly Microsoft.Cci.PrimitiveTypeCode _keyTypeCode;

		// Token: 0x04001371 RID: 4977
		private readonly object _fallThroughLabel;

		// Token: 0x04001372 RID: 4978
		[Nullable(new byte[] { 0, 0, 1, 1 })]
		private readonly ImmutableArray<KeyValuePair<ConstantValue, object>> _sortedCaseLabels;

		// Token: 0x04001373 RID: 4979
		private const int LinearSearchThreshold = 3;

		// Token: 0x02000782 RID: 1922
		[Nullable(0)]
		private struct SwitchBucket
		{
			// Token: 0x1700115D RID: 4445
			// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x000B0DDC File Offset: 0x000AEFDC
			internal bool IsDegenerate
			{
				get
				{
					return this._isKnownDegenerate;
				}
			}

			// Token: 0x06003EE1 RID: 16097 RVA: 0x000B0DE4 File Offset: 0x000AEFE4
			internal SwitchBucket([Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<KeyValuePair<ConstantValue, object>> allLabels, int index)
			{
				this._startLabelIndex = index;
				this._endLabelIndex = index;
				this._allLabels = allLabels;
				this._isKnownDegenerate = true;
			}

			// Token: 0x06003EE2 RID: 16098 RVA: 0x000B0E02 File Offset: 0x000AF002
			private SwitchBucket([Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<KeyValuePair<ConstantValue, object>> allLabels, int startIndex, int endIndex)
			{
				this._startLabelIndex = startIndex;
				this._endLabelIndex = endIndex;
				this._allLabels = allLabels;
				this._isKnownDegenerate = false;
			}

			// Token: 0x06003EE3 RID: 16099 RVA: 0x000B0E20 File Offset: 0x000AF020
			internal SwitchBucket([Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<KeyValuePair<ConstantValue, object>> allLabels, int startIndex, int endIndex, bool isDegenerate)
			{
				this._startLabelIndex = startIndex;
				this._endLabelIndex = endIndex;
				this._allLabels = allLabels;
				this._isKnownDegenerate = isDegenerate;
			}

			// Token: 0x1700115E RID: 4446
			// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x000B0E3F File Offset: 0x000AF03F
			internal uint LabelsCount
			{
				get
				{
					return (uint)(this._endLabelIndex - this._startLabelIndex + 1);
				}
			}

			// Token: 0x1700115F RID: 4447
			[Nullable(new byte[] { 0, 1, 1 })]
			internal KeyValuePair<ConstantValue, object> this[int i]
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					return this._allLabels[i + this._startLabelIndex];
				}
			}

			// Token: 0x17001160 RID: 4448
			// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x000B0E73 File Offset: 0x000AF073
			internal ulong BucketSize
			{
				get
				{
					return SwitchIntegralJumpTableEmitter.SwitchBucket.GetBucketSize(this.StartConstant, this.EndConstant);
				}
			}

			// Token: 0x17001161 RID: 4449
			// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x000B0E88 File Offset: 0x000AF088
			internal int DegenerateBucketSplit
			{
				get
				{
					if (this.IsDegenerate)
					{
						return 0;
					}
					ImmutableArray<KeyValuePair<ConstantValue, object>> allLabels = this._allLabels;
					int num = 0;
					ConstantValue constantValue = this.StartConstant;
					object obj = allLabels[this._startLabelIndex].Value;
					for (int i = this._startLabelIndex + 1; i <= this._endLabelIndex; i++)
					{
						KeyValuePair<ConstantValue, object> keyValuePair = allLabels[i];
						if (obj != keyValuePair.Value || !this.IsContiguous(constantValue, keyValuePair.Key))
						{
							if (num != 0)
							{
								return -1;
							}
							num = i;
							obj = keyValuePair.Value;
						}
						constantValue = keyValuePair.Key;
					}
					return num;
				}
			}

			// Token: 0x06003EE8 RID: 16104 RVA: 0x000B0F20 File Offset: 0x000AF120
			private bool IsContiguous(ConstantValue lastConst, ConstantValue nextConst)
			{
				return lastConst.IsNumeric && nextConst.IsNumeric && SwitchIntegralJumpTableEmitter.SwitchBucket.GetBucketSize(lastConst, nextConst) == 2UL;
			}

			// Token: 0x06003EE9 RID: 16105 RVA: 0x000B0F40 File Offset: 0x000AF140
			private static ulong GetBucketSize(ConstantValue startConstant, ConstantValue endConstant)
			{
				ulong num;
				if (startConstant.IsNegativeNumeric || endConstant.IsNegativeNumeric)
				{
					num = (ulong)(endConstant.Int64Value - startConstant.Int64Value + 1L);
				}
				else
				{
					num = endConstant.UInt64Value - startConstant.UInt64Value + 1UL;
				}
				return num;
			}

			// Token: 0x06003EEA RID: 16106 RVA: 0x000B0F84 File Offset: 0x000AF184
			private static bool BucketOverflowUInt64Limit(ConstantValue startConstant, ConstantValue endConstant)
			{
				if (startConstant.Discriminator == ConstantValueTypeDiscriminator.Int64)
				{
					return startConstant.Int64Value == long.MinValue && endConstant.Int64Value == long.MaxValue;
				}
				return startConstant.Discriminator == ConstantValueTypeDiscriminator.UInt64 && startConstant.UInt64Value == 0UL && endConstant.UInt64Value == ulong.MaxValue;
			}

			// Token: 0x06003EEB RID: 16107 RVA: 0x000B0FDF File Offset: 0x000AF1DF
			private static bool BucketOverflow(ConstantValue startConstant, ConstantValue endConstant)
			{
				return SwitchIntegralJumpTableEmitter.SwitchBucket.BucketOverflowUInt64Limit(startConstant, endConstant) || SwitchIntegralJumpTableEmitter.SwitchBucket.GetBucketSize(startConstant, endConstant) > 2147483647UL;
			}

			// Token: 0x17001162 RID: 4450
			// (get) Token: 0x06003EEC RID: 16108 RVA: 0x000B0FFB File Offset: 0x000AF1FB
			internal int StartLabelIndex
			{
				get
				{
					return this._startLabelIndex;
				}
			}

			// Token: 0x17001163 RID: 4451
			// (get) Token: 0x06003EED RID: 16109 RVA: 0x000B1003 File Offset: 0x000AF203
			internal int EndLabelIndex
			{
				get
				{
					return this._endLabelIndex;
				}
			}

			// Token: 0x17001164 RID: 4452
			// (get) Token: 0x06003EEE RID: 16110 RVA: 0x000B100C File Offset: 0x000AF20C
			internal ConstantValue StartConstant
			{
				get
				{
					return this._allLabels[this._startLabelIndex].Key;
				}
			}

			// Token: 0x17001165 RID: 4453
			// (get) Token: 0x06003EEF RID: 16111 RVA: 0x000B1038 File Offset: 0x000AF238
			internal ConstantValue EndConstant
			{
				get
				{
					return this._allLabels[this._endLabelIndex].Key;
				}
			}

			// Token: 0x06003EF0 RID: 16112 RVA: 0x000B1061 File Offset: 0x000AF261
			private static bool IsValidSwitchBucketConstant(ConstantValue constant)
			{
				return constant != null && SwitchConstantValueHelper.IsValidSwitchCaseLabelConstant(constant) && !constant.IsNull && !constant.IsString;
			}

			// Token: 0x06003EF1 RID: 16113 RVA: 0x000B1087 File Offset: 0x000AF287
			private static bool IsValidSwitchBucketConstantPair(ConstantValue startConstant, ConstantValue endConstant)
			{
				return SwitchIntegralJumpTableEmitter.SwitchBucket.IsValidSwitchBucketConstant(startConstant) && SwitchIntegralJumpTableEmitter.SwitchBucket.IsValidSwitchBucketConstant(endConstant) && startConstant.IsUnsigned == endConstant.IsUnsigned;
			}

			// Token: 0x06003EF2 RID: 16114 RVA: 0x000B10A9 File Offset: 0x000AF2A9
			private static bool IsSparse(uint labelsCount, ulong bucketSize)
			{
				return bucketSize >= (ulong)(labelsCount * 2U);
			}

			// Token: 0x06003EF3 RID: 16115 RVA: 0x000B10B8 File Offset: 0x000AF2B8
			internal static bool MergeIsAdvantageous(SwitchIntegralJumpTableEmitter.SwitchBucket bucket1, SwitchIntegralJumpTableEmitter.SwitchBucket bucket2)
			{
				ConstantValue startConstant = bucket1.StartConstant;
				ConstantValue endConstant = bucket2.EndConstant;
				if (SwitchIntegralJumpTableEmitter.SwitchBucket.BucketOverflow(startConstant, endConstant))
				{
					return false;
				}
				uint num = bucket1.LabelsCount + bucket2.LabelsCount;
				ulong bucketSize = SwitchIntegralJumpTableEmitter.SwitchBucket.GetBucketSize(startConstant, endConstant);
				return !SwitchIntegralJumpTableEmitter.SwitchBucket.IsSparse(num, bucketSize);
			}

			// Token: 0x06003EF4 RID: 16116 RVA: 0x000B1100 File Offset: 0x000AF300
			internal bool TryMergeWith(SwitchIntegralJumpTableEmitter.SwitchBucket prevBucket)
			{
				if (SwitchIntegralJumpTableEmitter.SwitchBucket.MergeIsAdvantageous(prevBucket, this))
				{
					this = new SwitchIntegralJumpTableEmitter.SwitchBucket(this._allLabels, prevBucket._startLabelIndex, this._endLabelIndex);
					return true;
				}
				return false;
			}

			// Token: 0x04001D83 RID: 7555
			[Nullable(new byte[] { 0, 0, 1, 1 })]
			private readonly ImmutableArray<KeyValuePair<ConstantValue, object>> _allLabels;

			// Token: 0x04001D84 RID: 7556
			private readonly int _startLabelIndex;

			// Token: 0x04001D85 RID: 7557
			private readonly int _endLabelIndex;

			// Token: 0x04001D86 RID: 7558
			private readonly bool _isKnownDegenerate;
		}
	}
}
