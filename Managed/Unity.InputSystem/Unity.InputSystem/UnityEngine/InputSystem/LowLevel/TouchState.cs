using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DC RID: 220
	[StructLayout(LayoutKind.Explicit, Size = 56)]
	public struct TouchState : IInputStateTypeInfo
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x000452AF File Offset: 0x000434AF
		public static FourCC Format
		{
			get
			{
				return new FourCC('T', 'O', 'U', 'C');
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x000452BE File Offset: 0x000434BE
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x000452C6 File Offset: 0x000434C6
		public TouchPhase phase
		{
			get
			{
				return (TouchPhase)this.phaseId;
			}
			set
			{
				this.phaseId = (byte)value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x000452D0 File Offset: 0x000434D0
		public bool isNoneEndedOrCanceled
		{
			get
			{
				return this.phase == TouchPhase.None || this.phase == TouchPhase.Ended || this.phase == TouchPhase.Canceled;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x000452EE File Offset: 0x000434EE
		public bool isInProgress
		{
			get
			{
				return this.phase == TouchPhase.Began || this.phase == TouchPhase.Moved || this.phase == TouchPhase.Stationary;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0004530D File Offset: 0x0004350D
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x0004531A File Offset: 0x0004351A
		public bool isPrimaryTouch
		{
			get
			{
				return (this.flags & 8) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 8;
					return;
				}
				this.flags &= 247;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00045342 File Offset: 0x00043542
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00045350 File Offset: 0x00043550
		internal bool isOrphanedPrimaryTouch
		{
			get
			{
				return (this.flags & 64) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 64;
					return;
				}
				this.flags &= 191;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00045379 File Offset: 0x00043579
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x00045386 File Offset: 0x00043586
		public bool isIndirectTouch
		{
			get
			{
				return (this.flags & 1) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 1;
					return;
				}
				this.flags &= 254;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x000453AE File Offset: 0x000435AE
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x000453B6 File Offset: 0x000435B6
		public bool isTap
		{
			get
			{
				return this.isTapPress;
			}
			set
			{
				this.isTapPress = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x000453BF File Offset: 0x000435BF
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x000453CD File Offset: 0x000435CD
		internal bool isTapPress
		{
			get
			{
				return (this.flags & 16) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 16;
					return;
				}
				this.flags &= 239;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x000453F6 File Offset: 0x000435F6
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x00045404 File Offset: 0x00043604
		internal bool isTapRelease
		{
			get
			{
				return (this.flags & 32) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 32;
					return;
				}
				this.flags &= 223;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0004542D File Offset: 0x0004362D
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x0004543E File Offset: 0x0004363E
		internal bool beganInSameFrame
		{
			get
			{
				return (this.flags & 128) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 128;
					return;
				}
				this.flags &= 127;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00045467 File Offset: 0x00043667
		public FourCC format
		{
			get
			{
				return TouchState.Format;
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00045470 File Offset: 0x00043670
		public override string ToString()
		{
			return string.Format("{{ id={0} phase={1} pos={2} delta={3} pressure={4} radius={5} primary={6} }}", new object[] { this.touchId, this.phase, this.position, this.delta, this.pressure, this.radius, this.isPrimaryTouch });
		}

		// Token: 0x0400058B RID: 1419
		internal const int kSizeInBytes = 56;

		// Token: 0x0400058C RID: 1420
		[InputControl(displayName = "Touch ID", layout = "Integer", synthetic = true, dontReset = true)]
		[FieldOffset(0)]
		public int touchId;

		// Token: 0x0400058D RID: 1421
		[InputControl(displayName = "Position", dontReset = true)]
		[FieldOffset(4)]
		public Vector2 position;

		// Token: 0x0400058E RID: 1422
		[InputControl(displayName = "Delta", layout = "Delta")]
		[FieldOffset(12)]
		public Vector2 delta;

		// Token: 0x0400058F RID: 1423
		[InputControl(displayName = "Pressure", layout = "Axis")]
		[FieldOffset(20)]
		public float pressure;

		// Token: 0x04000590 RID: 1424
		[InputControl(displayName = "Radius")]
		[FieldOffset(24)]
		public Vector2 radius;

		// Token: 0x04000591 RID: 1425
		[InputControl(name = "phase", displayName = "Touch Phase", layout = "TouchPhase", synthetic = true)]
		[InputControl(name = "press", displayName = "Touch Contact?", layout = "TouchPress", useStateFrom = "phase")]
		[FieldOffset(32)]
		public byte phaseId;

		// Token: 0x04000592 RID: 1426
		[InputControl(name = "tapCount", displayName = "Tap Count", layout = "Integer")]
		[FieldOffset(33)]
		public byte tapCount;

		// Token: 0x04000593 RID: 1427
		[InputControl(name = "displayIndex", displayName = "Display Index", layout = "Integer")]
		[FieldOffset(34)]
		public byte displayIndex;

		// Token: 0x04000594 RID: 1428
		[InputControl(name = "indirectTouch", displayName = "Indirect Touch?", layout = "Button", bit = 0U, synthetic = true)]
		[InputControl(name = "tap", displayName = "Tap", layout = "Button", bit = 4U)]
		[FieldOffset(35)]
		public byte flags;

		// Token: 0x04000595 RID: 1429
		[FieldOffset(36)]
		internal uint updateStepCount;

		// Token: 0x04000596 RID: 1430
		[InputControl(displayName = "Start Time", layout = "Double", synthetic = true)]
		[FieldOffset(40)]
		public double startTime;

		// Token: 0x04000597 RID: 1431
		[InputControl(displayName = "Start Position", synthetic = true)]
		[FieldOffset(48)]
		public Vector2 startPosition;
	}
}
