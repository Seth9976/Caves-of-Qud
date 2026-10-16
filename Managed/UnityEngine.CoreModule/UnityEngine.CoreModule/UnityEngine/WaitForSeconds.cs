using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002C3 RID: 707
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class WaitForSeconds : YieldInstruction
	{
		// Token: 0x0600226F RID: 8815 RVA: 0x0004365D File Offset: 0x0004185D
		public WaitForSeconds(float seconds)
		{
			this.m_Seconds = seconds;
		}

		// Token: 0x04000A08 RID: 2568
		internal float m_Seconds;
	}
}
