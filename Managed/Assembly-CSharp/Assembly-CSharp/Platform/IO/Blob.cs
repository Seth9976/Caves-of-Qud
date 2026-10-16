using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using LaundryBear.PlatformServices;
using Newtonsoft.Json;

namespace Platform.IO
{
	// Token: 0x02000335 RID: 821
	[NullableContext(1)]
	[Nullable(0)]
	public static class Blob
	{
		// Token: 0x06001EC0 RID: 7872 RVA: 0x00097154 File Offset: 0x00095354
		public static StorageResult WriteAllBytes(string path, byte[] content)
		{
			return Blob.WriteAllBytesAsync(path, content).AwaitResult<StorageResult>();
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00097164 File Offset: 0x00095364
		public static Task<StorageResult> WriteAllBytesAsync(string path, byte[] content)
		{
			TaskCompletionSource<StorageResult> tcs = new TaskCompletionSource<StorageResult>();
			if (!Path.IsROM(path))
			{
				State.GetStorage().SaveBlob(path, content, delegate(StorageResult result)
				{
					tcs.SetResult(result);
				});
			}
			else
			{
				tcs.SetResult(StorageResult.InvalidPermissions);
			}
			return tcs.Task;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000971BC File Offset: 0x000953BC
		public static StorageResult WriteAllText(string path, string content)
		{
			return Blob.WriteAllTextAsync(path, content).AwaitResult<StorageResult>();
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000971CC File Offset: 0x000953CC
		public static Task<StorageResult> WriteAllTextAsync(string path, string content)
		{
			TaskCompletionSource<StorageResult> tcs = new TaskCompletionSource<StorageResult>();
			if (!Path.IsROM(path))
			{
				State.GetStorage().SaveBlob(path, content, delegate(StorageResult result)
				{
					tcs.SetResult(result);
				});
			}
			else
			{
				tcs.SetResult(StorageResult.InvalidPermissions);
			}
			return tcs.Task;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00097224 File Offset: 0x00095424
		public static Task<LoadBytesResult> ReadAllBytesAsync(string path)
		{
			TaskCompletionSource<LoadBytesResult> tcs = new TaskCompletionSource<LoadBytesResult>();
			if (Path.IsROM(path))
			{
				File.ReadAllBytesAsync(path, default(CancellationToken)).ContinueWith(delegate(Task<byte[]> task)
				{
					if (task.IsCompletedSuccessfully)
					{
						tcs.SetResult(new LoadBytesResult
						{
							result = StorageResult.Success,
							path = path,
							content = task.Result
						});
						return;
					}
					tcs.SetResult(new LoadBytesResult
					{
						result = task.Exception.ExceptionToStorageResult(),
						path = path,
						content = null
					});
				});
			}
			else
			{
				State.GetStorage().LoadBlob(path, delegate(StorageResult result, byte[] content)
				{
					tcs.SetResult(new LoadBytesResult
					{
						result = result,
						path = path,
						content = content
					});
				});
			}
			return tcs.Task;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000972A5 File Offset: 0x000954A5
		public static LoadBytesResult ReadAllBytes(string path)
		{
			return Blob.ReadAllBytesAsync(path).AwaitResult<LoadBytesResult>();
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000972B2 File Offset: 0x000954B2
		public static LoadTextResult ReadAllText(string path)
		{
			return Blob.ReadAllTextAsync(path).AwaitResult<LoadTextResult>();
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x000972C0 File Offset: 0x000954C0
		public static Task<LoadTextResult> ReadAllTextAsync(string path)
		{
			TaskCompletionSource<LoadTextResult> tcs = new TaskCompletionSource<LoadTextResult>();
			if (Path.IsROM(path))
			{
				File.ReadAllTextAsync(path, default(CancellationToken)).ContinueWith(delegate(Task<string> task)
				{
					if (task.IsCompletedSuccessfully)
					{
						tcs.SetResult(new LoadTextResult
						{
							result = StorageResult.Success,
							path = path,
							content = task.Result
						});
						return;
					}
					tcs.SetResult(new LoadTextResult
					{
						result = task.Exception.ExceptionToStorageResult(),
						path = path,
						content = null
					});
				});
			}
			else
			{
				State.GetStorage().LoadBlob(path, delegate(StorageResult result, string content)
				{
					tcs.SetResult(new LoadTextResult
					{
						result = result,
						path = path,
						content = content
					});
				});
			}
			return tcs.Task;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00097341 File Offset: 0x00095541
		[NullableContext(0)]
		public static ObjectLoadResult<FileMetadata> ReadMetadata([Nullable(1)] string path)
		{
			return Blob.FileInfoAsync(path).AwaitResult<ObjectLoadResult<FileMetadata>>();
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00097350 File Offset: 0x00095550
		[return: Nullable(new byte[] { 1, 0 })]
		public static Task<ObjectLoadResult<FileMetadata>> FileInfoAsync(string path)
		{
			TaskCompletionSource<ObjectLoadResult<FileMetadata>> tcs = new TaskCompletionSource<ObjectLoadResult<FileMetadata>>();
			if (Path.IsROM(path))
			{
				try
				{
					FileInfo fileInfo = new FileInfo(path);
					FileMetadata fileMetadata = default(FileMetadata);
					fileMetadata.SizeInBytes = fileInfo.Length;
					tcs.SetResult(ObjectLoadResult<FileMetadata>.CreateSuccess(fileMetadata));
				}
				catch (Exception ex)
				{
					ObjectLoadResult<FileMetadata> objectLoadResult = ObjectLoadResult<FileMetadata>.CreateError(ex.ExceptionToStorageResult(), "Could not get file info from ROM path");
					tcs.SetResult(objectLoadResult);
				}
				return tcs.Task;
			}
			State.GetStorage().FileMetadata(path, delegate(StorageResult result, FileMetadata data)
			{
				ObjectLoadResult<FileMetadata> objectLoadResult2 = new ObjectLoadResult<FileMetadata>(new PlatformIOResult(result, null), data);
				tcs.SetResult(objectLoadResult2);
			});
			return tcs.Task;
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00097404 File Offset: 0x00095604
		public static StorageResult Exists(string path)
		{
			return Blob.ExistsAsync(path).AwaitResult<StorageResult>();
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00097414 File Offset: 0x00095614
		public static Task<StorageResult> ExistsAsync(string path)
		{
			TaskCompletionSource<StorageResult> tcs = new TaskCompletionSource<StorageResult>();
			if (Path.IsROM(path))
			{
				if (File.Exists(path))
				{
					tcs.SetResult(StorageResult.Success);
				}
				else
				{
					tcs.SetResult(StorageResult.FileNotFound);
				}
			}
			else
			{
				State.GetStorage().FileExists(path, delegate(StorageResult result)
				{
					tcs.SetResult(result);
				});
			}
			return tcs.Task;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00097480 File Offset: 0x00095680
		public static StorageResult Delete(string path)
		{
			return Blob.DeleteAsync(path).AwaitResult<StorageResult>();
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x00097490 File Offset: 0x00095690
		public static async Task<StorageResult> DeleteAsync(string path)
		{
			StorageResult storageResult;
			if (Path.IsROM(path))
			{
				storageResult = StorageResult.InvalidPermissions;
			}
			else
			{
				TaskAwaiter<StorageResult> taskAwaiter = Blob.ExistsAsync(path).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<StorageResult> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<StorageResult>);
				}
				if (!taskAwaiter.GetResult().WasSuccessful())
				{
					storageResult = StorageResult.Success;
				}
				else
				{
					TaskCompletionSource<StorageResult> tcs = new TaskCompletionSource<StorageResult>();
					State.GetStorage().DeleteBlob(path, delegate(StorageResult result)
					{
						tcs.SetResult(result);
					});
					storageResult = await tcs.Task;
				}
			}
			return storageResult;
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x000974D4 File Offset: 0x000956D4
		public static Task<PlatformIOResult> CopyAsync(string sourceFileName, string destFileName, bool overwrite)
		{
			Blob.<CopyAsync>d__14 <CopyAsync>d__;
			<CopyAsync>d__.<>t__builder = AsyncTaskMethodBuilder<PlatformIOResult>.Create();
			<CopyAsync>d__.sourceFileName = sourceFileName;
			<CopyAsync>d__.destFileName = destFileName;
			<CopyAsync>d__.overwrite = overwrite;
			<CopyAsync>d__.<>1__state = -1;
			<CopyAsync>d__.<>t__builder.Start<Blob.<CopyAsync>d__14>(ref <CopyAsync>d__);
			return <CopyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00097527 File Offset: 0x00095727
		public static PlatformIOResult Copy(string sourceFileName, string destFileName, bool overwrite)
		{
			return Blob.CopyAsync(sourceFileName, destFileName, overwrite).AwaitResult<PlatformIOResult>();
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00097538 File Offset: 0x00095738
		public static async Task<PlatformIOResult> MoveAsync(string sourceFileName, string destFileName, bool overwrite = false)
		{
			bool flag = !overwrite;
			if (flag)
			{
				flag = (await Blob.ExistsAsync(destFileName)).WasSuccessful();
			}
			PlatformIOResult platformIOResult;
			if (flag)
			{
				platformIOResult = new PlatformIOResult(StorageResult.OperationCancelled, string.Format("Can't move because {0} already exists and {1} is {2}", destFileName, "overwrite", overwrite));
			}
			else
			{
				PlatformIOResult platformIOResult2 = await Blob.CopyAsync(sourceFileName, destFileName, overwrite);
				if (!platformIOResult2.WasSuccessful())
				{
					platformIOResult = platformIOResult2;
				}
				else
				{
					StorageResult storageResult = await Blob.DeleteAsync(sourceFileName);
					if (!storageResult.WasSuccessful())
					{
						platformIOResult = new PlatformIOResult(storageResult, "Error deleting " + sourceFileName);
					}
					else
					{
						platformIOResult = new PlatformIOResult(StorageResult.Success, null);
					}
				}
			}
			return platformIOResult;
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x0009758B File Offset: 0x0009578B
		public static PlatformIOResult Move(string sourceFileName, string destFileName, bool overwrite = false)
		{
			return Blob.MoveAsync(sourceFileName, destFileName, overwrite).AwaitResult<PlatformIOResult>();
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x0009759A File Offset: 0x0009579A
		public static PlatformIOResult WriteAllJson(string path, object content, [Nullable(2)] JsonSerializerSettings settings = null)
		{
			return Blob.WriteAllJsonAsync(path, content, settings).AwaitResult<PlatformIOResult>();
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x000975AC File Offset: 0x000957AC
		public static async Task<PlatformIOResult> WriteAllJsonAsync(string path, object content, [Nullable(2)] JsonSerializerSettings settings = null)
		{
			if (settings == null)
			{
				settings = new JsonSerializerSettings();
			}
			string text;
			try
			{
				text = JsonConvert.SerializeObject(content, settings);
			}
			catch (Exception)
			{
				return new PlatformIOResult(StorageResult.UnknownFailure, "Error deserializes file");
			}
			StorageResult storageResult = await Blob.WriteAllTextAsync(path, text);
			PlatformIOResult platformIOResult;
			if (!storageResult.WasSuccessful())
			{
				platformIOResult = new PlatformIOResult(storageResult, "Error Saving Json File at " + path);
			}
			else
			{
				platformIOResult = new PlatformIOResult(storageResult, null);
			}
			return platformIOResult;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00097600 File Offset: 0x00095800
		[NullableContext(2)]
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public static async Task<ObjectLoadResult<T>> ReadJsonAsync<T>([Nullable(1)] string path, JsonSerializerSettings settings = null)
		{
			if (settings == null)
			{
				settings = new JsonSerializerSettings();
			}
			LoadTextResult loadTextResult = await Blob.ReadAllTextAsync(path);
			ObjectLoadResult<T> objectLoadResult;
			if (!loadTextResult.result.WasSuccessful())
			{
				objectLoadResult = ObjectLoadResult<T>.CreateError(loadTextResult.result, null);
			}
			else
			{
				try
				{
					objectLoadResult = ObjectLoadResult<T>.CreateSuccess(JsonConvert.DeserializeObject<T>(loadTextResult.content, settings));
				}
				catch (Exception ex)
				{
					objectLoadResult = ObjectLoadResult<T>.CreateError(StorageResult.UnknownFailure, string.Format("Error from {0}: {1}", "DeserializeObject", ex));
				}
			}
			return objectLoadResult;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0009764B File Offset: 0x0009584B
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ObjectLoadResult<T> ReadAllJson<T>([Nullable(1)] string path, JsonSerializerSettings settings = null)
		{
			return Blob.ReadJsonAsync<T>(path, settings).AwaitResult<ObjectLoadResult<T>>();
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0009765C File Offset: 0x0009585C
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public static Task<ObjectLoadResult<Stream>> OpenAsync(string path, FileMode mode, FileAccess access)
		{
			TaskCompletionSource<ObjectLoadResult<Stream>> tcs = new TaskCompletionSource<ObjectLoadResult<Stream>>();
			if (Path.IsROM(path))
			{
				if (mode == FileMode.OpenOrCreate || access == FileAccess.ReadWrite)
				{
					tcs.SetResult(ObjectLoadResult<Stream>.CreateError(StorageResult.InvalidPermissions, string.Format("Can't {0} path {1} because it is ROM. {2} is {3} and {4} is {5}.", new object[] { "OpenAsync", path, "mode", mode, "access", access })));
					return tcs.Task;
				}
				try
				{
					FileStream fileStream = File.Open(path, mode.ToSystemIOCounterpart(), access.ToSystemIOCounterpart());
					tcs.SetResult(ObjectLoadResult<Stream>.CreateSuccess(fileStream));
					return tcs.Task;
				}
				catch (Exception ex)
				{
					StorageResult storageResult = ex.ExceptionToStorageResult();
					tcs.SetResult(ObjectLoadResult<Stream>.CreateError(storageResult, ex.Message));
					return tcs.Task;
				}
			}
			bool flag = Blob.Exists(path).WasSuccessful();
			if (mode == FileMode.Open && !flag)
			{
				tcs.SetResult(new ObjectLoadResult<Stream>(new PlatformIOResult(StorageResult.FileNotFound, "File didn't exist when opening up stream, but filemode is not set to create"), null));
				return tcs.Task;
			}
			if (mode == FileMode.OpenOrCreate && !flag)
			{
				StorageResult storageResult2 = Blob.WriteAllBytes(path, Array.Empty<byte>());
				if (!storageResult2.WasSuccessful())
				{
					tcs.SetResult(new ObjectLoadResult<Stream>(new PlatformIOResult(storageResult2, "Could not create file when opening up stream"), null));
					return tcs.Task;
				}
			}
			State.GetStorage().OpenStream(path, mode, access, delegate(StorageResult result, Stream stream)
			{
				tcs.SetResult(new ObjectLoadResult<Stream>(new PlatformIOResult(result, null), stream));
			});
			return tcs.Task;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00097810 File Offset: 0x00095A10
		[return: Nullable(new byte[] { 0, 1 })]
		public static ObjectLoadResult<Stream> Open(string path, FileMode mode, FileAccess access, FileShare fileShare)
		{
			if (access == FileAccess.ReadWrite)
			{
				return ObjectLoadResult<Stream>.CreateError(StorageResult.InvalidPermissions, "Stream with Write enabled Not Implemented");
			}
			return Blob.OpenAsync(path, mode, access).Result;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00097830 File Offset: 0x00095A30
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public static Task<ObjectLoadResult<Stream>> OpenWriteAsync(string path)
		{
			return Blob.OpenAsync(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0009783A File Offset: 0x00095A3A
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public static Task<ObjectLoadResult<Stream>> OpenWriteAsync(string path, FileMode mode, FileAccess fileAccess)
		{
			return Blob.OpenAsync(path, mode, fileAccess);
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00097844 File Offset: 0x00095A44
		[return: Nullable(new byte[] { 0, 1 })]
		public static ObjectLoadResult<Stream> OpenWrite(string path)
		{
			return Blob.OpenWriteAsync(path).Result;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00097851 File Offset: 0x00095A51
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public static Task<ObjectLoadResult<Stream>> OpenReadAsync(string path)
		{
			return Blob.OpenAsync(path, FileMode.Open, FileAccess.Read);
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0009785B File Offset: 0x00095A5B
		[return: Nullable(new byte[] { 0, 1 })]
		public static ObjectLoadResult<Stream> OpenRead(string path)
		{
			return Blob.OpenReadAsync(path).Result;
		}
	}
}
