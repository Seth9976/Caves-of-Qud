using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000104 RID: 260
	public class InvertProcessor : InputProcessor<float>
	{
		// Token: 0x06000F31 RID: 3889 RVA: 0x0004A920 File Offset: 0x00048B20
		public override float Process(float value, InputControl control)
		{
			return value * -1f;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0004A929 File Offset: 0x00048B29
		public override string ToString()
		{
			return "Invert()";
		}
	}
}
