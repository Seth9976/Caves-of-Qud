using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/UI/UIStructs.h")]
	[RequireComponent(typeof(RectTransform))]
	[NativeClass("UI::Canvas")]
	[NativeHeader("Modules/UI/Canvas.h")]
	[NativeHeader("Modules/UI/CanvasManager.h")]
	public sealed class Canvas : Behaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000076 RID: 118 RVA: 0x00002EFC File Offset: 0x000010FC
		// (remove) Token: 0x06000077 RID: 119 RVA: 0x00002F30 File Offset: 0x00001130
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Canvas.WillRenderCanvases preWillRenderCanvases;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000078 RID: 120 RVA: 0x00002F64 File Offset: 0x00001164
		// (remove) Token: 0x06000079 RID: 121 RVA: 0x00002F98 File Offset: 0x00001198
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Canvas.WillRenderCanvases willRenderCanvases;

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002FCC File Offset: 0x000011CC
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002FF0 File Offset: 0x000011F0
		public RenderMode renderMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_renderMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_renderMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003014 File Offset: 0x00001214
		public bool isRootCanvas
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_isRootCanvas_Injected(intPtr);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003038 File Offset: 0x00001238
		public Rect pixelRect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rect rect;
				Canvas.get_pixelRect_Injected(intPtr, out rect);
				return rect;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003060 File Offset: 0x00001260
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003084 File Offset: 0x00001284
		public float scaleFactor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_scaleFactor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_scaleFactor_Injected(intPtr, value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000030A8 File Offset: 0x000012A8
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000030CC File Offset: 0x000012CC
		public float referencePixelsPerUnit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_referencePixelsPerUnit_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_referencePixelsPerUnit_Injected(intPtr, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000030F0 File Offset: 0x000012F0
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003114 File Offset: 0x00001314
		public bool overridePixelPerfect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_overridePixelPerfect_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_overridePixelPerfect_Injected(intPtr, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003138 File Offset: 0x00001338
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000315C File Offset: 0x0000135C
		public bool vertexColorAlwaysGammaSpace
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_vertexColorAlwaysGammaSpace_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_vertexColorAlwaysGammaSpace_Injected(intPtr, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003180 File Offset: 0x00001380
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000031A4 File Offset: 0x000013A4
		public bool pixelPerfect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_pixelPerfect_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_pixelPerfect_Injected(intPtr, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000031C8 File Offset: 0x000013C8
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000031EC File Offset: 0x000013EC
		public float planeDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_planeDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_planeDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003210 File Offset: 0x00001410
		public int renderOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_renderOrder_Injected(intPtr);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003234 File Offset: 0x00001434
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003258 File Offset: 0x00001458
		public bool overrideSorting
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_overrideSorting_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_overrideSorting_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000327C File Offset: 0x0000147C
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000032A0 File Offset: 0x000014A0
		public int sortingOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_sortingOrder_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_sortingOrder_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000032C4 File Offset: 0x000014C4
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000032E8 File Offset: 0x000014E8
		public int targetDisplay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_targetDisplay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_targetDisplay_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000330C File Offset: 0x0000150C
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003330 File Offset: 0x00001530
		public int sortingLayerID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_sortingLayerID_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_sortingLayerID_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003354 File Offset: 0x00001554
		public int cachedSortingLayerValue
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_cachedSortingLayerValue_Injected(intPtr);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003378 File Offset: 0x00001578
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000339C File Offset: 0x0000159C
		public AdditionalCanvasShaderChannels additionalShaderChannels
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_additionalShaderChannels_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_additionalShaderChannels_Injected(intPtr, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000033C0 File Offset: 0x000015C0
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003400 File Offset: 0x00001600
		public unsafe string sortingLayerName
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					Canvas.get_sortingLayerName_Injected(intPtr, out managedSpanWrapper);
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
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
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
					Canvas.set_sortingLayerName_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003464 File Offset: 0x00001664
		public Canvas rootCanvas
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Canvas>(Canvas.get_rootCanvas_Injected(intPtr));
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000348C File Offset: 0x0000168C
		public Vector2 renderingDisplaySize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Canvas.get_renderingDisplaySize_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000034B4 File Offset: 0x000016B4
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000034D8 File Offset: 0x000016D8
		public StandaloneRenderResize updateRectTransformForStandalone
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_updateRectTransformForStandalone_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_updateRectTransformForStandalone_Injected(intPtr, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000034FB File Offset: 0x000016FB
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003502 File Offset: 0x00001702
		internal static Action<int> externBeginRenderOverlays
		{
			get; [VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			set;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000350A File Offset: 0x0000170A
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003511 File Offset: 0x00001711
		internal static Action<int, int> externRenderOverlaysBefore
		{
			get; [VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			set;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003519 File Offset: 0x00001719
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003520 File Offset: 0x00001720
		internal static Action<int> externEndRenderOverlays
		{
			get; [VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			set;
		}

		// Token: 0x060000A2 RID: 162
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[FreeFunction("UI::CanvasManager::SetExternalCanvasEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetExternalCanvasEnabled(bool enabled);

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003528 File Offset: 0x00001728
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003550 File Offset: 0x00001750
		[NativeProperty("Camera", false, TargetType.Function)]
		public Camera worldCamera
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Camera>(Canvas.get_worldCamera_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_worldCamera_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(value));
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003578 File Offset: 0x00001778
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000359C File Offset: 0x0000179C
		[NativeProperty("SortingBucketNormalizedSize", false, TargetType.Function)]
		public float normalizedSortingGridSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_normalizedSortingGridSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_normalizedSortingGridSize_Injected(intPtr, value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000035E4 File Offset: 0x000017E4
		[NativeProperty("SortingBucketNormalizedSize", false, TargetType.Function)]
		[Obsolete("Setting normalizedSize via a int is not supported. Please use normalizedSortingGridSize", false)]
		public int sortingGridNormalizedSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Canvas.get_sortingGridNormalizedSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Canvas.set_sortingGridNormalizedSize_Injected(intPtr, value);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003608 File Offset: 0x00001808
		[Obsolete("Shared default material now used for text and general UI elements, call Canvas.GetDefaultCanvasMaterial()", false)]
		[FreeFunction("UI::GetDefaultUIMaterial")]
		public static Material GetDefaultCanvasTextMaterial()
		{
			return Unmarshal.UnmarshalUnityObject<Material>(Canvas.GetDefaultCanvasTextMaterial_Injected());
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003620 File Offset: 0x00001820
		[FreeFunction("UI::GetDefaultUIMaterial")]
		public static Material GetDefaultCanvasMaterial()
		{
			return Unmarshal.UnmarshalUnityObject<Material>(Canvas.GetDefaultCanvasMaterial_Injected());
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003638 File Offset: 0x00001838
		[FreeFunction("UI::GetETC1SupportedCanvasMaterial")]
		public static Material GetETC1SupportedCanvasMaterial()
		{
			return Unmarshal.UnmarshalUnityObject<Material>(Canvas.GetETC1SupportedCanvasMaterial_Injected());
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003650 File Offset: 0x00001850
		internal void UpdateCanvasRectTransform(bool alignWithCamera)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Canvas>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Canvas.UpdateCanvasRectTransform_Injected(intPtr, alignWithCamera);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003673 File Offset: 0x00001873
		public static void ForceUpdateCanvases()
		{
			Canvas.SendPreWillRenderCanvases();
			Canvas.SendWillRenderCanvases();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003682 File Offset: 0x00001882
		[RequiredByNativeCode]
		private static void SendPreWillRenderCanvases()
		{
			Canvas.WillRenderCanvases willRenderCanvases = Canvas.preWillRenderCanvases;
			if (willRenderCanvases != null)
			{
				willRenderCanvases();
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003696 File Offset: 0x00001896
		[RequiredByNativeCode]
		private static void SendWillRenderCanvases()
		{
			Canvas.WillRenderCanvases willRenderCanvases = Canvas.willRenderCanvases;
			if (willRenderCanvases != null)
			{
				willRenderCanvases();
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000036AA File Offset: 0x000018AA
		[RequiredByNativeCode]
		private static void BeginRenderExtraOverlays(int displayIndex)
		{
			Action<int> externBeginRenderOverlays = Canvas.externBeginRenderOverlays;
			if (externBeginRenderOverlays != null)
			{
				externBeginRenderOverlays(displayIndex);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000036BF File Offset: 0x000018BF
		[RequiredByNativeCode]
		private static void RenderExtraOverlaysBefore(int displayIndex, int sortingOrder)
		{
			Action<int, int> externRenderOverlaysBefore = Canvas.externRenderOverlaysBefore;
			if (externRenderOverlaysBefore != null)
			{
				externRenderOverlaysBefore(displayIndex, sortingOrder);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000036D5 File Offset: 0x000018D5
		[RequiredByNativeCode]
		private static void EndRenderExtraOverlays(int displayIndex)
		{
			Action<int> externEndRenderOverlays = Canvas.externEndRenderOverlays;
			if (externEndRenderOverlays != null)
			{
				externEndRenderOverlays(displayIndex);
			}
		}

		// Token: 0x060000B4 RID: 180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderMode get_renderMode_Injected(IntPtr _unity_self);

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderMode_Injected(IntPtr _unity_self, RenderMode value);

		// Token: 0x060000B6 RID: 182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isRootCanvas_Injected(IntPtr _unity_self);

		// Token: 0x060000B7 RID: 183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pixelRect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x060000B8 RID: 184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_scaleFactor_Injected(IntPtr _unity_self);

		// Token: 0x060000B9 RID: 185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_scaleFactor_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000BA RID: 186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_referencePixelsPerUnit_Injected(IntPtr _unity_self);

		// Token: 0x060000BB RID: 187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_referencePixelsPerUnit_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000BC RID: 188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_overridePixelPerfect_Injected(IntPtr _unity_self);

		// Token: 0x060000BD RID: 189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_overridePixelPerfect_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000BE RID: 190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_vertexColorAlwaysGammaSpace_Injected(IntPtr _unity_self);

		// Token: 0x060000BF RID: 191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_vertexColorAlwaysGammaSpace_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000C0 RID: 192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_pixelPerfect_Injected(IntPtr _unity_self);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pixelPerfect_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_planeDistance_Injected(IntPtr _unity_self);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_planeDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000C4 RID: 196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_renderOrder_Injected(IntPtr _unity_self);

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_overrideSorting_Injected(IntPtr _unity_self);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_overrideSorting_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingOrder_Injected(IntPtr _unity_self);

		// Token: 0x060000C8 RID: 200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingOrder_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_targetDisplay_Injected(IntPtr _unity_self);

		// Token: 0x060000CA RID: 202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetDisplay_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000CB RID: 203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingLayerID_Injected(IntPtr _unity_self);

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingLayerID_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_cachedSortingLayerValue_Injected(IntPtr _unity_self);

		// Token: 0x060000CE RID: 206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AdditionalCanvasShaderChannels get_additionalShaderChannels_Injected(IntPtr _unity_self);

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_additionalShaderChannels_Injected(IntPtr _unity_self, AdditionalCanvasShaderChannels value);

		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_sortingLayerName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060000D1 RID: 209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingLayerName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060000D2 RID: 210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_rootCanvas_Injected(IntPtr _unity_self);

		// Token: 0x060000D3 RID: 211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_renderingDisplaySize_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060000D4 RID: 212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern StandaloneRenderResize get_updateRectTransformForStandalone_Injected(IntPtr _unity_self);

		// Token: 0x060000D5 RID: 213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateRectTransformForStandalone_Injected(IntPtr _unity_self, StandaloneRenderResize value);

		// Token: 0x060000D6 RID: 214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_worldCamera_Injected(IntPtr _unity_self);

		// Token: 0x060000D7 RID: 215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_worldCamera_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_normalizedSortingGridSize_Injected(IntPtr _unity_self);

		// Token: 0x060000D9 RID: 217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_normalizedSortingGridSize_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000DA RID: 218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingGridNormalizedSize_Injected(IntPtr _unity_self);

		// Token: 0x060000DB RID: 219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingGridNormalizedSize_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultCanvasTextMaterial_Injected();

		// Token: 0x060000DD RID: 221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultCanvasMaterial_Injected();

		// Token: 0x060000DE RID: 222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetETC1SupportedCanvasMaterial_Injected();

		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateCanvasRectTransform_Injected(IntPtr _unity_self, bool alignWithCamera);

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x060000E1 RID: 225
		public delegate void WillRenderCanvases();
	}
}
