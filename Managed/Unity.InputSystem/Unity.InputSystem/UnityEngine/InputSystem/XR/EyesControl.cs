using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000072 RID: 114
	public class EyesControl : InputControl<Eyes>
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00038B62 File Offset: 0x00036D62
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x00038B6A File Offset: 0x00036D6A
		[InputControl(offset = 0U, displayName = "LeftEyePosition")]
		public Vector3Control leftEyePosition { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00038B73 File Offset: 0x00036D73
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x00038B7B File Offset: 0x00036D7B
		[InputControl(offset = 12U, displayName = "LeftEyeRotation")]
		public QuaternionControl leftEyeRotation { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00038B84 File Offset: 0x00036D84
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00038B8C File Offset: 0x00036D8C
		[InputControl(offset = 28U, displayName = "RightEyePosition")]
		public Vector3Control rightEyePosition { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00038B95 File Offset: 0x00036D95
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x00038B9D File Offset: 0x00036D9D
		[InputControl(offset = 40U, displayName = "RightEyeRotation")]
		public QuaternionControl rightEyeRotation { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00038BA6 File Offset: 0x00036DA6
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x00038BAE File Offset: 0x00036DAE
		[InputControl(offset = 56U, displayName = "FixationPoint")]
		public Vector3Control fixationPoint { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00038BB7 File Offset: 0x00036DB7
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x00038BBF File Offset: 0x00036DBF
		[InputControl(offset = 68U, displayName = "LeftEyeOpenAmount")]
		public AxisControl leftEyeOpenAmount { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00038BC8 File Offset: 0x00036DC8
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00038BD0 File Offset: 0x00036DD0
		[InputControl(offset = 72U, displayName = "RightEyeOpenAmount")]
		public AxisControl rightEyeOpenAmount { get; set; }

		// Token: 0x06000A6C RID: 2668 RVA: 0x00038BDC File Offset: 0x00036DDC
		protected override void FinishSetup()
		{
			this.leftEyePosition = base.GetChildControl<Vector3Control>("leftEyePosition");
			this.leftEyeRotation = base.GetChildControl<QuaternionControl>("leftEyeRotation");
			this.rightEyePosition = base.GetChildControl<Vector3Control>("rightEyePosition");
			this.rightEyeRotation = base.GetChildControl<QuaternionControl>("rightEyeRotation");
			this.fixationPoint = base.GetChildControl<Vector3Control>("fixationPoint");
			this.leftEyeOpenAmount = base.GetChildControl<AxisControl>("leftEyeOpenAmount");
			this.rightEyeOpenAmount = base.GetChildControl<AxisControl>("rightEyeOpenAmount");
			base.FinishSetup();
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00038C68 File Offset: 0x00036E68
		public unsafe override Eyes ReadUnprocessedValueFromState(void* statePtr)
		{
			return new Eyes
			{
				leftEyePosition = this.leftEyePosition.ReadUnprocessedValueFromStateWithCaching(statePtr),
				leftEyeRotation = this.leftEyeRotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyePosition = this.rightEyePosition.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyeRotation = this.rightEyeRotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				fixationPoint = this.fixationPoint.ReadUnprocessedValueFromStateWithCaching(statePtr),
				leftEyeOpenAmount = this.leftEyeOpenAmount.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyeOpenAmount = this.rightEyeOpenAmount.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00038D04 File Offset: 0x00036F04
		public unsafe override void WriteValueIntoState(Eyes value, void* statePtr)
		{
			this.leftEyePosition.WriteValueIntoState(value.leftEyePosition, statePtr);
			this.leftEyeRotation.WriteValueIntoState(value.leftEyeRotation, statePtr);
			this.rightEyePosition.WriteValueIntoState(value.rightEyePosition, statePtr);
			this.rightEyeRotation.WriteValueIntoState(value.rightEyeRotation, statePtr);
			this.fixationPoint.WriteValueIntoState(value.fixationPoint, statePtr);
			this.leftEyeOpenAmount.WriteValueIntoState(value.leftEyeOpenAmount, statePtr);
			this.rightEyeOpenAmount.WriteValueIntoState(value.rightEyeOpenAmount, statePtr);
		}
	}
}
