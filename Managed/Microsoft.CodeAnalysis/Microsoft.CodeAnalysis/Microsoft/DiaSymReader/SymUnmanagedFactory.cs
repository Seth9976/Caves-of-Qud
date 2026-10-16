using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000F2 RID: 242
	internal static class SymUnmanagedFactory
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00018858 File Offset: 0x00016A58
		internal static string DiaSymReaderModuleName
		{
			get
			{
				if (IntPtr.Size != 4)
				{
					return "Microsoft.DiaSymReader.Native.amd64.dll";
				}
				return "Microsoft.DiaSymReader.Native.x86.dll";
			}
		}

		// Token: 0x06000855 RID: 2133
		[DefaultDllImportSearchPaths(4098)]
		[DllImport("Microsoft.DiaSymReader.Native.x86.dll", EntryPoint = "CreateSymReader")]
		private static extern void CreateSymReader32(ref Guid id, [MarshalAs(25)] out object symReader);

		// Token: 0x06000856 RID: 2134
		[DefaultDllImportSearchPaths(4098)]
		[DllImport("Microsoft.DiaSymReader.Native.amd64.dll", EntryPoint = "CreateSymReader")]
		private static extern void CreateSymReader64(ref Guid id, [MarshalAs(25)] out object symReader);

		// Token: 0x06000857 RID: 2135
		[DefaultDllImportSearchPaths(4098)]
		[DllImport("Microsoft.DiaSymReader.Native.x86.dll", EntryPoint = "CreateSymWriter")]
		private static extern void CreateSymWriter32(ref Guid id, [MarshalAs(25)] out object symWriter);

		// Token: 0x06000858 RID: 2136
		[DefaultDllImportSearchPaths(4098)]
		[DllImport("Microsoft.DiaSymReader.Native.amd64.dll", EntryPoint = "CreateSymWriter")]
		private static extern void CreateSymWriter64(ref Guid id, [MarshalAs(25)] out object symWriter);

		// Token: 0x06000859 RID: 2137
		[DllImport("kernel32")]
		private static extern IntPtr LoadLibrary(string path);

		// Token: 0x0600085A RID: 2138
		[DllImport("kernel32")]
		private static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x0600085B RID: 2139
		[DllImport("kernel32")]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

		// Token: 0x0600085C RID: 2140 RVA: 0x00018870 File Offset: 0x00016A70
		internal static string GetEnvironmentVariable(string name)
		{
			string text;
			try
			{
				Func<string, string> value = SymUnmanagedFactory.s_lazyGetEnvironmentVariable.Value;
				text = ((value != null) ? value.Invoke(name) : null);
			}
			catch
			{
				text = null;
			}
			return text;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000188B0 File Offset: 0x00016AB0
		private static object TryLoadFromAlternativePath(Guid clsid, string factoryName)
		{
			string environmentVariable = SymUnmanagedFactory.GetEnvironmentVariable("MICROSOFT_DIASYMREADER_NATIVE_ALT_LOAD_PATH");
			if (string.IsNullOrEmpty(environmentVariable))
			{
				return null;
			}
			IntPtr intPtr = SymUnmanagedFactory.LoadLibrary(Path.Combine(environmentVariable, SymUnmanagedFactory.DiaSymReaderModuleName));
			if (intPtr == IntPtr.Zero)
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			object obj = null;
			try
			{
				IntPtr procAddress = SymUnmanagedFactory.GetProcAddress(intPtr, factoryName);
				if (procAddress == IntPtr.Zero)
				{
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
				Marshal.GetDelegateForFunctionPointer<SymUnmanagedFactory.NativeFactory>(procAddress)(ref clsid, out obj);
			}
			finally
			{
				if (obj == null && !SymUnmanagedFactory.FreeLibrary(intPtr))
				{
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
			}
			return obj;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00018954 File Offset: 0x00016B54
		private static Type GetComTypeType(ref Type lazyType, Guid clsid)
		{
			if (lazyType == null)
			{
				lazyType = Marshal.GetTypeFromCLSID(clsid);
			}
			return lazyType;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001896C File Offset: 0x00016B6C
		internal static object CreateObject(bool createReader, bool useAlternativeLoadPath, bool useComRegistry, out string moduleName, out Exception loadException)
		{
			object obj = null;
			loadException = null;
			moduleName = null;
			Guid guid;
			guid..ctor(createReader ? "0A3976C5-4529-4ef8-B0B0-42EED37082CD" : "0AE2DEB0-F901-478b-BB9F-881EE8066788");
			try
			{
				try
				{
					if (IntPtr.Size == 4)
					{
						if (createReader)
						{
							SymUnmanagedFactory.CreateSymReader32(ref guid, out obj);
						}
						else
						{
							SymUnmanagedFactory.CreateSymWriter32(ref guid, out obj);
						}
					}
					else if (createReader)
					{
						SymUnmanagedFactory.CreateSymReader64(ref guid, out obj);
					}
					else
					{
						SymUnmanagedFactory.CreateSymWriter64(ref guid, out obj);
					}
				}
				catch (DllNotFoundException ex) when (useAlternativeLoadPath)
				{
					obj = SymUnmanagedFactory.TryLoadFromAlternativePath(guid, createReader ? "CreateSymReader" : "CreateSymWriter");
					if (obj == null)
					{
						loadException = ex;
					}
				}
			}
			catch (Exception ex2)
			{
				loadException = ex2;
				obj = null;
			}
			if (obj != null)
			{
				moduleName = SymUnmanagedFactory.DiaSymReaderModuleName;
			}
			else if (useComRegistry)
			{
				try
				{
					obj = Activator.CreateInstance(createReader ? SymUnmanagedFactory.GetComTypeType(ref SymUnmanagedFactory.s_lazySymReaderComType, guid) : SymUnmanagedFactory.GetComTypeType(ref SymUnmanagedFactory.s_lazySymWriterComType, guid));
					moduleName = "diasymreader.dll";
				}
				catch (Exception ex3)
				{
					loadException = ex3;
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x0400024D RID: 589
		private const string AlternateLoadPathEnvironmentVariableName = "MICROSOFT_DIASYMREADER_NATIVE_ALT_LOAD_PATH";

		// Token: 0x0400024E RID: 590
		private const string LegacyDiaSymReaderModuleName = "diasymreader.dll";

		// Token: 0x0400024F RID: 591
		private const string DiaSymReaderModuleName32 = "Microsoft.DiaSymReader.Native.x86.dll";

		// Token: 0x04000250 RID: 592
		private const string DiaSymReaderModuleName64 = "Microsoft.DiaSymReader.Native.amd64.dll";

		// Token: 0x04000251 RID: 593
		private const string CreateSymReaderFactoryName = "CreateSymReader";

		// Token: 0x04000252 RID: 594
		private const string CreateSymWriterFactoryName = "CreateSymWriter";

		// Token: 0x04000253 RID: 595
		private const string SymWriterClsid = "0AE2DEB0-F901-478b-BB9F-881EE8066788";

		// Token: 0x04000254 RID: 596
		private const string SymReaderClsid = "0A3976C5-4529-4ef8-B0B0-42EED37082CD";

		// Token: 0x04000255 RID: 597
		private static Type s_lazySymReaderComType;

		// Token: 0x04000256 RID: 598
		private static Type s_lazySymWriterComType;

		// Token: 0x04000257 RID: 599
		private static readonly Lazy<Func<string, string>> s_lazyGetEnvironmentVariable = new Lazy<Func<string, string>>(delegate
		{
			try
			{
				foreach (MethodInfo methodInfo in IntrospectionExtensions.GetTypeInfo(typeof(Environment)).GetDeclaredMethods("GetEnvironmentVariable"))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType == typeof(string))
					{
						return (Func<string, string>)methodInfo.CreateDelegate(typeof(Func<string, string>));
					}
				}
			}
			catch
			{
			}
			return null;
		});

		// Token: 0x02000533 RID: 1331
		// (Invoke) Token: 0x06003385 RID: 13189
		private delegate void NativeFactory(ref Guid id, [MarshalAs(25)] out object instance);
	}
}
