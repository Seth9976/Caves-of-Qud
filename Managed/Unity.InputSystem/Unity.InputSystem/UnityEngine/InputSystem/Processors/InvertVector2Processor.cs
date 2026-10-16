using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000105 RID: 261
	public class InvertVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06000F34 RID: 3892 RVA: 0x0004A938 File Offset: 0x00048B38
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			if (this.invertX)
			{
				value.x *= -1f;
			}
			if (this.invertY)
			{
				value.y *= -1f;
			}
			return value;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0004A96B File Offset: 0x00048B6B
		public override string ToString()
		{
			return string.Format("InvertVector2(invertX={0},invertY={1})", this.invertX, this.invertY);
		}

		// Token: 0x04000658 RID: 1624
		public bool invertX = true;

		// Token: 0x04000659 RID: 1625
		public bool invertY = true;
	}
}
