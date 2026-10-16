using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000074 RID: 116
	public struct BufferedRumble
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0003917B File Offset: 0x0003737B
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x00039183 File Offset: 0x00037383
		public HapticCapabilities capabilities { readonly get; private set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0003918C File Offset: 0x0003738C
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x00039194 File Offset: 0x00037394
		private InputDevice device { readonly get; set; }

		// Token: 0x06000A75 RID: 2677 RVA: 0x000391A0 File Offset: 0x000373A0
		public BufferedRumble(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.device = device;
			GetHapticCapabilitiesCommand getHapticCapabilitiesCommand = GetHapticCapabilitiesCommand.Create();
			device.ExecuteCommand<GetHapticCapabilitiesCommand>(ref getHapticCapabilitiesCommand);
			this.capabilities = getHapticCapabilitiesCommand.capabilities;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000391E0 File Offset: 0x000373E0
		public void EnqueueRumble(byte[] samples)
		{
			SendBufferedHapticCommand sendBufferedHapticCommand = SendBufferedHapticCommand.Create(samples);
			this.device.ExecuteCommand<SendBufferedHapticCommand>(ref sendBufferedHapticCommand);
		}
	}
}
