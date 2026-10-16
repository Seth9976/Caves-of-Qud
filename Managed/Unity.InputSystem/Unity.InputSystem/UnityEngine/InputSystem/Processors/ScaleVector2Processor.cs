using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x0200010B RID: 267
	public class ScaleVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06000F48 RID: 3912 RVA: 0x0004AB73 File Offset: 0x00048D73
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			return new Vector2(value.x * this.x, value.y * this.y);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0004AB94 File Offset: 0x00048D94
		public override string ToString()
		{
			return string.Format("ScaleVector2(x={0},y={1})", this.x, this.y);
		}

		// Token: 0x04000661 RID: 1633
		[Tooltip("Scale factor to multiply the incoming Vector2's X component by.")]
		public float x = 1f;

		// Token: 0x04000662 RID: 1634
		[Tooltip("Scale factor to multiply the incoming Vector2's Y component by.")]
		public float y = 1f;
	}
}
