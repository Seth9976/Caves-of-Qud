using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020008F8 RID: 2296
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeMethodInfo : MethodInfo, ISerializable
	{
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004D53 RID: 19795 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004D54 RID: 19796 RVA: 0x000F40B1 File Offset: 0x000F22B1
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004D55 RID: 19797 RVA: 0x000F3A43 File Offset: 0x000F1C43
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x000F40BC File Offset: 0x000F22BC
		internal override string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			TypeNameFormatFlags typeNameFormatFlags = (serialization ? TypeNameFormatFlags.FormatSerialization : TypeNameFormatFlags.FormatBasic);
			if (this.IsGenericMethod)
			{
				stringBuilder.Append(RuntimeMethodHandle.ConstructInstantiation(this, typeNameFormatFlags));
			}
			stringBuilder.Append("(");
			RuntimeParameterInfo.FormatParameters(stringBuilder, this.GetParametersNoCopy(), this.CallingConvention, serialization);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x000F4128 File Offset: 0x000F2328
		public override Delegate CreateDelegate(Type delegateType)
		{
			return Delegate.CreateDelegate(delegateType, this);
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x000F4131 File Offset: 0x000F2331
		public override Delegate CreateDelegate(Type delegateType, object target)
		{
			return Delegate.CreateDelegate(delegateType, target, this);
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x000F413B File Offset: 0x000F233B
		public override string ToString()
		{
			return this.ReturnType.FormatTypeName() + " " + this.FormatNameAndSig(false);
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x000F4159 File Offset: 0x000F2359
		internal RuntimeModule GetRuntimeModule()
		{
			return ((RuntimeType)this.DeclaringType).GetRuntimeModule();
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x000F416C File Offset: 0x000F236C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Method, (this.IsGenericMethod & !this.IsGenericMethodDefinition) ? this.GetGenericArguments() : null);
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x000F41C1 File Offset: 0x000F23C1
		internal string SerializationToString()
		{
			return this.ReturnType.FormatTypeName(true) + " " + this.FormatNameAndSig(true);
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x000F41E0 File Offset: 0x000F23E0
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(handle.Value, IntPtr.Zero, false);
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x000F41F4 File Offset: 0x000F23F4
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle, RuntimeTypeHandle reflectedType)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(handle.Value, reflectedType.Value, false);
		}

		// Token: 0x06004D5F RID: 19807
		[PreserveDependency(".ctor(System.Reflection.ExceptionHandlingClause[],System.Reflection.LocalVariableInfo[],System.Byte[],System.Boolean,System.Int32,System.Int32)", "System.Reflection.MethodBody")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodBody GetMethodBodyInternal(IntPtr handle);

		// Token: 0x06004D60 RID: 19808 RVA: 0x000F420A File Offset: 0x000F240A
		internal static MethodBody GetMethodBody(IntPtr handle)
		{
			return RuntimeMethodInfo.GetMethodBodyInternal(handle);
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x000F4212 File Offset: 0x000F2412
		internal static MethodBase GetMethodFromHandleInternalType(IntPtr method_handle, IntPtr type_handle)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(method_handle, type_handle, true);
		}

		// Token: 0x06004D62 RID: 19810
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MethodBase GetMethodFromHandleInternalType_native(IntPtr method_handle, IntPtr type_handle, bool genericCheck);

		// Token: 0x06004D63 RID: 19811 RVA: 0x000F421C File Offset: 0x000F241C
		internal RuntimeMethodInfo()
		{
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x000F4224 File Offset: 0x000F2424
		internal RuntimeMethodInfo(RuntimeMethodHandle mhandle)
		{
			this.mhandle = mhandle.Value;
		}

		// Token: 0x06004D65 RID: 19813
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string get_name(MethodBase method);

		// Token: 0x06004D66 RID: 19814
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodInfo get_base_method(RuntimeMethodInfo method, bool definition);

		// Token: 0x06004D67 RID: 19815
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeMethodInfo method);

		// Token: 0x06004D68 RID: 19816 RVA: 0x000F4239 File Offset: 0x000F2439
		public override MethodInfo GetBaseDefinition()
		{
			return RuntimeMethodInfo.get_base_method(this, true);
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x000F4242 File Offset: 0x000F2442
		internal MethodInfo GetBaseMethod()
		{
			return RuntimeMethodInfo.get_base_method(this, false);
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004D6A RID: 19818 RVA: 0x000F424B File Offset: 0x000F244B
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004D6B RID: 19819 RVA: 0x000F4253 File Offset: 0x000F2453
		public override Type ReturnType
		{
			get
			{
				return MonoMethodInfo.GetReturnType(this.mhandle);
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x000F424B File Offset: 0x000F244B
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004D6D RID: 19821 RVA: 0x000F4260 File Offset: 0x000F2460
		public override int MetadataToken
		{
			get
			{
				return RuntimeMethodInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x000F4268 File Offset: 0x000F2468
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x000F4278 File Offset: 0x000F2478
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			if (parametersInfo.Length == 0)
			{
				return parametersInfo;
			}
			ParameterInfo[] array = new ParameterInfo[parametersInfo.Length];
			Array.FastCopy(parametersInfo, 0, array, 0, parametersInfo.Length);
			return array;
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x000F42AF File Offset: 0x000F24AF
		internal override ParameterInfo[] GetParametersInternal()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x000F42BD File Offset: 0x000F24BD
		internal override int GetParametersCount()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this).Length;
		}

		// Token: 0x06004D72 RID: 19826
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x06004D73 RID: 19827 RVA: 0x000F42D0 File Offset: 0x000F24D0
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (!base.IsStatic && !this.DeclaringType.IsInstanceOfType(obj))
			{
				if (obj == null)
				{
					throw new TargetException("Non-static method requires a target.");
				}
				throw new TargetException("Object does not match target type.");
			}
			else
			{
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				ParameterInfo[] parametersInternal = this.GetParametersInternal();
				RuntimeMethodInfo.ConvertValues(binder, parameters, parametersInternal, culture, invokeAttr);
				if (this.ContainsGenericParameters)
				{
					throw new InvalidOperationException("Late bound operations cannot be performed on types or methods for which ContainsGenericParameters is true.");
				}
				object obj2 = null;
				Exception ex;
				if ((invokeAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default)
				{
					try
					{
						obj2 = this.InternalInvoke(obj, parameters, out ex);
						goto IL_0090;
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (OverflowException)
					{
						throw;
					}
					catch (Exception ex2)
					{
						throw new TargetInvocationException(ex2);
					}
				}
				obj2 = this.InternalInvoke(obj, parameters, out ex);
				IL_0090:
				if (ex != null)
				{
					throw ex;
				}
				return obj2;
			}
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x000F439C File Offset: 0x000F259C
		internal static void ConvertValues(Binder binder, object[] args, ParameterInfo[] pinfo, CultureInfo culture, BindingFlags invokeAttr)
		{
			if (args == null)
			{
				if (pinfo.Length == 0)
				{
					return;
				}
				throw new TargetParameterCountException();
			}
			else
			{
				if (pinfo.Length != args.Length)
				{
					throw new TargetParameterCountException();
				}
				for (int i = 0; i < args.Length; i++)
				{
					object obj = args[i];
					ParameterInfo parameterInfo = pinfo[i];
					if (obj == Type.Missing)
					{
						if (parameterInfo.DefaultValue == DBNull.Value)
						{
							throw new ArgumentException(Environment.GetResourceString("Missing parameter does not have a default value."), "parameters");
						}
						args[i] = parameterInfo.DefaultValue;
					}
					else
					{
						RuntimeType runtimeType = (RuntimeType)parameterInfo.ParameterType;
						args[i] = runtimeType.CheckValue(obj, binder, culture, invokeAttr);
					}
				}
				return;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004D75 RID: 19829 RVA: 0x000F442A File Offset: 0x000F262A
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x000F4437 File Offset: 0x000F2637
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004D77 RID: 19831 RVA: 0x000F4444 File Offset: 0x000F2644
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004D78 RID: 19832 RVA: 0x000F4451 File Offset: 0x000F2651
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004D79 RID: 19833 RVA: 0x000F4459 File Offset: 0x000F2659
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004D7A RID: 19834 RVA: 0x000F4466 File Offset: 0x000F2666
		public override string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return RuntimeMethodInfo.get_name(this);
			}
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x00052A66 File Offset: 0x00050C66
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x000F1969 File Offset: 0x000EFB69
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x000F1972 File Offset: 0x000EFB72
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004D7E RID: 19838
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetPInvoke(out PInvokeAttributes flags, out string entryPoint, out string dllName);

		// Token: 0x06004D7F RID: 19839 RVA: 0x000F4480 File Offset: 0x000F2680
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new PreserveSigAttribute();
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				array[num++] = DllImportAttribute.GetCustomAttribute(this);
			}
			return array;
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x000F4504 File Offset: 0x000F2704
		internal CustomAttributeData[] GetPseudoCustomAttributesData()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			CustomAttributeData[] array = new CustomAttributeData[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new CustomAttributeData(typeof(PreserveSigAttribute).GetConstructor(Type.EmptyTypes));
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				array[num++] = this.GetDllImportAttributeData();
			}
			return array;
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x000F459C File Offset: 0x000F279C
		private CustomAttributeData GetDllImportAttributeData()
		{
			if ((this.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			string text = null;
			PInvokeAttributes pinvokeAttributes = PInvokeAttributes.CharSetNotSpec;
			string text2;
			this.GetPInvoke(out pinvokeAttributes, out text2, out text);
			CharSet charSet;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				goto IL_005C;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				goto IL_005C;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				goto IL_005C;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				goto IL_005C;
			}
			charSet = CharSet.None;
			IL_005C:
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			CallingConvention callingConvention;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvWinapi)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.Winapi;
					goto IL_00BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl;
					goto IL_00BB;
				}
			}
			else
			{
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvStdcall)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.StdCall;
					goto IL_00BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvThiscall)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.ThisCall;
					goto IL_00BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.FastCall;
					goto IL_00BB;
				}
			}
			callingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl;
			IL_00BB:
			bool flag = (pinvokeAttributes & PInvokeAttributes.NoMangle) > PInvokeAttributes.CharSetNotSpec;
			bool flag2 = (pinvokeAttributes & PInvokeAttributes.SupportsLastError) > PInvokeAttributes.CharSetNotSpec;
			bool flag3 = (pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
			bool flag4 = (pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
			bool flag5 = (this.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
			CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(typeof(string), text)
			};
			Type typeFromHandle = typeof(DllImportAttribute);
			CustomAttributeNamedArgument[] array2 = new CustomAttributeNamedArgument[]
			{
				new CustomAttributeNamedArgument(typeFromHandle.GetField("EntryPoint"), text2),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CharSet"), charSet),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ExactSpelling"), flag),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("SetLastError"), flag2),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("PreserveSig"), flag5),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CallingConvention"), callingConvention),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("BestFitMapping"), flag3),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ThrowOnUnmappableChar"), flag4)
			};
			return new CustomAttributeData(typeFromHandle.GetConstructor(new Type[] { typeof(string) }), array, array2);
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x000F47E8 File Offset: 0x000F29E8
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException("not a generic method definition");
			}
			if (this.GetGenericArguments().Length != methodInstantiation.Length)
			{
				throw new ArgumentException("Incorrect length");
			}
			bool flag = false;
			foreach (Type type in methodInstantiation)
			{
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				if (!(type is RuntimeType))
				{
					flag = true;
				}
			}
			if (flag)
			{
				if (RuntimeFeature.IsDynamicCodeSupported)
				{
					return new MethodOnTypeBuilderInst(this, methodInstantiation);
				}
				throw new NotSupportedException("User types are not supported under full aot");
			}
			else
			{
				MethodInfo methodInfo = this.MakeGenericMethod_impl(methodInstantiation);
				if (methodInfo == null)
				{
					throw new ArgumentException(string.Format("The method has {0} generic parameter(s) but {1} generic argument(s) were provided.", this.GetGenericArguments().Length, methodInstantiation.Length));
				}
				return methodInfo;
			}
		}

		// Token: 0x06004D83 RID: 19843
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo MakeGenericMethod_impl(Type[] types);

		// Token: 0x06004D84 RID: 19844
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetGenericArguments();

		// Token: 0x06004D85 RID: 19845
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetGenericMethodDefinition_impl();

		// Token: 0x06004D86 RID: 19846 RVA: 0x000F48AD File Offset: 0x000F2AAD
		public override MethodInfo GetGenericMethodDefinition()
		{
			MethodInfo genericMethodDefinition_impl = this.GetGenericMethodDefinition_impl();
			if (genericMethodDefinition_impl == null)
			{
				throw new InvalidOperationException();
			}
			return genericMethodDefinition_impl;
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004D87 RID: 19847
		public override extern bool IsGenericMethodDefinition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004D88 RID: 19848
		public override extern bool IsGenericMethod
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004D89 RID: 19849 RVA: 0x000F48C4 File Offset: 0x000F2AC4
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericMethod)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (genericArguments[i].ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x000F4905 File Offset: 0x000F2B05
		public override MethodBody GetMethodBody()
		{
			return RuntimeMethodInfo.GetMethodBody(this.mhandle);
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x000F3C9C File Offset: 0x000F1E9C
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004D8C RID: 19852
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004D8D RID: 19853 RVA: 0x000F4912 File Offset: 0x000F2B12
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004D8E RID: 19854 RVA: 0x000F491D File Offset: 0x000F2B1D
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004D8F RID: 19855 RVA: 0x000F4928 File Offset: 0x000F2B28
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x000F4933 File Offset: 0x000F2B33
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeMethodInfo>(other);
		}

		// Token: 0x0400305A RID: 12378
		internal IntPtr mhandle;

		// Token: 0x0400305B RID: 12379
		private string name;

		// Token: 0x0400305C RID: 12380
		private Type reftype;
	}
}
