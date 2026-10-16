using System;

namespace HarmonyLib
{
	// Token: 0x0200004D RID: 77
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = true)]
	public class HarmonyDelegate : HarmonyPatch
	{
		// Token: 0x06000176 RID: 374 RVA: 0x0000AE45 File Offset: 0x00009045
		public HarmonyDelegate(Type declaringType)
			: base(declaringType)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000AE4E File Offset: 0x0000904E
		public HarmonyDelegate(Type declaringType, Type[] argumentTypes)
			: base(declaringType, argumentTypes)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000AE58 File Offset: 0x00009058
		public HarmonyDelegate(Type declaringType, string methodName)
			: base(declaringType, methodName)
		{
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000AE62 File Offset: 0x00009062
		public HarmonyDelegate(Type declaringType, string methodName, params Type[] argumentTypes)
			: base(declaringType, methodName, argumentTypes)
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000AE6D File Offset: 0x0000906D
		public HarmonyDelegate(Type declaringType, string methodName, Type[] argumentTypes, ArgumentType[] argumentVariations)
			: base(declaringType, methodName, argumentTypes, argumentVariations)
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000AE7A File Offset: 0x0000907A
		public HarmonyDelegate(Type declaringType, MethodDispatchType methodDispatchType)
			: base(declaringType, MethodType.Normal)
		{
			this.info.nonVirtualDelegate = methodDispatchType == MethodDispatchType.Call;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000AE93 File Offset: 0x00009093
		public HarmonyDelegate(Type declaringType, MethodDispatchType methodDispatchType, params Type[] argumentTypes)
			: base(declaringType, MethodType.Normal, argumentTypes)
		{
			this.info.nonVirtualDelegate = methodDispatchType == MethodDispatchType.Call;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000AEAD File Offset: 0x000090AD
		public HarmonyDelegate(Type declaringType, MethodDispatchType methodDispatchType, Type[] argumentTypes, ArgumentType[] argumentVariations)
			: base(declaringType, MethodType.Normal, argumentTypes, argumentVariations)
		{
			this.info.nonVirtualDelegate = methodDispatchType == MethodDispatchType.Call;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000AEC9 File Offset: 0x000090C9
		public HarmonyDelegate(Type declaringType, string methodName, MethodDispatchType methodDispatchType)
			: base(declaringType, methodName, MethodType.Normal)
		{
			this.info.nonVirtualDelegate = methodDispatchType == MethodDispatchType.Call;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000AEE3 File Offset: 0x000090E3
		public HarmonyDelegate(string methodName)
			: base(methodName)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000AEEC File Offset: 0x000090EC
		public HarmonyDelegate(string methodName, params Type[] argumentTypes)
			: base(methodName, argumentTypes)
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000AEF6 File Offset: 0x000090F6
		public HarmonyDelegate(string methodName, Type[] argumentTypes, ArgumentType[] argumentVariations)
			: base(methodName, argumentTypes, argumentVariations)
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000AF01 File Offset: 0x00009101
		public HarmonyDelegate(string methodName, MethodDispatchType methodDispatchType)
			: base(methodName, MethodType.Normal)
		{
			this.info.nonVirtualDelegate = methodDispatchType == MethodDispatchType.Call;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000AF1A File Offset: 0x0000911A
		public HarmonyDelegate(MethodDispatchType methodDispatchType)
		{
			this.info.nonVirtualDelegate = methodDispatchType == MethodDispatchType.Call;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000AF31 File Offset: 0x00009131
		public HarmonyDelegate(MethodDispatchType methodDispatchType, params Type[] argumentTypes)
			: base(MethodType.Normal, argumentTypes)
		{
			this.info.nonVirtualDelegate = methodDispatchType == MethodDispatchType.Call;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000AF4A File Offset: 0x0000914A
		public HarmonyDelegate(MethodDispatchType methodDispatchType, Type[] argumentTypes, ArgumentType[] argumentVariations)
			: base(MethodType.Normal, argumentTypes, argumentVariations)
		{
			this.info.nonVirtualDelegate = methodDispatchType == MethodDispatchType.Call;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000AF64 File Offset: 0x00009164
		public HarmonyDelegate(Type[] argumentTypes)
			: base(argumentTypes)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000AF6D File Offset: 0x0000916D
		public HarmonyDelegate(Type[] argumentTypes, ArgumentType[] argumentVariations)
			: base(argumentTypes, argumentVariations)
		{
		}
	}
}
