using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200029E RID: 670
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Runtime/BaseClasses/BitField.h")]
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	[NativeClass("BitField", "struct BitField;")]
	public struct LayerMask
	{
		// Token: 0x06002135 RID: 8501 RVA: 0x00040704 File Offset: 0x0003E904
		public static implicit operator int(LayerMask mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0004071C File Offset: 0x0003E91C
		public static implicit operator LayerMask(int intVal)
		{
			LayerMask layerMask;
			layerMask.m_Mask = intVal;
			return layerMask;
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x00040738 File Offset: 0x0003E938
		// (set) Token: 0x06002138 RID: 8504 RVA: 0x00040750 File Offset: 0x0003E950
		public int value
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0004075C File Offset: 0x0003E95C
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[NativeMethod("LayerToString")]
		public static string LayerToName(int layer)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				LayerMask.LayerToName_Injected(layer, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0004078C File Offset: 0x0003E98C
		[NativeMethod("StringToLayer")]
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		public unsafe static int NameToLayer(string layerName)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(layerName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = layerName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = LayerMask.NameToLayer_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000407E4 File Offset: 0x0003E9E4
		public static int GetMask(params string[] layerNames)
		{
			bool flag = layerNames == null;
			if (flag)
			{
				throw new ArgumentNullException("layerNames");
			}
			int num = 0;
			foreach (string text in layerNames)
			{
				int num2 = LayerMask.NameToLayer(text);
				bool flag2 = num2 != -1;
				if (flag2)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}

		// Token: 0x0600213C RID: 8508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LayerToName_Injected(int layer, out ManagedSpanWrapper ret);

		// Token: 0x0600213D RID: 8509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int NameToLayer_Injected(ref ManagedSpanWrapper layerName);

		// Token: 0x040009C2 RID: 2498
		[NativeName("m_Bits")]
		private int m_Mask;
	}
}
