using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F2 RID: 242
	internal static class InputRuntimeExtensions
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x00047C58 File Offset: 0x00045E58
		public unsafe static long DeviceCommand<TCommand>(this IInputRuntime runtime, int deviceId, ref TCommand command) where TCommand : struct, IInputDeviceCommandInfo
		{
			if (runtime == null)
			{
				throw new ArgumentNullException("runtime");
			}
			return runtime.DeviceCommand(deviceId, (InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command));
		}
	}
}
