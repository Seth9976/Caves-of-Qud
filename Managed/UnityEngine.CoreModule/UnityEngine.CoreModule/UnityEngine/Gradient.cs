using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000213 RID: 531
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Math/Gradient.bindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class Gradient : IEquatable<Gradient>
	{
		// Token: 0x06001BCA RID: 7114
		[FreeFunction(Name = "Gradient_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Init();

		// Token: 0x06001BCB RID: 7115 RVA: 0x000323F8 File Offset: 0x000305F8
		[FreeFunction(Name = "Gradient_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		private void Cleanup()
		{
			IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Gradient.Cleanup_Injected(intPtr);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0003241C File Offset: 0x0003061C
		[FreeFunction("Gradient_Bindings::Internal_Equals", IsThreadSafe = true, HasExplicitThis = true)]
		private bool Internal_Equals(IntPtr other)
		{
			IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Gradient.Internal_Equals_Injected(intPtr, other);
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0003243F File Offset: 0x0003063F
		[RequiredByNativeCode]
		public Gradient()
		{
			this.m_Ptr = Gradient.Init();
			this.m_RequiresNativeCleanup = true;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0003245B File Offset: 0x0003065B
		[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
		internal Gradient(IntPtr ptr)
		{
			this.m_Ptr = ptr;
			this.m_RequiresNativeCleanup = false;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00032474 File Offset: 0x00030674
		protected override void Finalize()
		{
			try
			{
				bool requiresNativeCleanup = this.m_RequiresNativeCleanup;
				if (requiresNativeCleanup)
				{
					this.Cleanup();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x000324B0 File Offset: 0x000306B0
		[FreeFunction(Name = "Gradient_Bindings::Evaluate", IsThreadSafe = true, HasExplicitThis = true)]
		public Color Evaluate(float time)
		{
			IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			Gradient.Evaluate_Injected(intPtr, time, out color);
			return color;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x000324D8 File Offset: 0x000306D8
		// (set) Token: 0x06001BD2 RID: 7122 RVA: 0x0003251C File Offset: 0x0003071C
		public unsafe GradientColorKey[] colorKeys
		{
			[FreeFunction("Gradient_Bindings::GetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			get
			{
				GradientColorKey[] array2;
				try
				{
					IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Gradient.get_colorKeys_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					GradientColorKey[] array;
					blittableArrayWrapper.Unmarshal<GradientColorKey>(ref array);
					array2 = array;
				}
				return array2;
			}
			[FreeFunction("Gradient_Bindings::SetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<GradientColorKey> span = new Span<GradientColorKey>(value);
				fixed (GradientColorKey* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Gradient.set_colorKeys_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x00032564 File Offset: 0x00030764
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x000325A8 File Offset: 0x000307A8
		public unsafe GradientAlphaKey[] alphaKeys
		{
			[FreeFunction("Gradient_Bindings::GetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			get
			{
				GradientAlphaKey[] array2;
				try
				{
					IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Gradient.get_alphaKeys_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					GradientAlphaKey[] array;
					blittableArrayWrapper.Unmarshal<GradientAlphaKey>(ref array);
					array2 = array;
				}
				return array2;
			}
			[FreeFunction("Gradient_Bindings::SetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<GradientAlphaKey> span = new Span<GradientAlphaKey>(value);
				fixed (GradientAlphaKey* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Gradient.set_alphaKeys_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x000325F0 File Offset: 0x000307F0
		// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x00032614 File Offset: 0x00030814
		[NativeProperty(IsThreadSafe = true)]
		public GradientMode mode
		{
			get
			{
				IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Gradient.get_mode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Gradient.set_mode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00032638 File Offset: 0x00030838
		// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x0003265C File Offset: 0x0003085C
		[NativeProperty(IsThreadSafe = true)]
		public ColorSpace colorSpace
		{
			get
			{
				IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Gradient.get_colorSpace_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Gradient.set_colorSpace_Injected(intPtr, value);
			}
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00032680 File Offset: 0x00030880
		[FreeFunction(Name = "Gradient_Bindings::SetKeys", IsThreadSafe = true, HasExplicitThis = true)]
		public unsafe void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys)
		{
			IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<GradientColorKey> span = new Span<GradientColorKey>(colorKeys);
			fixed (GradientColorKey* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<GradientAlphaKey> span2 = new Span<GradientAlphaKey>(alphaKeys);
				fixed (GradientAlphaKey* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					Gradient.SetKeys_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000326F4 File Offset: 0x000308F4
		public override bool Equals(object o)
		{
			bool flag = o == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == o;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = o.GetType() != base.GetType();
					flag2 = !flag4 && this.Equals((Gradient)o);
				}
			}
			return flag2;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x00032748 File Offset: 0x00030948
		public bool Equals(Gradient other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == other;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = this.m_Ptr.Equals(other.m_Ptr);
					flag2 = flag4 || this.Internal_Equals(other.m_Ptr);
				}
			}
			return flag2;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000327A0 File Offset: 0x000309A0
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x06001BDD RID: 7133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Cleanup_Injected(IntPtr _unity_self);

		// Token: 0x06001BDE RID: 7134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Equals_Injected(IntPtr _unity_self, IntPtr other);

		// Token: 0x06001BDF RID: 7135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Evaluate_Injected(IntPtr _unity_self, float time, out Color ret);

		// Token: 0x06001BE0 RID: 7136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_colorKeys_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06001BE1 RID: 7137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_colorKeys_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06001BE2 RID: 7138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_alphaKeys_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06001BE3 RID: 7139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_alphaKeys_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06001BE4 RID: 7140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GradientMode get_mode_Injected(IntPtr _unity_self);

		// Token: 0x06001BE5 RID: 7141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mode_Injected(IntPtr _unity_self, GradientMode value);

		// Token: 0x06001BE6 RID: 7142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ColorSpace get_colorSpace_Injected(IntPtr _unity_self);

		// Token: 0x06001BE7 RID: 7143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_colorSpace_Injected(IntPtr _unity_self, ColorSpace value);

		// Token: 0x06001BE8 RID: 7144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetKeys_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colorKeys, ref ManagedSpanWrapper alphaKeys);

		// Token: 0x04000877 RID: 2167
		[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
		internal IntPtr m_Ptr;

		// Token: 0x04000878 RID: 2168
		private bool m_RequiresNativeCleanup;

		// Token: 0x02000214 RID: 532
		internal static class BindingsMarshaller
		{
			// Token: 0x06001BE9 RID: 7145 RVA: 0x000327BD File Offset: 0x000309BD
			public static IntPtr ConvertToNative(Gradient graident)
			{
				return graident.m_Ptr;
			}

			// Token: 0x06001BEA RID: 7146 RVA: 0x000327C5 File Offset: 0x000309C5
			public static Gradient ConvertToManaged(IntPtr ptr)
			{
				return new Gradient(ptr);
			}
		}
	}
}
