using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("AudioScriptingClasses.h")]
	[NativeHeader("Runtime/Video/ScriptBindings/WebCamTexture.bindings.h")]
	[NativeHeader("Runtime/Video/BaseWebCamTexture.h")]
	public sealed class WebCamTexture : Texture
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600022D RID: 557
		public static extern WebCamDevice[] devices
		{
			[StaticAccessor("WebCamTextureBindings", StaticAccessorType.DoubleColon)]
			[NativeName("Internal_GetDevices")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00004E20 File Offset: 0x00003020
		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight, int requestedFPS)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, requestedFPS);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00004E36 File Offset: 0x00003036
		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, 0);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00004E4B File Offset: 0x0000304B
		public WebCamTexture(string deviceName)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, 0, 0, 0);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00004E60 File Offset: 0x00003060
		public WebCamTexture(int requestedWidth, int requestedHeight, int requestedFPS)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, "", requestedWidth, requestedHeight, requestedFPS);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00004E79 File Offset: 0x00003079
		public WebCamTexture(int requestedWidth, int requestedHeight)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, "", requestedWidth, requestedHeight, 0);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00004E92 File Offset: 0x00003092
		public WebCamTexture()
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, "", 0, 0, 0);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00004EAC File Offset: 0x000030AC
		public void Play()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			WebCamTexture.Play_Injected(intPtr);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00004ED0 File Offset: 0x000030D0
		public void Pause()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			WebCamTexture.Pause_Injected(intPtr);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00004EF4 File Offset: 0x000030F4
		public void Stop()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			WebCamTexture.Stop_Injected(intPtr);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00004F18 File Offset: 0x00003118
		public bool isPlaying
		{
			[NativeName("IsPlaying")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WebCamTexture.get_isPlaying_Injected(intPtr);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00004F3C File Offset: 0x0000313C
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00004F7C File Offset: 0x0000317C
		[NativeName("Device")]
		public unsafe string deviceName
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					WebCamTexture.get_deviceName_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					WebCamTexture.set_deviceName_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00004FE0 File Offset: 0x000031E0
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00005004 File Offset: 0x00003204
		public float requestedFPS
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WebCamTexture.get_requestedFPS_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WebCamTexture.set_requestedFPS_Injected(intPtr, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00005028 File Offset: 0x00003228
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000504C File Offset: 0x0000324C
		public int requestedWidth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WebCamTexture.get_requestedWidth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WebCamTexture.set_requestedWidth_Injected(intPtr, value);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00005070 File Offset: 0x00003270
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00005094 File Offset: 0x00003294
		public int requestedHeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WebCamTexture.get_requestedHeight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WebCamTexture.set_requestedHeight_Injected(intPtr, value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000050B8 File Offset: 0x000032B8
		public int videoRotationAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WebCamTexture.get_videoRotationAngle_Injected(intPtr);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000050DC File Offset: 0x000032DC
		public bool videoVerticallyMirrored
		{
			[NativeName("IsVideoVerticallyMirrored")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WebCamTexture.get_videoVerticallyMirrored_Injected(intPtr);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00005100 File Offset: 0x00003300
		public bool didUpdateThisFrame
		{
			[NativeName("DidUpdateThisFrame")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WebCamTexture.get_didUpdateThisFrame_Injected(intPtr);
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00005124 File Offset: 0x00003324
		[FreeFunction("WebCamTextureBindings::Internal_GetPixel", HasExplicitThis = true)]
		public Color GetPixel(int x, int y)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			WebCamTexture.GetPixel_Injected(intPtr, x, y, out color);
			return color;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000514C File Offset: 0x0000334C
		public Color[] GetPixels()
		{
			return this.GetPixels(0, 0, this.width, this.height);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00005174 File Offset: 0x00003374
		[FreeFunction("WebCamTextureBindings::Internal_GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return WebCamTexture.GetPixels_Injected(intPtr, x, y, blockWidth, blockHeight);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000519C File Offset: 0x0000339C
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			return this.GetPixels32(null);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000051B8 File Offset: 0x000033B8
		[FreeFunction("WebCamTextureBindings::Internal_GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color32[] GetPixels32([DefaultValue("null")] [Unmarshalled] Color32[] colors)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return WebCamTexture.GetPixels32_Injected(intPtr, colors);
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000051DC File Offset: 0x000033DC
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00005216 File Offset: 0x00003416
		public Vector2? autoFocusPoint
		{
			get
			{
				return (this.internalAutoFocusPoint.x < 0f) ? null : new Vector2?(this.internalAutoFocusPoint);
			}
			set
			{
				this.internalAutoFocusPoint = ((value == null) ? new Vector2(-1f, -1f) : value.Value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00005244 File Offset: 0x00003444
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000526C File Offset: 0x0000346C
		internal Vector2 internalAutoFocusPoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				WebCamTexture.get_internalAutoFocusPoint_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WebCamTexture.set_internalAutoFocusPoint_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00005290 File Offset: 0x00003490
		public bool isDepth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WebCamTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WebCamTexture.get_isDepth_Injected(intPtr);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000052B4 File Offset: 0x000034B4
		[StaticAccessor("WebCamTextureBindings", StaticAccessorType.DoubleColon)]
		private unsafe static void Internal_CreateWebCamTexture([Writable] WebCamTexture self, string scriptingDevice, int requestedWidth, int requestedHeight, int maxFramerate)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(scriptingDevice, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = scriptingDevice.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				WebCamTexture.Internal_CreateWebCamTexture_Injected(self, ref managedSpanWrapper, requestedWidth, requestedHeight, maxFramerate);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600024E RID: 590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self);

		// Token: 0x0600024F RID: 591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Pause_Injected(IntPtr _unity_self);

		// Token: 0x06000250 RID: 592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self);

		// Token: 0x06000251 RID: 593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPlaying_Injected(IntPtr _unity_self);

		// Token: 0x06000252 RID: 594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_deviceName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000253 RID: 595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_deviceName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06000254 RID: 596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_requestedFPS_Injected(IntPtr _unity_self);

		// Token: 0x06000255 RID: 597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_requestedFPS_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000256 RID: 598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_requestedWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000257 RID: 599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_requestedWidth_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000258 RID: 600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_requestedHeight_Injected(IntPtr _unity_self);

		// Token: 0x06000259 RID: 601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_requestedHeight_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600025A RID: 602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_videoRotationAngle_Injected(IntPtr _unity_self);

		// Token: 0x0600025B RID: 603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_videoVerticallyMirrored_Injected(IntPtr _unity_self);

		// Token: 0x0600025C RID: 604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_didUpdateThisFrame_Injected(IntPtr _unity_self);

		// Token: 0x0600025D RID: 605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPixel_Injected(IntPtr _unity_self, int x, int y, out Color ret);

		// Token: 0x0600025E RID: 606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color[] GetPixels_Injected(IntPtr _unity_self, int x, int y, int blockWidth, int blockHeight);

		// Token: 0x0600025F RID: 607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color32[] GetPixels32_Injected(IntPtr _unity_self, [DefaultValue("null")] Color32[] colors);

		// Token: 0x06000260 RID: 608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_internalAutoFocusPoint_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000261 RID: 609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_internalAutoFocusPoint_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000262 RID: 610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isDepth_Injected(IntPtr _unity_self);

		// Token: 0x06000263 RID: 611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWebCamTexture_Injected([Writable] WebCamTexture self, ref ManagedSpanWrapper scriptingDevice, int requestedWidth, int requestedHeight, int maxFramerate);
	}
}
