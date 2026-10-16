using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine.Networking
{
	// Token: 0x02000014 RID: 20
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequest.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UnityWebRequest : IDisposable
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003DEC File Offset: 0x00001FEC
		[NativeMethod(IsThreadSafe = true)]
		[NativeConditional("ENABLE_UNITYWEBREQUEST")]
		private static string GetWebErrorString(UnityWebRequest.UnityWebRequestError err)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				UnityWebRequest.GetWebErrorString_Injected(err, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003E1C File Offset: 0x0000201C
		[VisibleToOtherModules]
		internal static string GetHTTPStatusString(long responseCode)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				UnityWebRequest.GetHTTPStatusString_Injected(responseCode, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003E4C File Offset: 0x0000204C
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003E54 File Offset: 0x00002054
		public bool disposeCertificateHandlerOnDispose { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003E5D File Offset: 0x0000205D
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003E65 File Offset: 0x00002065
		public bool disposeDownloadHandlerOnDispose { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003E6E File Offset: 0x0000206E
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003E76 File Offset: 0x00002076
		public bool disposeUploadHandlerOnDispose { get; set; }

		// Token: 0x0600009A RID: 154 RVA: 0x00003E7F File Offset: 0x0000207F
		public static void ClearCookieCache()
		{
			UnityWebRequest.ClearCookieCache(null, null);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003E8C File Offset: 0x0000208C
		public static void ClearCookieCache(Uri uri)
		{
			bool flag = uri == null;
			if (flag)
			{
				UnityWebRequest.ClearCookieCache(null, null);
			}
			else
			{
				string host = uri.Host;
				string text = uri.AbsolutePath;
				bool flag2 = text == "/";
				if (flag2)
				{
					text = null;
				}
				UnityWebRequest.ClearCookieCache(host, text);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003ED8 File Offset: 0x000020D8
		private unsafe static void ClearCookieCache(string domain, string path)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(domain, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = domain.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = path.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				UnityWebRequest.ClearCookieCache_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x0600009D RID: 157
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Create();

		// Token: 0x0600009E RID: 158 RVA: 0x00003F60 File Offset: 0x00002160
		[NativeMethod(IsThreadSafe = true)]
		private void Release()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			UnityWebRequest.Release_Injected(intPtr);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003F84 File Offset: 0x00002184
		internal void InternalDestroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Abort();
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003FC1 File Offset: 0x000021C1
		private void InternalSetDefaults()
		{
			this.disposeDownloadHandlerOnDispose = true;
			this.disposeUploadHandlerOnDispose = true;
			this.disposeCertificateHandlerOnDispose = true;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003FDC File Offset: 0x000021DC
		public UnityWebRequest()
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003FF8 File Offset: 0x000021F8
		public UnityWebRequest(string url)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000401C File Offset: 0x0000221C
		public UnityWebRequest(Uri uri)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004040 File Offset: 0x00002240
		public UnityWebRequest(string url, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000406C File Offset: 0x0000226C
		public UnityWebRequest(Uri uri, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004098 File Offset: 0x00002298
		public UnityWebRequest(string url, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000040D5 File Offset: 0x000022D5
		public UnityWebRequest(Uri uri, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004114 File Offset: 0x00002314
		~UnityWebRequest()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000414C File Offset: 0x0000234C
		public void Dispose()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004164 File Offset: 0x00002364
		private void DisposeHandlers()
		{
			bool disposeDownloadHandlerOnDispose = this.disposeDownloadHandlerOnDispose;
			if (disposeDownloadHandlerOnDispose)
			{
				DownloadHandler downloadHandler = this.downloadHandler;
				bool flag = downloadHandler != null;
				if (flag)
				{
					downloadHandler.Dispose();
				}
			}
			bool disposeUploadHandlerOnDispose = this.disposeUploadHandlerOnDispose;
			if (disposeUploadHandlerOnDispose)
			{
				UploadHandler uploadHandler = this.uploadHandler;
				bool flag2 = uploadHandler != null;
				if (flag2)
				{
					uploadHandler.Dispose();
				}
			}
			bool disposeCertificateHandlerOnDispose = this.disposeCertificateHandlerOnDispose;
			if (disposeCertificateHandlerOnDispose)
			{
				CertificateHandler certificateHandler = this.certificateHandler;
				bool flag3 = certificateHandler != null;
				if (flag3)
				{
					certificateHandler.Dispose();
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000041EC File Offset: 0x000023EC
		[NativeThrows]
		internal UnityWebRequestAsyncOperation BeginWebRequest()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = UnityWebRequest.BeginWebRequest_Injected(intPtr);
			return (intPtr2 == 0) ? null : UnityWebRequestAsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr2);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000421C File Offset: 0x0000241C
		[Obsolete("Use SendWebRequest.  It returns a UnityWebRequestAsyncOperation which contains a reference to the WebRequest object.", false)]
		public AsyncOperation Send()
		{
			return this.SendWebRequest();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004234 File Offset: 0x00002434
		public UnityWebRequestAsyncOperation SendWebRequest()
		{
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = this.BeginWebRequest();
			bool flag = unityWebRequestAsyncOperation != null;
			if (flag)
			{
				unityWebRequestAsyncOperation.webRequest = this;
			}
			return unityWebRequestAsyncOperation;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004260 File Offset: 0x00002460
		[NativeMethod(IsThreadSafe = true)]
		public void Abort()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			UnityWebRequest.Abort_Injected(intPtr);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004284 File Offset: 0x00002484
		private UnityWebRequest.UnityWebRequestError SetMethod(UnityWebRequest.UnityWebRequestMethod methodType)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetMethod_Injected(intPtr, methodType);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000042A8 File Offset: 0x000024A8
		internal void InternalSetMethod(UnityWebRequest.UnityWebRequestMethod methodType)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetMethod(methodType);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000042EC File Offset: 0x000024EC
		private unsafe UnityWebRequest.UnityWebRequestError SetCustomMethod(string customMethodName)
		{
			UnityWebRequest.UnityWebRequestError unityWebRequestError;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(customMethodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = customMethodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				unityWebRequestError = UnityWebRequest.SetCustomMethod_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return unityWebRequestError;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004354 File Offset: 0x00002554
		internal void InternalSetCustomMethod(string customMethodName)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetCustomMethod(customMethodName);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004398 File Offset: 0x00002598
		internal UnityWebRequest.UnityWebRequestMethod GetMethod()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetMethod_Injected(intPtr);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000043BC File Offset: 0x000025BC
		internal string GetCustomMethod()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				UnityWebRequest.GetCustomMethod_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000043FC File Offset: 0x000025FC
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004458 File Offset: 0x00002658
		public string method
		{
			get
			{
				string text;
				switch (this.GetMethod())
				{
				case UnityWebRequest.UnityWebRequestMethod.Get:
					text = "GET";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Post:
					text = "POST";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Put:
					text = "PUT";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Head:
					text = "HEAD";
					break;
				default:
					text = this.GetCustomMethod();
					break;
				}
				return text;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					throw new ArgumentException("Cannot set a UnityWebRequest's method to an empty or null string");
				}
				string text = value.ToUpper();
				string text2 = text;
				if (!(text2 == "GET"))
				{
					if (!(text2 == "POST"))
					{
						if (!(text2 == "PUT"))
						{
							if (!(text2 == "HEAD"))
							{
								this.InternalSetCustomMethod(value.ToUpper());
							}
							else
							{
								this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Head);
							}
						}
						else
						{
							this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Put);
						}
					}
					else
					{
						this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Post);
					}
				}
				else
				{
					this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Get);
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000044F4 File Offset: 0x000026F4
		private UnityWebRequest.UnityWebRequestError GetError()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetError_Injected(intPtr);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004518 File Offset: 0x00002718
		public string error
		{
			get
			{
				UnityWebRequest.Result result = this.result;
				UnityWebRequest.Result result2 = result;
				string text;
				if (result2 > UnityWebRequest.Result.Success)
				{
					if (result2 != UnityWebRequest.Result.ProtocolError)
					{
						text = UnityWebRequest.GetWebErrorString(this.GetError());
					}
					else
					{
						text = string.Format("HTTP/1.1 {0} {1}", this.responseCode, UnityWebRequest.GetHTTPStatusString(this.responseCode));
					}
				}
				else
				{
					text = null;
				}
				return text;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004574 File Offset: 0x00002774
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004598 File Offset: 0x00002798
		private bool use100Continue
		{
			get
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return UnityWebRequest.get_use100Continue_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				UnityWebRequest.set_use100Continue_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000045BC File Offset: 0x000027BC
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000045D4 File Offset: 0x000027D4
		public bool useHttpContinue
		{
			get
			{
				return this.use100Continue;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its 100-Continue setting cannot be altered");
				}
				this.use100Continue = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004604 File Offset: 0x00002804
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000461C File Offset: 0x0000281C
		public string url
		{
			get
			{
				return this.GetUrl();
			}
			set
			{
				string text = "https://localhost/";
				this.InternalSetUrl(WebRequestUtils.MakeInitialUrl(value, text));
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004640 File Offset: 0x00002840
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004660 File Offset: 0x00002860
		public Uri uri
		{
			get
			{
				return new Uri(this.GetUrl());
			}
			set
			{
				bool flag = !value.IsAbsoluteUri;
				if (flag)
				{
					throw new ArgumentException("URI must be absolute");
				}
				this.InternalSetUrl(WebRequestUtils.MakeUriString(value, value.OriginalString, false));
				this.m_Uri = value;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000046A4 File Offset: 0x000028A4
		private string GetUrl()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				UnityWebRequest.GetUrl_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000046E4 File Offset: 0x000028E4
		private unsafe UnityWebRequest.UnityWebRequestError SetUrl(string url)
		{
			UnityWebRequest.UnityWebRequestError unityWebRequestError;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(url, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = url.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				unityWebRequestError = UnityWebRequest.SetUrl_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return unityWebRequestError;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000474C File Offset: 0x0000294C
		private void InternalSetUrl(string url)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its URL cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetUrl(url);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004790 File Offset: 0x00002990
		public long responseCode
		{
			get
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return UnityWebRequest.get_responseCode_Injected(intPtr);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000047B4 File Offset: 0x000029B4
		private float GetUploadProgress()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetUploadProgress_Injected(intPtr);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000047D8 File Offset: 0x000029D8
		private bool IsExecuting()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.IsExecuting_Injected(intPtr);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000047FC File Offset: 0x000029FC
		public float uploadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float num;
				if (flag)
				{
					num = -1f;
				}
				else
				{
					num = this.GetUploadProgress();
				}
				return num;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004834 File Offset: 0x00002A34
		public bool isModifiable
		{
			[NativeMethod("IsModifiable")]
			get
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return UnityWebRequest.get_isModifiable_Injected(intPtr);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004858 File Offset: 0x00002A58
		public bool isDone
		{
			get
			{
				return this.result > UnityWebRequest.Result.InProgress;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004874 File Offset: 0x00002A74
		[Obsolete("UnityWebRequest.isNetworkError is deprecated. Use (UnityWebRequest.result == UnityWebRequest.Result.ConnectionError) instead.", false)]
		public bool isNetworkError
		{
			get
			{
				return this.result == UnityWebRequest.Result.ConnectionError;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004890 File Offset: 0x00002A90
		[Obsolete("UnityWebRequest.isHttpError is deprecated. Use (UnityWebRequest.result == UnityWebRequest.Result.ProtocolError) instead.", false)]
		public bool isHttpError
		{
			get
			{
				return this.result == UnityWebRequest.Result.ProtocolError;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000048AC File Offset: 0x00002AAC
		public UnityWebRequest.Result result
		{
			[NativeMethod("GetResult")]
			get
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return UnityWebRequest.get_result_Injected(intPtr);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000048D0 File Offset: 0x00002AD0
		private float GetDownloadProgress()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetDownloadProgress_Injected(intPtr);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000048F4 File Offset: 0x00002AF4
		public float downloadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float num;
				if (flag)
				{
					num = -1f;
				}
				else
				{
					num = this.GetDownloadProgress();
				}
				return num;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000492C File Offset: 0x00002B2C
		public ulong uploadedBytes
		{
			get
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return UnityWebRequest.get_uploadedBytes_Injected(intPtr);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004950 File Offset: 0x00002B50
		public ulong downloadedBytes
		{
			get
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return UnityWebRequest.get_downloadedBytes_Injected(intPtr);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004974 File Offset: 0x00002B74
		private int GetRedirectLimit()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetRedirectLimit_Injected(intPtr);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004998 File Offset: 0x00002B98
		[NativeThrows]
		private void SetRedirectLimitFromScripting(int limit)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			UnityWebRequest.SetRedirectLimitFromScripting_Injected(intPtr, limit);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000049BC File Offset: 0x00002BBC
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000049D4 File Offset: 0x00002BD4
		public int redirectLimit
		{
			get
			{
				return this.GetRedirectLimit();
			}
			set
			{
				this.SetRedirectLimitFromScripting(value);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000049E0 File Offset: 0x00002BE0
		private bool GetChunked()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetChunked_Injected(intPtr);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004A04 File Offset: 0x00002C04
		private UnityWebRequest.UnityWebRequestError SetChunked(bool chunked)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetChunked_Injected(intPtr, chunked);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004A28 File Offset: 0x00002C28
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004A40 File Offset: 0x00002C40
		[Obsolete("HTTP/2 and many HTTP/1.1 servers don't support this; we recommend leaving it set to false (default).", false)]
		public bool chunkedTransfer
		{
			get
			{
				return this.GetChunked();
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its chunked transfer encoding setting cannot be altered");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetChunked(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004A84 File Offset: 0x00002C84
		public unsafe string GetRequestHeader(string name)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				UnityWebRequest.GetRequestHeader_Injected(intPtr, ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004AF4 File Offset: 0x00002CF4
		[NativeMethod("SetRequestHeader")]
		internal unsafe UnityWebRequest.UnityWebRequestError InternalSetRequestHeader(string name, string value)
		{
			UnityWebRequest.UnityWebRequestError unityWebRequestError;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = value.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				unityWebRequestError = UnityWebRequest.InternalSetRequestHeader_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return unityWebRequestError;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004B90 File Offset: 0x00002D90
		public void SetRequestHeader(string name, string value)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentException("Cannot set a Request Header with a null or empty name");
			}
			bool flag2 = value == null;
			if (flag2)
			{
				throw new ArgumentException("Cannot set a Request header with a null");
			}
			bool flag3 = !this.isModifiable;
			if (flag3)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request headers cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.InternalSetRequestHeader(name, value);
			bool flag4 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag4)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004C00 File Offset: 0x00002E00
		public unsafe string GetResponseHeader(string name)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				UnityWebRequest.GetResponseHeader_Injected(intPtr, ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004C70 File Offset: 0x00002E70
		internal string[] GetResponseHeaderKeys()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetResponseHeaderKeys_Injected(intPtr);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004C94 File Offset: 0x00002E94
		public Dictionary<string, string> GetResponseHeaders()
		{
			string[] responseHeaderKeys = this.GetResponseHeaderKeys();
			bool flag = responseHeaderKeys == null || responseHeaderKeys.Length == 0;
			Dictionary<string, string> dictionary;
			if (flag)
			{
				dictionary = null;
			}
			else
			{
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>(responseHeaderKeys.Length, StringComparer.OrdinalIgnoreCase);
				for (int i = 0; i < responseHeaderKeys.Length; i++)
				{
					string responseHeader = this.GetResponseHeader(responseHeaderKeys[i]);
					dictionary2.Add(responseHeaderKeys[i], responseHeader);
				}
				dictionary = dictionary2;
			}
			return dictionary;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004D04 File Offset: 0x00002F04
		private UnityWebRequest.UnityWebRequestError SetUploadHandler(UploadHandler uh)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetUploadHandler_Injected(intPtr, (uh == null) ? ((IntPtr)0) : UploadHandler.BindingsMarshaller.ConvertToNative(uh));
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004D38 File Offset: 0x00002F38
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004D50 File Offset: 0x00002F50
		public UploadHandler uploadHandler
		{
			get
			{
				return this.m_UploadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the upload handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetUploadHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_UploadHandler = value;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004D9C File Offset: 0x00002F9C
		private UnityWebRequest.UnityWebRequestError SetDownloadHandler(DownloadHandler dh)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetDownloadHandler_Injected(intPtr, (dh == null) ? ((IntPtr)0) : DownloadHandler.BindingsMarshaller.ConvertToNative(dh));
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004DD0 File Offset: 0x00002FD0
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public DownloadHandler downloadHandler
		{
			get
			{
				return this.m_DownloadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the download handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetDownloadHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_DownloadHandler = value;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004E34 File Offset: 0x00003034
		private UnityWebRequest.UnityWebRequestError SetCertificateHandler(CertificateHandler ch)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetCertificateHandler_Injected(intPtr, (ch == null) ? ((IntPtr)0) : CertificateHandler.BindingsMarshaller.ConvertToNative(ch));
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004E68 File Offset: 0x00003068
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004E80 File Offset: 0x00003080
		public CertificateHandler certificateHandler
		{
			get
			{
				return this.m_CertificateHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the certificate handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetCertificateHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_CertificateHandler = value;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004ECC File Offset: 0x000030CC
		private int GetTimeoutMsec()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetTimeoutMsec_Injected(intPtr);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004EF0 File Offset: 0x000030F0
		private UnityWebRequest.UnityWebRequestError SetTimeoutMsec(int timeout)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetTimeoutMsec_Injected(intPtr, timeout);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004F14 File Offset: 0x00003114
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00004F34 File Offset: 0x00003134
		public int timeout
		{
			get
			{
				return this.GetTimeoutMsec() / 1000;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				value = Math.Max(value, 0);
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetTimeoutMsec(value * 1000);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004F88 File Offset: 0x00003188
		private bool GetSuppressErrorsToConsole()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetSuppressErrorsToConsole_Injected(intPtr);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004FAC File Offset: 0x000031AC
		private UnityWebRequest.UnityWebRequestError SetSuppressErrorsToConsole(bool suppress)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetSuppressErrorsToConsole_Injected(intPtr, suppress);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004FD0 File Offset: 0x000031D0
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004FE8 File Offset: 0x000031E8
		internal bool suppressErrorsToConsole
		{
			get
			{
				return this.GetSuppressErrorsToConsole();
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetSuppressErrorsToConsole(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000502C File Offset: 0x0000322C
		public static UnityWebRequest Get(string uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005054 File Offset: 0x00003254
		public static UnityWebRequest Get(Uri uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000507C File Offset: 0x0000327C
		public static UnityWebRequest Delete(string uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000509C File Offset: 0x0000329C
		public static UnityWebRequest Delete(Uri uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000050BC File Offset: 0x000032BC
		public static UnityWebRequest Head(string uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000050DC File Offset: 0x000032DC
		public static UnityWebRequest Head(Uri uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000050FB File Offset: 0x000032FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture(string uri)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000050FB File Offset: 0x000032FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture(string uri, bool nonReadable)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005108 File Offset: 0x00003308
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAudioClip is obsolete. Use UnityWebRequestMultimedia.GetAudioClip instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestMultimedia.GetAudioClip(*)", true)]
		public static UnityWebRequest GetAudioClip(string uri, AudioType audioType)
		{
			return null;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000511C File Offset: 0x0000331C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri)
		{
			return null;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005130 File Offset: 0x00003330
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint crc)
		{
			return null;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005144 File Offset: 0x00003344
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint version, uint crc)
		{
			return null;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005158 File Offset: 0x00003358
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, Hash128 hash, uint crc)
		{
			return null;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000516C File Offset: 0x0000336C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, CachedAssetBundle cachedAssetBundle, uint crc)
		{
			return null;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005180 File Offset: 0x00003380
		public static UnityWebRequest Put(string uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000051AC File Offset: 0x000033AC
		public static UnityWebRequest Put(Uri uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000051D8 File Offset: 0x000033D8
		public static UnityWebRequest Put(string uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000520C File Offset: 0x0000340C
		public static UnityWebRequest Put(Uri uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005240 File Offset: 0x00003440
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.Post with only a string data is obsolete. Use UnityWebRequest.Post with content type argument or UnityWebRequest.PostWwwForm instead (UnityUpgradable) -> [UnityEngine] UnityWebRequest.PostWwwForm(*)", false)]
		public static UnityWebRequest Post(string uri, string postData)
		{
			return UnityWebRequest.PostWwwForm(uri, postData);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000525C File Offset: 0x0000345C
		[Obsolete("UnityWebRequest.Post with only a string data is obsolete. Use UnityWebRequest.Post with content type argument or UnityWebRequest.PostWwwForm instead (UnityUpgradable) -> [UnityEngine] UnityWebRequest.PostWwwForm(*)", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static UnityWebRequest Post(Uri uri, string postData)
		{
			return UnityWebRequest.PostWwwForm(uri, postData);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005278 File Offset: 0x00003478
		public static UnityWebRequest PostWwwForm(string uri, string form)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPostWwwForm(unityWebRequest, form);
			return unityWebRequest;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000052A0 File Offset: 0x000034A0
		public static UnityWebRequest PostWwwForm(Uri uri, string form)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPostWwwForm(unityWebRequest, form);
			return unityWebRequest;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000052C8 File Offset: 0x000034C8
		private static void SetupPostWwwForm(UnityWebRequest request, string postData)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag = string.IsNullOrEmpty(postData);
			if (!flag)
			{
				string text = WWWTranscoder.DataEncode(postData, Encoding.UTF8);
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				request.uploadHandler = new UploadHandlerRaw(bytes);
				request.uploadHandler.contentType = "application/x-www-form-urlencoded";
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005328 File Offset: 0x00003528
		public static UnityWebRequest Post(string uri, string postData, string contentType)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, postData, contentType);
			return unityWebRequest;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005350 File Offset: 0x00003550
		public static UnityWebRequest Post(Uri uri, string postData, string contentType)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, postData, contentType);
			return unityWebRequest;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005378 File Offset: 0x00003578
		private static void SetupPost(UnityWebRequest request, string postData, string contentType)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag = string.IsNullOrEmpty(postData);
			if (flag)
			{
				request.SetRequestHeader("Content-Type", contentType);
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes(postData);
				request.uploadHandler = new UploadHandlerRaw(bytes);
				request.uploadHandler.contentType = contentType;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000053D4 File Offset: 0x000035D4
		public static UnityWebRequest Post(string uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formData);
			return unityWebRequest;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000053FC File Offset: 0x000035FC
		public static UnityWebRequest Post(Uri uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formData);
			return unityWebRequest;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005424 File Offset: 0x00003624
		private static void SetupPost(UnityWebRequest request, WWWForm formData)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag = formData == null;
			if (!flag)
			{
				byte[] array = formData.data;
				bool flag2 = array.Length == 0;
				if (flag2)
				{
					array = null;
				}
				bool flag3 = array != null;
				if (flag3)
				{
					request.uploadHandler = new UploadHandlerRaw(array);
				}
				Dictionary<string, string> headers = formData.headers;
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					request.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000054D0 File Offset: 0x000036D0
		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] array = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, array);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000054F0 File Offset: 0x000036F0
		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] array = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, array);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005510 File Offset: 0x00003710
		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, multipartFormSections, boundary);
			return unityWebRequest;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005538 File Offset: 0x00003738
		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, multipartFormSections, boundary);
			return unityWebRequest;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005560 File Offset: 0x00003760
		private static void SetupPost(UnityWebRequest request, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			byte[] array = null;
			bool flag = multipartFormSections != null && multipartFormSections.Count != 0;
			if (flag)
			{
				array = UnityWebRequest.SerializeFormSections(multipartFormSections, boundary);
			}
			bool flag2 = array == null;
			if (!flag2)
			{
				request.uploadHandler = new UploadHandlerRaw(array)
				{
					contentType = "multipart/form-data; boundary=" + Encoding.UTF8.GetString(boundary, 0, boundary.Length)
				};
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000055D0 File Offset: 0x000037D0
		public static UnityWebRequest Post(string uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formFields);
			return unityWebRequest;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000055F8 File Offset: 0x000037F8
		public static UnityWebRequest Post(Uri uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formFields);
			return unityWebRequest;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005620 File Offset: 0x00003820
		private static void SetupPost(UnityWebRequest request, Dictionary<string, string> formFields)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			byte[] array = null;
			bool flag = formFields != null && formFields.Count != 0;
			if (flag)
			{
				array = UnityWebRequest.SerializeSimpleForm(formFields);
			}
			bool flag2 = array == null;
			if (!flag2)
			{
				request.uploadHandler = new UploadHandlerRaw(array)
				{
					contentType = "application/x-www-form-urlencoded"
				};
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000567C File Offset: 0x0000387C
		public static string EscapeURL(string s)
		{
			return UnityWebRequest.EscapeURL(s, Encoding.UTF8);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000569C File Offset: 0x0000389C
		public static string EscapeURL(string s, Encoding e)
		{
			bool flag = s == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				bool flag2 = s == "";
				if (flag2)
				{
					text = "";
				}
				else
				{
					bool flag3 = e == null;
					if (flag3)
					{
						text = null;
					}
					else
					{
						byte[] bytes = e.GetBytes(s);
						byte[] array = WWWTranscoder.URLEncode(bytes);
						text = e.GetString(array);
					}
				}
			}
			return text;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000056F8 File Offset: 0x000038F8
		public static string UnEscapeURL(string s)
		{
			return UnityWebRequest.UnEscapeURL(s, Encoding.UTF8);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005718 File Offset: 0x00003918
		public static string UnEscapeURL(string s, Encoding e)
		{
			bool flag = s == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				bool flag2 = s.IndexOf('%') == -1 && s.IndexOf('+') == -1;
				if (flag2)
				{
					text = s;
				}
				else
				{
					byte[] bytes = e.GetBytes(s);
					byte[] array = WWWTranscoder.URLDecode(bytes);
					text = e.GetString(array);
				}
			}
			return text;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005770 File Offset: 0x00003970
		public static byte[] SerializeFormSections(List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			bool flag = multipartFormSections == null || multipartFormSections.Count == 0;
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes("\r\n");
				byte[] bytes2 = WWWForm.DefaultEncoding.GetBytes("--");
				int num = 0;
				foreach (IMultipartFormSection multipartFormSection in multipartFormSections)
				{
					num += 64 + multipartFormSection.sectionData.Length;
				}
				List<byte> list = new List<byte>(num);
				foreach (IMultipartFormSection multipartFormSection2 in multipartFormSections)
				{
					string text = "form-data";
					string sectionName = multipartFormSection2.sectionName;
					string fileName = multipartFormSection2.fileName;
					string text2 = "Content-Disposition: " + text;
					bool flag2 = !string.IsNullOrEmpty(sectionName);
					if (flag2)
					{
						text2 = text2 + "; name=\"" + sectionName + "\"";
					}
					bool flag3 = !string.IsNullOrEmpty(fileName);
					if (flag3)
					{
						text2 = text2 + "; filename=\"" + fileName + "\"";
					}
					text2 += "\r\n";
					string contentType = multipartFormSection2.contentType;
					bool flag4 = !string.IsNullOrEmpty(contentType);
					if (flag4)
					{
						text2 = text2 + "Content-Type: " + contentType + "\r\n";
					}
					list.AddRange(bytes);
					list.AddRange(bytes2);
					list.AddRange(boundary);
					list.AddRange(bytes);
					list.AddRange(Encoding.UTF8.GetBytes(text2));
					list.AddRange(bytes);
					list.AddRange(multipartFormSection2.sectionData);
				}
				list.AddRange(bytes);
				list.AddRange(bytes2);
				list.AddRange(boundary);
				list.AddRange(bytes2);
				list.AddRange(bytes);
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000059A0 File Offset: 0x00003BA0
		public static byte[] GenerateBoundary()
		{
			byte[] array = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = Random.Range(48, 110);
				bool flag = num > 57;
				if (flag)
				{
					num += 7;
				}
				bool flag2 = num > 90;
				if (flag2)
				{
					num += 6;
				}
				array[i] = (byte)num;
			}
			return array;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005A00 File Offset: 0x00003C00
		public static byte[] SerializeSimpleForm(Dictionary<string, string> formFields)
		{
			string text = "";
			foreach (KeyValuePair<string, string> keyValuePair in formFields)
			{
				bool flag = text.Length > 0;
				if (flag)
				{
					text += "&";
				}
				text = text + WWWTranscoder.DataEncode(keyValuePair.Key) + "=" + WWWTranscoder.DataEncode(keyValuePair.Value);
			}
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x0600011C RID: 284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWebErrorString_Injected(UnityWebRequest.UnityWebRequestError err, out ManagedSpanWrapper ret);

		// Token: 0x0600011D RID: 285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHTTPStatusString_Injected(long responseCode, out ManagedSpanWrapper ret);

		// Token: 0x0600011E RID: 286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearCookieCache_Injected(ref ManagedSpanWrapper domain, ref ManagedSpanWrapper path);

		// Token: 0x0600011F RID: 287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Release_Injected(IntPtr _unity_self);

		// Token: 0x06000120 RID: 288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr BeginWebRequest_Injected(IntPtr _unity_self);

		// Token: 0x06000121 RID: 289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Abort_Injected(IntPtr _unity_self);

		// Token: 0x06000122 RID: 290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetMethod_Injected(IntPtr _unity_self, UnityWebRequest.UnityWebRequestMethod methodType);

		// Token: 0x06000123 RID: 291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetCustomMethod_Injected(IntPtr _unity_self, ref ManagedSpanWrapper customMethodName);

		// Token: 0x06000124 RID: 292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestMethod GetMethod_Injected(IntPtr _unity_self);

		// Token: 0x06000125 RID: 293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCustomMethod_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000126 RID: 294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError GetError_Injected(IntPtr _unity_self);

		// Token: 0x06000127 RID: 295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_use100Continue_Injected(IntPtr _unity_self);

		// Token: 0x06000128 RID: 296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_use100Continue_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000129 RID: 297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetUrl_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600012A RID: 298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetUrl_Injected(IntPtr _unity_self, ref ManagedSpanWrapper url);

		// Token: 0x0600012B RID: 299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long get_responseCode_Injected(IntPtr _unity_self);

		// Token: 0x0600012C RID: 300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetUploadProgress_Injected(IntPtr _unity_self);

		// Token: 0x0600012D RID: 301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsExecuting_Injected(IntPtr _unity_self);

		// Token: 0x0600012E RID: 302
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isModifiable_Injected(IntPtr _unity_self);

		// Token: 0x0600012F RID: 303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.Result get_result_Injected(IntPtr _unity_self);

		// Token: 0x06000130 RID: 304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetDownloadProgress_Injected(IntPtr _unity_self);

		// Token: 0x06000131 RID: 305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_uploadedBytes_Injected(IntPtr _unity_self);

		// Token: 0x06000132 RID: 306
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_downloadedBytes_Injected(IntPtr _unity_self);

		// Token: 0x06000133 RID: 307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRedirectLimit_Injected(IntPtr _unity_self);

		// Token: 0x06000134 RID: 308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRedirectLimitFromScripting_Injected(IntPtr _unity_self, int limit);

		// Token: 0x06000135 RID: 309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetChunked_Injected(IntPtr _unity_self);

		// Token: 0x06000136 RID: 310
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetChunked_Injected(IntPtr _unity_self, bool chunked);

		// Token: 0x06000137 RID: 311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRequestHeader_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, out ManagedSpanWrapper ret);

		// Token: 0x06000138 RID: 312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError InternalSetRequestHeader_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, ref ManagedSpanWrapper value);

		// Token: 0x06000139 RID: 313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetResponseHeader_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, out ManagedSpanWrapper ret);

		// Token: 0x0600013A RID: 314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetResponseHeaderKeys_Injected(IntPtr _unity_self);

		// Token: 0x0600013B RID: 315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetUploadHandler_Injected(IntPtr _unity_self, IntPtr uh);

		// Token: 0x0600013C RID: 316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetDownloadHandler_Injected(IntPtr _unity_self, IntPtr dh);

		// Token: 0x0600013D RID: 317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetCertificateHandler_Injected(IntPtr _unity_self, IntPtr ch);

		// Token: 0x0600013E RID: 318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetTimeoutMsec_Injected(IntPtr _unity_self);

		// Token: 0x0600013F RID: 319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetTimeoutMsec_Injected(IntPtr _unity_self, int timeout);

		// Token: 0x06000140 RID: 320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetSuppressErrorsToConsole_Injected(IntPtr _unity_self);

		// Token: 0x06000141 RID: 321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetSuppressErrorsToConsole_Injected(IntPtr _unity_self, bool suppress);

		// Token: 0x04000024 RID: 36
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000025 RID: 37
		[NonSerialized]
		internal DownloadHandler m_DownloadHandler;

		// Token: 0x04000026 RID: 38
		[NonSerialized]
		internal UploadHandler m_UploadHandler;

		// Token: 0x04000027 RID: 39
		[NonSerialized]
		internal CertificateHandler m_CertificateHandler;

		// Token: 0x04000028 RID: 40
		[NonSerialized]
		internal Uri m_Uri;

		// Token: 0x04000029 RID: 41
		public const string kHttpVerbGET = "GET";

		// Token: 0x0400002A RID: 42
		public const string kHttpVerbHEAD = "HEAD";

		// Token: 0x0400002B RID: 43
		public const string kHttpVerbPOST = "POST";

		// Token: 0x0400002C RID: 44
		public const string kHttpVerbPUT = "PUT";

		// Token: 0x0400002D RID: 45
		public const string kHttpVerbCREATE = "CREATE";

		// Token: 0x0400002E RID: 46
		public const string kHttpVerbDELETE = "DELETE";

		// Token: 0x02000015 RID: 21
		internal enum UnityWebRequestMethod
		{
			// Token: 0x04000033 RID: 51
			Get,
			// Token: 0x04000034 RID: 52
			Post,
			// Token: 0x04000035 RID: 53
			Put,
			// Token: 0x04000036 RID: 54
			Head,
			// Token: 0x04000037 RID: 55
			Custom
		}

		// Token: 0x02000016 RID: 22
		internal enum UnityWebRequestError
		{
			// Token: 0x04000039 RID: 57
			OK,
			// Token: 0x0400003A RID: 58
			OKCached,
			// Token: 0x0400003B RID: 59
			Unknown,
			// Token: 0x0400003C RID: 60
			SDKError,
			// Token: 0x0400003D RID: 61
			UnsupportedProtocol,
			// Token: 0x0400003E RID: 62
			MalformattedUrl,
			// Token: 0x0400003F RID: 63
			CannotResolveProxy,
			// Token: 0x04000040 RID: 64
			CannotResolveHost,
			// Token: 0x04000041 RID: 65
			CannotConnectToHost,
			// Token: 0x04000042 RID: 66
			AccessDenied,
			// Token: 0x04000043 RID: 67
			GenericHttpError,
			// Token: 0x04000044 RID: 68
			WriteError,
			// Token: 0x04000045 RID: 69
			ReadError,
			// Token: 0x04000046 RID: 70
			OutOfMemory,
			// Token: 0x04000047 RID: 71
			Timeout,
			// Token: 0x04000048 RID: 72
			HTTPPostError,
			// Token: 0x04000049 RID: 73
			SSLCannotConnect,
			// Token: 0x0400004A RID: 74
			Aborted,
			// Token: 0x0400004B RID: 75
			TooManyRedirects,
			// Token: 0x0400004C RID: 76
			ReceivedNoData,
			// Token: 0x0400004D RID: 77
			SSLNotSupported,
			// Token: 0x0400004E RID: 78
			FailedToSendData,
			// Token: 0x0400004F RID: 79
			FailedToReceiveData,
			// Token: 0x04000050 RID: 80
			SSLCertificateError,
			// Token: 0x04000051 RID: 81
			SSLCipherNotAvailable,
			// Token: 0x04000052 RID: 82
			SSLCACertError,
			// Token: 0x04000053 RID: 83
			UnrecognizedContentEncoding,
			// Token: 0x04000054 RID: 84
			LoginFailed,
			// Token: 0x04000055 RID: 85
			SSLShutdownFailed,
			// Token: 0x04000056 RID: 86
			RedirectLimitInvalid,
			// Token: 0x04000057 RID: 87
			InvalidRedirect,
			// Token: 0x04000058 RID: 88
			CannotModifyRequest,
			// Token: 0x04000059 RID: 89
			HeaderNameContainsInvalidCharacters,
			// Token: 0x0400005A RID: 90
			HeaderValueContainsInvalidCharacters,
			// Token: 0x0400005B RID: 91
			CannotOverrideSystemHeaders,
			// Token: 0x0400005C RID: 92
			AlreadySent,
			// Token: 0x0400005D RID: 93
			InvalidMethod,
			// Token: 0x0400005E RID: 94
			NotImplemented,
			// Token: 0x0400005F RID: 95
			NoInternetConnection,
			// Token: 0x04000060 RID: 96
			DataProcessingError,
			// Token: 0x04000061 RID: 97
			InsecureConnectionNotAllowed
		}

		// Token: 0x02000017 RID: 23
		public enum Result
		{
			// Token: 0x04000063 RID: 99
			InProgress,
			// Token: 0x04000064 RID: 100
			Success,
			// Token: 0x04000065 RID: 101
			ConnectionError,
			// Token: 0x04000066 RID: 102
			ProtocolError,
			// Token: 0x04000067 RID: 103
			DataProcessingError
		}

		// Token: 0x02000018 RID: 24
		internal static class BindingsMarshaller
		{
			// Token: 0x06000142 RID: 322 RVA: 0x00005AA4 File Offset: 0x00003CA4
			public static IntPtr ConvertToNative(UnityWebRequest unityWebRequest)
			{
				return unityWebRequest.m_Ptr;
			}
		}
	}
}
