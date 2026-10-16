using System;
using Microsoft.Win32;

namespace System.Web.Configuration
{
	// Token: 0x020005B2 RID: 1458
	internal class MachineKeyRegistryStorage
	{
		// Token: 0x06003E80 RID: 16000 RVA: 0x000A3794 File Offset: 0x000A1994
		static MachineKeyRegistryStorage()
		{
			string applicationName = AppDomain.CurrentDomain.SetupInformation.ApplicationName;
			if (applicationName == null)
			{
				return;
			}
			string text = applicationName.GetHashCode().ToString("x");
			MachineKeyRegistryStorage.keyEncryption = string.Concat(new string[]
			{
				"software\\mono\\asp.net\\",
				Environment.Version.ToString(),
				"\\autogenkeys\\",
				text,
				"-",
				1.ToString()
			});
			MachineKeyRegistryStorage.keyValidation = string.Concat(new string[]
			{
				"software\\mono\\asp.net\\",
				Environment.Version.ToString(),
				"\\autogenkeys\\",
				text,
				"-",
				0.ToString()
			});
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x000A3854 File Offset: 0x000A1A54
		public static byte[] Retrieve(MachineKeyRegistryStorage.KeyType kt)
		{
			string text;
			if (kt != MachineKeyRegistryStorage.KeyType.Validation)
			{
				if (kt != MachineKeyRegistryStorage.KeyType.Encryption)
				{
					throw new ArgumentException("Unknown key type.");
				}
				text = MachineKeyRegistryStorage.keyEncryption;
			}
			else
			{
				text = MachineKeyRegistryStorage.keyValidation;
			}
			if (text == null)
			{
				return null;
			}
			object obj = null;
			try
			{
				obj = MachineKeyRegistryStorage.OpenRegistryKey(text, false).GetValue("AutoGenKey", null);
			}
			catch (Exception)
			{
				return null;
			}
			if (obj == null || obj.GetType() != typeof(byte[]))
			{
				return null;
			}
			return (byte[])obj;
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x000A38DC File Offset: 0x000A1ADC
		private static RegistryKey OpenRegistryKey(string path, bool write)
		{
			string[] array = path.Split('\\', StringSplitOptions.None);
			int num = array.Length;
			RegistryKey registryKey = Registry.CurrentUser;
			for (int i = 0; i < num; i++)
			{
				RegistryKey registryKey2 = registryKey.OpenSubKey(array[i], true);
				if (registryKey2 == null)
				{
					if (!write)
					{
						return null;
					}
					registryKey2 = registryKey.CreateSubKey(array[i]);
				}
				registryKey = registryKey2;
			}
			return registryKey;
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x000A3930 File Offset: 0x000A1B30
		public static void Store(byte[] buf, MachineKeyRegistryStorage.KeyType kt)
		{
			if (buf == null)
			{
				return;
			}
			string text;
			if (kt != MachineKeyRegistryStorage.KeyType.Validation)
			{
				if (kt != MachineKeyRegistryStorage.KeyType.Encryption)
				{
					throw new ArgumentException("Unknown key type.");
				}
				text = MachineKeyRegistryStorage.keyEncryption;
			}
			else
			{
				text = MachineKeyRegistryStorage.keyValidation;
			}
			if (text == null)
			{
				return;
			}
			try
			{
				using (RegistryKey registryKey = MachineKeyRegistryStorage.OpenRegistryKey(text, true))
				{
					registryKey.SetValue("AutoGenKey", buf, RegistryValueKind.Binary);
					registryKey.SetValue("AutoGenKeyCreationTime", DateTime.Now.Ticks, RegistryValueKind.QWord);
					registryKey.SetValue("AutoGenKeyFormat", 2, RegistryValueKind.DWord);
					registryKey.Flush();
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("(info) Auto generated encryption keys not saved: {0}", ex);
			}
		}

		// Token: 0x0400223A RID: 8762
		private static string keyEncryption;

		// Token: 0x0400223B RID: 8763
		private static string keyValidation;

		// Token: 0x020005B3 RID: 1459
		public enum KeyType
		{
			// Token: 0x0400223D RID: 8765
			Validation,
			// Token: 0x0400223E RID: 8766
			Encryption
		}
	}
}
