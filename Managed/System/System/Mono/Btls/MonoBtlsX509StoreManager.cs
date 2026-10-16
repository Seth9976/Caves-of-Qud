using System;
using System.IO;
using Mono.Security.X509;

namespace Mono.Btls
{
	// Token: 0x0200010F RID: 271
	internal static class MonoBtlsX509StoreManager
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x00011730 File Offset: 0x0000F930
		private static void Initialize()
		{
			if (MonoBtlsX509StoreManager.initialized)
			{
				return;
			}
			try
			{
				MonoBtlsX509StoreManager.DoInitialize();
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("MonoBtlsX509StoreManager.Initialize() threw exception: {0}", ex);
			}
			finally
			{
				MonoBtlsX509StoreManager.initialized = true;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00011784 File Offset: 0x0000F984
		private static void DoInitialize()
		{
			string newCurrentUserPath = X509StoreManager.NewCurrentUserPath;
			MonoBtlsX509StoreManager.userTrustedRootPath = Path.Combine(newCurrentUserPath, "Trust");
			MonoBtlsX509StoreManager.userIntermediateCAPath = Path.Combine(newCurrentUserPath, "CA");
			MonoBtlsX509StoreManager.userUntrustedPath = Path.Combine(newCurrentUserPath, "Disallowed");
			string newLocalMachinePath = X509StoreManager.NewLocalMachinePath;
			MonoBtlsX509StoreManager.machineTrustedRootPath = Path.Combine(newLocalMachinePath, "Trust");
			MonoBtlsX509StoreManager.machineIntermediateCAPath = Path.Combine(newLocalMachinePath, "CA");
			MonoBtlsX509StoreManager.machineUntrustedPath = Path.Combine(newLocalMachinePath, "Disallowed");
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000117FC File Offset: 0x0000F9FC
		public static bool HasStore(MonoBtlsX509StoreType type)
		{
			string storePath = MonoBtlsX509StoreManager.GetStorePath(type);
			return storePath != null && Directory.Exists(storePath);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001181C File Offset: 0x0000FA1C
		public static string GetStorePath(MonoBtlsX509StoreType type)
		{
			MonoBtlsX509StoreManager.Initialize();
			switch (type)
			{
			case MonoBtlsX509StoreType.MachineTrustedRoots:
				return MonoBtlsX509StoreManager.machineTrustedRootPath;
			case MonoBtlsX509StoreType.MachineIntermediateCA:
				return MonoBtlsX509StoreManager.machineIntermediateCAPath;
			case MonoBtlsX509StoreType.MachineUntrusted:
				return MonoBtlsX509StoreManager.machineUntrustedPath;
			case MonoBtlsX509StoreType.UserTrustedRoots:
				return MonoBtlsX509StoreManager.userTrustedRootPath;
			case MonoBtlsX509StoreType.UserIntermediateCA:
				return MonoBtlsX509StoreManager.userIntermediateCAPath;
			case MonoBtlsX509StoreType.UserUntrusted:
				return MonoBtlsX509StoreManager.userUntrustedPath;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000473 RID: 1139
		private static bool initialized;

		// Token: 0x04000474 RID: 1140
		private static string machineTrustedRootPath;

		// Token: 0x04000475 RID: 1141
		private static string machineIntermediateCAPath;

		// Token: 0x04000476 RID: 1142
		private static string machineUntrustedPath;

		// Token: 0x04000477 RID: 1143
		private static string userTrustedRootPath;

		// Token: 0x04000478 RID: 1144
		private static string userIntermediateCAPath;

		// Token: 0x04000479 RID: 1145
		private static string userUntrustedPath;
	}
}
