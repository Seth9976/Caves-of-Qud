using System;

namespace System.IO.Ports
{
	// Token: 0x0200052C RID: 1324
	internal static class InternalResources
	{
		// Token: 0x06002A8F RID: 10895 RVA: 0x00092CAF File Offset: 0x00090EAF
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(SR.GetString("Unable to read beyond the end of the stream."));
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x00092CC0 File Offset: 0x00090EC0
		internal static string GetMessage(int errorCode)
		{
			return SR.GetString("Unknown Error '{0}'.", new object[] { errorCode });
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x00092CDB File Offset: 0x00090EDB
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, SR.GetString("The port is closed."));
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x00092CED File Offset: 0x00090EED
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(SR.GetString("IAsyncResult object did not come from the corresponding async method on this type."));
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x00092CFE File Offset: 0x00090EFE
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(SR.GetString("EndRead can only be called once for each asynchronous operation."));
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00092D0F File Offset: 0x00090F0F
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(SR.GetString("EndWrite can only be called once for each asynchronous operation."));
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x00092D20 File Offset: 0x00090F20
		internal static void WinIOError(int errorCode, string str)
		{
			if (errorCode <= 5)
			{
				if (errorCode - 2 > 1)
				{
					if (errorCode == 5)
					{
						if (str.Length == 0)
						{
							throw new UnauthorizedAccessException(SR.GetString("Access to the path is denied."));
						}
						throw new UnauthorizedAccessException(SR.GetString("Access to the path '{0}' is denied.", new object[] { str }));
					}
				}
				else
				{
					if (str.Length == 0)
					{
						throw new IOException(SR.GetString("The specified port does not exist."));
					}
					throw new IOException(SR.GetString("The port '{0}' does not exist.", new object[] { str }));
				}
			}
			else if (errorCode != 32)
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(SR.GetString("The specified file name or path is too long, or a component of the specified path is too long."));
				}
			}
			else
			{
				if (str.Length == 0)
				{
					throw new IOException(SR.GetString("The process cannot access the file because it is being used by another process."));
				}
				throw new IOException(SR.GetString("The process cannot access the file '{0}' because it is being used by another process.", new object[] { str }));
			}
			throw new IOException(InternalResources.GetMessage(errorCode), InternalResources.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x00092E0C File Offset: 0x0009100C
		internal static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}
	}
}
