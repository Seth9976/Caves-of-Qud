using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000077 RID: 119
	[RequireComponent(typeof(Transform))]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[NativeHeader("Modules/Animation/Constraints/ParentConstraint.h")]
	public sealed class ParentConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x0000DE80 File Offset: 0x0000C080
		private ParentConstraint()
		{
			ParentConstraint.Internal_Create(this);
		}

		// Token: 0x060007AF RID: 1967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] ParentConstraint self);

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0000DE94 File Offset: 0x0000C094
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public float weight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParentConstraint.get_weight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParentConstraint.set_weight_Injected(intPtr, value);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0000DF00 File Offset: 0x0000C100
		public bool constraintActive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParentConstraint.get_constraintActive_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParentConstraint.set_constraintActive_Injected(intPtr, value);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0000DF24 File Offset: 0x0000C124
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0000DF48 File Offset: 0x0000C148
		public bool locked
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParentConstraint.get_locked_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParentConstraint.set_locked_Injected(intPtr, value);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0000DF6C File Offset: 0x0000C16C
		public int sourceCount
		{
			get
			{
				return ParentConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000DF84 File Offset: 0x0000C184
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static int GetSourceCountInternal([NotNull] ParentConstraint self)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			return ParentConstraint.GetSourceCountInternal_Injected(intPtr);
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		public Vector3 translationAtRest
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ParentConstraint.get_translationAtRest_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParentConstraint.set_translationAtRest_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0000E008 File Offset: 0x0000C208
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0000E030 File Offset: 0x0000C230
		public Vector3 rotationAtRest
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ParentConstraint.get_rotationAtRest_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParentConstraint.set_rotationAtRest_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0000E054 File Offset: 0x0000C254
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0000E098 File Offset: 0x0000C298
		public unsafe Vector3[] translationOffsets
		{
			get
			{
				Vector3[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					ParentConstraint.get_translationOffsets_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Vector3[] array;
					blittableArrayWrapper.Unmarshal<Vector3>(ref array);
					array2 = array;
				}
				return array2;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<Vector3> span = new Span<Vector3>(value);
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					ParentConstraint.set_translationOffsets_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0000E124 File Offset: 0x0000C324
		public unsafe Vector3[] rotationOffsets
		{
			get
			{
				Vector3[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					ParentConstraint.get_rotationOffsets_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Vector3[] array;
					blittableArrayWrapper.Unmarshal<Vector3>(ref array);
					array2 = array;
				}
				return array2;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<Vector3> span = new Span<Vector3>(value);
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					ParentConstraint.set_rotationOffsets_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0000E16C File Offset: 0x0000C36C
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0000E190 File Offset: 0x0000C390
		public Axis translationAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParentConstraint.get_translationAxis_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParentConstraint.set_translationAxis_Injected(intPtr, value);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		public Axis rotationAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParentConstraint.get_rotationAxis_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParentConstraint.set_rotationAxis_Injected(intPtr, value);
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		public Vector3 GetTranslationOffset(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetTranslationOffsetInternal(index);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0000E21D File Offset: 0x0000C41D
		public void SetTranslationOffset(int index, Vector3 value)
		{
			this.ValidateSourceIndex(index);
			this.SetTranslationOffsetInternal(index, value);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0000E234 File Offset: 0x0000C434
		[NativeName("GetTranslationOffset")]
		private Vector3 GetTranslationOffsetInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			ParentConstraint.GetTranslationOffsetInternal_Injected(intPtr, index, out vector);
			return vector;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0000E25C File Offset: 0x0000C45C
		[NativeName("SetTranslationOffset")]
		private void SetTranslationOffsetInternal(int index, Vector3 value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParentConstraint.SetTranslationOffsetInternal_Injected(intPtr, index, ref value);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0000E284 File Offset: 0x0000C484
		public Vector3 GetRotationOffset(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetRotationOffsetInternal(index);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0000E2A5 File Offset: 0x0000C4A5
		public void SetRotationOffset(int index, Vector3 value)
		{
			this.ValidateSourceIndex(index);
			this.SetRotationOffsetInternal(index, value);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		[NativeName("GetRotationOffset")]
		private Vector3 GetRotationOffsetInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			ParentConstraint.GetRotationOffsetInternal_Injected(intPtr, index, out vector);
			return vector;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		[NativeName("SetRotationOffset")]
		private void SetRotationOffsetInternal(int index, Vector3 value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParentConstraint.SetRotationOffsetInternal_Injected(intPtr, index, ref value);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0000E30C File Offset: 0x0000C50C
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The ParentConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0000E374 File Offset: 0x0000C574
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public void GetSources([NotNull] List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				ThrowHelper.ThrowArgumentNullException(sources, "sources");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParentConstraint.GetSources_Injected(intPtr, sources);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			ParentConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static void SetSourcesInternal([NotNull] ParentConstraint self, List<ConstraintSource> sources)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			ParentConstraint.SetSourcesInternal_Injected(intPtr, sources);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0000E40C File Offset: 0x0000C60C
		public int AddSource(ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ParentConstraint.AddSource_Injected(intPtr, ref source);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0000E430 File Offset: 0x0000C630
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0000E444 File Offset: 0x0000C644
		[NativeName("RemoveSource")]
		private void RemoveSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParentConstraint.RemoveSourceInternal_Injected(intPtr, index);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0000E468 File Offset: 0x0000C668
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0000E48C File Offset: 0x0000C68C
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ConstraintSource constraintSource;
			ParentConstraint.GetSourceInternal_Injected(intPtr, index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000E4B2 File Offset: 0x0000C6B2
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParentConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParentConstraint.SetSourceInternal_Injected(intPtr, index, ref source);
		}

		// Token: 0x060007D7 RID: 2007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_weight_Injected(IntPtr _unity_self);

		// Token: 0x060007D8 RID: 2008
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_weight_Injected(IntPtr _unity_self, float value);

		// Token: 0x060007D9 RID: 2009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_constraintActive_Injected(IntPtr _unity_self);

		// Token: 0x060007DA RID: 2010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_constraintActive_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060007DB RID: 2011
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_locked_Injected(IntPtr _unity_self);

		// Token: 0x060007DC RID: 2012
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_locked_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060007DD RID: 2013
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal_Injected(IntPtr self);

		// Token: 0x060007DE RID: 2014
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_translationAtRest_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060007DF RID: 2015
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_translationAtRest_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060007E0 RID: 2016
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationAtRest_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060007E1 RID: 2017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationAtRest_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060007E2 RID: 2018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_translationOffsets_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060007E3 RID: 2019
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_translationOffsets_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060007E4 RID: 2020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationOffsets_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060007E5 RID: 2021
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationOffsets_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060007E6 RID: 2022
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Axis get_translationAxis_Injected(IntPtr _unity_self);

		// Token: 0x060007E7 RID: 2023
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_translationAxis_Injected(IntPtr _unity_self, Axis value);

		// Token: 0x060007E8 RID: 2024
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Axis get_rotationAxis_Injected(IntPtr _unity_self);

		// Token: 0x060007E9 RID: 2025
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationAxis_Injected(IntPtr _unity_self, Axis value);

		// Token: 0x060007EA RID: 2026
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTranslationOffsetInternal_Injected(IntPtr _unity_self, int index, out Vector3 ret);

		// Token: 0x060007EB RID: 2027
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTranslationOffsetInternal_Injected(IntPtr _unity_self, int index, [In] ref Vector3 value);

		// Token: 0x060007EC RID: 2028
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotationOffsetInternal_Injected(IntPtr _unity_self, int index, out Vector3 ret);

		// Token: 0x060007ED RID: 2029
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotationOffsetInternal_Injected(IntPtr _unity_self, int index, [In] ref Vector3 value);

		// Token: 0x060007EE RID: 2030
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSources_Injected(IntPtr _unity_self, List<ConstraintSource> sources);

		// Token: 0x060007EF RID: 2031
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal_Injected(IntPtr self, List<ConstraintSource> sources);

		// Token: 0x060007F0 RID: 2032
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddSource_Injected(IntPtr _unity_self, [In] ref ConstraintSource source);

		// Token: 0x060007F1 RID: 2033
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveSourceInternal_Injected(IntPtr _unity_self, int index);

		// Token: 0x060007F2 RID: 2034
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSourceInternal_Injected(IntPtr _unity_self, int index, out ConstraintSource ret);

		// Token: 0x060007F3 RID: 2035
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourceInternal_Injected(IntPtr _unity_self, int index, [In] ref ConstraintSource source);
	}
}
