using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements
{
	// Token: 0x020005FA RID: 1530
	[NativeType(Header = "Modules/UIElements/VisualNodeClassNameStore.h")]
	internal class VisualNodeClassNameStore : IDisposable
	{
		// Token: 0x06002D9C RID: 11676 RVA: 0x000A2154 File Offset: 0x000A0354
		public VisualNodeClassNameStore()
			: this(VisualNodeClassNameStore.Internal_Create(), false)
		{
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000A2164 File Offset: 0x000A0364
		private VisualNodeClassNameStore(IntPtr ptr, bool isWrapper)
		{
			this.m_Ptr = ptr;
			this.m_IsWrapper = isWrapper;
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000A2198 File Offset: 0x000A0398
		~VisualNodeClassNameStore()
		{
			this.Dispose(false);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000A21CC File Offset: 0x000A03CC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000A21E0 File Offset: 0x000A03E0
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				bool flag2 = !this.m_IsWrapper;
				if (flag2)
				{
					VisualNodeClassNameStore.Internal_Destroy(this.m_Ptr);
				}
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000A2228 File Offset: 0x000A0428
		public string GetClassNameManaged(int id)
		{
			int i = this.m_ClassNames.Length;
			bool flag = (ulong)id < (ulong)((long)i);
			if (flag)
			{
				bool flag2 = !string.IsNullOrEmpty(this.m_ClassNames[id]);
				if (flag2)
				{
					return this.m_ClassNames[id];
				}
			}
			else
			{
				while (i <= id)
				{
					i *= 2;
				}
				Array.Resize<string>(ref this.m_ClassNames, i);
			}
			string className = this.GetClassName(id);
			this.m_ClassNames[id] = className;
			return className;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000A22A8 File Offset: 0x000A04A8
		public int GetClassNameIdManaged(string className)
		{
			int classNameId;
			bool flag = this.m_Map.TryGetValue(className, out classNameId);
			int num;
			if (flag)
			{
				num = classNameId;
			}
			else
			{
				classNameId = this.GetClassNameId(className);
				this.m_Map.Add(className, classNameId);
				num = classNameId;
			}
			return num;
		}

		// Token: 0x06002DA3 RID: 11683
		[FreeFunction("VisualNodeClassNameStore::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x06002DA4 RID: 11684
		[FreeFunction("VisualNodeClassNameStore::Destroy")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000A22E8 File Offset: 0x000A04E8
		[NativeThrows]
		internal unsafe int Insert(string className)
		{
			int num;
			try
			{
				IntPtr intPtr = VisualNodeClassNameStore.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(className, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = className.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = VisualNodeClassNameStore.Insert_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000A2350 File Offset: 0x000A0550
		[NativeThrows]
		internal unsafe int GetClassNameId(string className)
		{
			int classNameId_Injected;
			try
			{
				IntPtr intPtr = VisualNodeClassNameStore.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(className, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = className.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				classNameId_Injected = VisualNodeClassNameStore.GetClassNameId_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return classNameId_Injected;
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000A23B8 File Offset: 0x000A05B8
		[NativeThrows]
		internal string GetClassName(int id)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = VisualNodeClassNameStore.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				VisualNodeClassNameStore.GetClassName_Injected(intPtr, id, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06002DA8 RID: 11688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Insert_Injected(IntPtr _unity_self, ref ManagedSpanWrapper className);

		// Token: 0x06002DA9 RID: 11689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetClassNameId_Injected(IntPtr _unity_self, ref ManagedSpanWrapper className);

		// Token: 0x06002DAA RID: 11690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetClassName_Injected(IntPtr _unity_self, int id, out ManagedSpanWrapper ret);

		// Token: 0x04001495 RID: 5269
		[RequiredByNativeCode]
		private IntPtr m_Ptr;

		// Token: 0x04001496 RID: 5270
		[RequiredByNativeCode]
		private bool m_IsWrapper;

		// Token: 0x04001497 RID: 5271
		private string[] m_ClassNames = new string[512];

		// Token: 0x04001498 RID: 5272
		private Dictionary<string, int> m_Map = new Dictionary<string, int>();

		// Token: 0x020005FB RID: 1531
		[UsedImplicitly]
		internal static class BindingsMarshaller
		{
			// Token: 0x06002DAB RID: 11691 RVA: 0x000A23F8 File Offset: 0x000A05F8
			public static IntPtr ConvertToNative(VisualNodeClassNameStore store)
			{
				return store.m_Ptr;
			}

			// Token: 0x06002DAC RID: 11692 RVA: 0x000A2400 File Offset: 0x000A0600
			public static VisualNodeClassNameStore ConvertToManaged(IntPtr ptr)
			{
				return new VisualNodeClassNameStore(ptr, true);
			}
		}
	}
}
