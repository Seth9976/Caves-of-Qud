using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000120 RID: 288
	[InputControlLayout(stateType = typeof(TouchState))]
	public class TouchControl : InputControl<TouchState>
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0004FA8F File Offset: 0x0004DC8F
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x0004FA97 File Offset: 0x0004DC97
		public TouchPressControl press { get; set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0004FAA0 File Offset: 0x0004DCA0
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0004FAA8 File Offset: 0x0004DCA8
		public IntegerControl displayIndex { get; set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0004FAB1 File Offset: 0x0004DCB1
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x0004FAB9 File Offset: 0x0004DCB9
		public IntegerControl touchId { get; set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0004FAC2 File Offset: 0x0004DCC2
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x0004FACA File Offset: 0x0004DCCA
		public Vector2Control position { get; set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0004FAD3 File Offset: 0x0004DCD3
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x0004FADB File Offset: 0x0004DCDB
		public DeltaControl delta { get; set; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0004FAE4 File Offset: 0x0004DCE4
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x0004FAEC File Offset: 0x0004DCEC
		public AxisControl pressure { get; set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0004FAF5 File Offset: 0x0004DCF5
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x0004FAFD File Offset: 0x0004DCFD
		public Vector2Control radius { get; set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0004FB06 File Offset: 0x0004DD06
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x0004FB0E File Offset: 0x0004DD0E
		public TouchPhaseControl phase { get; set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0004FB17 File Offset: 0x0004DD17
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x0004FB1F File Offset: 0x0004DD1F
		public ButtonControl indirectTouch { get; set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x0004FB28 File Offset: 0x0004DD28
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x0004FB30 File Offset: 0x0004DD30
		public ButtonControl tap { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0004FB39 File Offset: 0x0004DD39
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x0004FB41 File Offset: 0x0004DD41
		public IntegerControl tapCount { get; set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0004FB4A File Offset: 0x0004DD4A
		// (set) Token: 0x06001085 RID: 4229 RVA: 0x0004FB52 File Offset: 0x0004DD52
		public DoubleControl startTime { get; set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0004FB5B File Offset: 0x0004DD5B
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x0004FB63 File Offset: 0x0004DD63
		public Vector2Control startPosition { get; set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0004FB6C File Offset: 0x0004DD6C
		public unsafe bool isInProgress
		{
			get
			{
				TouchPhase touchPhase = (TouchPhase)(*this.phase.value);
				return touchPhase - TouchPhase.Began <= 1 || touchPhase == TouchPhase.Stationary;
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0004FB93 File Offset: 0x0004DD93
		public TouchControl()
		{
			this.m_StateBlock.format = new FourCC('T', 'O', 'U', 'C');
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0004FBB4 File Offset: 0x0004DDB4
		protected override void FinishSetup()
		{
			this.press = base.GetChildControl<TouchPressControl>("press");
			this.displayIndex = base.GetChildControl<IntegerControl>("displayIndex");
			this.touchId = base.GetChildControl<IntegerControl>("touchId");
			this.position = base.GetChildControl<Vector2Control>("position");
			this.delta = base.GetChildControl<DeltaControl>("delta");
			this.pressure = base.GetChildControl<AxisControl>("pressure");
			this.radius = base.GetChildControl<Vector2Control>("radius");
			this.phase = base.GetChildControl<TouchPhaseControl>("phase");
			this.indirectTouch = base.GetChildControl<ButtonControl>("indirectTouch");
			this.tap = base.GetChildControl<ButtonControl>("tap");
			this.tapCount = base.GetChildControl<IntegerControl>("tapCount");
			this.startTime = base.GetChildControl<DoubleControl>("startTime");
			this.startPosition = base.GetChildControl<Vector2Control>("startPosition");
			base.FinishSetup();
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004FCA4 File Offset: 0x0004DEA4
		public unsafe override TouchState ReadUnprocessedValueFromState(void* statePtr)
		{
			TouchState* ptr = (TouchState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			return *ptr;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0004FCC8 File Offset: 0x0004DEC8
		public unsafe override void WriteValueIntoState(TouchState value, void* statePtr)
		{
			TouchState* ptr = (TouchState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			UnsafeUtility.MemCpy((void*)ptr, UnsafeUtility.AddressOf<TouchState>(ref value), (long)UnsafeUtility.SizeOf<TouchState>());
		}
	}
}
