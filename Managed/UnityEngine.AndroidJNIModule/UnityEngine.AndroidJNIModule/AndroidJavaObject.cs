using System;
using System.Text;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	public class AndroidJavaObject : IDisposable
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002892 File Offset: 0x00000A92
		public AndroidJavaObject(string className, string[] args)
			: this()
		{
			this._AndroidJavaObject(className, new object[] { args });
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002892 File Offset: 0x00000A92
		public AndroidJavaObject(string className, AndroidJavaObject[] args)
			: this()
		{
			this._AndroidJavaObject(className, new object[] { args });
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002892 File Offset: 0x00000A92
		public AndroidJavaObject(string className, AndroidJavaClass[] args)
			: this()
		{
			this._AndroidJavaObject(className, new object[] { args });
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002892 File Offset: 0x00000A92
		public AndroidJavaObject(string className, AndroidJavaProxy[] args)
			: this()
		{
			this._AndroidJavaObject(className, new object[] { args });
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002892 File Offset: 0x00000A92
		public AndroidJavaObject(string className, AndroidJavaRunnable[] args)
			: this()
		{
			this._AndroidJavaObject(className, new object[] { args });
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028AE File Offset: 0x00000AAE
		public AndroidJavaObject(string className, params object[] args)
			: this()
		{
			this._AndroidJavaObject(className, args);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028C4 File Offset: 0x00000AC4
		public AndroidJavaObject(IntPtr jobject)
			: this()
		{
			bool flag = jobject == IntPtr.Zero;
			if (flag)
			{
				throw new Exception("JNI: Init'd AndroidJavaObject with null ptr!");
			}
			IntPtr objectClass = AndroidJNISafe.GetObjectClass(jobject);
			this.m_jobject = new GlobalJavaObjectRef(jobject);
			this.m_jclass = new GlobalJavaObjectRef(objectClass);
			AndroidJNISafe.DeleteLocalRef(objectClass);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000291A File Offset: 0x00000B1A
		public AndroidJavaObject(IntPtr clazz, IntPtr constructorID, params object[] args)
		{
			this.m_jclass = new GlobalJavaObjectRef(clazz);
			this._AndroidJavaObject(constructorID, args);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002939 File Offset: 0x00000B39
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000294B File Offset: 0x00000B4B
		public void Call<T>(string methodName, T[] args)
		{
			this._Call(methodName, new object[] { args });
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002960 File Offset: 0x00000B60
		public void Call<T>(IntPtr methodID, T[] args)
		{
			this._Call(methodID, new object[] { args });
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002975 File Offset: 0x00000B75
		public void Call(string methodName, params object[] args)
		{
			this._Call(methodName, args);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002981 File Offset: 0x00000B81
		public void Call(IntPtr methodID, params object[] args)
		{
			this._Call(methodID, args);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000298D File Offset: 0x00000B8D
		public void CallStatic<T>(string methodName, T[] args)
		{
			this._CallStatic(methodName, new object[] { args });
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029A2 File Offset: 0x00000BA2
		public void CallStatic<T>(IntPtr methodID, T[] args)
		{
			this._CallStatic(methodID, new object[] { args });
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000029B7 File Offset: 0x00000BB7
		public void CallStatic(string methodName, params object[] args)
		{
			this._CallStatic(methodName, args);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029C3 File Offset: 0x00000BC3
		public void CallStatic(IntPtr methodID, params object[] args)
		{
			this._CallStatic(methodID, args);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000029D0 File Offset: 0x00000BD0
		public FieldType Get<FieldType>(string fieldName)
		{
			return this._Get<FieldType>(fieldName);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029EC File Offset: 0x00000BEC
		public FieldType Get<FieldType>(IntPtr fieldID)
		{
			return this._Get<FieldType>(fieldID);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A05 File Offset: 0x00000C05
		public void Set<FieldType>(string fieldName, FieldType val)
		{
			this._Set<FieldType>(fieldName, val);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A11 File Offset: 0x00000C11
		public void Set<FieldType>(IntPtr fieldID, FieldType val)
		{
			this._Set<FieldType>(fieldID, val);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A20 File Offset: 0x00000C20
		public FieldType GetStatic<FieldType>(string fieldName)
		{
			return this._GetStatic<FieldType>(fieldName);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A3C File Offset: 0x00000C3C
		public FieldType GetStatic<FieldType>(IntPtr fieldID)
		{
			return this._GetStatic<FieldType>(fieldID);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A55 File Offset: 0x00000C55
		public void SetStatic<FieldType>(string fieldName, FieldType val)
		{
			this._SetStatic<FieldType>(fieldName, val);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A61 File Offset: 0x00000C61
		public void SetStatic<FieldType>(IntPtr fieldID, FieldType val)
		{
			this._SetStatic<FieldType>(fieldID, val);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A70 File Offset: 0x00000C70
		public IntPtr GetRawObject()
		{
			return this._GetRawObject();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A88 File Offset: 0x00000C88
		public IntPtr GetRawClass()
		{
			return this._GetRawClass();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public AndroidJavaObject CloneReference()
		{
			bool flag = this.m_jclass == null;
			if (flag)
			{
				throw new Exception("Cannot clone a disposed reference");
			}
			bool flag2 = this.m_jobject != null;
			AndroidJavaObject androidJavaObject;
			if (flag2)
			{
				androidJavaObject = new AndroidJavaObject
				{
					m_jobject = new GlobalJavaObjectRef(this.m_jobject),
					m_jclass = new GlobalJavaObjectRef(this.m_jclass)
				};
			}
			else
			{
				androidJavaObject = new AndroidJavaClass(this.m_jclass);
			}
			return androidJavaObject;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B20 File Offset: 0x00000D20
		public ReturnType Call<ReturnType, T>(string methodName, T[] args)
		{
			return this._Call<ReturnType>(methodName, new object[] { args });
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B44 File Offset: 0x00000D44
		public ReturnType Call<ReturnType, T>(IntPtr methodID, T[] args)
		{
			return this._Call<ReturnType>(methodID, new object[] { args });
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B68 File Offset: 0x00000D68
		public ReturnType Call<ReturnType>(string methodName, params object[] args)
		{
			return this._Call<ReturnType>(methodName, args);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B84 File Offset: 0x00000D84
		public ReturnType Call<ReturnType>(IntPtr methodID, params object[] args)
		{
			return this._Call<ReturnType>(methodID, args);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public ReturnType CallStatic<ReturnType, T>(string methodName, T[] args)
		{
			return this._CallStatic<ReturnType>(methodName, new object[] { args });
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public ReturnType CallStatic<ReturnType, T>(IntPtr methodID, T[] args)
		{
			return this._CallStatic<ReturnType>(methodID, new object[] { args });
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public ReturnType CallStatic<ReturnType>(string methodName, params object[] args)
		{
			return this._CallStatic<ReturnType>(methodName, args);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C04 File Offset: 0x00000E04
		public ReturnType CallStatic<ReturnType>(IntPtr methodID, params object[] args)
		{
			return this._CallStatic<ReturnType>(methodID, args);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002C20 File Offset: 0x00000E20
		protected void DebugPrint(string msg)
		{
			bool flag = !AndroidJavaObject.enableDebugPrints;
			if (!flag)
			{
				Debug.Log(msg);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C44 File Offset: 0x00000E44
		protected void DebugPrint(string call, string methodName, string signature, object[] args)
		{
			bool flag = !AndroidJavaObject.enableDebugPrints;
			if (!flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in args)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append((obj == null) ? "<null>" : obj.GetType().ToString());
				}
				string[] array = new string[7];
				array[0] = call;
				array[1] = "(\"";
				array[2] = methodName;
				array[3] = "\"";
				int num = 4;
				StringBuilder stringBuilder2 = stringBuilder;
				array[num] = ((stringBuilder2 != null) ? stringBuilder2.ToString() : null);
				array[5] = ") = ";
				array[6] = signature;
				Debug.Log(string.Concat(array));
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CF4 File Offset: 0x00000EF4
		private void _AndroidJavaObject(string className, params object[] args)
		{
			this.DebugPrint("Creating AndroidJavaObject from " + className);
			IntPtr intPtr = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			this.m_jclass = new GlobalJavaObjectRef(intPtr);
			AndroidJNISafe.DeleteLocalRef(intPtr);
			IntPtr constructorID = AndroidJNIHelper.GetConstructorID(this.m_jclass, args);
			this._AndroidJavaObject(constructorID, args);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D54 File Offset: 0x00000F54
		private unsafe void _AndroidJavaObject(IntPtr constructorID, params object[] args)
		{
			checked
			{
				Span<jvalue> span;
				if (args == null || args.Length == 0)
				{
					span = default(Span<jvalue>);
				}
				else
				{
					int num = args.Length;
					Span<jvalue> span2 = new Span<jvalue>(stackalloc byte[unchecked((UIntPtr)num) * (UIntPtr)sizeof(jvalue)], num);
					span = span2;
				}
				Span<jvalue> span3 = span;
				AndroidJNIHelper.CreateJNIArgArray(args, span3);
				try
				{
					IntPtr intPtr = AndroidJNISafe.NewObject(this.m_jclass, constructorID, span3);
					this.m_jobject = new GlobalJavaObjectRef(intPtr);
					AndroidJNISafe.DeleteLocalRef(intPtr);
				}
				finally
				{
					AndroidJNIHelper.DeleteJNIArgArray(args, span3);
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002DE0 File Offset: 0x00000FE0
		internal AndroidJavaObject()
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002DEC File Offset: 0x00000FEC
		~AndroidJavaObject()
		{
			this.Dispose(false);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002E20 File Offset: 0x00001020
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_jobject != null;
			if (flag)
			{
				this.m_jobject.Dispose();
				this.m_jobject = null;
			}
			bool flag2 = this.m_jclass != null;
			if (flag2)
			{
				this.m_jclass.Dispose();
				this.m_jclass = null;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E74 File Offset: 0x00001074
		protected void _Call(string methodName, params object[] args)
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID(this.m_jclass, methodName, args, false);
			this._Call(methodID, args);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002EA0 File Offset: 0x000010A0
		protected unsafe void _Call(IntPtr methodID, params object[] args)
		{
			checked
			{
				Span<jvalue> span;
				if (args == null || args.Length == 0)
				{
					span = default(Span<jvalue>);
				}
				else
				{
					int num = args.Length;
					Span<jvalue> span2 = new Span<jvalue>(stackalloc byte[unchecked((UIntPtr)num) * (UIntPtr)sizeof(jvalue)], num);
					span = span2;
				}
				Span<jvalue> span3 = span;
				bool flag = span3.Length > 0;
				if (flag)
				{
					AndroidJNISafe.PushLocalFrame(span3.Length);
					AndroidJNIHelper.CreateJNIArgArray(args, span3);
				}
				try
				{
					AndroidJNISafe.CallVoidMethod(this.m_jobject, methodID, span3);
				}
				finally
				{
					bool flag2 = span3.Length > 0;
					if (flag2)
					{
						AndroidJNI.PopLocalFrame(IntPtr.Zero);
					}
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F48 File Offset: 0x00001148
		protected ReturnType _Call<ReturnType>(string methodName, params object[] args)
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, false);
			return this._Call<ReturnType>(methodID, args);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F78 File Offset: 0x00001178
		protected unsafe ReturnType _Call<ReturnType>(IntPtr methodID, params object[] args)
		{
			Span<jvalue> span3;
			checked
			{
				Span<jvalue> span;
				if (args == null || args.Length == 0)
				{
					span = default(Span<jvalue>);
				}
				else
				{
					int num = args.Length;
					Span<jvalue> span2 = new Span<jvalue>(stackalloc byte[unchecked((UIntPtr)num) * (UIntPtr)sizeof(jvalue)], num);
					span = span2;
				}
				span3 = span;
			}
			AndroidJNI.PushLocalFrame(span3.Length + 1);
			AndroidJNIHelper.CreateJNIArgArray(args, span3);
			ReturnType returnType;
			try
			{
				bool flag = AndroidReflection.IsPrimitive(typeof(ReturnType));
				if (flag)
				{
					bool flag2 = typeof(ReturnType) == typeof(int);
					if (flag2)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallIntMethod(this.m_jobject, methodID, span3));
					}
					else
					{
						bool flag3 = typeof(ReturnType) == typeof(bool);
						if (flag3)
						{
							returnType = (ReturnType)((object)AndroidJNISafe.CallBooleanMethod(this.m_jobject, methodID, span3));
						}
						else
						{
							bool flag4 = typeof(ReturnType) == typeof(byte);
							if (flag4)
							{
								Debug.LogWarning("Return type <Byte> for Java method call is obsolete, use return type <SByte> instead");
								returnType = (ReturnType)((object)((byte)AndroidJNISafe.CallSByteMethod(this.m_jobject, methodID, span3)));
							}
							else
							{
								bool flag5 = typeof(ReturnType) == typeof(sbyte);
								if (flag5)
								{
									returnType = (ReturnType)((object)AndroidJNISafe.CallSByteMethod(this.m_jobject, methodID, span3));
								}
								else
								{
									bool flag6 = typeof(ReturnType) == typeof(short);
									if (flag6)
									{
										returnType = (ReturnType)((object)AndroidJNISafe.CallShortMethod(this.m_jobject, methodID, span3));
									}
									else
									{
										bool flag7 = typeof(ReturnType) == typeof(long);
										if (flag7)
										{
											returnType = (ReturnType)((object)AndroidJNISafe.CallLongMethod(this.m_jobject, methodID, span3));
										}
										else
										{
											bool flag8 = typeof(ReturnType) == typeof(float);
											if (flag8)
											{
												returnType = (ReturnType)((object)AndroidJNISafe.CallFloatMethod(this.m_jobject, methodID, span3));
											}
											else
											{
												bool flag9 = typeof(ReturnType) == typeof(double);
												if (flag9)
												{
													returnType = (ReturnType)((object)AndroidJNISafe.CallDoubleMethod(this.m_jobject, methodID, span3));
												}
												else
												{
													bool flag10 = typeof(ReturnType) == typeof(char);
													if (flag10)
													{
														returnType = (ReturnType)((object)AndroidJNISafe.CallCharMethod(this.m_jobject, methodID, span3));
													}
													else
													{
														returnType = default(ReturnType);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag11 = typeof(ReturnType) == typeof(string);
					if (flag11)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStringMethod(this.m_jobject, methodID, span3));
					}
					else
					{
						bool flag12 = typeof(ReturnType) == typeof(AndroidJavaClass);
						if (flag12)
						{
							IntPtr intPtr = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, span3);
							returnType = ((intPtr == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)new AndroidJavaClass(intPtr))));
						}
						else
						{
							bool flag13 = typeof(ReturnType) == typeof(AndroidJavaObject);
							if (flag13)
							{
								IntPtr intPtr2 = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, span3);
								returnType = ((intPtr2 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)new AndroidJavaObject(intPtr2))));
							}
							else
							{
								bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(ReturnType));
								if (!flag14)
								{
									string text = "JNI: Unknown return type '";
									Type typeFromHandle = typeof(ReturnType);
									throw new Exception(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + "'");
								}
								IntPtr intPtr3 = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, span3);
								returnType = AndroidJavaObject.FromJavaArray<ReturnType>(intPtr3);
							}
						}
					}
				}
			}
			finally
			{
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
			}
			return returnType;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000033EC File Offset: 0x000015EC
		protected FieldType _Get<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, false);
			return this._Get<FieldType>(fieldID);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003418 File Offset: 0x00001618
		protected FieldType _Get<FieldType>(IntPtr fieldID)
		{
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			FieldType fieldType;
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					fieldType = (FieldType)((object)AndroidJNISafe.GetIntField(this.m_jobject, fieldID));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						fieldType = (FieldType)((object)AndroidJNISafe.GetBooleanField(this.m_jobject, fieldID));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java get field call is obsolete, use field type <SByte> instead");
							fieldType = (FieldType)((object)((byte)AndroidJNISafe.GetSByteField(this.m_jobject, fieldID)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								fieldType = (FieldType)((object)AndroidJNISafe.GetSByteField(this.m_jobject, fieldID));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									fieldType = (FieldType)((object)AndroidJNISafe.GetShortField(this.m_jobject, fieldID));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										fieldType = (FieldType)((object)AndroidJNISafe.GetLongField(this.m_jobject, fieldID));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											fieldType = (FieldType)((object)AndroidJNISafe.GetFloatField(this.m_jobject, fieldID));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												fieldType = (FieldType)((object)AndroidJNISafe.GetDoubleField(this.m_jobject, fieldID));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													fieldType = (FieldType)((object)AndroidJNISafe.GetCharField(this.m_jobject, fieldID));
												}
												else
												{
													fieldType = default(FieldType);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					fieldType = (FieldType)((object)AndroidJNISafe.GetStringField(this.m_jobject, fieldID));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						IntPtr objectField = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
						fieldType = ((objectField == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(objectField))));
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							IntPtr objectField2 = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
							fieldType = ((objectField2 == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(objectField2))));
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
							if (!flag14)
							{
								string text = "JNI: Unknown field type '";
								Type typeFromHandle = typeof(FieldType);
								throw new Exception(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + "'");
							}
							IntPtr objectField3 = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
							fieldType = AndroidJavaObject.FromJavaArrayDeleteLocalRef<FieldType>(objectField3);
						}
					}
				}
			}
			return fieldType;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000037FC File Offset: 0x000019FC
		protected void _Set<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, false);
			this._Set<FieldType>(fieldID, val);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003828 File Offset: 0x00001A28
		protected void _Set<FieldType>(IntPtr fieldID, FieldType val)
		{
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					AndroidJNISafe.SetIntField(this.m_jobject, fieldID, (int)((object)val));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						AndroidJNISafe.SetBooleanField(this.m_jobject, fieldID, (bool)((object)val));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java set field call is obsolete, use field type <SByte> instead");
							AndroidJNISafe.SetSByteField(this.m_jobject, fieldID, (sbyte)((byte)((object)val)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								AndroidJNISafe.SetSByteField(this.m_jobject, fieldID, (sbyte)((object)val));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									AndroidJNISafe.SetShortField(this.m_jobject, fieldID, (short)((object)val));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										AndroidJNISafe.SetLongField(this.m_jobject, fieldID, (long)((object)val));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											AndroidJNISafe.SetFloatField(this.m_jobject, fieldID, (float)((object)val));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												AndroidJNISafe.SetDoubleField(this.m_jobject, fieldID, (double)((object)val));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													AndroidJNISafe.SetCharField(this.m_jobject, fieldID, (char)((object)val));
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					AndroidJNISafe.SetStringField(this.m_jobject, fieldID, (string)((object)val));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaClass)((object)val)).m_jclass);
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaObject)((object)val)).m_jobject);
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(AndroidJavaProxy), typeof(FieldType));
							if (flag14)
							{
								AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaProxy)((object)val)).GetRawProxy());
							}
							else
							{
								bool flag15 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
								if (!flag15)
								{
									string text = "JNI: Unknown field type '";
									Type typeFromHandle = typeof(FieldType);
									throw new Exception(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + "'");
								}
								IntPtr intPtr = AndroidJNIHelper.ConvertToJNIArray((Array)((object)val));
								AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, intPtr);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003C60 File Offset: 0x00001E60
		protected void _CallStatic(string methodName, params object[] args)
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID(this.m_jclass, methodName, args, true);
			this._CallStatic(methodID, args);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003C8C File Offset: 0x00001E8C
		protected unsafe void _CallStatic(IntPtr methodID, params object[] args)
		{
			checked
			{
				Span<jvalue> span;
				if (args == null || args.Length == 0)
				{
					span = default(Span<jvalue>);
				}
				else
				{
					int num = args.Length;
					Span<jvalue> span2 = new Span<jvalue>(stackalloc byte[unchecked((UIntPtr)num) * (UIntPtr)sizeof(jvalue)], num);
					span = span2;
				}
				Span<jvalue> span3 = span;
				bool flag = span3.Length > 0;
				if (flag)
				{
					AndroidJNISafe.PushLocalFrame(span3.Length);
					AndroidJNIHelper.CreateJNIArgArray(args, span3);
				}
				try
				{
					AndroidJNISafe.CallStaticVoidMethod(this.m_jclass, methodID, span3);
				}
				finally
				{
					bool flag2 = span3.Length > 0;
					if (flag2)
					{
						AndroidJNI.PopLocalFrame(IntPtr.Zero);
					}
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003D34 File Offset: 0x00001F34
		protected ReturnType _CallStatic<ReturnType>(string methodName, params object[] args)
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, true);
			return this._CallStatic<ReturnType>(methodID, args);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003D64 File Offset: 0x00001F64
		protected unsafe ReturnType _CallStatic<ReturnType>(IntPtr methodID, params object[] args)
		{
			Span<jvalue> span3;
			checked
			{
				Span<jvalue> span;
				if (args == null || args.Length == 0)
				{
					span = default(Span<jvalue>);
				}
				else
				{
					int num = args.Length;
					Span<jvalue> span2 = new Span<jvalue>(stackalloc byte[unchecked((UIntPtr)num) * (UIntPtr)sizeof(jvalue)], num);
					span = span2;
				}
				span3 = span;
			}
			AndroidJNI.PushLocalFrame(span3.Length + 1);
			AndroidJNIHelper.CreateJNIArgArray(args, span3);
			ReturnType returnType;
			try
			{
				bool flag = AndroidReflection.IsPrimitive(typeof(ReturnType));
				if (flag)
				{
					bool flag2 = typeof(ReturnType) == typeof(int);
					if (flag2)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticIntMethod(this.m_jclass, methodID, span3));
					}
					else
					{
						bool flag3 = typeof(ReturnType) == typeof(bool);
						if (flag3)
						{
							returnType = (ReturnType)((object)AndroidJNISafe.CallStaticBooleanMethod(this.m_jclass, methodID, span3));
						}
						else
						{
							bool flag4 = typeof(ReturnType) == typeof(byte);
							if (flag4)
							{
								Debug.LogWarning("Return type <Byte> for Java method call is obsolete, use return type <SByte> instead");
								returnType = (ReturnType)((object)((byte)AndroidJNISafe.CallStaticSByteMethod(this.m_jclass, methodID, span3)));
							}
							else
							{
								bool flag5 = typeof(ReturnType) == typeof(sbyte);
								if (flag5)
								{
									returnType = (ReturnType)((object)AndroidJNISafe.CallStaticSByteMethod(this.m_jclass, methodID, span3));
								}
								else
								{
									bool flag6 = typeof(ReturnType) == typeof(short);
									if (flag6)
									{
										returnType = (ReturnType)((object)AndroidJNISafe.CallStaticShortMethod(this.m_jclass, methodID, span3));
									}
									else
									{
										bool flag7 = typeof(ReturnType) == typeof(long);
										if (flag7)
										{
											returnType = (ReturnType)((object)AndroidJNISafe.CallStaticLongMethod(this.m_jclass, methodID, span3));
										}
										else
										{
											bool flag8 = typeof(ReturnType) == typeof(float);
											if (flag8)
											{
												returnType = (ReturnType)((object)AndroidJNISafe.CallStaticFloatMethod(this.m_jclass, methodID, span3));
											}
											else
											{
												bool flag9 = typeof(ReturnType) == typeof(double);
												if (flag9)
												{
													returnType = (ReturnType)((object)AndroidJNISafe.CallStaticDoubleMethod(this.m_jclass, methodID, span3));
												}
												else
												{
													bool flag10 = typeof(ReturnType) == typeof(char);
													if (flag10)
													{
														returnType = (ReturnType)((object)AndroidJNISafe.CallStaticCharMethod(this.m_jclass, methodID, span3));
													}
													else
													{
														returnType = default(ReturnType);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag11 = typeof(ReturnType) == typeof(string);
					if (flag11)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticStringMethod(this.m_jclass, methodID, span3));
					}
					else
					{
						bool flag12 = typeof(ReturnType) == typeof(AndroidJavaClass);
						if (flag12)
						{
							IntPtr intPtr = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, span3);
							returnType = ((intPtr == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)new AndroidJavaClass(intPtr))));
						}
						else
						{
							bool flag13 = typeof(ReturnType) == typeof(AndroidJavaObject);
							if (flag13)
							{
								IntPtr intPtr2 = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, span3);
								returnType = ((intPtr2 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)new AndroidJavaObject(intPtr2))));
							}
							else
							{
								bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(ReturnType));
								if (!flag14)
								{
									string text = "JNI: Unknown return type '";
									Type typeFromHandle = typeof(ReturnType);
									throw new Exception(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + "'");
								}
								IntPtr intPtr3 = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, span3);
								returnType = AndroidJavaObject.FromJavaArray<ReturnType>(intPtr3);
							}
						}
					}
				}
			}
			finally
			{
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
			}
			return returnType;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000041D8 File Offset: 0x000023D8
		protected FieldType _GetStatic<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, true);
			return this._GetStatic<FieldType>(fieldID);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004204 File Offset: 0x00002404
		protected FieldType _GetStatic<FieldType>(IntPtr fieldID)
		{
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			FieldType fieldType;
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					fieldType = (FieldType)((object)AndroidJNISafe.GetStaticIntField(this.m_jclass, fieldID));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						fieldType = (FieldType)((object)AndroidJNISafe.GetStaticBooleanField(this.m_jclass, fieldID));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java get field call is obsolete, use field type <SByte> instead");
							fieldType = (FieldType)((object)((byte)AndroidJNISafe.GetStaticSByteField(this.m_jclass, fieldID)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								fieldType = (FieldType)((object)AndroidJNISafe.GetStaticSByteField(this.m_jclass, fieldID));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									fieldType = (FieldType)((object)AndroidJNISafe.GetStaticShortField(this.m_jclass, fieldID));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										fieldType = (FieldType)((object)AndroidJNISafe.GetStaticLongField(this.m_jclass, fieldID));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											fieldType = (FieldType)((object)AndroidJNISafe.GetStaticFloatField(this.m_jclass, fieldID));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												fieldType = (FieldType)((object)AndroidJNISafe.GetStaticDoubleField(this.m_jclass, fieldID));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													fieldType = (FieldType)((object)AndroidJNISafe.GetStaticCharField(this.m_jclass, fieldID));
												}
												else
												{
													fieldType = default(FieldType);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					fieldType = (FieldType)((object)AndroidJNISafe.GetStaticStringField(this.m_jclass, fieldID));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						IntPtr staticObjectField = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
						fieldType = ((staticObjectField == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(staticObjectField))));
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							IntPtr staticObjectField2 = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
							fieldType = ((staticObjectField2 == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(staticObjectField2))));
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
							if (!flag14)
							{
								string text = "JNI: Unknown field type '";
								Type typeFromHandle = typeof(FieldType);
								throw new Exception(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + "'");
							}
							IntPtr staticObjectField3 = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
							fieldType = AndroidJavaObject.FromJavaArrayDeleteLocalRef<FieldType>(staticObjectField3);
						}
					}
				}
			}
			return fieldType;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000045E8 File Offset: 0x000027E8
		protected void _SetStatic<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, true);
			this._SetStatic<FieldType>(fieldID, val);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004614 File Offset: 0x00002814
		protected void _SetStatic<FieldType>(IntPtr fieldID, FieldType val)
		{
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					AndroidJNISafe.SetStaticIntField(this.m_jclass, fieldID, (int)((object)val));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						AndroidJNISafe.SetStaticBooleanField(this.m_jclass, fieldID, (bool)((object)val));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java set field call is obsolete, use field type <SByte> instead");
							AndroidJNISafe.SetStaticSByteField(this.m_jclass, fieldID, (sbyte)((byte)((object)val)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								AndroidJNISafe.SetStaticSByteField(this.m_jclass, fieldID, (sbyte)((object)val));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									AndroidJNISafe.SetStaticShortField(this.m_jclass, fieldID, (short)((object)val));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										AndroidJNISafe.SetStaticLongField(this.m_jclass, fieldID, (long)((object)val));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											AndroidJNISafe.SetStaticFloatField(this.m_jclass, fieldID, (float)((object)val));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												AndroidJNISafe.SetStaticDoubleField(this.m_jclass, fieldID, (double)((object)val));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													AndroidJNISafe.SetStaticCharField(this.m_jclass, fieldID, (char)((object)val));
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					AndroidJNISafe.SetStaticStringField(this.m_jclass, fieldID, (string)((object)val));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaClass)((object)val)).m_jclass);
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaObject)((object)val)).m_jobject);
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(AndroidJavaProxy), typeof(FieldType));
							if (flag14)
							{
								AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaProxy)((object)val)).GetRawProxy());
							}
							else
							{
								bool flag15 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
								if (!flag15)
								{
									string text = "JNI: Unknown field type '";
									Type typeFromHandle = typeof(FieldType);
									throw new Exception(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + "'");
								}
								IntPtr intPtr = AndroidJNIHelper.ConvertToJNIArray((Array)((object)val));
								AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, intPtr);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004A48 File Offset: 0x00002C48
		internal static AndroidJavaObject AndroidJavaObjectDeleteLocalRef(IntPtr jobject)
		{
			AndroidJavaObject androidJavaObject;
			try
			{
				androidJavaObject = new AndroidJavaObject(jobject);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jobject);
			}
			return androidJavaObject;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004A7C File Offset: 0x00002C7C
		internal static AndroidJavaClass AndroidJavaClassDeleteLocalRef(IntPtr jclass)
		{
			AndroidJavaClass androidJavaClass;
			try
			{
				androidJavaClass = new AndroidJavaClass(jclass);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jclass);
			}
			return androidJavaClass;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004AB0 File Offset: 0x00002CB0
		internal static ReturnType FromJavaArrayDeleteLocalRef<ReturnType>(IntPtr jobject)
		{
			bool flag = jobject == IntPtr.Zero;
			ReturnType returnType;
			if (flag)
			{
				returnType = default(ReturnType);
			}
			else
			{
				try
				{
					returnType = (ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(jobject));
				}
				finally
				{
					AndroidJNISafe.DeleteLocalRef(jobject);
				}
			}
			return returnType;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004B0C File Offset: 0x00002D0C
		internal static ReturnType FromJavaArray<ReturnType>(IntPtr jobject)
		{
			bool flag = jobject == IntPtr.Zero;
			ReturnType returnType;
			if (flag)
			{
				returnType = default(ReturnType);
			}
			else
			{
				returnType = (ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(jobject));
			}
			return returnType;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004B4C File Offset: 0x00002D4C
		protected IntPtr _GetRawObject()
		{
			return (this.m_jobject == null) ? IntPtr.Zero : this.m_jobject;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004B78 File Offset: 0x00002D78
		protected IntPtr _GetRawClass()
		{
			return this.m_jclass;
		}

		// Token: 0x04000009 RID: 9
		private static bool enableDebugPrints;

		// Token: 0x0400000A RID: 10
		internal GlobalJavaObjectRef m_jobject;

		// Token: 0x0400000B RID: 11
		internal GlobalJavaObjectRef m_jclass;
	}
}
