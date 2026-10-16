using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013E RID: 318
	internal class ForDeviceEventObservable : IObservable<InputEventPtr>
	{
		// Token: 0x060011B0 RID: 4528 RVA: 0x00054C68 File Offset: 0x00052E68
		public ForDeviceEventObservable(IObservable<InputEventPtr> source, Type deviceType, InputDevice device)
		{
			this.m_Source = source;
			this.m_DeviceType = deviceType;
			this.m_Device = device;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00054C85 File Offset: 0x00052E85
		public IDisposable Subscribe(IObserver<InputEventPtr> observer)
		{
			return this.m_Source.Subscribe(new ForDeviceEventObservable.ForDevice(this.m_DeviceType, this.m_Device, observer));
		}

		// Token: 0x04000726 RID: 1830
		private IObservable<InputEventPtr> m_Source;

		// Token: 0x04000727 RID: 1831
		private InputDevice m_Device;

		// Token: 0x04000728 RID: 1832
		private Type m_DeviceType;

		// Token: 0x0200024C RID: 588
		private class ForDevice : IObserver<InputEventPtr>
		{
			// Token: 0x0600165F RID: 5727 RVA: 0x0006726D File Offset: 0x0006546D
			public ForDevice(Type deviceType, InputDevice device, IObserver<InputEventPtr> observer)
			{
				this.m_Device = device;
				this.m_DeviceType = deviceType;
				this.m_Observer = observer;
			}

			// Token: 0x06001660 RID: 5728 RVA: 0x0006728A File Offset: 0x0006548A
			public void OnCompleted()
			{
			}

			// Token: 0x06001661 RID: 5729 RVA: 0x0006728C File Offset: 0x0006548C
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x06001662 RID: 5730 RVA: 0x00067294 File Offset: 0x00065494
			public void OnNext(InputEventPtr value)
			{
				if (this.m_DeviceType != null)
				{
					InputDevice deviceById = InputSystem.GetDeviceById(value.deviceId);
					if (deviceById == null)
					{
						return;
					}
					if (!this.m_DeviceType.IsInstanceOfType(deviceById))
					{
						return;
					}
				}
				if (this.m_Device != null && value.deviceId != this.m_Device.deviceId)
				{
					return;
				}
				this.m_Observer.OnNext(value);
			}

			// Token: 0x04000C9D RID: 3229
			private IObserver<InputEventPtr> m_Observer;

			// Token: 0x04000C9E RID: 3230
			private InputDevice m_Device;

			// Token: 0x04000C9F RID: 3231
			private Type m_DeviceType;
		}
	}
}
