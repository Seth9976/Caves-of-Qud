using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D5 RID: 213
	internal struct PointerState : IInputStateTypeInfo
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0004522B File Offset: 0x0004342B
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('P', 'T', 'R', ' ');
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0004523A File Offset: 0x0004343A
		public FourCC format
		{
			get
			{
				return PointerState.kFormat;
			}
		}

		// Token: 0x04000578 RID: 1400
		private uint pointerId;

		// Token: 0x04000579 RID: 1401
		[InputControl(layout = "Vector2", displayName = "Position", usage = "Point", dontReset = true)]
		public Vector2 position;

		// Token: 0x0400057A RID: 1402
		[InputControl(layout = "Delta", displayName = "Delta", usage = "Secondary2DMotion")]
		public Vector2 delta;

		// Token: 0x0400057B RID: 1403
		[InputControl(layout = "Analog", displayName = "Pressure", usage = "Pressure", defaultState = 1f)]
		public float pressure;

		// Token: 0x0400057C RID: 1404
		[InputControl(layout = "Vector2", displayName = "Radius", usage = "Radius")]
		public Vector2 radius;

		// Token: 0x0400057D RID: 1405
		[InputControl(name = "press", displayName = "Press", layout = "Button", format = "BIT", bit = 0U)]
		public ushort buttons;

		// Token: 0x0400057E RID: 1406
		[InputControl(name = "displayIndex", layout = "Integer", displayName = "Display Index")]
		public ushort displayIndex;
	}
}
