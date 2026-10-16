using System;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	internal class AndroidJNISafe
	{
		// Token: 0x060001BE RID: 446 RVA: 0x000087F8 File Offset: 0x000069F8
		public static void CheckException()
		{
			IntPtr intPtr = AndroidJNI.ExceptionOccurred();
			bool flag = intPtr != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.ExceptionClear();
				IntPtr intPtr2 = AndroidJNI.FindClass("java/lang/Throwable");
				IntPtr intPtr3 = AndroidJNI.FindClass("android/util/Log");
				try
				{
					IntPtr methodID = AndroidJNI.GetMethodID(intPtr2, "toString", "()Ljava/lang/String;");
					IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr3, "getStackTraceString", "(Ljava/lang/Throwable;)Ljava/lang/String;");
					string text = AndroidJNI.CallStringMethod(intPtr, methodID, new jvalue[0]);
					jvalue[] array = new jvalue[1];
					array[0].l = intPtr;
					string text2 = AndroidJNI.CallStaticStringMethod(intPtr3, staticMethodID, array);
					throw new AndroidJavaException(text, text2);
				}
				finally
				{
					AndroidJNISafe.DeleteLocalRef(intPtr);
					AndroidJNISafe.DeleteLocalRef(intPtr2);
					AndroidJNISafe.DeleteLocalRef(intPtr3);
				}
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000088C4 File Offset: 0x00006AC4
		public static void DeleteGlobalRef(IntPtr globalref)
		{
			bool flag = globalref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteGlobalRef(globalref);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000088E8 File Offset: 0x00006AE8
		public static void QueueDeleteGlobalRef(IntPtr globalref)
		{
			bool flag = globalref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.QueueDeleteGlobalRef(globalref);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000890C File Offset: 0x00006B0C
		public static void DeleteWeakGlobalRef(IntPtr globalref)
		{
			bool flag = globalref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteWeakGlobalRef(globalref);
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008930 File Offset: 0x00006B30
		public static void DeleteLocalRef(IntPtr localref)
		{
			bool flag = localref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteLocalRef(localref);
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008954 File Offset: 0x00006B54
		public static IntPtr NewString(string chars)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.NewString(chars);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008988 File Offset: 0x00006B88
		public static IntPtr NewStringUTF(string bytes)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.NewStringUTF(bytes);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000089BC File Offset: 0x00006BBC
		public static string GetStringChars(IntPtr str)
		{
			string stringChars;
			try
			{
				stringChars = AndroidJNI.GetStringChars(str);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringChars;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000089F0 File Offset: 0x00006BF0
		public static string GetStringUTFChars(IntPtr str)
		{
			string stringUTFChars;
			try
			{
				stringUTFChars = AndroidJNI.GetStringUTFChars(str);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringUTFChars;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008A24 File Offset: 0x00006C24
		public static IntPtr GetObjectClass(IntPtr ptr)
		{
			IntPtr objectClass;
			try
			{
				objectClass = AndroidJNI.GetObjectClass(ptr);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectClass;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008A58 File Offset: 0x00006C58
		public static IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticMethodID;
			try
			{
				staticMethodID = AndroidJNI.GetStaticMethodID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticMethodID;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008A8C File Offset: 0x00006C8C
		public static IntPtr GetMethodID(IntPtr obj, string name, string sig)
		{
			IntPtr methodID;
			try
			{
				methodID = AndroidJNI.GetMethodID(obj, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return methodID;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public static IntPtr GetFieldID(IntPtr clazz, string name, string sig)
		{
			IntPtr fieldID;
			try
			{
				fieldID = AndroidJNI.GetFieldID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return fieldID;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public static IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticFieldID;
			try
			{
				staticFieldID = AndroidJNI.GetStaticFieldID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticFieldID;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008B28 File Offset: 0x00006D28
		public static IntPtr FromReflectedMethod(IntPtr refMethod)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.FromReflectedMethod(refMethod);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008B5C File Offset: 0x00006D5C
		public static IntPtr FromReflectedField(IntPtr refField)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.FromReflectedField(refField);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008B90 File Offset: 0x00006D90
		public static IntPtr FindClass(string name)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.FindClass(name);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008BC4 File Offset: 0x00006DC4
		public static void PushLocalFrame(int capacity)
		{
			bool flag = AndroidJNI.PushLocalFrame(capacity) < 0;
			if (flag)
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.NewObject(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008C08 File Offset: 0x00006E08
		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.NewObject(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008C3C File Offset: 0x00006E3C
		public static void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val)
		{
			try
			{
				AndroidJNI.SetStaticObjectField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008C70 File Offset: 0x00006E70
		public static void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val)
		{
			try
			{
				AndroidJNI.SetStaticStringField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008CA4 File Offset: 0x00006EA4
		public static void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val)
		{
			try
			{
				AndroidJNI.SetStaticCharField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008CD8 File Offset: 0x00006ED8
		public static void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val)
		{
			try
			{
				AndroidJNI.SetStaticDoubleField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008D0C File Offset: 0x00006F0C
		public static void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val)
		{
			try
			{
				AndroidJNI.SetStaticFloatField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008D40 File Offset: 0x00006F40
		public static void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val)
		{
			try
			{
				AndroidJNI.SetStaticLongField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008D74 File Offset: 0x00006F74
		public static void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val)
		{
			try
			{
				AndroidJNI.SetStaticShortField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008DA8 File Offset: 0x00006FA8
		public static void SetStaticSByteField(IntPtr clazz, IntPtr fieldID, sbyte val)
		{
			try
			{
				AndroidJNI.SetStaticSByteField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008DDC File Offset: 0x00006FDC
		public static void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val)
		{
			try
			{
				AndroidJNI.SetStaticBooleanField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008E10 File Offset: 0x00007010
		public static void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val)
		{
			try
			{
				AndroidJNI.SetStaticIntField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008E44 File Offset: 0x00007044
		public static IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID)
		{
			IntPtr staticObjectField;
			try
			{
				staticObjectField = AndroidJNI.GetStaticObjectField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticObjectField;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008E78 File Offset: 0x00007078
		public static string GetStaticStringField(IntPtr clazz, IntPtr fieldID)
		{
			string staticStringField;
			try
			{
				staticStringField = AndroidJNI.GetStaticStringField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticStringField;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008EAC File Offset: 0x000070AC
		public static char GetStaticCharField(IntPtr clazz, IntPtr fieldID)
		{
			char staticCharField;
			try
			{
				staticCharField = AndroidJNI.GetStaticCharField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticCharField;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008EE0 File Offset: 0x000070E0
		public static double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID)
		{
			double staticDoubleField;
			try
			{
				staticDoubleField = AndroidJNI.GetStaticDoubleField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticDoubleField;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008F14 File Offset: 0x00007114
		public static float GetStaticFloatField(IntPtr clazz, IntPtr fieldID)
		{
			float staticFloatField;
			try
			{
				staticFloatField = AndroidJNI.GetStaticFloatField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticFloatField;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008F48 File Offset: 0x00007148
		public static long GetStaticLongField(IntPtr clazz, IntPtr fieldID)
		{
			long staticLongField;
			try
			{
				staticLongField = AndroidJNI.GetStaticLongField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticLongField;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008F7C File Offset: 0x0000717C
		public static short GetStaticShortField(IntPtr clazz, IntPtr fieldID)
		{
			short staticShortField;
			try
			{
				staticShortField = AndroidJNI.GetStaticShortField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticShortField;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008FB0 File Offset: 0x000071B0
		public static sbyte GetStaticSByteField(IntPtr clazz, IntPtr fieldID)
		{
			sbyte staticSByteField;
			try
			{
				staticSByteField = AndroidJNI.GetStaticSByteField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticSByteField;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008FE4 File Offset: 0x000071E4
		public static bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID)
		{
			bool staticBooleanField;
			try
			{
				staticBooleanField = AndroidJNI.GetStaticBooleanField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticBooleanField;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009018 File Offset: 0x00007218
		public static int GetStaticIntField(IntPtr clazz, IntPtr fieldID)
		{
			int staticIntField;
			try
			{
				staticIntField = AndroidJNI.GetStaticIntField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticIntField;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000904C File Offset: 0x0000724C
		public static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			AndroidJNISafe.CallStaticVoidMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009060 File Offset: 0x00007260
		public static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				AndroidJNI.CallStaticVoidMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00009094 File Offset: 0x00007294
		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticObjectMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000090B4 File Offset: 0x000072B4
		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.CallStaticObjectMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000090E8 File Offset: 0x000072E8
		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticStringMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009108 File Offset: 0x00007308
		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			string text = null;
			string text2;
			try
			{
				text = AndroidJNI.CallStaticStringMethod(clazz, methodID, args);
				text2 = text;
			}
			finally
			{
				bool flag = text == null;
				if (flag)
				{
					AndroidJNISafe.CheckException();
				}
			}
			return text2;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009148 File Offset: 0x00007348
		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticCharMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009168 File Offset: 0x00007368
		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			char c;
			try
			{
				c = AndroidJNI.CallStaticCharMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return c;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000919C File Offset: 0x0000739C
		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticDoubleMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000091BC File Offset: 0x000073BC
		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			double num;
			try
			{
				num = AndroidJNI.CallStaticDoubleMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000091F0 File Offset: 0x000073F0
		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticFloatMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009210 File Offset: 0x00007410
		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			float num;
			try
			{
				num = AndroidJNI.CallStaticFloatMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009244 File Offset: 0x00007444
		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticLongMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009264 File Offset: 0x00007464
		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			long num;
			try
			{
				num = AndroidJNI.CallStaticLongMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009298 File Offset: 0x00007498
		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticShortMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000092B8 File Offset: 0x000074B8
		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			short num;
			try
			{
				num = AndroidJNI.CallStaticShortMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000092EC File Offset: 0x000074EC
		public static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticSByteMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000930C File Offset: 0x0000750C
		public static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			sbyte b;
			try
			{
				b = AndroidJNI.CallStaticSByteMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return b;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009340 File Offset: 0x00007540
		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticBooleanMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009360 File Offset: 0x00007560
		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			bool flag;
			try
			{
				flag = AndroidJNI.CallStaticBooleanMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return flag;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009394 File Offset: 0x00007594
		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticIntMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000093B4 File Offset: 0x000075B4
		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			int num;
			try
			{
				num = AndroidJNI.CallStaticIntMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000093E8 File Offset: 0x000075E8
		public static void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val)
		{
			try
			{
				AndroidJNI.SetObjectField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000941C File Offset: 0x0000761C
		public static void SetStringField(IntPtr obj, IntPtr fieldID, string val)
		{
			try
			{
				AndroidJNI.SetStringField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009450 File Offset: 0x00007650
		public static void SetCharField(IntPtr obj, IntPtr fieldID, char val)
		{
			try
			{
				AndroidJNI.SetCharField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009484 File Offset: 0x00007684
		public static void SetDoubleField(IntPtr obj, IntPtr fieldID, double val)
		{
			try
			{
				AndroidJNI.SetDoubleField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000094B8 File Offset: 0x000076B8
		public static void SetFloatField(IntPtr obj, IntPtr fieldID, float val)
		{
			try
			{
				AndroidJNI.SetFloatField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000094EC File Offset: 0x000076EC
		public static void SetLongField(IntPtr obj, IntPtr fieldID, long val)
		{
			try
			{
				AndroidJNI.SetLongField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009520 File Offset: 0x00007720
		public static void SetShortField(IntPtr obj, IntPtr fieldID, short val)
		{
			try
			{
				AndroidJNI.SetShortField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009554 File Offset: 0x00007754
		public static void SetSByteField(IntPtr obj, IntPtr fieldID, sbyte val)
		{
			try
			{
				AndroidJNI.SetSByteField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009588 File Offset: 0x00007788
		public static void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val)
		{
			try
			{
				AndroidJNI.SetBooleanField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000095BC File Offset: 0x000077BC
		public static void SetIntField(IntPtr obj, IntPtr fieldID, int val)
		{
			try
			{
				AndroidJNI.SetIntField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000095F0 File Offset: 0x000077F0
		public static IntPtr GetObjectField(IntPtr obj, IntPtr fieldID)
		{
			IntPtr objectField;
			try
			{
				objectField = AndroidJNI.GetObjectField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectField;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009624 File Offset: 0x00007824
		public static string GetStringField(IntPtr obj, IntPtr fieldID)
		{
			string stringField;
			try
			{
				stringField = AndroidJNI.GetStringField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringField;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009658 File Offset: 0x00007858
		public static char GetCharField(IntPtr obj, IntPtr fieldID)
		{
			char charField;
			try
			{
				charField = AndroidJNI.GetCharField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return charField;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000968C File Offset: 0x0000788C
		public static double GetDoubleField(IntPtr obj, IntPtr fieldID)
		{
			double doubleField;
			try
			{
				doubleField = AndroidJNI.GetDoubleField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return doubleField;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000096C0 File Offset: 0x000078C0
		public static float GetFloatField(IntPtr obj, IntPtr fieldID)
		{
			float floatField;
			try
			{
				floatField = AndroidJNI.GetFloatField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return floatField;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000096F4 File Offset: 0x000078F4
		public static long GetLongField(IntPtr obj, IntPtr fieldID)
		{
			long longField;
			try
			{
				longField = AndroidJNI.GetLongField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return longField;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009728 File Offset: 0x00007928
		public static short GetShortField(IntPtr obj, IntPtr fieldID)
		{
			short shortField;
			try
			{
				shortField = AndroidJNI.GetShortField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return shortField;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000975C File Offset: 0x0000795C
		public static sbyte GetSByteField(IntPtr obj, IntPtr fieldID)
		{
			sbyte sbyteField;
			try
			{
				sbyteField = AndroidJNI.GetSByteField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return sbyteField;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00009790 File Offset: 0x00007990
		public static bool GetBooleanField(IntPtr obj, IntPtr fieldID)
		{
			bool booleanField;
			try
			{
				booleanField = AndroidJNI.GetBooleanField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return booleanField;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000097C4 File Offset: 0x000079C4
		public static int GetIntField(IntPtr obj, IntPtr fieldID)
		{
			int intField;
			try
			{
				intField = AndroidJNI.GetIntField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intField;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000097F8 File Offset: 0x000079F8
		public static void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			AndroidJNISafe.CallVoidMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000980C File Offset: 0x00007A0C
		public static void CallVoidMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				AndroidJNI.CallVoidMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009840 File Offset: 0x00007A40
		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallObjectMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009860 File Offset: 0x00007A60
		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.CallObjectMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009894 File Offset: 0x00007A94
		public static string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStringMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000098B4 File Offset: 0x00007AB4
		public static string CallStringMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			string text = null;
			string text2;
			try
			{
				text = AndroidJNI.CallStringMethod(obj, methodID, args);
				text2 = text;
			}
			finally
			{
				bool flag = text == null;
				if (flag)
				{
					AndroidJNISafe.CheckException();
				}
			}
			return text2;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000098F4 File Offset: 0x00007AF4
		public static char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallCharMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009914 File Offset: 0x00007B14
		public static char CallCharMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			char c;
			try
			{
				c = AndroidJNI.CallCharMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return c;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009948 File Offset: 0x00007B48
		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallDoubleMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009968 File Offset: 0x00007B68
		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			double num;
			try
			{
				num = AndroidJNI.CallDoubleMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000999C File Offset: 0x00007B9C
		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallFloatMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000099BC File Offset: 0x00007BBC
		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			float num;
			try
			{
				num = AndroidJNI.CallFloatMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000099F0 File Offset: 0x00007BF0
		public static long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallLongMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009A10 File Offset: 0x00007C10
		public static long CallLongMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			long num;
			try
			{
				num = AndroidJNI.CallLongMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009A44 File Offset: 0x00007C44
		public static short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallShortMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009A64 File Offset: 0x00007C64
		public static short CallShortMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			short num;
			try
			{
				num = AndroidJNI.CallShortMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009A98 File Offset: 0x00007C98
		public static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallSByteMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009AB8 File Offset: 0x00007CB8
		public static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			sbyte b;
			try
			{
				b = AndroidJNI.CallSByteMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return b;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009AEC File Offset: 0x00007CEC
		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallBooleanMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009B0C File Offset: 0x00007D0C
		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			bool flag;
			try
			{
				flag = AndroidJNI.CallBooleanMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return flag;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009B40 File Offset: 0x00007D40
		public static int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallIntMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009B60 File Offset: 0x00007D60
		public static int CallIntMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			int num;
			try
			{
				num = AndroidJNI.CallIntMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009B94 File Offset: 0x00007D94
		public static IntPtr[] FromObjectArray(IntPtr array)
		{
			IntPtr[] array2;
			try
			{
				array2 = AndroidJNI.FromObjectArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public static char[] FromCharArray(IntPtr array)
		{
			char[] array2;
			try
			{
				array2 = AndroidJNI.FromCharArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009BFC File Offset: 0x00007DFC
		public static double[] FromDoubleArray(IntPtr array)
		{
			double[] array2;
			try
			{
				array2 = AndroidJNI.FromDoubleArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009C30 File Offset: 0x00007E30
		public static float[] FromFloatArray(IntPtr array)
		{
			float[] array2;
			try
			{
				array2 = AndroidJNI.FromFloatArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009C64 File Offset: 0x00007E64
		public static long[] FromLongArray(IntPtr array)
		{
			long[] array2;
			try
			{
				array2 = AndroidJNI.FromLongArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009C98 File Offset: 0x00007E98
		public static short[] FromShortArray(IntPtr array)
		{
			short[] array2;
			try
			{
				array2 = AndroidJNI.FromShortArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009CCC File Offset: 0x00007ECC
		public static byte[] FromByteArray(IntPtr array)
		{
			byte[] array2;
			try
			{
				array2 = AndroidJNI.FromByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009D00 File Offset: 0x00007F00
		public static sbyte[] FromSByteArray(IntPtr array)
		{
			sbyte[] array2;
			try
			{
				array2 = AndroidJNI.FromSByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009D34 File Offset: 0x00007F34
		public static bool[] FromBooleanArray(IntPtr array)
		{
			bool[] array2;
			try
			{
				array2 = AndroidJNI.FromBooleanArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009D68 File Offset: 0x00007F68
		public static int[] FromIntArray(IntPtr array)
		{
			int[] array2;
			try
			{
				array2 = AndroidJNI.FromIntArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009D9C File Offset: 0x00007F9C
		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToObjectArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009DD0 File Offset: 0x00007FD0
		public static IntPtr ToObjectArray(IntPtr[] array, IntPtr type)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToObjectArray(array, type);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009E04 File Offset: 0x00008004
		public static IntPtr ToCharArray(char[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToCharArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009E38 File Offset: 0x00008038
		public static IntPtr ToDoubleArray(double[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToDoubleArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009E6C File Offset: 0x0000806C
		public static IntPtr ToFloatArray(float[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToFloatArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009EA0 File Offset: 0x000080A0
		public static IntPtr ToLongArray(long[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToLongArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009ED4 File Offset: 0x000080D4
		public static IntPtr ToShortArray(short[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToShortArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009F08 File Offset: 0x00008108
		public static IntPtr ToByteArray(byte[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009F3C File Offset: 0x0000813C
		public static IntPtr ToSByteArray(sbyte[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToSByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009F70 File Offset: 0x00008170
		public static IntPtr ToBooleanArray(bool[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToBooleanArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009FA4 File Offset: 0x000081A4
		public static IntPtr ToIntArray(int[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToIntArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009FD8 File Offset: 0x000081D8
		public static IntPtr GetObjectArrayElement(IntPtr array, int index)
		{
			IntPtr objectArrayElement;
			try
			{
				objectArrayElement = AndroidJNI.GetObjectArrayElement(array, index);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectArrayElement;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A00C File Offset: 0x0000820C
		public static int GetArrayLength(IntPtr array)
		{
			int arrayLength;
			try
			{
				arrayLength = AndroidJNI.GetArrayLength(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return arrayLength;
		}
	}
}
