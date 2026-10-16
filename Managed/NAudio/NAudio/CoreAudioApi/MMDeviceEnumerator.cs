using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x0200012B RID: 299
	public class MMDeviceEnumerator : IDisposable
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x00013C55 File Offset: 0x00011E55
		public MMDeviceEnumerator()
		{
			if (Environment.OSVersion.Version.Major < 6)
			{
				throw new NotSupportedException("This functionality is only supported on Windows Vista or newer.");
			}
			this.realEnumerator = new MMDeviceEnumeratorComObject() as IMMDeviceEnumerator;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00013C8C File Offset: 0x00011E8C
		public MMDeviceCollection EnumerateAudioEndPoints(DataFlow dataFlow, DeviceState dwStateMask)
		{
			IMMDeviceCollection immdeviceCollection;
			Marshal.ThrowExceptionForHR(this.realEnumerator.EnumAudioEndpoints(dataFlow, dwStateMask, out immdeviceCollection));
			return new MMDeviceCollection(immdeviceCollection);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00013CB4 File Offset: 0x00011EB4
		public MMDevice GetDefaultAudioEndpoint(DataFlow dataFlow, Role role)
		{
			IMMDevice immdevice = null;
			Marshal.ThrowExceptionForHR(this.realEnumerator.GetDefaultAudioEndpoint(dataFlow, role, out immdevice));
			return new MMDevice(immdevice);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00013CE0 File Offset: 0x00011EE0
		public bool HasDefaultAudioEndpoint(DataFlow dataFlow, Role role)
		{
			IMMDevice immdevice = null;
			int defaultAudioEndpoint = this.realEnumerator.GetDefaultAudioEndpoint(dataFlow, role, out immdevice);
			if (defaultAudioEndpoint == 0)
			{
				Marshal.ReleaseComObject(immdevice);
				return true;
			}
			if (defaultAudioEndpoint == -2147023728)
			{
				return false;
			}
			Marshal.ThrowExceptionForHR(defaultAudioEndpoint);
			return false;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00013D1C File Offset: 0x00011F1C
		public MMDevice GetDevice(string id)
		{
			IMMDevice immdevice = null;
			Marshal.ThrowExceptionForHR(this.realEnumerator.GetDevice(id, out immdevice));
			return new MMDevice(immdevice);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00013D44 File Offset: 0x00011F44
		public int RegisterEndpointNotificationCallback([MarshalAs(28)] [In] IMMNotificationClient client)
		{
			return this.realEnumerator.RegisterEndpointNotificationCallback(client);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00013D52 File Offset: 0x00011F52
		public int UnregisterEndpointNotificationCallback([MarshalAs(28)] [In] IMMNotificationClient client)
		{
			return this.realEnumerator.UnregisterEndpointNotificationCallback(client);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00013D60 File Offset: 0x00011F60
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00013D6F File Offset: 0x00011F6F
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.realEnumerator != null)
			{
				Marshal.ReleaseComObject(this.realEnumerator);
				this.realEnumerator = null;
			}
		}

		// Token: 0x0400071D RID: 1821
		private IMMDeviceEnumerator realEnumerator;
	}
}
