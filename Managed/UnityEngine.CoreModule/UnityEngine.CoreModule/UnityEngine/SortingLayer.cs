using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000F9 RID: 249
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	public struct SortingLayer
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00009740 File Offset: 0x00007940
		public int id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00009758 File Offset: 0x00007958
		public string name
		{
			get
			{
				return SortingLayer.IDToName(this.m_Id);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00009778 File Offset: 0x00007978
		public int value
		{
			get
			{
				return SortingLayer.GetLayerValueFromID(this.m_Id);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00009798 File Offset: 0x00007998
		public static SortingLayer[] layers
		{
			get
			{
				int[] sortingLayerIDsInternal = SortingLayer.GetSortingLayerIDsInternal();
				SortingLayer[] array = new SortingLayer[sortingLayerIDsInternal.Length];
				for (int i = 0; i < sortingLayerIDsInternal.Length; i++)
				{
					array[i].m_Id = sortingLayerIDsInternal[i];
				}
				return array;
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000097E0 File Offset: 0x000079E0
		[FreeFunction("GetTagManager().GetSortingLayerIDs")]
		private static int[] GetSortingLayerIDsInternal()
		{
			int[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				SortingLayer.GetSortingLayerIDsInternal_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600051B RID: 1307
		[FreeFunction("GetTagManager().GetSortingLayerValueFromUniqueID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLayerValueFromID(int id);

		// Token: 0x0600051C RID: 1308 RVA: 0x00009814 File Offset: 0x00007A14
		[FreeFunction("GetTagManager().GetSortingLayerValueFromName")]
		public unsafe static int GetLayerValueFromName(string name)
		{
			int layerValueFromName_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				layerValueFromName_Injected = SortingLayer.GetLayerValueFromName_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return layerValueFromName_Injected;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000986C File Offset: 0x00007A6C
		[FreeFunction("GetTagManager().GetSortingLayerUniqueIDFromName")]
		public unsafe static int NameToID(string name)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = SortingLayer.NameToID_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000098C4 File Offset: 0x00007AC4
		[FreeFunction("GetTagManager().GetSortingLayerNameFromUniqueID")]
		public static string IDToName(int id)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SortingLayer.IDToName_Injected(id, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600051F RID: 1311
		[FreeFunction("GetTagManager().IsSortingLayerUniqueIDValid")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsValid(int id);

		// Token: 0x06000520 RID: 1312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSortingLayerIDsInternal_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000521 RID: 1313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerValueFromName_Injected(ref ManagedSpanWrapper name);

		// Token: 0x06000522 RID: 1314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int NameToID_Injected(ref ManagedSpanWrapper name);

		// Token: 0x06000523 RID: 1315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IDToName_Injected(int id, out ManagedSpanWrapper ret);

		// Token: 0x040002E6 RID: 742
		private int m_Id;

		// Token: 0x040002E7 RID: 743
		public static SortingLayer.LayerCallback onLayerAdded;

		// Token: 0x040002E8 RID: 744
		public static SortingLayer.LayerCallback onLayerRemoved;

		// Token: 0x040002E9 RID: 745
		internal static SortingLayer.LayerChangedCallback onLayerChanged;

		// Token: 0x020000FA RID: 250
		// (Invoke) Token: 0x06000525 RID: 1317
		public delegate void LayerCallback(SortingLayer layer);

		// Token: 0x020000FB RID: 251
		// (Invoke) Token: 0x06000529 RID: 1321
		internal delegate void LayerChangedCallback();
	}
}
