using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000070 RID: 112
	[NativeHeader("Modules/Animation/Constraints/RotationConstraint.h")]
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class RotationConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060006E3 RID: 1763 RVA: 0x0000C931 File Offset: 0x0000AB31
		private RotationConstraint()
		{
			RotationConstraint.Internal_Create(this);
		}

		// Token: 0x060006E4 RID: 1764
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] RotationConstraint self);

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0000C944 File Offset: 0x0000AB44
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0000C968 File Offset: 0x0000AB68
		public float weight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RotationConstraint.get_weight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RotationConstraint.set_weight_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0000C98C File Offset: 0x0000AB8C
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		public Vector3 rotationAtRest
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				RotationConstraint.get_rotationAtRest_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RotationConstraint.set_rotationAtRest_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x0000CA00 File Offset: 0x0000AC00
		public Vector3 rotationOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				RotationConstraint.get_rotationOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RotationConstraint.set_rotationOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0000CA24 File Offset: 0x0000AC24
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x0000CA48 File Offset: 0x0000AC48
		public Axis rotationAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RotationConstraint.get_rotationAxis_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RotationConstraint.set_rotationAxis_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0000CA90 File Offset: 0x0000AC90
		public bool constraintActive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RotationConstraint.get_constraintActive_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RotationConstraint.set_constraintActive_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		public bool locked
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RotationConstraint.get_locked_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RotationConstraint.set_locked_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		public int sourceCount
		{
			get
			{
				return RotationConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000CB14 File Offset: 0x0000AD14
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static int GetSourceCountInternal([NotNull] RotationConstraint self)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			return RotationConstraint.GetSourceCountInternal_Injected(intPtr);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000CB4C File Offset: 0x0000AD4C
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public void GetSources([NotNull] List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				ThrowHelper.ThrowArgumentNullException(sources, "sources");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RotationConstraint.GetSources_Injected(intPtr, sources);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000CB80 File Offset: 0x0000AD80
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			RotationConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000CBAC File Offset: 0x0000ADAC
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static void SetSourcesInternal([NotNull] RotationConstraint self, List<ConstraintSource> sources)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			RotationConstraint.SetSourcesInternal_Injected(intPtr, sources);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		public int AddSource(ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RotationConstraint.AddSource_Injected(intPtr, ref source);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000CC08 File Offset: 0x0000AE08
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		[NativeName("RemoveSource")]
		private void RemoveSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RotationConstraint.RemoveSourceInternal_Injected(intPtr, index);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0000CC40 File Offset: 0x0000AE40
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0000CC64 File Offset: 0x0000AE64
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ConstraintSource constraintSource;
			RotationConstraint.GetSourceInternal_Injected(intPtr, index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000CC8A File Offset: 0x0000AE8A
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RotationConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RotationConstraint.SetSourceInternal_Injected(intPtr, index, ref source);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The RotationConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060006FE RID: 1790
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_weight_Injected(IntPtr _unity_self);

		// Token: 0x060006FF RID: 1791
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_weight_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000700 RID: 1792
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationAtRest_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000701 RID: 1793
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationAtRest_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000702 RID: 1794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationOffset_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000703 RID: 1795
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationOffset_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000704 RID: 1796
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Axis get_rotationAxis_Injected(IntPtr _unity_self);

		// Token: 0x06000705 RID: 1797
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationAxis_Injected(IntPtr _unity_self, Axis value);

		// Token: 0x06000706 RID: 1798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_constraintActive_Injected(IntPtr _unity_self);

		// Token: 0x06000707 RID: 1799
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_constraintActive_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000708 RID: 1800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_locked_Injected(IntPtr _unity_self);

		// Token: 0x06000709 RID: 1801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_locked_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600070A RID: 1802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal_Injected(IntPtr self);

		// Token: 0x0600070B RID: 1803
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSources_Injected(IntPtr _unity_self, List<ConstraintSource> sources);

		// Token: 0x0600070C RID: 1804
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal_Injected(IntPtr self, List<ConstraintSource> sources);

		// Token: 0x0600070D RID: 1805
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddSource_Injected(IntPtr _unity_self, [In] ref ConstraintSource source);

		// Token: 0x0600070E RID: 1806
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveSourceInternal_Injected(IntPtr _unity_self, int index);

		// Token: 0x0600070F RID: 1807
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSourceInternal_Injected(IntPtr _unity_self, int index, out ConstraintSource ret);

		// Token: 0x06000710 RID: 1808
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourceInternal_Injected(IntPtr _unity_self, int index, [In] ref ConstraintSource source);
	}
}
