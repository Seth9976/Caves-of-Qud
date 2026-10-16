using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LaundryBear.PlatformServices;
using Newtonsoft.Json;

namespace Platform.IO
{
	// Token: 0x02000337 RID: 823
	public static class File
	{
		// Token: 0x06001EE9 RID: 7913 RVA: 0x00097B05 File Offset: 0x00095D05
		public static void WriteAllBytes(string path, byte[] content)
		{
			Blob.WriteAllBytes(path, content).ThrowIfFailed();
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x00097B14 File Offset: 0x00095D14
		public static async Task WriteAllBytesAsync(string path, byte[] content)
		{
			(await Blob.WriteAllBytesAsync(path, content)).ThrowIfFailed();
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00097B5F File Offset: 0x00095D5F
		public static void WriteAllText(string path, string content)
		{
			Blob.WriteAllText(path, content).ThrowIfFailed();
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00097B70 File Offset: 0x00095D70
		public static async Task WriteAllTextAsync(string path, string content)
		{
			(await Blob.WriteAllTextAsync(path, content)).ThrowIfFailed();
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x00097BBC File Offset: 0x00095DBC
		public static byte[] ReadAllBytes(string path)
		{
			return Blob.ReadAllBytes(path).ThrowIfFailed().content;
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00097BDC File Offset: 0x00095DDC
		public static async Task<byte[]> ReadAllBytesAsync(string path)
		{
			TaskAwaiter<LoadBytesResult> taskAwaiter = Blob.ReadAllBytesAsync(path).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<LoadBytesResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<LoadBytesResult>);
			}
			return taskAwaiter.GetResult().ThrowIfFailed().content;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00097C20 File Offset: 0x00095E20
		public static string ReadAllText(string path)
		{
			return Blob.ReadAllText(path).ThrowIfFailed().content;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00097C40 File Offset: 0x00095E40
		public static async Task<string> ReadAllTextAsync(string path)
		{
			TaskAwaiter<LoadTextResult> taskAwaiter = Blob.ReadAllTextAsync(path).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<LoadTextResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<LoadTextResult>);
			}
			return taskAwaiter.GetResult().ThrowIfFailed().content;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00097C83 File Offset: 0x00095E83
		public static FileInfo GetInfo(string path)
		{
			return new FileInfo(path);
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00097C8B File Offset: 0x00095E8B
		public static bool Exists(string path)
		{
			return Blob.Exists(path).WasSuccessful();
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00097C98 File Offset: 0x00095E98
		public static async Task<bool> ExistsAsync(string path)
		{
			return (await Blob.ExistsAsync(path)).WasSuccessful();
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00097CDB File Offset: 0x00095EDB
		public static void Delete(string path)
		{
			Blob.Delete(path).ThrowIfFailed();
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00097CEC File Offset: 0x00095EEC
		public static async Task<StorageResult> DeleteAsync(string path)
		{
			return (await Blob.DeleteAsync(path)).ThrowIfFailed();
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00097D30 File Offset: 0x00095F30
		public static async Task CopyAsync(string sourceFileName, string destFileName, bool overwrite = false)
		{
			(await Blob.CopyAsync(sourceFileName, destFileName, overwrite)).ThrowIfFailed();
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00097D84 File Offset: 0x00095F84
		public static void Copy(string sourceFileName, string destFileName, bool overwrite = false)
		{
			Blob.Copy(sourceFileName, destFileName, overwrite).ThrowIfFailed();
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00097DA4 File Offset: 0x00095FA4
		public static void CopyOverwriteByChunk(string sourceFileName, string destFileName, int chunksize = 32)
		{
			long num = 0L;
			long length = new FileInfo(sourceFileName).Length;
			long num2 = Math.Min(length, (long)(chunksize * 1024 * 1024));
			byte[] array = new byte[num2];
			while (num < length)
			{
				long num3 = 0L;
				using (Stream result = File.OpenReadAsync(sourceFileName).Result)
				{
					using (Stream result2 = File.OpenWriteAsync(destFileName, FileMode.Truncate, FileAccess.Write).Result)
					{
						result.Position = num;
						result2.Position = num;
						num3 = (long)result.Read(array, (int)num, (int)num2);
						result2.Write(array, 0, (int)num3);
					}
				}
				num += num3;
			}
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00097E6C File Offset: 0x0009606C
		public static async Task CopyOverwriteByChunkAsync(string sourceFileName, string destFileName, int chunksize = 32)
		{
			long totalAmountWritten = 0L;
			FileInfo fileInfo = new FileInfo(sourceFileName);
			long length = fileInfo.Length;
			long maxIndividualWrite = Math.Min(length, (long)(chunksize * 1024 * 1024));
			byte[] copiedData = new byte[maxIndividualWrite];
			while (totalAmountWritten < length)
			{
				long amountWritten = 0L;
				Stream stream = await File.OpenReadAsync(sourceFileName);
				using (Stream sourceStream = stream)
				{
					stream = await File.OpenWriteAsync(destFileName, FileMode.Truncate, FileAccess.Write);
					using (Stream destStream = stream)
					{
						sourceStream.Position = totalAmountWritten;
						destStream.Position = totalAmountWritten;
						amountWritten = (long)(await sourceStream.ReadAsync(copiedData, (int)totalAmountWritten, (int)maxIndividualWrite));
						await destStream.WriteAsync(copiedData, 0, (int)amountWritten);
					}
					Stream destStream = null;
				}
				Stream sourceStream = null;
				totalAmountWritten += amountWritten;
			}
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00097EC0 File Offset: 0x000960C0
		public static async Task MoveAsync(string sourceFileName, string destFileName, bool overwrite = false)
		{
			(await Blob.MoveAsync(sourceFileName, destFileName, overwrite)).ThrowIfFailed();
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00097F14 File Offset: 0x00096114
		public static void Move(string sourceFileName, string destFileName, bool overwrite = false)
		{
			Blob.Move(sourceFileName, destFileName, overwrite).ThrowIfFailed();
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00097F34 File Offset: 0x00096134
		public static Stream Open(string path, FileMode mode, FileAccess access, FileShare fileShare)
		{
			return Blob.Open(path, mode, access, fileShare).ThrowIfFailed().content;
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00097F58 File Offset: 0x00096158
		public static async Task<Stream> OpenAsync(string path, FileMode mode, FileAccess access)
		{
			TaskAwaiter<ObjectLoadResult<Stream>> taskAwaiter = Blob.OpenAsync(path, mode, access).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<ObjectLoadResult<Stream>> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<ObjectLoadResult<Stream>>);
			}
			return taskAwaiter.GetResult().ThrowIfFailed().content;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00097FAC File Offset: 0x000961AC
		public static Stream OpenRead(string path)
		{
			return Blob.OpenRead(path).ThrowIfFailed().content;
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x00097FCC File Offset: 0x000961CC
		public static async Task<Stream> OpenReadAsync(string path)
		{
			TaskAwaiter<ObjectLoadResult<Stream>> taskAwaiter = Blob.OpenReadAsync(path).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<ObjectLoadResult<Stream>> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<ObjectLoadResult<Stream>>);
			}
			return taskAwaiter.GetResult().ThrowIfFailed().content;
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x00098010 File Offset: 0x00096210
		public static Stream OpenWrite(string path)
		{
			return Blob.OpenWrite(path).ThrowIfFailed().content;
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x00098030 File Offset: 0x00096230
		public static async Task<Stream> OpenWriteAsync(string path)
		{
			TaskAwaiter<ObjectLoadResult<Stream>> taskAwaiter = Blob.OpenWriteAsync(path).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<ObjectLoadResult<Stream>> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<ObjectLoadResult<Stream>>);
			}
			return taskAwaiter.GetResult().ThrowIfFailed().content;
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x00098074 File Offset: 0x00096274
		public static async Task<Stream> OpenWriteAsync(string path, FileMode mode, FileAccess fileAccess)
		{
			TaskAwaiter<ObjectLoadResult<Stream>> taskAwaiter = Blob.OpenWriteAsync(path, mode, fileAccess).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<ObjectLoadResult<Stream>> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<ObjectLoadResult<Stream>>);
			}
			return taskAwaiter.GetResult().ThrowIfFailed().content;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000980C8 File Offset: 0x000962C8
		public static void AppendAllText(string path, string content)
		{
			using (Stream stream = File.OpenWrite(path))
			{
				stream.Seek(0L, SeekOrigin.End);
				stream.Write(Encoding.UTF8.GetBytes(content));
			}
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00098118 File Offset: 0x00096318
		public static StreamWriter CreateText(string path)
		{
			return new StreamWriter(File.OpenWrite(path));
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x00098128 File Offset: 0x00096328
		public static void WriteAllJson(string path, object content, JsonSerializerSettings settings = null)
		{
			Blob.WriteAllJson(path, content, settings).ThrowIfFailed();
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00098148 File Offset: 0x00096348
		public static async Task WriteAllJsonAsync(string path, object content, JsonSerializerSettings settings = null)
		{
			(await Blob.WriteAllJsonAsync(path, content, settings)).ThrowIfFailed();
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0009819C File Offset: 0x0009639C
		public static T ReadAllJson<T>(string path, JsonSerializerSettings settings = null)
		{
			return Blob.ReadAllJson<T>(path, settings).ThrowIfFailed().content;
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000981C0 File Offset: 0x000963C0
		public static async Task<T> ReadAllJsonAsync<T>(string path, JsonSerializerSettings settings = null)
		{
			TaskAwaiter<ObjectLoadResult<T>> taskAwaiter = Blob.ReadJsonAsync<T>(path, settings).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<ObjectLoadResult<T>> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<ObjectLoadResult<T>>);
			}
			return taskAwaiter.GetResult().ThrowIfFailed().content;
		}
	}
}
