using System;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Processors;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000116 RID: 278
	public class AxisControl : InputControl<float>
	{
		// Token: 0x0600101B RID: 4123 RVA: 0x0004EA74 File Offset: 0x0004CC74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected float Preprocess(float value)
		{
			if (this.scale)
			{
				value *= this.scaleFactor;
			}
			if (this.clamp == AxisControl.Clamp.ToConstantBeforeNormalize)
			{
				if (value < this.clampMin || value > this.clampMax)
				{
					value = this.clampConstant;
				}
			}
			else if (this.clamp == AxisControl.Clamp.BeforeNormalize)
			{
				value = Mathf.Clamp(value, this.clampMin, this.clampMax);
			}
			if (this.normalize)
			{
				value = NormalizeProcessor.Normalize(value, this.normalizeMin, this.normalizeMax, this.normalizeZero);
			}
			if (this.clamp == AxisControl.Clamp.AfterNormalize)
			{
				value = Mathf.Clamp(value, this.clampMin, this.clampMax);
			}
			if (this.invert)
			{
				value *= -1f;
			}
			return value;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0004EB28 File Offset: 0x0004CD28
		private float Unpreprocess(float value)
		{
			if (this.invert)
			{
				value *= -1f;
			}
			if (this.normalize)
			{
				value = NormalizeProcessor.Denormalize(value, this.normalizeMin, this.normalizeMax, this.normalizeZero);
			}
			if (this.scale)
			{
				value /= this.scaleFactor;
			}
			return value;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0004EB7B File Offset: 0x0004CD7B
		public AxisControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatFloat;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0004EB94 File Offset: 0x0004CD94
		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!base.hasDefaultState && this.normalize && Mathf.Abs(this.normalizeZero) > Mathf.Epsilon)
			{
				this.m_DefaultState = base.stateBlock.FloatToPrimitiveValue(this.normalizeZero);
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0004EBE4 File Offset: 0x0004CDE4
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			int num = this.m_OptimizedControlDataType;
			if (num != 1113150533)
			{
				if (num == 1179407392)
				{
					return *(float*)((byte*)statePtr + this.m_StateBlock.m_ByteOffset);
				}
				float num2 = base.stateBlock.ReadFloat(statePtr);
				return this.Preprocess(num2);
			}
			else
			{
				if (((byte*)statePtr)[this.m_StateBlock.m_ByteOffset] == 0)
				{
					return 0f;
				}
				return 1f;
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0004EC54 File Offset: 0x0004CE54
		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			int num = this.m_OptimizedControlDataType;
			if (num == 1113150533)
			{
				((byte*)statePtr)[this.m_StateBlock.m_ByteOffset] = ((value >= 0.5f) ? 1 : 0);
				return;
			}
			if (num == 1179407392)
			{
				*(float*)((byte*)statePtr + this.m_StateBlock.m_ByteOffset) = value;
				return;
			}
			value = this.Unpreprocess(value);
			base.stateBlock.WriteFloat(statePtr, value);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0004ECC4 File Offset: 0x0004CEC4
		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			float num = base.ReadValueFromState(firstStatePtr);
			float num2 = base.ReadValueFromState(secondStatePtr);
			return !Mathf.Approximately(num, num2);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0004ECE9 File Offset: 0x0004CEE9
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return this.EvaluateMagnitude(base.ReadValueFromStateWithCaching(statePtr));
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0004ECF8 File Offset: 0x0004CEF8
		private float EvaluateMagnitude(float value)
		{
			if (this.m_MinValue.isEmpty || this.m_MaxValue.isEmpty)
			{
				return Mathf.Abs(value);
			}
			float num = this.m_MinValue.ToSingle(null);
			float num2 = this.m_MaxValue.ToSingle(null);
			float num3 = Mathf.Clamp(value, num, num2);
			if (num >= 0f)
			{
				return NormalizeProcessor.Normalize(num3, num, num2, 0f);
			}
			if (num3 < 0f)
			{
				return NormalizeProcessor.Normalize(Mathf.Abs(num3), 0f, Mathf.Abs(num), 0f);
			}
			return NormalizeProcessor.Normalize(num3, 0f, num2, 0f);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0004ED94 File Offset: 0x0004CF94
		protected override FourCC CalculateOptimizedControlDataType()
		{
			bool flag = this.clamp == AxisControl.Clamp.None && !this.invert && !this.normalize && !this.scale;
			if (flag && this.m_StateBlock.format == InputStateBlock.FormatFloat && this.m_StateBlock.sizeInBits == 32U && this.m_StateBlock.bitOffset == 0U)
			{
				return InputStateBlock.FormatFloat;
			}
			if (flag && this.m_StateBlock.format == InputStateBlock.FormatBit && this.m_StateBlock.sizeInBits == 8U && this.m_StateBlock.bitOffset == 0U)
			{
				return InputStateBlock.FormatByte;
			}
			return InputStateBlock.FormatInvalid;
		}

		// Token: 0x040006B2 RID: 1714
		public AxisControl.Clamp clamp;

		// Token: 0x040006B3 RID: 1715
		public float clampMin;

		// Token: 0x040006B4 RID: 1716
		public float clampMax;

		// Token: 0x040006B5 RID: 1717
		public float clampConstant;

		// Token: 0x040006B6 RID: 1718
		public bool invert;

		// Token: 0x040006B7 RID: 1719
		public bool normalize;

		// Token: 0x040006B8 RID: 1720
		public float normalizeMin;

		// Token: 0x040006B9 RID: 1721
		public float normalizeMax;

		// Token: 0x040006BA RID: 1722
		public float normalizeZero;

		// Token: 0x040006BB RID: 1723
		public bool scale;

		// Token: 0x040006BC RID: 1724
		public float scaleFactor;

		// Token: 0x0200023B RID: 571
		public enum Clamp
		{
			// Token: 0x04000C5C RID: 3164
			None,
			// Token: 0x04000C5D RID: 3165
			BeforeNormalize,
			// Token: 0x04000C5E RID: 3166
			AfterNormalize,
			// Token: 0x04000C5F RID: 3167
			ToConstantBeforeNormalize
		}
	}
}
