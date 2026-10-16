using System;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	public sealed class ExitGUIException : Exception
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0000E476 File Offset: 0x0000C676
		public ExitGUIException()
		{
			GUIUtility.guiIsExiting = true;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000E487 File Offset: 0x0000C687
		internal ExitGUIException(string message)
			: base(message)
		{
			GUIUtility.guiIsExiting = true;
		}
	}
}
