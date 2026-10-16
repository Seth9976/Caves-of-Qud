using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001FB RID: 507
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/CustomRenderTexture.h")]
	public sealed class CustomRenderTexture : RenderTexture
	{
		// Token: 0x06001A81 RID: 6785
		[FreeFunction(Name = "CustomRenderTextureScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateCustomRenderTexture([Writable] CustomRenderTexture rt);

		// Token: 0x06001A82 RID: 6786 RVA: 0x0002E7AC File Offset: 0x0002C9AC
		[NativeName("TriggerUpdate")]
		private void TriggerUpdate(int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomRenderTexture.TriggerUpdate_Injected(intPtr, count);
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0002E7CF File Offset: 0x0002C9CF
		public void Update(int count)
		{
			CustomRenderTextureManager.InvokeTriggerUpdate(this, count);
			this.TriggerUpdate(count);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0002E7E2 File Offset: 0x0002C9E2
		public void Update()
		{
			this.Update(1);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0002E7F0 File Offset: 0x0002C9F0
		[NativeName("TriggerInitialization")]
		private void TriggerInitialization()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomRenderTexture.TriggerInitialization_Injected(intPtr);
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0002E812 File Offset: 0x0002CA12
		public void Initialize()
		{
			this.TriggerInitialization();
			CustomRenderTextureManager.InvokeTriggerInitialize(this);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0002E824 File Offset: 0x0002CA24
		public void ClearUpdateZones()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomRenderTexture.ClearUpdateZones_Injected(intPtr);
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0002E848 File Offset: 0x0002CA48
		// (set) Token: 0x06001A89 RID: 6793 RVA: 0x0002E870 File Offset: 0x0002CA70
		public Material material
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(CustomRenderTexture.get_material_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_material_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0002E898 File Offset: 0x0002CA98
		// (set) Token: 0x06001A8B RID: 6795 RVA: 0x0002E8C0 File Offset: 0x0002CAC0
		public Material initializationMaterial
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(CustomRenderTexture.get_initializationMaterial_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_initializationMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0002E8E8 File Offset: 0x0002CAE8
		// (set) Token: 0x06001A8D RID: 6797 RVA: 0x0002E910 File Offset: 0x0002CB10
		public Texture initializationTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture>(CustomRenderTexture.get_initializationTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_initializationTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(value));
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0002E938 File Offset: 0x0002CB38
		[FreeFunction(Name = "CustomRenderTextureScripting::GetUpdateZonesInternal", HasExplicitThis = true)]
		internal void GetUpdateZonesInternal([NotNull] object updateZones)
		{
			if (updateZones == null)
			{
				ThrowHelper.ThrowArgumentNullException(updateZones, "updateZones");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomRenderTexture.GetUpdateZonesInternal_Injected(intPtr, updateZones);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0002E96A File Offset: 0x0002CB6A
		public void GetUpdateZones(List<CustomRenderTextureUpdateZone> updateZones)
		{
			this.GetUpdateZonesInternal(updateZones);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0002E978 File Offset: 0x0002CB78
		[FreeFunction(Name = "CustomRenderTextureScripting::SetUpdateZonesInternal", HasExplicitThis = true)]
		private unsafe void SetUpdateZonesInternal(CustomRenderTextureUpdateZone[] updateZones)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<CustomRenderTextureUpdateZone> span = new Span<CustomRenderTextureUpdateZone>(updateZones);
			fixed (CustomRenderTextureUpdateZone* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CustomRenderTexture.SetUpdateZonesInternal_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0002E9C0 File Offset: 0x0002CBC0
		[FreeFunction(Name = "CustomRenderTextureScripting::GetDoubleBufferRenderTexture", HasExplicitThis = true)]
		public RenderTexture GetDoubleBufferRenderTexture()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<RenderTexture>(CustomRenderTexture.GetDoubleBufferRenderTexture_Injected(intPtr));
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0002E9E8 File Offset: 0x0002CBE8
		public void EnsureDoubleBufferConsistency()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomRenderTexture.EnsureDoubleBufferConsistency_Injected(intPtr);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0002EA0C File Offset: 0x0002CC0C
		public void SetUpdateZones(CustomRenderTextureUpdateZone[] updateZones)
		{
			bool flag = updateZones == null;
			if (flag)
			{
				throw new ArgumentNullException("updateZones");
			}
			this.SetUpdateZonesInternal(updateZones);
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0002EA38 File Offset: 0x0002CC38
		// (set) Token: 0x06001A95 RID: 6805 RVA: 0x0002EA5C File Offset: 0x0002CC5C
		public CustomRenderTextureInitializationSource initializationSource
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomRenderTexture.get_initializationSource_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_initializationSource_Injected(intPtr, value);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0002EA80 File Offset: 0x0002CC80
		// (set) Token: 0x06001A97 RID: 6807 RVA: 0x0002EAA8 File Offset: 0x0002CCA8
		public Color initializationColor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				CustomRenderTexture.get_initializationColor_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_initializationColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x0002EACC File Offset: 0x0002CCCC
		// (set) Token: 0x06001A99 RID: 6809 RVA: 0x0002EAF0 File Offset: 0x0002CCF0
		public CustomRenderTextureUpdateMode updateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomRenderTexture.get_updateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_updateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0002EB14 File Offset: 0x0002CD14
		// (set) Token: 0x06001A9B RID: 6811 RVA: 0x0002EB38 File Offset: 0x0002CD38
		public CustomRenderTextureUpdateMode initializationMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomRenderTexture.get_initializationMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_initializationMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x0002EB5C File Offset: 0x0002CD5C
		// (set) Token: 0x06001A9D RID: 6813 RVA: 0x0002EB80 File Offset: 0x0002CD80
		public CustomRenderTextureUpdateZoneSpace updateZoneSpace
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomRenderTexture.get_updateZoneSpace_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_updateZoneSpace_Injected(intPtr, value);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0002EBA4 File Offset: 0x0002CDA4
		// (set) Token: 0x06001A9F RID: 6815 RVA: 0x0002EBC8 File Offset: 0x0002CDC8
		public int shaderPass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomRenderTexture.get_shaderPass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_shaderPass_Injected(intPtr, value);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x0002EBEC File Offset: 0x0002CDEC
		// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x0002EC10 File Offset: 0x0002CE10
		public uint cubemapFaceMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomRenderTexture.get_cubemapFaceMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_cubemapFaceMask_Injected(intPtr, value);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0002EC34 File Offset: 0x0002CE34
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x0002EC58 File Offset: 0x0002CE58
		public bool doubleBuffered
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomRenderTexture.get_doubleBuffered_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_doubleBuffered_Injected(intPtr, value);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x0002EC7C File Offset: 0x0002CE7C
		// (set) Token: 0x06001AA5 RID: 6821 RVA: 0x0002ECA0 File Offset: 0x0002CEA0
		public bool wrapUpdateZones
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomRenderTexture.get_wrapUpdateZones_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_wrapUpdateZones_Injected(intPtr, value);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
		// (set) Token: 0x06001AA7 RID: 6823 RVA: 0x0002ECE8 File Offset: 0x0002CEE8
		public float updatePeriod
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomRenderTexture.get_updatePeriod_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomRenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CustomRenderTexture.set_updatePeriod_Injected(intPtr, value);
			}
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0002ED0C File Offset: 0x0002CF0C
		public CustomRenderTexture(int width, int height, RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite)
			: this(width, height, RenderTexture.GetCompatibleFormat(format, readWrite))
		{
			bool flag = this != null;
			if (flag)
			{
				base.SetShadowSamplingMode(RenderTexture.GetShadowSamplingModeForFormat(format));
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0002ED43 File Offset: 0x0002CF43
		[ExcludeFromDocs]
		public CustomRenderTexture(int width, int height, RenderTextureFormat format)
			: this(width, height, format, RenderTextureReadWrite.Default)
		{
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0002ED51 File Offset: 0x0002CF51
		[ExcludeFromDocs]
		public CustomRenderTexture(int width, int height)
			: this(width, height, SystemInfo.GetGraphicsFormat(DefaultFormat.LDR))
		{
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0002ED64 File Offset: 0x0002CF64
		[ExcludeFromDocs]
		public CustomRenderTexture(int width, int height, [DefaultValue("DefaultFormat.LDR")] DefaultFormat defaultFormat)
			: this(width, height, RenderTexture.GetDefaultColorFormat(defaultFormat))
		{
			bool flag = defaultFormat == DefaultFormat.DepthStencil || defaultFormat == DefaultFormat.Shadow;
			if (flag)
			{
				base.depthStencilFormat = SystemInfo.GetGraphicsFormat(defaultFormat);
				base.SetShadowSamplingMode(RenderTexture.GetShadowSamplingModeForFormat(defaultFormat));
			}
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0002EDAC File Offset: 0x0002CFAC
		[ExcludeFromDocs]
		public CustomRenderTexture(int width, int height, GraphicsFormat format)
		{
			bool flag = format != GraphicsFormat.None && !base.ValidateFormat(format, GraphicsFormatUsage.Render);
			if (!flag)
			{
				CustomRenderTexture.Internal_CreateCustomRenderTexture(this);
				this.width = width;
				this.height = height;
				base.graphicsFormat = format;
				base.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		// Token: 0x06001AAD RID: 6829
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TriggerUpdate_Injected(IntPtr _unity_self, int count);

		// Token: 0x06001AAE RID: 6830
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TriggerInitialization_Injected(IntPtr _unity_self);

		// Token: 0x06001AAF RID: 6831
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearUpdateZones_Injected(IntPtr _unity_self);

		// Token: 0x06001AB0 RID: 6832
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_material_Injected(IntPtr _unity_self);

		// Token: 0x06001AB1 RID: 6833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_material_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06001AB2 RID: 6834
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_initializationMaterial_Injected(IntPtr _unity_self);

		// Token: 0x06001AB3 RID: 6835
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_initializationMaterial_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06001AB4 RID: 6836
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_initializationTexture_Injected(IntPtr _unity_self);

		// Token: 0x06001AB5 RID: 6837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_initializationTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06001AB6 RID: 6838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetUpdateZonesInternal_Injected(IntPtr _unity_self, object updateZones);

		// Token: 0x06001AB7 RID: 6839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetUpdateZonesInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper updateZones);

		// Token: 0x06001AB8 RID: 6840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDoubleBufferRenderTexture_Injected(IntPtr _unity_self);

		// Token: 0x06001AB9 RID: 6841
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnsureDoubleBufferConsistency_Injected(IntPtr _unity_self);

		// Token: 0x06001ABA RID: 6842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CustomRenderTextureInitializationSource get_initializationSource_Injected(IntPtr _unity_self);

		// Token: 0x06001ABB RID: 6843
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_initializationSource_Injected(IntPtr _unity_self, CustomRenderTextureInitializationSource value);

		// Token: 0x06001ABC RID: 6844
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_initializationColor_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x06001ABD RID: 6845
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_initializationColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x06001ABE RID: 6846
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CustomRenderTextureUpdateMode get_updateMode_Injected(IntPtr _unity_self);

		// Token: 0x06001ABF RID: 6847
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateMode_Injected(IntPtr _unity_self, CustomRenderTextureUpdateMode value);

		// Token: 0x06001AC0 RID: 6848
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CustomRenderTextureUpdateMode get_initializationMode_Injected(IntPtr _unity_self);

		// Token: 0x06001AC1 RID: 6849
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_initializationMode_Injected(IntPtr _unity_self, CustomRenderTextureUpdateMode value);

		// Token: 0x06001AC2 RID: 6850
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CustomRenderTextureUpdateZoneSpace get_updateZoneSpace_Injected(IntPtr _unity_self);

		// Token: 0x06001AC3 RID: 6851
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateZoneSpace_Injected(IntPtr _unity_self, CustomRenderTextureUpdateZoneSpace value);

		// Token: 0x06001AC4 RID: 6852
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_shaderPass_Injected(IntPtr _unity_self);

		// Token: 0x06001AC5 RID: 6853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shaderPass_Injected(IntPtr _unity_self, int value);

		// Token: 0x06001AC6 RID: 6854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_cubemapFaceMask_Injected(IntPtr _unity_self);

		// Token: 0x06001AC7 RID: 6855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cubemapFaceMask_Injected(IntPtr _unity_self, uint value);

		// Token: 0x06001AC8 RID: 6856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_doubleBuffered_Injected(IntPtr _unity_self);

		// Token: 0x06001AC9 RID: 6857
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_doubleBuffered_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001ACA RID: 6858
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_wrapUpdateZones_Injected(IntPtr _unity_self);

		// Token: 0x06001ACB RID: 6859
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapUpdateZones_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001ACC RID: 6860
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_updatePeriod_Injected(IntPtr _unity_self);

		// Token: 0x06001ACD RID: 6861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updatePeriod_Injected(IntPtr _unity_self, float value);
	}
}
