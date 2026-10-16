using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Net
{
	// Token: 0x020005E0 RID: 1504
	internal static class NclUtilities
	{
		// Token: 0x06003043 RID: 12355 RVA: 0x000A647C File Offset: 0x000A467C
		internal static bool IsThreadPoolLow()
		{
			int num;
			int num2;
			ThreadPool.GetAvailableThreads(out num, out num2);
			return num < 2;
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000A6496 File Offset: 0x000A4696
		internal static bool HasShutdownStarted
		{
			get
			{
				return Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload();
			}
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000A64AC File Offset: 0x000A46AC
		internal static bool IsCredentialFailure(SecurityStatus error)
		{
			return error == SecurityStatus.LogonDenied || error == SecurityStatus.UnknownCredentials || error == SecurityStatus.NoImpersonation || error == SecurityStatus.NoAuthenticatingAuthority || error == SecurityStatus.UntrustedRoot || error == SecurityStatus.CertExpired || error == SecurityStatus.SmartcardLogonRequired || error == SecurityStatus.BadBinding;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000A64FC File Offset: 0x000A46FC
		internal static bool IsClientFault(SecurityStatus error)
		{
			return error == SecurityStatus.InvalidToken || error == SecurityStatus.CannotPack || error == SecurityStatus.QopNotSupported || error == SecurityStatus.NoCredentials || error == SecurityStatus.MessageAltered || error == SecurityStatus.OutOfSequence || error == SecurityStatus.IncompleteMessage || error == SecurityStatus.IncompleteCredentials || error == SecurityStatus.WrongPrincipal || error == SecurityStatus.TimeSkew || error == SecurityStatus.IllegalMessage || error == SecurityStatus.CertUnknown || error == SecurityStatus.AlgorithmMismatch || error == SecurityStatus.SecurityQosFailed || error == SecurityStatus.UnsupportedPreauth;
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003047 RID: 12359 RVA: 0x000A6583 File Offset: 0x000A4783
		internal static ContextCallback ContextRelativeDemandCallback
		{
			get
			{
				if (NclUtilities.s_ContextRelativeDemandCallback == null)
				{
					NclUtilities.s_ContextRelativeDemandCallback = new ContextCallback(NclUtilities.DemandCallback);
				}
				return NclUtilities.s_ContextRelativeDemandCallback;
			}
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x00003917 File Offset: 0x00001B17
		private static void DemandCallback(object state)
		{
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000A65A8 File Offset: 0x000A47A8
		internal static bool GuessWhetherHostIsLoopback(string host)
		{
			string text = host.ToLowerInvariant();
			return text == "localhost" || text == "loopback";
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000A65D9 File Offset: 0x000A47D9
		internal static bool IsFatal(Exception exception)
		{
			return exception != null && (exception is OutOfMemoryException || exception is StackOverflowException || exception is ThreadAbortException);
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000A65FC File Offset: 0x000A47FC
		internal static bool IsAddressLocal(IPAddress ipAddress)
		{
			IPAddress[] localAddresses = NclUtilities.LocalAddresses;
			for (int i = 0; i < localAddresses.Length; i++)
			{
				if (ipAddress.Equals(localAddresses[i], false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000A662C File Offset: 0x000A482C
		private static IPHostEntry GetLocalHost()
		{
			return Dns.GetHostByName(Dns.GetHostName());
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x000A6638 File Offset: 0x000A4838
		internal static IPAddress[] LocalAddresses
		{
			get
			{
				IPAddress[] array = NclUtilities._LocalAddresses;
				if (array != null)
				{
					return array;
				}
				object localAddressesLock = NclUtilities.LocalAddressesLock;
				IPAddress[] array2;
				lock (localAddressesLock)
				{
					array = NclUtilities._LocalAddresses;
					if (array != null)
					{
						array2 = array;
					}
					else
					{
						List<IPAddress> list = new List<IPAddress>();
						try
						{
							IPHostEntry localHost = NclUtilities.GetLocalHost();
							if (localHost != null)
							{
								if (localHost.HostName != null)
								{
									int num = localHost.HostName.IndexOf('.');
									if (num != -1)
									{
										NclUtilities._LocalDomainName = localHost.HostName.Substring(num);
									}
								}
								IPAddress[] addressList = localHost.AddressList;
								if (addressList != null)
								{
									foreach (IPAddress ipaddress in addressList)
									{
										list.Add(ipaddress);
									}
								}
							}
						}
						catch
						{
						}
						array = new IPAddress[list.Count];
						int num2 = 0;
						foreach (IPAddress ipaddress2 in list)
						{
							array[num2] = ipaddress2;
							num2++;
						}
						NclUtilities._LocalAddresses = array;
						array2 = array;
					}
				}
				return array2;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x000A6778 File Offset: 0x000A4978
		private static object LocalAddressesLock
		{
			get
			{
				if (NclUtilities._LocalAddressesLock == null)
				{
					Interlocked.CompareExchange(ref NclUtilities._LocalAddressesLock, new object(), null);
				}
				return NclUtilities._LocalAddressesLock;
			}
		}

		// Token: 0x04001B1F RID: 6943
		private static volatile ContextCallback s_ContextRelativeDemandCallback;

		// Token: 0x04001B20 RID: 6944
		private static volatile IPAddress[] _LocalAddresses;

		// Token: 0x04001B21 RID: 6945
		private static object _LocalAddressesLock;

		// Token: 0x04001B22 RID: 6946
		private const int HostNameBufferLength = 256;

		// Token: 0x04001B23 RID: 6947
		internal static string _LocalDomainName;
	}
}
