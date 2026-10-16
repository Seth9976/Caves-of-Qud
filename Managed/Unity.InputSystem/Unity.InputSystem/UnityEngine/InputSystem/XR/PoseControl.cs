using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Scripting;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000064 RID: 100
	[Preserve]
	[InputControlLayout(stateType = typeof(PoseState))]
	public class PoseControl : InputControl<PoseState>
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0003736B File Offset: 0x0003556B
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00037373 File Offset: 0x00035573
		public ButtonControl isTracked { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0003737C File Offset: 0x0003557C
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00037384 File Offset: 0x00035584
		public IntegerControl trackingState { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0003738D File Offset: 0x0003558D
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x00037395 File Offset: 0x00035595
		public Vector3Control position { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0003739E File Offset: 0x0003559E
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x000373A6 File Offset: 0x000355A6
		public QuaternionControl rotation { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x000373AF File Offset: 0x000355AF
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x000373B7 File Offset: 0x000355B7
		public Vector3Control velocity { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000373C0 File Offset: 0x000355C0
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x000373C8 File Offset: 0x000355C8
		public Vector3Control angularVelocity { get; set; }

		// Token: 0x060009EA RID: 2538 RVA: 0x000373D1 File Offset: 0x000355D1
		public PoseControl()
		{
			this.m_StateBlock.format = PoseState.s_Format;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000373EC File Offset: 0x000355EC
		protected override void FinishSetup()
		{
			this.isTracked = base.GetChildControl<ButtonControl>("isTracked");
			this.trackingState = base.GetChildControl<IntegerControl>("trackingState");
			this.position = base.GetChildControl<Vector3Control>("position");
			this.rotation = base.GetChildControl<QuaternionControl>("rotation");
			this.velocity = base.GetChildControl<Vector3Control>("velocity");
			this.angularVelocity = base.GetChildControl<Vector3Control>("angularVelocity");
			base.FinishSetup();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00037468 File Offset: 0x00035668
		public unsafe override PoseState ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1349481317)
			{
				return *(PoseState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new PoseState
			{
				isTracked = (this.isTracked.ReadUnprocessedValueFromStateWithCaching(statePtr) > 0.5f),
				trackingState = (InputTrackingState)this.trackingState.ReadUnprocessedValueFromStateWithCaching(statePtr),
				position = this.position.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rotation = this.rotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				velocity = this.velocity.ReadUnprocessedValueFromStateWithCaching(statePtr),
				angularVelocity = this.angularVelocity.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0003751C File Offset: 0x0003571C
		public unsafe override void WriteValueIntoState(PoseState value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1349481317)
			{
				*(PoseState*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.isTracked.WriteValueIntoState(value.isTracked, statePtr);
			this.trackingState.WriteValueIntoState((uint)value.trackingState, statePtr);
			this.position.WriteValueIntoState(value.position, statePtr);
			this.rotation.WriteValueIntoState(value.rotation, statePtr);
			this.velocity.WriteValueIntoState(value.velocity, statePtr);
			this.angularVelocity.WriteValueIntoState(value.angularVelocity, statePtr);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000375BC File Offset: 0x000357BC
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 480U && this.m_StateBlock.bitOffset == 0U && this.isTracked.optimizedControlDataType == 1113150533 && this.trackingState.optimizedControlDataType == 1229870112 && this.position.optimizedControlDataType == 1447379763 && this.rotation.optimizedControlDataType == 1364541780 && this.velocity.optimizedControlDataType == 1447379763 && this.angularVelocity.optimizedControlDataType == 1447379763 && this.trackingState.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 4U && this.position.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 8U && this.rotation.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 20U && this.velocity.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 36U && this.angularVelocity.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 48U)
			{
				return 1349481317;
			}
			return 0;
		}
	}
}
