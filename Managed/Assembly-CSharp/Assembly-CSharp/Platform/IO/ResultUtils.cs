using System;
using System.IO;
using System.Threading.Tasks;
using LaundryBear.PlatformServices;
using Unity.Properties;
using UnityEngine;

namespace Platform.IO
{
	// Token: 0x02000348 RID: 840
	public static class ResultUtils
	{
		// Token: 0x06001F6A RID: 8042 RVA: 0x00098EBC File Offset: 0x000970BC
		public static StorageResult ExceptionToStorageResult(this Exception e)
		{
			if (e == null)
			{
				return StorageResult.Success;
			}
			if (e.InnerException != null)
			{
				e = e.InnerException;
			}
			if (e is FileNotFoundException)
			{
				return StorageResult.FileNotFound;
			}
			if (e is DirectoryNotFoundException)
			{
				return StorageResult.DirectoryNotFound;
			}
			if (e is DriveNotFoundException)
			{
				return StorageResult.DriveNotFound;
			}
			if (e is PathTooLongException)
			{
				return StorageResult.PathTooLong;
			}
			if (e is InvalidPathException)
			{
				return StorageResult.InvalidPath;
			}
			if (!(e is UnauthorizedAccessException))
			{
				return StorageResult.UnknownFailure;
			}
			return StorageResult.InvalidPermissions;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00098F20 File Offset: 0x00097120
		public static Exception StorageResultToException(this StorageResult result, string appendedOnErrorMessage = null)
		{
			if (result.WasSuccessful())
			{
				return null;
			}
			string text = ((appendedOnErrorMessage != null) ? string.Format("{0}\n{1}", result, appendedOnErrorMessage) : string.Format("{0}", result));
			switch (result)
			{
			case StorageResult.FileNotFound:
				return new FileNotFoundException(text);
			case StorageResult.DirectoryNotFound:
				return new DirectoryNotFoundException(text);
			case StorageResult.DriveNotFound:
				return new DriveNotFoundException(text);
			case StorageResult.AlreadyExists:
				return new InvalidOperationException(text);
			case StorageResult.PathTooLong:
				return new PathTooLongException(text);
			case StorageResult.QuotaExceeded:
				return new OutOfStorageException("Storage space is full. Please make more space to avoid loss of data.");
			case StorageResult.InvalidPermissions:
				return new UnauthorizedAccessException(text);
			case StorageResult.InUse:
				return new AccessViolationException(text);
			case StorageResult.StorageCountExceeded:
				return new IOException(text);
			case StorageResult.Corrupted:
				return new IOException(text);
			}
			return new IOException(string.Format("Platform.IO:: Unknown Failure from {0}", result));
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00098FFB File Offset: 0x000971FB
		public static StorageResult ThrowIfFailed(this StorageResult result)
		{
			if (result == StorageResult.Success)
			{
				return result;
			}
			throw result.StorageResultToException("Platform.IO:: Exception");
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0009900D File Offset: 0x0009720D
		public static StorageResult ThrowIfFailedWithDetails(this StorageResult result, string details)
		{
			if (result == StorageResult.Success)
			{
				return result;
			}
			throw result.StorageResultToException("Platform.IO:: exception, Details " + details);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00099025 File Offset: 0x00097225
		public static StorageResult LogIfFailedWithDetails(this StorageResult result, string details)
		{
			if (result != StorageResult.Success)
			{
				Debug.LogError(string.Format("Platform.IO:: {0}\nDetails: {1}", result, details));
			}
			return result;
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x00099041 File Offset: 0x00097241
		public static T AwaitResult<T>(this Task<T> result)
		{
			result.Wait();
			return result.Result;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0009904F File Offset: 0x0009724F
		public static StorageResult LogIfErrored(this StorageResult result)
		{
			if (result != StorageResult.Success)
			{
				Debug.LogError(string.Format("Platform.IO:: {0}", result));
			}
			return result;
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0009906A File Offset: 0x0009726A
		public static bool WasSuccessful(this StorageResult result)
		{
			return result == StorageResult.Success;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00099070 File Offset: 0x00097270
		public static FileMode ToSystemIOCounterpart(this FileMode mode)
		{
			if (mode == FileMode.Open)
			{
				return FileMode.Open;
			}
			if (mode == FileMode.OpenOrCreate)
			{
				return FileMode.OpenOrCreate;
			}
			throw new NotImplementedException(string.Format("Platform.IO:: {0} {1} is not supported", "mode", mode));
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x00099098 File Offset: 0x00097298
		public static FileAccess ToSystemIOCounterpart(this FileAccess access)
		{
			if (access == FileAccess.Read)
			{
				return FileAccess.Read;
			}
			if (access != FileAccess.ReadWrite)
			{
				throw new NotImplementedException(string.Format("Platform.IO:: {0} {1} is not supported", "access", access));
			}
			return FileAccess.ReadWrite;
		}
	}
}
