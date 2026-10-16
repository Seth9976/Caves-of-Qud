using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000108 RID: 264
	public class NormalizeVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x0004AB0F File Offset: 0x00048D0F
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			return value.normalized;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0004AB18 File Offset: 0x00048D18
		public override string ToString()
		{
			return "NormalizeVector2()";
		}
	}
}
