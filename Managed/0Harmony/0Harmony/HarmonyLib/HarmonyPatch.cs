using System;
using System.Collections.Generic;

namespace HarmonyLib
{
	// Token: 0x0200004C RID: 76
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true)]
	public class HarmonyPatch : HarmonyAttribute
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000AB1C File Offset: 0x00008D1C
		public HarmonyPatch()
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000AB24 File Offset: 0x00008D24
		public HarmonyPatch(Type declaringType)
		{
			this.info.declaringType = declaringType;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000AB38 File Offset: 0x00008D38
		public HarmonyPatch(Type declaringType, Type[] argumentTypes)
		{
			this.info.declaringType = declaringType;
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000AB58 File Offset: 0x00008D58
		public HarmonyPatch(Type declaringType, string methodName)
		{
			this.info.declaringType = declaringType;
			this.info.methodName = methodName;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000AB78 File Offset: 0x00008D78
		public HarmonyPatch(Type declaringType, string methodName, params Type[] argumentTypes)
		{
			this.info.declaringType = declaringType;
			this.info.methodName = methodName;
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		public HarmonyPatch(Type declaringType, string methodName, Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			this.info.declaringType = declaringType;
			this.info.methodName = methodName;
			this.ParseSpecialArguments(argumentTypes, argumentVariations);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000ABCD File Offset: 0x00008DCD
		public HarmonyPatch(Type declaringType, MethodType methodType)
		{
			this.info.declaringType = declaringType;
			this.info.methodType = new MethodType?(methodType);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000ABF2 File Offset: 0x00008DF2
		public HarmonyPatch(Type declaringType, MethodType methodType, params Type[] argumentTypes)
		{
			this.info.declaringType = declaringType;
			this.info.methodType = new MethodType?(methodType);
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000AC23 File Offset: 0x00008E23
		public HarmonyPatch(Type declaringType, MethodType methodType, Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			this.info.declaringType = declaringType;
			this.info.methodType = new MethodType?(methodType);
			this.ParseSpecialArguments(argumentTypes, argumentVariations);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000AC51 File Offset: 0x00008E51
		public HarmonyPatch(Type declaringType, string methodName, MethodType methodType)
		{
			this.info.declaringType = declaringType;
			this.info.methodName = methodName;
			this.info.methodType = new MethodType?(methodType);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000AC82 File Offset: 0x00008E82
		public HarmonyPatch(string methodName)
		{
			this.info.methodName = methodName;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000AC96 File Offset: 0x00008E96
		public HarmonyPatch(string methodName, params Type[] argumentTypes)
		{
			this.info.methodName = methodName;
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000ACB6 File Offset: 0x00008EB6
		public HarmonyPatch(string methodName, Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			this.info.methodName = methodName;
			this.ParseSpecialArguments(argumentTypes, argumentVariations);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000ACD2 File Offset: 0x00008ED2
		public HarmonyPatch(string methodName, MethodType methodType)
		{
			this.info.methodName = methodName;
			this.info.methodType = new MethodType?(methodType);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000ACF7 File Offset: 0x00008EF7
		public HarmonyPatch(MethodType methodType)
		{
			this.info.methodType = new MethodType?(methodType);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000AD10 File Offset: 0x00008F10
		public HarmonyPatch(MethodType methodType, params Type[] argumentTypes)
		{
			this.info.methodType = new MethodType?(methodType);
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000AD35 File Offset: 0x00008F35
		public HarmonyPatch(MethodType methodType, Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			this.info.methodType = new MethodType?(methodType);
			this.ParseSpecialArguments(argumentTypes, argumentVariations);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000AD56 File Offset: 0x00008F56
		public HarmonyPatch(Type[] argumentTypes)
		{
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000AD6A File Offset: 0x00008F6A
		public HarmonyPatch(Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			this.ParseSpecialArguments(argumentTypes, argumentVariations);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000AD7A File Offset: 0x00008F7A
		public HarmonyPatch(string typeName, string methodName, MethodType methodType = MethodType.Normal)
		{
			this.info.declaringType = AccessTools.TypeByName(typeName);
			this.info.methodName = methodName;
			this.info.methodType = new MethodType?(methodType);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		private void ParseSpecialArguments(Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			if (argumentVariations == null || argumentVariations.Length == 0)
			{
				this.info.argumentTypes = argumentTypes;
				return;
			}
			if (argumentTypes.Length < argumentVariations.Length)
			{
				throw new ArgumentException("argumentVariations contains more elements than argumentTypes", "argumentVariations");
			}
			List<Type> list = new List<Type>();
			for (int i = 0; i < argumentTypes.Length; i++)
			{
				Type type = argumentTypes[i];
				switch (argumentVariations[i])
				{
				case ArgumentType.Ref:
				case ArgumentType.Out:
					type = type.MakeByRefType();
					break;
				case ArgumentType.Pointer:
					type = type.MakePointerType();
					break;
				}
				list.Add(type);
			}
			this.info.argumentTypes = list.ToArray();
		}
	}
}
