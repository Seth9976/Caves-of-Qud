using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011C RID: 284
	public class IntegerControl : InputControl<int>
	{
		// Token: 0x0600104E RID: 4174 RVA: 0x0004F58B File Offset: 0x0004D78B
		public IntegerControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatInt;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0004F5A3 File Offset: 0x0004D7A3
		public unsafe override int ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1229870112)
			{
				return *(int*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return this.m_StateBlock.ReadInt(statePtr);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0004F5D2 File Offset: 0x0004D7D2
		public unsafe override void WriteValueIntoState(int value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1229870112)
			{
				*(int*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.m_StateBlock.WriteInt(statePtr, value);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0004F603 File Offset: 0x0004D803
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.format == InputStateBlock.FormatInt && this.m_StateBlock.sizeInBits == 32U && this.m_StateBlock.bitOffset == 0U)
			{
				return InputStateBlock.FormatInt;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
