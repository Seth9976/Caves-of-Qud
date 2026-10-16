using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	internal class AndroidReflection
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00004C68 File Offset: 0x00002E68
		public static bool IsPrimitive(Type t)
		{
			return t.IsPrimitive;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004C80 File Offset: 0x00002E80
		public static bool IsAssignableFrom(Type t, Type from)
		{
			return t.IsAssignableFrom(from);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004C9C File Offset: 0x00002E9C
		private static IntPtr GetStaticMethodID(string clazz, string methodName, string signature)
		{
			IntPtr intPtr = AndroidJNISafe.FindClass(clazz);
			IntPtr staticMethodID;
			try
			{
				staticMethodID = AndroidJNISafe.GetStaticMethodID(intPtr, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return staticMethodID;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004CD8 File Offset: 0x00002ED8
		private static IntPtr GetMethodID(string clazz, string methodName, string signature)
		{
			IntPtr intPtr = AndroidJNISafe.FindClass(clazz);
			IntPtr methodID;
			try
			{
				methodID = AndroidJNISafe.GetMethodID(intPtr, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return methodID;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004D14 File Offset: 0x00002F14
		public static IntPtr GetConstructorMember(IntPtr jclass, string signature)
		{
			jvalue[] array = new jvalue[2];
			IntPtr intPtr;
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewString(signature);
				intPtr = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetConstructorID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
			}
			return intPtr;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004D88 File Offset: 0x00002F88
		public static IntPtr GetMethodMember(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			jvalue[] array = new jvalue[4];
			IntPtr intPtr;
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewString(methodName);
				array[2].l = AndroidJNISafe.NewString(signature);
				array[3].z = isStatic;
				intPtr = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetMethodID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
				AndroidJNISafe.DeleteLocalRef(array[2].l);
			}
			return intPtr;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004E2C File Offset: 0x0000302C
		public static IntPtr GetFieldMember(IntPtr jclass, string fieldName, string signature, bool isStatic)
		{
			jvalue[] array = new jvalue[4];
			IntPtr intPtr;
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewString(fieldName);
				array[2].l = AndroidJNISafe.NewString(signature);
				array[3].z = isStatic;
				intPtr = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetFieldID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
				AndroidJNISafe.DeleteLocalRef(array[2].l);
			}
			return intPtr;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004ED0 File Offset: 0x000030D0
		public static IntPtr GetFieldClass(IntPtr field)
		{
			return AndroidJNISafe.CallObjectMethod(field, AndroidReflection.s_FieldGetDeclaringClass, null);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004EF0 File Offset: 0x000030F0
		public static string GetFieldSignature(IntPtr field)
		{
			jvalue[] array = new jvalue[1];
			array[0].l = field;
			return AndroidJNISafe.CallStaticStringMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetFieldSignature, array);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004F2C File Offset: 0x0000312C
		public static IntPtr NewProxyInstance(IntPtr player, IntPtr delegateHandle, IntPtr interfaze)
		{
			jvalue[] array = new jvalue[3];
			array[0].l = player;
			array[1].j = delegateHandle.ToInt64();
			array[2].l = interfaze;
			return AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperNewProxyInstance, array);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004F88 File Offset: 0x00003188
		internal static IntPtr CreateInvocationError(Exception ex, bool methodNotFound)
		{
			jvalue[] array = new jvalue[2];
			array[0].j = GCHandle.ToIntPtr(GCHandle.Alloc(ex)).ToInt64();
			array[1].z = methodNotFound;
			return AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperCeateInvocationError, array);
		}

		// Token: 0x0400000C RID: 12
		private const string RELECTION_HELPER_CLASS_NAME = "com/unity3d/player/ReflectionHelper";

		// Token: 0x0400000D RID: 13
		private static readonly GlobalJavaObjectRef s_ReflectionHelperClass = new GlobalJavaObjectRef(AndroidJNISafe.FindClass("com/unity3d/player/ReflectionHelper"));

		// Token: 0x0400000E RID: 14
		private static readonly IntPtr s_ReflectionHelperGetConstructorID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getConstructorID", "(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/reflect/Constructor;");

		// Token: 0x0400000F RID: 15
		private static readonly IntPtr s_ReflectionHelperGetMethodID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getMethodID", "(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;Z)Ljava/lang/reflect/Method;");

		// Token: 0x04000010 RID: 16
		private static readonly IntPtr s_ReflectionHelperGetFieldID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getFieldID", "(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;Z)Ljava/lang/reflect/Field;");

		// Token: 0x04000011 RID: 17
		private static readonly IntPtr s_ReflectionHelperGetFieldSignature = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getFieldSignature", "(Ljava/lang/reflect/Field;)Ljava/lang/String;");

		// Token: 0x04000012 RID: 18
		private static readonly IntPtr s_ReflectionHelperNewProxyInstance = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "newProxyInstance", "(Lcom/unity3d/player/UnityPlayer;JLjava/lang/Class;)Ljava/lang/Object;");

		// Token: 0x04000013 RID: 19
		private static readonly IntPtr s_ReflectionHelperCeateInvocationError = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "createInvocationError", "(JZ)Ljava/lang/Object;");

		// Token: 0x04000014 RID: 20
		private static readonly IntPtr s_FieldGetDeclaringClass = AndroidReflection.GetMethodID("java/lang/reflect/Field", "getDeclaringClass", "()Ljava/lang/Class;");
	}
}
