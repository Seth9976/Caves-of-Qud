using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000119 RID: 281
	public class DiscreteButtonControl : ButtonControl
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x0004F104 File Offset: 0x0004D304
		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!base.stateBlock.format.IsIntegerFormat())
			{
				throw new NotSupportedException(string.Format("Non-integer format '{0}' is not supported for DiscreteButtonControl '{1}'", base.stateBlock.format, this));
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0004F150 File Offset: 0x0004D350
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			int num = MemoryHelpers.ReadTwosComplementMultipleBitsAsInt((void*)((byte*)statePtr + this.m_StateBlock.byteOffset), this.m_StateBlock.bitOffset, this.m_StateBlock.sizeInBits);
			float num2 = 0f;
			if (this.minValue > this.maxValue)
			{
				if (this.wrapAtValue == this.nullValue)
				{
					this.wrapAtValue = this.minValue;
				}
				if ((num >= this.minValue && num <= this.wrapAtValue) || (num != this.nullValue && num <= this.maxValue))
				{
					num2 = 1f;
				}
			}
			else
			{
				num2 = ((num >= this.minValue && num <= this.maxValue) ? 1f : 0f);
			}
			return base.Preprocess(num2);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0004F208 File Offset: 0x0004D408
		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			if (this.writeMode == DiscreteButtonControl.WriteMode.WriteNullAndMaxValue)
			{
				void* ptr = (void*)((byte*)statePtr + this.m_StateBlock.byteOffset);
				int num = ((value >= base.pressPointOrDefault) ? this.maxValue : this.nullValue);
				MemoryHelpers.WriteIntAsTwosComplementMultipleBits(ptr, this.m_StateBlock.bitOffset, this.m_StateBlock.sizeInBits, num);
				return;
			}
			throw new NotSupportedException("Writing value states for DiscreteButtonControl is not supported as a single value may correspond to multiple states");
		}

		// Token: 0x040006CA RID: 1738
		public int minValue;

		// Token: 0x040006CB RID: 1739
		public int maxValue;

		// Token: 0x040006CC RID: 1740
		public int wrapAtValue;

		// Token: 0x040006CD RID: 1741
		public int nullValue;

		// Token: 0x040006CE RID: 1742
		public DiscreteButtonControl.WriteMode writeMode;

		// Token: 0x0200023C RID: 572
		public enum WriteMode
		{
			// Token: 0x04000C61 RID: 3169
			WriteDisabled,
			// Token: 0x04000C62 RID: 3170
			WriteNullAndMaxValue
		}
	}
}
