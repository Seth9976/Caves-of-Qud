using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000071 RID: 113
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[NativeHeader("Modules/Animation/Constraints/ScaleConstraint.h")]
	[RequireComponent(typeof(Transform))]
	[UsedByNativeCode]
	public sealed class ScaleConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x0000CD2D File Offset: 0x0000AF2D
		private ScaleConstraint()
		{
			ScaleConstraint.Internal_Create(this);
		}

		// Token: 0x06000712 RID: 1810
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] ScaleConstraint self);

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0000CD40 File Offset: 0x0000AF40
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0000CD64 File Offset: 0x0000AF64
		public float weight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ScaleConstraint.get_weight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ScaleConstraint.set_weight_Injected(intPtr, value);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0000CD88 File Offset: 0x0000AF88
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		public Vector3 scaleAtRest
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ScaleConstraint.get_scaleAtRest_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ScaleConstraint.set_scaleAtRest_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		public Vector3 scaleOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ScaleConstraint.get_scaleOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ScaleConstraint.set_scaleOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0000CE20 File Offset: 0x0000B020
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0000CE44 File Offset: 0x0000B044
		public Axis scalingAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ScaleConstraint.get_scalingAxis_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ScaleConstraint.set_scalingAxis_Injected(intPtr, value);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0000CE68 File Offset: 0x0000B068
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public bool constraintActive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ScaleConstraint.get_constraintActive_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ScaleConstraint.set_constraintActive_Injected(intPtr, value);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		public bool locked
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ScaleConstraint.get_locked_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ScaleConstraint.set_locked_Injected(intPtr, value);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
		public int sourceCount
		{
			get
			{
				return ScaleConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000CF10 File Offset: 0x0000B110
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static int GetSourceCountInternal([NotNull] ScaleConstraint self)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			return ScaleConstraint.GetSourceCountInternal_Injected(intPtr);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000CF48 File Offset: 0x0000B148
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public void GetSources([NotNull] List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				ThrowHelper.ThrowArgumentNullException(sources, "sources");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ScaleConstraint.GetSources_Injected(intPtr, sources);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0000CF7C File Offset: 0x0000B17C
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			ScaleConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static void SetSourcesInternal([NotNull] ScaleConstraint self, List<ConstraintSource> sources)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			ScaleConstraint.SetSourcesInternal_Injected(intPtr, sources);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000CFE0 File Offset: 0x0000B1E0
		public int AddSource(ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ScaleConstraint.AddSource_Injected(intPtr, ref source);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000D004 File Offset: 0x0000B204
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000D018 File Offset: 0x0000B218
		[NativeName("RemoveSource")]
		private void RemoveSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ScaleConstraint.RemoveSourceInternal_Injected(intPtr, index);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000D03C File Offset: 0x0000B23C
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000D060 File Offset: 0x0000B260
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ConstraintSource constraintSource;
			ScaleConstraint.GetSourceInternal_Injected(intPtr, index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000D086 File Offset: 0x0000B286
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000D09C File Offset: 0x0000B29C
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScaleConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ScaleConstraint.SetSourceInternal_Injected(intPtr, index, ref source);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The ScaleConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x0600072C RID: 1836
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_weight_Injected(IntPtr _unity_self);

		// Token: 0x0600072D RID: 1837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_weight_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600072E RID: 1838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_scaleAtRest_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600072F RID: 1839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_scaleAtRest_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000730 RID: 1840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_scaleOffset_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000731 RID: 1841
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_scaleOffset_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000732 RID: 1842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Axis get_scalingAxis_Injected(IntPtr _unity_self);

		// Token: 0x06000733 RID: 1843
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_scalingAxis_Injected(IntPtr _unity_self, Axis value);

		// Token: 0x06000734 RID: 1844
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_constraintActive_Injected(IntPtr _unity_self);

		// Token: 0x06000735 RID: 1845
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_constraintActive_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000736 RID: 1846
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_locked_Injected(IntPtr _unity_self);

		// Token: 0x06000737 RID: 1847
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_locked_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000738 RID: 1848
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal_Injected(IntPtr self);

		// Token: 0x06000739 RID: 1849
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSources_Injected(IntPtr _unity_self, List<ConstraintSource> sources);

		// Token: 0x0600073A RID: 1850
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal_Injected(IntPtr self, List<ConstraintSource> sources);

		// Token: 0x0600073B RID: 1851
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddSource_Injected(IntPtr _unity_self, [In] ref ConstraintSource source);

		// Token: 0x0600073C RID: 1852
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveSourceInternal_Injected(IntPtr _unity_self, int index);

		// Token: 0x0600073D RID: 1853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSourceInternal_Injected(IntPtr _unity_self, int index, out ConstraintSource ret);

		// Token: 0x0600073E RID: 1854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourceInternal_Injected(IntPtr _unity_self, int index, [In] ref ConstraintSource source);
	}
}
