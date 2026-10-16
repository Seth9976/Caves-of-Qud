using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Android;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h")]
	[StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon)]
	[NativeConditional("PLATFORM_ANDROID")]
	[UsedByNativeCode]
	public static class AndroidJNIHelper
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000084 RID: 132
		// (set) Token: 0x06000085 RID: 133
		public static extern bool debug
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00006C54 File Offset: 0x00004E54
		public static IntPtr GetConstructorID(IntPtr javaClass)
		{
			return AndroidJNIHelper.GetConstructorID(javaClass, "");
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00006C74 File Offset: 0x00004E74
		public static IntPtr GetConstructorID(IntPtr javaClass, [DefaultValue("")] string signature)
		{
			return _AndroidJNIHelper.GetConstructorID(javaClass, signature);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006C90 File Offset: 0x00004E90
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName)
		{
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, "", false);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature)
		{
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, false);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006CCC File Offset: 0x00004ECC
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006CE8 File Offset: 0x00004EE8
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName)
		{
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, "", false);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006D08 File Offset: 0x00004F08
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature)
		{
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, false);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006D24 File Offset: 0x00004F24
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, isStatic);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006D40 File Offset: 0x00004F40
		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return _AndroidJNIHelper.CreateJavaRunnable(jrunnable);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006D58 File Offset: 0x00004F58
		public static IntPtr CreateJavaProxy(AndroidJavaProxy proxy)
		{
			GCHandle gchandle = GCHandle.Alloc(proxy);
			IntPtr intPtr;
			try
			{
				intPtr = _AndroidJNIHelper.CreateJavaProxy(AndroidApplication.UnityPlayerRaw, GCHandle.ToIntPtr(gchandle), proxy);
			}
			catch
			{
				gchandle.Free();
				throw;
			}
			return intPtr;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public static IntPtr ConvertToJNIArray(Array array)
		{
			return _AndroidJNIHelper.ConvertToJNIArray(array);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			jvalue[] array = new jvalue[args.Length];
			_AndroidJNIHelper.CreateJNIArgArray(args, array);
			return array;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public static void CreateJNIArgArray(object[] args, Span<jvalue> jniArgs)
		{
			bool flag = args.Length != jniArgs.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Both arrays must be of the same length, but are {0} and {1}", args.Length, jniArgs.Length));
			}
			_AndroidJNIHelper.CreateJNIArgArray(args, jniArgs);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006E31 File Offset: 0x00005031
		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006E41 File Offset: 0x00005041
		public static void DeleteJNIArgArray(object[] args, Span<jvalue> jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006E4C File Offset: 0x0000504C
		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return _AndroidJNIHelper.GetConstructorID(jclass, args);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006E68 File Offset: 0x00005068
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(jclass, methodName, args, isStatic);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006E84 File Offset: 0x00005084
		public static string GetSignature(object obj)
		{
			return _AndroidJNIHelper.GetSignature(obj);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006E9C File Offset: 0x0000509C
		public static string GetSignature(object[] args)
		{
			return _AndroidJNIHelper.GetSignature(args);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006EB4 File Offset: 0x000050B4
		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			return _AndroidJNIHelper.ConvertFromJNIArray<ArrayType>(array);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006ECC File Offset: 0x000050CC
		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID<ReturnType>(jclass, methodName, args, isStatic);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006EE8 File Offset: 0x000050E8
		public static IntPtr GetFieldID<FieldType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID<FieldType>(jclass, fieldName, isStatic);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006F04 File Offset: 0x00005104
		public static string GetSignature<ReturnType>(object[] args)
		{
			return _AndroidJNIHelper.GetSignature<ReturnType>(args);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006F1C File Offset: 0x0000511C
		private unsafe static IntPtr Box(jvalue val, string boxedClass, string signature)
		{
			IntPtr intPtr = AndroidJNISafe.FindClass(boxedClass);
			IntPtr intPtr2;
			try
			{
				IntPtr staticMethodID = AndroidJNISafe.GetStaticMethodID(intPtr, "valueOf", signature);
				Span<jvalue> span = new Span<jvalue>((void*)(&val), 1);
				intPtr2 = AndroidJNISafe.CallStaticObjectMethod(intPtr, staticMethodID, span);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return intPtr2;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006F74 File Offset: 0x00005174
		public static IntPtr Box(sbyte value)
		{
			return AndroidJNIHelper.Box(new jvalue
			{
				b = value
			}, "java/lang/Byte", "(B)Ljava/lang/Byte;");
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006FA8 File Offset: 0x000051A8
		public static IntPtr Box(short value)
		{
			return AndroidJNIHelper.Box(new jvalue
			{
				s = value
			}, "java/lang/Short", "(S)Ljava/lang/Short;");
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006FDC File Offset: 0x000051DC
		public static IntPtr Box(int value)
		{
			return AndroidJNIHelper.Box(new jvalue
			{
				i = value
			}, "java/lang/Integer", "(I)Ljava/lang/Integer;");
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007010 File Offset: 0x00005210
		public static IntPtr Box(long value)
		{
			return AndroidJNIHelper.Box(new jvalue
			{
				j = value
			}, "java/lang/Long", "(J)Ljava/lang/Long;");
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007044 File Offset: 0x00005244
		public static IntPtr Box(float value)
		{
			return AndroidJNIHelper.Box(new jvalue
			{
				f = value
			}, "java/lang/Float", "(F)Ljava/lang/Float;");
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007078 File Offset: 0x00005278
		public static IntPtr Box(double value)
		{
			return AndroidJNIHelper.Box(new jvalue
			{
				d = value
			}, "java/lang/Double", "(D)Ljava/lang/Double;");
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000070AC File Offset: 0x000052AC
		public static IntPtr Box(char value)
		{
			return AndroidJNIHelper.Box(new jvalue
			{
				c = value
			}, "java/lang/Character", "(C)Ljava/lang/Character;");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000070E0 File Offset: 0x000052E0
		public static IntPtr Box(bool value)
		{
			return AndroidJNIHelper.Box(new jvalue
			{
				z = value
			}, "java/lang/Boolean", "(Z)Ljava/lang/Boolean;");
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007114 File Offset: 0x00005314
		private static IntPtr GetUnboxMethod(IntPtr obj, string methodName, string signature)
		{
			IntPtr objectClass = AndroidJNISafe.GetObjectClass(obj);
			IntPtr methodID;
			try
			{
				methodID = AndroidJNISafe.GetMethodID(objectClass, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(objectClass);
			}
			return methodID;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007150 File Offset: 0x00005350
		public static void Unbox(IntPtr obj, out sbyte value)
		{
			IntPtr unboxMethod = AndroidJNIHelper.GetUnboxMethod(obj, "byteValue", "()B");
			value = AndroidJNISafe.CallSByteMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007184 File Offset: 0x00005384
		public static void Unbox(IntPtr obj, out short value)
		{
			IntPtr unboxMethod = AndroidJNIHelper.GetUnboxMethod(obj, "shortValue", "()S");
			value = AndroidJNISafe.CallShortMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000071B8 File Offset: 0x000053B8
		public static void Unbox(IntPtr obj, out int value)
		{
			IntPtr unboxMethod = AndroidJNIHelper.GetUnboxMethod(obj, "intValue", "()I");
			value = AndroidJNISafe.CallIntMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000071EC File Offset: 0x000053EC
		public static void Unbox(IntPtr obj, out long value)
		{
			IntPtr unboxMethod = AndroidJNIHelper.GetUnboxMethod(obj, "longValue", "()J");
			value = AndroidJNISafe.CallLongMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007220 File Offset: 0x00005420
		public static void Unbox(IntPtr obj, out float value)
		{
			IntPtr unboxMethod = AndroidJNIHelper.GetUnboxMethod(obj, "floatValue", "()F");
			value = AndroidJNISafe.CallFloatMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007254 File Offset: 0x00005454
		public static void Unbox(IntPtr obj, out double value)
		{
			IntPtr unboxMethod = AndroidJNIHelper.GetUnboxMethod(obj, "doubleValue", "()D");
			value = AndroidJNISafe.CallDoubleMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007288 File Offset: 0x00005488
		public static void Unbox(IntPtr obj, out char value)
		{
			IntPtr unboxMethod = AndroidJNIHelper.GetUnboxMethod(obj, "charValue", "()C");
			value = AndroidJNISafe.CallCharMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000072BC File Offset: 0x000054BC
		public static void Unbox(IntPtr obj, out bool value)
		{
			IntPtr unboxMethod = AndroidJNIHelper.GetUnboxMethod(obj, "booleanValue", "()Z");
			value = AndroidJNISafe.CallBooleanMethod(obj, unboxMethod, default(Span<jvalue>));
		}
	}
}
