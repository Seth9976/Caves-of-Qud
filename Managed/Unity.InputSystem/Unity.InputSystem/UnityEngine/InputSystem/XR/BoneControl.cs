using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000071 RID: 113
	public class BoneControl : InputControl<Bone>
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00038A60 File Offset: 0x00036C60
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00038A68 File Offset: 0x00036C68
		[InputControl(offset = 0U, displayName = "parentBoneIndex")]
		public IntegerControl parentBoneIndex { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00038A71 File Offset: 0x00036C71
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00038A79 File Offset: 0x00036C79
		[InputControl(offset = 4U, displayName = "Position")]
		public Vector3Control position { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00038A82 File Offset: 0x00036C82
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x00038A8A File Offset: 0x00036C8A
		[InputControl(offset = 16U, displayName = "Rotation")]
		public QuaternionControl rotation { get; set; }

		// Token: 0x06000A5A RID: 2650 RVA: 0x00038A93 File Offset: 0x00036C93
		protected override void FinishSetup()
		{
			this.parentBoneIndex = base.GetChildControl<IntegerControl>("parentBoneIndex");
			this.position = base.GetChildControl<Vector3Control>("position");
			this.rotation = base.GetChildControl<QuaternionControl>("rotation");
			base.FinishSetup();
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00038AD0 File Offset: 0x00036CD0
		public unsafe override Bone ReadUnprocessedValueFromState(void* statePtr)
		{
			return new Bone
			{
				parentBoneIndex = (uint)this.parentBoneIndex.ReadUnprocessedValueFromStateWithCaching(statePtr),
				position = this.position.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rotation = this.rotation.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00038B1F File Offset: 0x00036D1F
		public unsafe override void WriteValueIntoState(Bone value, void* statePtr)
		{
			this.parentBoneIndex.WriteValueIntoState((int)value.parentBoneIndex, statePtr);
			this.position.WriteValueIntoState(value.position, statePtr);
			this.rotation.WriteValueIntoState(value.rotation, statePtr);
		}
	}
}
