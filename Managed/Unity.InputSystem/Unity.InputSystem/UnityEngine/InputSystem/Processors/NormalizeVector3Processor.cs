using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000109 RID: 265
	public class NormalizeVector3Processor : InputProcessor<Vector3>
	{
		// Token: 0x06000F42 RID: 3906 RVA: 0x0004AB27 File Offset: 0x00048D27
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			return value.normalized;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0004AB30 File Offset: 0x00048D30
		public override string ToString()
		{
			return "NormalizeVector3()";
		}
	}
}
