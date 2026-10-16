using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000066 RID: 102
	[InputControlLayout(commonUsages = new string[] { "LeftHand", "RightHand" }, isGenericTypeOfDevice = true, displayName = "XR Controller")]
	public class XRController : TrackedDevice
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0003785D File Offset: 0x00035A5D
		public static XRController leftHand
		{
			get
			{
				return InputSystem.GetDevice<XRController>(CommonUsages.LeftHand);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00037869 File Offset: 0x00035A69
		public static XRController rightHand
		{
			get
			{
				return InputSystem.GetDevice<XRController>(CommonUsages.RightHand);
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00037878 File Offset: 0x00035A78
		protected override void FinishSetup()
		{
			base.FinishSetup();
			XRDeviceDescriptor xrdeviceDescriptor = XRDeviceDescriptor.FromJson(base.description.capabilities);
			if (xrdeviceDescriptor != null)
			{
				if ((xrdeviceDescriptor.characteristics & InputDeviceCharacteristics.Left) != InputDeviceCharacteristics.None)
				{
					InputSystem.SetDeviceUsage(this, CommonUsages.LeftHand);
					return;
				}
				if ((xrdeviceDescriptor.characteristics & InputDeviceCharacteristics.Right) != InputDeviceCharacteristics.None)
				{
					InputSystem.SetDeviceUsage(this, CommonUsages.RightHand);
				}
			}
		}
	}
}
