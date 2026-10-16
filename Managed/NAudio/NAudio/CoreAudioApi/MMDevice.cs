using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x0200012A RID: 298
	public class MMDevice
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x00013934 File Offset: 0x00011B34
		private void GetPropertyInformation()
		{
			IPropertyStore propertyStore;
			Marshal.ThrowExceptionForHR(this.deviceInterface.OpenPropertyStore(StorageAccessMode.Read, out propertyStore));
			this.propertyStore = new PropertyStore(propertyStore);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00013960 File Offset: 0x00011B60
		private AudioClient GetAudioClient()
		{
			object obj;
			Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IID_IAudioClient, ClsCtx.ALL, IntPtr.Zero, out obj));
			return new AudioClient(obj as IAudioClient);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00013998 File Offset: 0x00011B98
		private void GetAudioMeterInformation()
		{
			object obj;
			Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IID_IAudioMeterInformation, ClsCtx.ALL, IntPtr.Zero, out obj));
			this.audioMeterInformation = new AudioMeterInformation(obj as IAudioMeterInformation);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000139D4 File Offset: 0x00011BD4
		private void GetAudioEndpointVolume()
		{
			object obj;
			Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IID_IAudioEndpointVolume, ClsCtx.ALL, IntPtr.Zero, out obj));
			this.audioEndpointVolume = new AudioEndpointVolume(obj as IAudioEndpointVolume);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00013A10 File Offset: 0x00011C10
		private void GetAudioSessionManager()
		{
			object obj;
			Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IDD_IAudioSessionManager, ClsCtx.ALL, IntPtr.Zero, out obj));
			this.audioSessionManager = new AudioSessionManager(obj as IAudioSessionManager);
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00013A4C File Offset: 0x00011C4C
		public AudioClient AudioClient
		{
			get
			{
				return this.GetAudioClient();
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00013A54 File Offset: 0x00011C54
		public AudioMeterInformation AudioMeterInformation
		{
			get
			{
				if (this.audioMeterInformation == null)
				{
					this.GetAudioMeterInformation();
				}
				return this.audioMeterInformation;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00013A6A File Offset: 0x00011C6A
		public AudioEndpointVolume AudioEndpointVolume
		{
			get
			{
				if (this.audioEndpointVolume == null)
				{
					this.GetAudioEndpointVolume();
				}
				return this.audioEndpointVolume;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00013A80 File Offset: 0x00011C80
		public AudioSessionManager AudioSessionManager
		{
			get
			{
				if (this.audioSessionManager == null)
				{
					this.GetAudioSessionManager();
				}
				return this.audioSessionManager;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00013A96 File Offset: 0x00011C96
		public PropertyStore Properties
		{
			get
			{
				if (this.propertyStore == null)
				{
					this.GetPropertyInformation();
				}
				return this.propertyStore;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00013AAC File Offset: 0x00011CAC
		public string FriendlyName
		{
			get
			{
				if (this.propertyStore == null)
				{
					this.GetPropertyInformation();
				}
				if (this.propertyStore.Contains(PropertyKeys.PKEY_Device_FriendlyName))
				{
					return (string)this.propertyStore[PropertyKeys.PKEY_Device_FriendlyName].Value;
				}
				return "Unknown";
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00013AFC File Offset: 0x00011CFC
		public string DeviceFriendlyName
		{
			get
			{
				if (this.propertyStore == null)
				{
					this.GetPropertyInformation();
				}
				if (this.propertyStore.Contains(PropertyKeys.PKEY_DeviceInterface_FriendlyName))
				{
					return (string)this.propertyStore[PropertyKeys.PKEY_DeviceInterface_FriendlyName].Value;
				}
				return "Unknown";
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00013B4C File Offset: 0x00011D4C
		public string IconPath
		{
			get
			{
				if (this.propertyStore == null)
				{
					this.GetPropertyInformation();
				}
				if (this.propertyStore.Contains(PropertyKeys.PKEY_Device_IconPath))
				{
					return (string)this.propertyStore[PropertyKeys.PKEY_Device_IconPath].Value;
				}
				return "Unknown";
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00013B9C File Offset: 0x00011D9C
		public string ID
		{
			get
			{
				string text;
				Marshal.ThrowExceptionForHR(this.deviceInterface.GetId(out text));
				return text;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00013BBC File Offset: 0x00011DBC
		public DataFlow DataFlow
		{
			get
			{
				IMMEndpoint immendpoint = this.deviceInterface as IMMEndpoint;
				DataFlow dataFlow;
				immendpoint.GetDataFlow(out dataFlow);
				return dataFlow;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00013BE0 File Offset: 0x00011DE0
		public DeviceState State
		{
			get
			{
				DeviceState deviceState;
				Marshal.ThrowExceptionForHR(this.deviceInterface.GetState(out deviceState));
				return deviceState;
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00013C00 File Offset: 0x00011E00
		internal MMDevice(IMMDevice realDevice)
		{
			this.deviceInterface = realDevice;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00013C0F File Offset: 0x00011E0F
		public override string ToString()
		{
			return this.FriendlyName;
		}

		// Token: 0x04000714 RID: 1812
		private readonly IMMDevice deviceInterface;

		// Token: 0x04000715 RID: 1813
		private PropertyStore propertyStore;

		// Token: 0x04000716 RID: 1814
		private AudioMeterInformation audioMeterInformation;

		// Token: 0x04000717 RID: 1815
		private AudioEndpointVolume audioEndpointVolume;

		// Token: 0x04000718 RID: 1816
		private AudioSessionManager audioSessionManager;

		// Token: 0x04000719 RID: 1817
		private static Guid IID_IAudioMeterInformation = new Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064");

		// Token: 0x0400071A RID: 1818
		private static Guid IID_IAudioEndpointVolume = new Guid("5CDF2C82-841E-4546-9722-0CF74078229A");

		// Token: 0x0400071B RID: 1819
		private static Guid IID_IAudioClient = new Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2");

		// Token: 0x0400071C RID: 1820
		private static Guid IDD_IAudioSessionManager = new Guid("BFA971F1-4D5E-40BB-935E-967039BFBEE4");
	}
}
