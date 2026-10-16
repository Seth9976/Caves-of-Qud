using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020002D4 RID: 724
	public sealed class ShaderVariantCollection : Object
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x00044E2C File Offset: 0x0004302C
		public int shaderCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ShaderVariantCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ShaderVariantCollection.get_shaderCount_Injected(intPtr);
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x00044E50 File Offset: 0x00043050
		public int variantCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ShaderVariantCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ShaderVariantCollection.get_variantCount_Injected(intPtr);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x00044E74 File Offset: 0x00043074
		public int warmedUpVariantCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ShaderVariantCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ShaderVariantCollection.get_warmedUpVariantCount_Injected(intPtr);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x00044E98 File Offset: 0x00043098
		public bool isWarmedUp
		{
			[NativeName("IsWarmedUp")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ShaderVariantCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ShaderVariantCollection.get_isWarmedUp_Injected(intPtr);
			}
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00044EBC File Offset: 0x000430BC
		private bool AddVariant(Shader shader, PassType passType, [Unmarshalled] string[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ShaderVariantCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ShaderVariantCollection.AddVariant_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(shader), passType, keywords);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00044EE8 File Offset: 0x000430E8
		private bool RemoveVariant(Shader shader, PassType passType, [Unmarshalled] string[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ShaderVariantCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ShaderVariantCollection.RemoveVariant_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(shader), passType, keywords);
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00044F14 File Offset: 0x00043114
		private bool ContainsVariant(Shader shader, PassType passType, [Unmarshalled] string[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ShaderVariantCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ShaderVariantCollection.ContainsVariant_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(shader), passType, keywords);
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x00044F40 File Offset: 0x00043140
		[NativeName("ClearVariants")]
		public void Clear()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ShaderVariantCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ShaderVariantCollection.Clear_Injected(intPtr);
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00044F64 File Offset: 0x00043164
		[NativeName("WarmupShaders")]
		public void WarmUp()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ShaderVariantCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ShaderVariantCollection.WarmUp_Injected(intPtr);
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00044F88 File Offset: 0x00043188
		[NativeName("WarmupShadersProgressively")]
		public bool WarmUpProgressively(int variantCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ShaderVariantCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ShaderVariantCollection.WarmUpProgressively_Injected(intPtr, variantCount);
		}

		// Token: 0x06002334 RID: 9012
		[NativeName("CreateFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] ShaderVariantCollection svc);

		// Token: 0x06002335 RID: 9013 RVA: 0x00044FAB File Offset: 0x000431AB
		public ShaderVariantCollection()
		{
			ShaderVariantCollection.Internal_Create(this);
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00044FBC File Offset: 0x000431BC
		public bool Add(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.AddVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x00044FE8 File Offset: 0x000431E8
		public bool Remove(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.RemoveVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x00045014 File Offset: 0x00043214
		public bool Contains(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.ContainsVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x06002339 RID: 9017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_shaderCount_Injected(IntPtr _unity_self);

		// Token: 0x0600233A RID: 9018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_variantCount_Injected(IntPtr _unity_self);

		// Token: 0x0600233B RID: 9019
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_warmedUpVariantCount_Injected(IntPtr _unity_self);

		// Token: 0x0600233C RID: 9020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isWarmedUp_Injected(IntPtr _unity_self);

		// Token: 0x0600233D RID: 9021
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddVariant_Injected(IntPtr _unity_self, IntPtr shader, PassType passType, string[] keywords);

		// Token: 0x0600233E RID: 9022
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveVariant_Injected(IntPtr _unity_self, IntPtr shader, PassType passType, string[] keywords);

		// Token: 0x0600233F RID: 9023
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ContainsVariant_Injected(IntPtr _unity_self, IntPtr shader, PassType passType, string[] keywords);

		// Token: 0x06002340 RID: 9024
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clear_Injected(IntPtr _unity_self);

		// Token: 0x06002341 RID: 9025
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WarmUp_Injected(IntPtr _unity_self);

		// Token: 0x06002342 RID: 9026
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool WarmUpProgressively_Injected(IntPtr _unity_self, int variantCount);

		// Token: 0x020002D5 RID: 725
		public struct ShaderVariant
		{
			// Token: 0x06002343 RID: 9027 RVA: 0x00045040 File Offset: 0x00043240
			[FreeFunction]
			[NativeConditional("UNITY_EDITOR")]
			private static string CheckShaderVariant(Shader shader, PassType passType, string[] keywords)
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					ShaderVariantCollection.ShaderVariant.CheckShaderVariant_Injected(Object.MarshalledUnityObject.Marshal<Shader>(shader), passType, keywords, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}

			// Token: 0x06002344 RID: 9028 RVA: 0x00045078 File Offset: 0x00043278
			public ShaderVariant(Shader shader, PassType passType, params string[] keywords)
			{
				this.shader = shader;
				this.passType = passType;
				this.keywords = keywords;
			}

			// Token: 0x06002345 RID: 9029
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void CheckShaderVariant_Injected(IntPtr shader, PassType passType, string[] keywords, out ManagedSpanWrapper ret);

			// Token: 0x04000A23 RID: 2595
			public Shader shader;

			// Token: 0x04000A24 RID: 2596
			public PassType passType;

			// Token: 0x04000A25 RID: 2597
			public string[] keywords;
		}
	}
}
