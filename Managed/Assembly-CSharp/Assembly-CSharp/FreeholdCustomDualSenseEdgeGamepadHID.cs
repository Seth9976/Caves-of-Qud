using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Layouts;

// Token: 0x02000071 RID: 113
public class FreeholdCustomDualSenseEdgeGamepadHID : Gamepad
{
	// Token: 0x0600042E RID: 1070 RVA: 0x0001C408 File Offset: 0x0001A608
	static FreeholdCustomDualSenseEdgeGamepadHID()
	{
		string text = "DualSense Edge (Freehold Games)";
		InputDeviceMatcher inputDeviceMatcher = default(InputDeviceMatcher);
		inputDeviceMatcher = inputDeviceMatcher.WithInterface("HID", true);
		inputDeviceMatcher = inputDeviceMatcher.WithManufacturer("Sony Interactive Entertainment", true);
		InputSystem.RegisterLayout<DualSenseGamepadHID>(text, new InputDeviceMatcher?(inputDeviceMatcher.WithProduct("DualSense Edge Wireless Controller", true)));
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x0001C454 File Offset: 0x0001A654
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Init()
	{
	}
}
