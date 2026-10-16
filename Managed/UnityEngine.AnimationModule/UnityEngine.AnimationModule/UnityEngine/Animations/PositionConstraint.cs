using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200006F RID: 111
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/PositionConstraint.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class PositionConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x0000C536 File Offset: 0x0000A736
		private PositionConstraint()
		{
			PositionConstraint.Internal_Create(this);
		}

		// Token: 0x060006B6 RID: 1718
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] PositionConstraint self);

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0000C548 File Offset: 0x0000A748
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0000C56C File Offset: 0x0000A76C
		public float weight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PositionConstraint.get_weight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PositionConstraint.set_weight_Injected(intPtr, value);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0000C590 File Offset: 0x0000A790
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		public Vector3 translationAtRest
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				PositionConstraint.get_translationAtRest_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PositionConstraint.set_translationAtRest_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0000C604 File Offset: 0x0000A804
		public Vector3 translationOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				PositionConstraint.get_translationOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PositionConstraint.set_translationOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0000C628 File Offset: 0x0000A828
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0000C64C File Offset: 0x0000A84C
		public Axis translationAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PositionConstraint.get_translationAxis_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PositionConstraint.set_translationAxis_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0000C670 File Offset: 0x0000A870
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0000C694 File Offset: 0x0000A894
		public bool constraintActive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PositionConstraint.get_constraintActive_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PositionConstraint.set_constraintActive_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		public bool locked
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PositionConstraint.get_locked_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PositionConstraint.set_locked_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0000C700 File Offset: 0x0000A900
		public int sourceCount
		{
			get
			{
				return PositionConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000C718 File Offset: 0x0000A918
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static int GetSourceCountInternal([NotNull] PositionConstraint self)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			return PositionConstraint.GetSourceCountInternal_Injected(intPtr);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000C750 File Offset: 0x0000A950
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public void GetSources([NotNull] List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				ThrowHelper.ThrowArgumentNullException(sources, "sources");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PositionConstraint.GetSources_Injected(intPtr, sources);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000C784 File Offset: 0x0000A984
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			PositionConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static void SetSourcesInternal([NotNull] PositionConstraint self, List<ConstraintSource> sources)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			PositionConstraint.SetSourcesInternal_Injected(intPtr, sources);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public int AddSource(ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PositionConstraint.AddSource_Injected(intPtr, ref source);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000C80C File Offset: 0x0000AA0C
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000C820 File Offset: 0x0000AA20
		[NativeName("RemoveSource")]
		private void RemoveSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PositionConstraint.RemoveSourceInternal_Injected(intPtr, index);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000C844 File Offset: 0x0000AA44
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000C868 File Offset: 0x0000AA68
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ConstraintSource constraintSource;
			PositionConstraint.GetSourceInternal_Injected(intPtr, index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000C88E File Offset: 0x0000AA8E
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PositionConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PositionConstraint.SetSourceInternal_Injected(intPtr, index, ref source);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000C8CC File Offset: 0x0000AACC
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The PositionConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060006D0 RID: 1744
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_weight_Injected(IntPtr _unity_self);

		// Token: 0x060006D1 RID: 1745
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_weight_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006D2 RID: 1746
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_translationAtRest_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060006D3 RID: 1747
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_translationAtRest_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060006D4 RID: 1748
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_translationOffset_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060006D5 RID: 1749
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_translationOffset_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060006D6 RID: 1750
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Axis get_translationAxis_Injected(IntPtr _unity_self);

		// Token: 0x060006D7 RID: 1751
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_translationAxis_Injected(IntPtr _unity_self, Axis value);

		// Token: 0x060006D8 RID: 1752
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_constraintActive_Injected(IntPtr _unity_self);

		// Token: 0x060006D9 RID: 1753
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_constraintActive_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006DA RID: 1754
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_locked_Injected(IntPtr _unity_self);

		// Token: 0x060006DB RID: 1755
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_locked_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006DC RID: 1756
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal_Injected(IntPtr self);

		// Token: 0x060006DD RID: 1757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSources_Injected(IntPtr _unity_self, List<ConstraintSource> sources);

		// Token: 0x060006DE RID: 1758
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal_Injected(IntPtr self, List<ConstraintSource> sources);

		// Token: 0x060006DF RID: 1759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddSource_Injected(IntPtr _unity_self, [In] ref ConstraintSource source);

		// Token: 0x060006E0 RID: 1760
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveSourceInternal_Injected(IntPtr _unity_self, int index);

		// Token: 0x060006E1 RID: 1761
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSourceInternal_Injected(IntPtr _unity_self, int index, out ConstraintSource ret);

		// Token: 0x060006E2 RID: 1762
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourceInternal_Injected(IntPtr _unity_self, int index, [In] ref ConstraintSource source);
	}
}
