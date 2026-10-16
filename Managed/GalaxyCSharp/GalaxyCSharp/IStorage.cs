using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x0200016A RID: 362
	public class IStorage : IDisposable
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x0001AF76 File Offset: 0x00019176
		internal IStorage(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0001AF92 File Offset: 0x00019192
		internal static HandleRef getCPtr(IStorage obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0001AFB0 File Offset: 0x000191B0
		~IStorage()
		{
			this.Dispose();
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0001AFE0 File Offset: 0x000191E0
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IStorage(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0001B060 File Offset: 0x00019260
		public virtual void FileWrite(string fileName, byte[] data, uint dataSize)
		{
			GalaxyInstancePINVOKE.IStorage_FileWrite(this.swigCPtr, fileName, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0001B080 File Offset: 0x00019280
		public virtual uint FileRead(string fileName, byte[] data, uint dataSize)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_FileRead(this.swigCPtr, fileName, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0001B0AD File Offset: 0x000192AD
		public virtual void FileDelete(string fileName)
		{
			GalaxyInstancePINVOKE.IStorage_FileDelete(this.swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0001B0CC File Offset: 0x000192CC
		public virtual bool FileExists(string fileName)
		{
			bool flag = GalaxyInstancePINVOKE.IStorage_FileExists(this.swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0001B0F8 File Offset: 0x000192F8
		public virtual uint GetFileSize(string fileName)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_GetFileSize(this.swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0001B124 File Offset: 0x00019324
		public virtual uint GetFileTimestamp(string fileName)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_GetFileTimestamp(this.swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0001B150 File Offset: 0x00019350
		public virtual uint GetFileCount()
		{
			uint num = GalaxyInstancePINVOKE.IStorage_GetFileCount(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0001B17C File Offset: 0x0001937C
		public virtual string GetFileNameByIndex(uint index)
		{
			string text = GalaxyInstancePINVOKE.IStorage_GetFileNameByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0001B1A8 File Offset: 0x000193A8
		public virtual void GetFileNameCopyByIndex(uint index, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStorage_GetFileNameCopyByIndex(this.swigCPtr, index, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0001B200 File Offset: 0x00019400
		public virtual void FileShare(string fileName, IFileShareListener listener)
		{
			GalaxyInstancePINVOKE.IStorage_FileShare__SWIG_0(this.swigCPtr, fileName, IFileShareListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0001B224 File Offset: 0x00019424
		public virtual void FileShare(string fileName)
		{
			GalaxyInstancePINVOKE.IStorage_FileShare__SWIG_1(this.swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0001B242 File Offset: 0x00019442
		public virtual void DownloadSharedFile(ulong sharedFileID, ISharedFileDownloadListener listener)
		{
			GalaxyInstancePINVOKE.IStorage_DownloadSharedFile__SWIG_0(this.swigCPtr, sharedFileID, ISharedFileDownloadListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0001B266 File Offset: 0x00019466
		public virtual void DownloadSharedFile(ulong sharedFileID)
		{
			GalaxyInstancePINVOKE.IStorage_DownloadSharedFile__SWIG_1(this.swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0001B284 File Offset: 0x00019484
		public virtual string GetSharedFileName(ulong sharedFileID)
		{
			string text = GalaxyInstancePINVOKE.IStorage_GetSharedFileName(this.swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0001B2B0 File Offset: 0x000194B0
		public virtual void GetSharedFileNameCopy(ulong sharedFileID, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStorage_GetSharedFileNameCopy(this.swigCPtr, sharedFileID, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0001B308 File Offset: 0x00019508
		public virtual uint GetSharedFileSize(ulong sharedFileID)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_GetSharedFileSize(this.swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0001B334 File Offset: 0x00019534
		public virtual GalaxyID GetSharedFileOwner(ulong sharedFileID)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IStorage_GetSharedFileOwner(this.swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID galaxyID = null;
			if (intPtr != IntPtr.Zero)
			{
				galaxyID = new GalaxyID(intPtr, true);
			}
			return galaxyID;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0001B37C File Offset: 0x0001957C
		public virtual uint SharedFileRead(ulong sharedFileID, byte[] data, uint dataSize, uint offset)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_SharedFileRead__SWIG_0(this.swigCPtr, sharedFileID, data, dataSize, offset);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0001B3AC File Offset: 0x000195AC
		public virtual uint SharedFileRead(ulong sharedFileID, byte[] data, uint dataSize)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_SharedFileRead__SWIG_1(this.swigCPtr, sharedFileID, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0001B3D9 File Offset: 0x000195D9
		public virtual void SharedFileClose(ulong sharedFileID)
		{
			GalaxyInstancePINVOKE.IStorage_SharedFileClose(this.swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0001B3F8 File Offset: 0x000195F8
		public virtual uint GetDownloadedSharedFileCount()
		{
			uint num = GalaxyInstancePINVOKE.IStorage_GetDownloadedSharedFileCount(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0001B424 File Offset: 0x00019624
		public virtual ulong GetDownloadedSharedFileByIndex(uint index)
		{
			ulong num = GalaxyInstancePINVOKE.IStorage_GetDownloadedSharedFileByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x040002B0 RID: 688
		private HandleRef swigCPtr;

		// Token: 0x040002B1 RID: 689
		protected bool swigCMemOwn;
	}
}
