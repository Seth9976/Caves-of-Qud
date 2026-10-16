using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000122 RID: 290
	[InputControlLayout(hideInUI = true)]
	public class TouchPressControl : ButtonControl
	{
		// Token: 0x06001090 RID: 4240 RVA: 0x0004FD40 File Offset: 0x0004DF40
		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!base.stateBlock.format.IsIntegerFormat())
			{
				throw new NotSupportedException(string.Format("Non-integer format '{0}' is not supported for TouchButtonControl '{1}'", base.stateBlock.format, this));
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0004FD8C File Offset: 0x0004DF8C
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			TouchPhase touchPhase = (TouchPhase)MemoryHelpers.ReadMultipleBitsAsUInt((void*)((byte*)statePtr + this.m_StateBlock.byteOffset), this.m_StateBlock.bitOffset, this.m_StateBlock.sizeInBits);
			float num = 0f;
			if (touchPhase == TouchPhase.Began || touchPhase == TouchPhase.Stationary || touchPhase == TouchPhase.Moved)
			{
				num = 1f;
			}
			return base.Preprocess(num);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0004FDE1 File Offset: 0x0004DFE1
		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			throw new NotSupportedException();
		}
	}
}
