using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000D2 RID: 210
	internal class MonoBtlsBioMemory : MonoBtlsBio
	{
		// Token: 0x06000420 RID: 1056
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_bio_mem_new();

		// Token: 0x06000421 RID: 1057
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_mem_get_data(IntPtr handle, out IntPtr data);

		// Token: 0x06000422 RID: 1058 RVA: 0x0000CD7D File Offset: 0x0000AF7D
		public MonoBtlsBioMemory()
			: base(new MonoBtlsBio.BoringBioHandle(MonoBtlsBioMemory.mono_btls_bio_mem_new()))
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000CD90 File Offset: 0x0000AF90
		public byte[] GetData()
		{
			bool flag = false;
			byte[] array2;
			try
			{
				base.Handle.DangerousAddRef(ref flag);
				IntPtr intPtr;
				int num = MonoBtlsBioMemory.mono_btls_bio_mem_get_data(base.Handle.DangerousGetHandle(), out intPtr);
				base.CheckError(num > 0, "GetData");
				byte[] array = new byte[num];
				Marshal.Copy(intPtr, array, 0, num);
				array2 = array;
			}
			finally
			{
				if (flag)
				{
					base.Handle.DangerousRelease();
				}
			}
			return array2;
		}
	}
}
