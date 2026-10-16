using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000075 RID: 117
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[NativeHeader("Modules/Animation/Constraints/LookAtConstraint.h")]
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	public sealed class LookAtConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x0000D832 File Offset: 0x0000BA32
		private LookAtConstraint()
		{
			LookAtConstraint.Internal_Create(this);
		}

		// Token: 0x06000769 RID: 1897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] LookAtConstraint self);

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0000D844 File Offset: 0x0000BA44
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0000D868 File Offset: 0x0000BA68
		public float weight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LookAtConstraint.get_weight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LookAtConstraint.set_weight_Injected(intPtr, value);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0000D88C File Offset: 0x0000BA8C
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		public float roll
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LookAtConstraint.get_roll_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LookAtConstraint.set_roll_Injected(intPtr, value);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		public bool constraintActive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LookAtConstraint.get_constraintActive_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LookAtConstraint.set_constraintActive_Injected(intPtr, value);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0000D91C File Offset: 0x0000BB1C
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0000D940 File Offset: 0x0000BB40
		public bool locked
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LookAtConstraint.get_locked_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LookAtConstraint.set_locked_Injected(intPtr, value);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0000D964 File Offset: 0x0000BB64
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0000D98C File Offset: 0x0000BB8C
		public Vector3 rotationAtRest
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				LookAtConstraint.get_rotationAtRest_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LookAtConstraint.set_rotationAtRest_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0000D9B0 File Offset: 0x0000BBB0
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
		public Vector3 rotationOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				LookAtConstraint.get_rotationOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LookAtConstraint.set_rotationOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x0000DA24 File Offset: 0x0000BC24
		public Transform worldUpObject
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(LookAtConstraint.get_worldUpObject_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LookAtConstraint.set_worldUpObject_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(value));
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x0000DA70 File Offset: 0x0000BC70
		public bool useUpObject
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LookAtConstraint.get_useUpObject_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LookAtConstraint.set_useUpObject_Injected(intPtr, value);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0000DA94 File Offset: 0x0000BC94
		public int sourceCount
		{
			get
			{
				return LookAtConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static int GetSourceCountInternal([NotNull] LookAtConstraint self)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			return LookAtConstraint.GetSourceCountInternal_Injected(intPtr);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public void GetSources([NotNull] List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				ThrowHelper.ThrowArgumentNullException(sources, "sources");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			LookAtConstraint.GetSources_Injected(intPtr, sources);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000DB18 File Offset: 0x0000BD18
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			LookAtConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000DB44 File Offset: 0x0000BD44
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static void SetSourcesInternal([NotNull] LookAtConstraint self, List<ConstraintSource> sources)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			LookAtConstraint.SetSourcesInternal_Injected(intPtr, sources);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		public int AddSource(ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return LookAtConstraint.AddSource_Injected(intPtr, ref source);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		[NativeName("RemoveSource")]
		private void RemoveSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			LookAtConstraint.RemoveSourceInternal_Injected(intPtr, index);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ConstraintSource constraintSource;
			LookAtConstraint.GetSourceInternal_Injected(intPtr, index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000DC22 File Offset: 0x0000BE22
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000DC38 File Offset: 0x0000BE38
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LookAtConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			LookAtConstraint.SetSourceInternal_Injected(intPtr, index, ref source);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000DC60 File Offset: 0x0000BE60
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The LookAtConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x06000787 RID: 1927
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_weight_Injected(IntPtr _unity_self);

		// Token: 0x06000788 RID: 1928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_weight_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000789 RID: 1929
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_roll_Injected(IntPtr _unity_self);

		// Token: 0x0600078A RID: 1930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_roll_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600078B RID: 1931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_constraintActive_Injected(IntPtr _unity_self);

		// Token: 0x0600078C RID: 1932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_constraintActive_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600078D RID: 1933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_locked_Injected(IntPtr _unity_self);

		// Token: 0x0600078E RID: 1934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_locked_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600078F RID: 1935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationAtRest_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000790 RID: 1936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationAtRest_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000791 RID: 1937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationOffset_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000792 RID: 1938
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationOffset_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000793 RID: 1939
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_worldUpObject_Injected(IntPtr _unity_self);

		// Token: 0x06000794 RID: 1940
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_worldUpObject_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000795 RID: 1941
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useUpObject_Injected(IntPtr _unity_self);

		// Token: 0x06000796 RID: 1942
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useUpObject_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000797 RID: 1943
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal_Injected(IntPtr self);

		// Token: 0x06000798 RID: 1944
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSources_Injected(IntPtr _unity_self, List<ConstraintSource> sources);

		// Token: 0x06000799 RID: 1945
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal_Injected(IntPtr self, List<ConstraintSource> sources);

		// Token: 0x0600079A RID: 1946
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddSource_Injected(IntPtr _unity_self, [In] ref ConstraintSource source);

		// Token: 0x0600079B RID: 1947
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveSourceInternal_Injected(IntPtr _unity_self, int index);

		// Token: 0x0600079C RID: 1948
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSourceInternal_Injected(IntPtr _unity_self, int index, out ConstraintSource ret);

		// Token: 0x0600079D RID: 1949
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourceInternal_Injected(IntPtr _unity_self, int index, [In] ref ConstraintSource source);
	}
}
