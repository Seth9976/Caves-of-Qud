using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000BE RID: 190
	public class ICloudStorage : IDisposable
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x00016F75 File Offset: 0x00015175
		internal ICloudStorage(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00016F91 File Offset: 0x00015191
		internal static HandleRef getCPtr(ICloudStorage obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00016FB0 File Offset: 0x000151B0
		~ICloudStorage()
		{
			this.Dispose();
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00016FE0 File Offset: 0x000151E0
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICloudStorage(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00017060 File Offset: 0x00015260
		public virtual void GetFileList(string container, ICloudStorageGetFileListListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_GetFileList(this.swigCPtr, container, ICloudStorageGetFileListListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00017084 File Offset: 0x00015284
		public virtual string GetFileNameByIndex(uint index)
		{
			string text = GalaxyInstancePINVOKE.ICloudStorage_GetFileNameByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000170B0 File Offset: 0x000152B0
		public virtual uint GetFileSizeByIndex(uint index)
		{
			uint num = GalaxyInstancePINVOKE.ICloudStorage_GetFileSizeByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000170DC File Offset: 0x000152DC
		public virtual uint GetFileTimestampByIndex(uint index)
		{
			uint num = GalaxyInstancePINVOKE.ICloudStorage_GetFileTimestampByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00017108 File Offset: 0x00015308
		public virtual string GetFileHashByIndex(uint index)
		{
			string text = GalaxyInstancePINVOKE.ICloudStorage_GetFileHashByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00017133 File Offset: 0x00015333
		public virtual void GetFile(string container, string name, IntPtr userParam, SWIGTYPE_p_f_p_void_p_q_const__char_int__int writeFunc, ICloudStorageGetFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_GetFile__SWIG_0(this.swigCPtr, container, name, userParam, SWIGTYPE_p_f_p_void_p_q_const__char_int__int.getCPtr(writeFunc), ICloudStorageGetFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00017161 File Offset: 0x00015361
		public virtual void GetFile(string container, string name, byte[] dataBuffer, uint bufferLength, ICloudStorageGetFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_GetFile__SWIG_1(this.swigCPtr, container, name, dataBuffer, bufferLength, ICloudStorageGetFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001718A File Offset: 0x0001538A
		public virtual void GetFileMetadata(string container, string name, ICloudStorageGetFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_GetFileMetadata(this.swigCPtr, container, name, ICloudStorageGetFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000171B0 File Offset: 0x000153B0
		public virtual void PutFile(string container, string name, IntPtr userParam, SWIGTYPE_p_f_p_void_p_char_int__int readFunc, SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int rewindFunc, ICloudStoragePutFileListener listener, SavegameType savegameType, uint timeStamp, string hash)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_0(this.swigCPtr, container, name, userParam, SWIGTYPE_p_f_p_void_p_char_int__int.getCPtr(readFunc), SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int.getCPtr(rewindFunc), ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType, timeStamp, hash);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000171F8 File Offset: 0x000153F8
		public virtual void PutFile(string container, string name, IntPtr userParam, SWIGTYPE_p_f_p_void_p_char_int__int readFunc, SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int rewindFunc, ICloudStoragePutFileListener listener, SavegameType savegameType, uint timeStamp)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_1(this.swigCPtr, container, name, userParam, SWIGTYPE_p_f_p_void_p_char_int__int.getCPtr(readFunc), SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int.getCPtr(rewindFunc), ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType, timeStamp);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001723C File Offset: 0x0001543C
		public virtual void PutFile(string container, string name, IntPtr userParam, SWIGTYPE_p_f_p_void_p_char_int__int readFunc, SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int rewindFunc, ICloudStoragePutFileListener listener, SavegameType savegameType)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_2(this.swigCPtr, container, name, userParam, SWIGTYPE_p_f_p_void_p_char_int__int.getCPtr(readFunc), SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int.getCPtr(rewindFunc), ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00017273 File Offset: 0x00015473
		public virtual void PutFile(string container, string name, IntPtr userParam, SWIGTYPE_p_f_p_void_p_char_int__int readFunc, SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int rewindFunc, ICloudStoragePutFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_3(this.swigCPtr, container, name, userParam, SWIGTYPE_p_f_p_void_p_char_int__int.getCPtr(readFunc), SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int.getCPtr(rewindFunc), ICloudStoragePutFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000172A8 File Offset: 0x000154A8
		public virtual void PutFile(string container, string name, byte[] buffer, uint bufferLength, ICloudStoragePutFileListener listener, SavegameType savegameType, uint timeStamp, string hash)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_4(this.swigCPtr, container, name, buffer, bufferLength, ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType, timeStamp, hash);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000172E2 File Offset: 0x000154E2
		public virtual void PutFile(string container, string name, byte[] buffer, uint bufferLength, ICloudStoragePutFileListener listener, SavegameType savegameType, uint timeStamp)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_5(this.swigCPtr, container, name, buffer, bufferLength, ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType, timeStamp);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001730F File Offset: 0x0001550F
		public virtual void PutFile(string container, string name, byte[] buffer, uint bufferLength, ICloudStoragePutFileListener listener, SavegameType savegameType)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_6(this.swigCPtr, container, name, buffer, bufferLength, ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001733A File Offset: 0x0001553A
		public virtual void PutFile(string container, string name, byte[] buffer, uint bufferLength, ICloudStoragePutFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_7(this.swigCPtr, container, name, buffer, bufferLength, ICloudStoragePutFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00017363 File Offset: 0x00015563
		public virtual void CalculateHash(IntPtr userParam, SWIGTYPE_p_f_p_void_p_char_int__int readFunc, SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int rewindFunc, ref byte[] hashBuffer, uint hashBufferSize)
		{
			GalaxyInstancePINVOKE.ICloudStorage_CalculateHash__SWIG_0(this.swigCPtr, userParam, SWIGTYPE_p_f_p_void_p_char_int__int.getCPtr(readFunc), SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int.getCPtr(rewindFunc), hashBuffer, hashBufferSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00017392 File Offset: 0x00015592
		public virtual void CalculateHash(byte[] buffer, uint bufferLength, ref byte[] hashBuffer, uint hashBufferSize)
		{
			GalaxyInstancePINVOKE.ICloudStorage_CalculateHash__SWIG_1(this.swigCPtr, buffer, bufferLength, hashBuffer, hashBufferSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000173B5 File Offset: 0x000155B5
		public virtual void DeleteFile(string container, string name, ICloudStorageDeleteFileListener listener, string expectedHash)
		{
			GalaxyInstancePINVOKE.ICloudStorage_DeleteFile__SWIG_0(this.swigCPtr, container, name, ICloudStorageDeleteFileListener.getCPtr(listener), expectedHash);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000173DC File Offset: 0x000155DC
		public virtual void DeleteFile(string container, string name, ICloudStorageDeleteFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_DeleteFile__SWIG_1(this.swigCPtr, container, name, ICloudStorageDeleteFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00017401 File Offset: 0x00015601
		public virtual void OpenSavegame()
		{
			GalaxyInstancePINVOKE.ICloudStorage_OpenSavegame(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001741E File Offset: 0x0001561E
		public virtual void CloseSavegame()
		{
			GalaxyInstancePINVOKE.ICloudStorage_CloseSavegame(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x04000106 RID: 262
		private HandleRef swigCPtr;

		// Token: 0x04000107 RID: 263
		protected bool swigCMemOwn;

		// Token: 0x04000108 RID: 264
		public static readonly uint MIN_HASH_BUFFER_SIZE = GalaxyInstancePINVOKE.ICloudStorage_MIN_HASH_BUFFER_SIZE_get();

		// Token: 0x020000BF RID: 191
		public enum ReadPhase
		{
			// Token: 0x0400010A RID: 266
			CHECKSUM_CALCULATING,
			// Token: 0x0400010B RID: 267
			UPLOADING
		}
	}
}
