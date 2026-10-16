using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000243 RID: 579
	[NativeClass("RenderingLayerMask", "struct RenderingLayerMask;")]
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Runtime/Graphics/RenderingLayerMask.h")]
	public struct RenderingLayerMask
	{
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x0003B0CA File Offset: 0x000392CA
		public static RenderingLayerMask defaultRenderingLayerMask { get; } = new RenderingLayerMask
		{
			m_Bits = 1U
		};

		// Token: 0x06001EAC RID: 7852 RVA: 0x0003B0D4 File Offset: 0x000392D4
		public static implicit operator uint(RenderingLayerMask mask)
		{
			return mask.m_Bits;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0003B0EC File Offset: 0x000392EC
		public static implicit operator RenderingLayerMask(uint intVal)
		{
			RenderingLayerMask renderingLayerMask;
			renderingLayerMask.m_Bits = intVal;
			return renderingLayerMask;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0003B108 File Offset: 0x00039308
		public static implicit operator int(RenderingLayerMask mask)
		{
			return (int)mask.m_Bits;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0003B120 File Offset: 0x00039320
		public static implicit operator RenderingLayerMask(int intVal)
		{
			RenderingLayerMask renderingLayerMask;
			renderingLayerMask.m_Bits = (uint)intVal;
			return renderingLayerMask;
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x0003B13B File Offset: 0x0003933B
		// (set) Token: 0x06001EB1 RID: 7857 RVA: 0x0003B143 File Offset: 0x00039343
		public uint value
		{
			get
			{
				return this.m_Bits;
			}
			set
			{
				this.m_Bits = value;
			}
		}

		// Token: 0x06001EB2 RID: 7858
		[NativeMethod("GetDefaultRenderingLayerValue")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint Internal_GetDefaultRenderingLayerValue();

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0003B14C File Offset: 0x0003934C
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[NativeMethod("RenderingLayerToString")]
		public static string RenderingLayerToName(int layer)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				RenderingLayerMask.RenderingLayerToName_Injected(layer, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0003B17C File Offset: 0x0003937C
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[NativeMethod("StringToRenderingLayer")]
		public unsafe static int NameToRenderingLayer(string layerName)
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
				num = RenderingLayerMask.NameToRenderingLayer_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0003B1D4 File Offset: 0x000393D4
		public static uint GetMask(params string[] renderingLayerNames)
		{
			bool flag = renderingLayerNames == null;
			if (flag)
			{
				throw new ArgumentNullException("renderingLayerNames");
			}
			uint num = 0U;
			for (int i = 0; i < renderingLayerNames.Length; i++)
			{
				int num2 = RenderingLayerMask.NameToRenderingLayer(renderingLayerNames[i]);
				bool flag2 = num2 != -1;
				if (flag2)
				{
					num |= 1U << num2;
				}
			}
			return num;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0003B234 File Offset: 0x00039434
		public unsafe static uint GetMask(ReadOnlySpan<string> renderingLayerNames)
		{
			bool flag = renderingLayerNames == null;
			if (flag)
			{
				throw new ArgumentNullException("renderingLayerNames");
			}
			uint num = 0U;
			ReadOnlySpan<string> readOnlySpan = renderingLayerNames;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				string text = *readOnlySpan[i];
				int num2 = RenderingLayerMask.NameToRenderingLayer(text);
				bool flag2 = num2 != -1;
				if (flag2)
				{
					num |= 1U << num2;
				}
			}
			return num;
		}

		// Token: 0x06001EB7 RID: 7863
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetDefinedRenderingLayerCount();

		// Token: 0x06001EB8 RID: 7864
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLastDefinedRenderingLayerIndex();

		// Token: 0x06001EB9 RID: 7865
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetDefinedRenderingLayersCombinedMaskValue();

		// Token: 0x06001EBA RID: 7866
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetDefinedRenderingLayerNames();

		// Token: 0x06001EBB RID: 7867 RVA: 0x0003B2AC File Offset: 0x000394AC
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		public static int[] GetDefinedRenderingLayerValues()
		{
			int[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RenderingLayerMask.GetDefinedRenderingLayerValues_Injected(out blittableArrayWrapper);
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

		// Token: 0x06001EBC RID: 7868
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetRenderingLayerCount();

		// Token: 0x06001EBE RID: 7870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RenderingLayerToName_Injected(int layer, out ManagedSpanWrapper ret);

		// Token: 0x06001EBF RID: 7871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int NameToRenderingLayer_Injected(ref ManagedSpanWrapper layerName);

		// Token: 0x06001EC0 RID: 7872
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDefinedRenderingLayerValues_Injected(out BlittableArrayWrapper ret);

		// Token: 0x04000918 RID: 2328
		[NativeName("m_Bits")]
		private uint m_Bits;

		// Token: 0x0400091A RID: 2330
		internal const int maxRenderingLayerSize = 32;
	}
}
