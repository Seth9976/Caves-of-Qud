using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x0200019F RID: 415
	public sealed class WaveFormatCustomMarshaler : ICustomMarshaler
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x00017CDB File Offset: 0x00015EDB
		public static ICustomMarshaler GetInstance(string cookie)
		{
			if (WaveFormatCustomMarshaler.marshaler == null)
			{
				WaveFormatCustomMarshaler.marshaler = new WaveFormatCustomMarshaler();
			}
			return WaveFormatCustomMarshaler.marshaler;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00017CF3 File Offset: 0x00015EF3
		public void CleanUpManagedData(object ManagedObj)
		{
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00017CF5 File Offset: 0x00015EF5
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			Marshal.FreeHGlobal(pNativeData);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00017CFD File Offset: 0x00015EFD
		public int GetNativeDataSize()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00017D04 File Offset: 0x00015F04
		public IntPtr MarshalManagedToNative(object ManagedObj)
		{
			return WaveFormat.MarshalToPtr((WaveFormat)ManagedObj);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00017D11 File Offset: 0x00015F11
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return WaveFormat.MarshalFromPtr(pNativeData);
		}

		// Token: 0x040009A6 RID: 2470
		private static WaveFormatCustomMarshaler marshaler;
	}
}
