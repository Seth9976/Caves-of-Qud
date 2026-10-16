using System;
using Microsoft.Win32;

namespace NAudio.Wave
{
	// Token: 0x0200017C RID: 380
	internal static class WaveCapabilitiesHelpers
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x00016630 File Offset: 0x00014830
		public static string GetNameFromGuid(Guid guid)
		{
			string text = null;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\MediaCategories"))
			{
				using (RegistryKey registryKey2 = registryKey.OpenSubKey(guid.ToString("B")))
				{
					if (registryKey2 != null)
					{
						text = registryKey2.GetValue("Name") as string;
					}
				}
			}
			return text;
		}

		// Token: 0x04000920 RID: 2336
		public static readonly Guid MicrosoftDefaultManufacturerId = new Guid("d5a47fa8-6d98-11d1-a21a-00a0c9223196");

		// Token: 0x04000921 RID: 2337
		public static readonly Guid DefaultWaveOutGuid = new Guid("E36DC310-6D9A-11D1-A21A-00A0C9223196");

		// Token: 0x04000922 RID: 2338
		public static readonly Guid DefaultWaveInGuid = new Guid("E36DC311-6D9A-11D1-A21A-00A0C9223196");
	}
}
