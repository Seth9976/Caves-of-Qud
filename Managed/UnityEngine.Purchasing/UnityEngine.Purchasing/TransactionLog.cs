using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200002C RID: 44
	internal class TransactionLog
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000372A File Offset: 0x0000192A
		public TransactionLog(ILogger logger, string persistentDataPath)
		{
			this.logger = logger;
			if (!string.IsNullOrEmpty(persistentDataPath))
			{
				this.persistentDataPath = Path.Combine(Path.Combine(persistentDataPath, "Unity"), "UnityPurchasing");
			}
			this.recordCache = new HashSet<string>();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003767 File Offset: 0x00001967
		public void Clear()
		{
			Directory.Delete(this.persistentDataPath, true);
			this.recordCache.Clear();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003780 File Offset: 0x00001980
		public bool HasRecordOf(string transactionID)
		{
			if (string.IsNullOrEmpty(transactionID) || string.IsNullOrEmpty(this.persistentDataPath))
			{
				return false;
			}
			if (this.recordCache.Contains(transactionID))
			{
				return true;
			}
			bool flag = Directory.Exists(this.GetRecordPath(transactionID));
			if (flag)
			{
				this.recordCache.Add(transactionID);
			}
			return flag;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000037D0 File Offset: 0x000019D0
		public void Record(string transactionID)
		{
			if (!string.IsNullOrEmpty(transactionID) && !string.IsNullOrEmpty(this.persistentDataPath))
			{
				string recordPath = this.GetRecordPath(transactionID);
				try
				{
					Directory.CreateDirectory(recordPath);
					this.recordCache.Add(transactionID);
				}
				catch (Exception ex)
				{
					this.logger.LogException(ex);
				}
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003830 File Offset: 0x00001A30
		private string GetRecordPath(string transactionID)
		{
			return Path.Combine(this.persistentDataPath, TransactionLog.ComputeHash(transactionID));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003844 File Offset: 0x00001A44
		internal static string ComputeHash(string transactionID)
		{
			ulong num = 3074457345618258791UL;
			for (int i = 0; i < transactionID.Length; i++)
			{
				num += (ulong)transactionID[i];
				num *= 3074457345618258799UL;
			}
			StringBuilder stringBuilder = new StringBuilder(16);
			foreach (byte b in BitConverter.GetBytes(num))
			{
				stringBuilder.AppendFormat("{0:X2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000065 RID: 101
		private readonly ILogger logger;

		// Token: 0x04000066 RID: 102
		private readonly string persistentDataPath;

		// Token: 0x04000067 RID: 103
		private HashSet<string> recordCache;
	}
}
