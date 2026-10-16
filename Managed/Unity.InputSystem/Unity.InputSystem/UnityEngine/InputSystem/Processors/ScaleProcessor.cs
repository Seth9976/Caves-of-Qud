using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x0200010A RID: 266
	public class ScaleProcessor : InputProcessor<float>
	{
		// Token: 0x06000F45 RID: 3909 RVA: 0x0004AB3F File Offset: 0x00048D3F
		public override float Process(float value, InputControl control)
		{
			return value * this.factor;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0004AB49 File Offset: 0x00048D49
		public override string ToString()
		{
			return string.Format("Scale(factor={0})", this.factor);
		}

		// Token: 0x04000660 RID: 1632
		[Tooltip("Scale factor to multiply incoming float values by.")]
		public float factor = 1f;
	}
}
