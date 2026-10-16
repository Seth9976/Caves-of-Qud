using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MonoMod.Utils
{
	// Token: 0x0200043E RID: 1086
	internal static class DynDll
	{
		// Token: 0x060016D3 RID: 5843 RVA: 0x0004C687 File Offset: 0x0004A887
		public static T AsDelegate<T>(this IntPtr s) where T : class
		{
			return Marshal.GetDelegateForFunctionPointer(s, typeof(T)) as T;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0004C6A3 File Offset: 0x0004A8A3
		public static void ResolveDynDllImports(this Type type, Dictionary<string, List<DynDllMapping>> mappings = null)
		{
			DynDll.InternalResolveDynDllImports(type, null, mappings);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0004C6AD File Offset: 0x0004A8AD
		public static void ResolveDynDllImports(object instance, Dictionary<string, List<DynDllMapping>> mappings = null)
		{
			DynDll.InternalResolveDynDllImports(instance.GetType(), instance, mappings);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0004C6BC File Offset: 0x0004A8BC
		private static void InternalResolveDynDllImports(Type type, object instance, Dictionary<string, List<DynDllMapping>> mappings)
		{
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			if (instance == null)
			{
				bindingFlags |= BindingFlags.Static;
			}
			else
			{
				bindingFlags |= BindingFlags.Instance;
			}
			foreach (FieldInfo fieldInfo in type.GetFields(bindingFlags))
			{
				bool flag = true;
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(DynDllImportAttribute), true);
				int j = 0;
				while (j < customAttributes.Length)
				{
					DynDllImportAttribute dynDllImportAttribute = (DynDllImportAttribute)customAttributes[j];
					flag = false;
					IntPtr zero = IntPtr.Zero;
					List<DynDllMapping> list;
					if (mappings != null && mappings.TryGetValue(dynDllImportAttribute.LibraryName, out list))
					{
						bool flag2 = false;
						foreach (DynDllMapping dynDllMapping in list)
						{
							if (DynDll.TryOpenLibrary(dynDllMapping.LibraryName, out zero, true, dynDllMapping.Flags))
							{
								flag2 = true;
								break;
							}
						}
						if (flag2)
						{
							goto IL_00DC;
						}
					}
					else if (DynDll.TryOpenLibrary(dynDllImportAttribute.LibraryName, out zero, false, null))
					{
						goto IL_00DC;
					}
					IL_0158:
					j++;
					continue;
					IL_00DC:
					foreach (string text in dynDllImportAttribute.EntryPoints.Concat<string>(new string[]
					{
						fieldInfo.Name,
						fieldInfo.FieldType.Name
					}))
					{
						IntPtr intPtr;
						if (zero.TryGetFunction(text, out intPtr))
						{
							fieldInfo.SetValue(instance, Marshal.GetDelegateForFunctionPointer(intPtr, fieldInfo.FieldType));
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						goto IL_0158;
					}
					break;
				}
				if (!flag)
				{
					throw new EntryPointNotFoundException("No matching entry point found for " + fieldInfo.Name + " in " + fieldInfo.DeclaringType.FullName);
				}
			}
		}

		// Token: 0x060016D7 RID: 5847
		[DllImport("kernel32", SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x060016D8 RID: 5848
		[DllImport("kernel32", SetLastError = true)]
		private static extern IntPtr LoadLibrary(string lpFileName);

		// Token: 0x060016D9 RID: 5849
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool FreeLibrary(IntPtr hLibModule);

		// Token: 0x060016DA RID: 5850
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x060016DB RID: 5851
		[DllImport("dl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "dlopen")]
		private static extern IntPtr dl_dlopen(string filename, int flags);

		// Token: 0x060016DC RID: 5852
		[DllImport("dl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "dlclose")]
		private static extern bool dl_dlclose(IntPtr handle);

		// Token: 0x060016DD RID: 5853
		[DllImport("dl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "dlsym")]
		private static extern IntPtr dl_dlsym(IntPtr handle, string symbol);

		// Token: 0x060016DE RID: 5854
		[DllImport("dl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "dlerror")]
		private static extern IntPtr dl_dlerror();

		// Token: 0x060016DF RID: 5855
		[DllImport("libdl.so.2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "dlopen")]
		private static extern IntPtr dl2_dlopen(string filename, int flags);

		// Token: 0x060016E0 RID: 5856
		[DllImport("libdl.so.2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "dlclose")]
		private static extern bool dl2_dlclose(IntPtr handle);

		// Token: 0x060016E1 RID: 5857
		[DllImport("libdl.so.2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "dlsym")]
		private static extern IntPtr dl2_dlsym(IntPtr handle, string symbol);

		// Token: 0x060016E2 RID: 5858
		[DllImport("libdl.so.2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "dlerror")]
		private static extern IntPtr dl2_dlerror();

		// Token: 0x060016E3 RID: 5859 RVA: 0x0004C888 File Offset: 0x0004AA88
		private static IntPtr dlopen(string filename, int flags)
		{
			IntPtr intPtr;
			for (;;)
			{
				try
				{
					int num = DynDll.dlVersion;
					if (num != 0 && num == 1)
					{
						intPtr = DynDll.dl2_dlopen(filename, flags);
					}
					else
					{
						intPtr = DynDll.dl_dlopen(filename, flags);
					}
				}
				catch (DllNotFoundException obj) when (DynDll.dlVersion > 0)
				{
					DynDll.dlVersion--;
					continue;
				}
				break;
			}
			return intPtr;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0004C8F4 File Offset: 0x0004AAF4
		private static bool dlclose(IntPtr handle)
		{
			bool flag;
			for (;;)
			{
				try
				{
					int num = DynDll.dlVersion;
					if (num != 0 && num == 1)
					{
						flag = DynDll.dl2_dlclose(handle);
					}
					else
					{
						flag = DynDll.dl_dlclose(handle);
					}
				}
				catch (DllNotFoundException obj) when (DynDll.dlVersion > 0)
				{
					DynDll.dlVersion--;
					continue;
				}
				break;
			}
			return flag;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0004C95C File Offset: 0x0004AB5C
		private static IntPtr dlsym(IntPtr handle, string symbol)
		{
			IntPtr intPtr;
			for (;;)
			{
				try
				{
					int num = DynDll.dlVersion;
					if (num != 0 && num == 1)
					{
						intPtr = DynDll.dl2_dlsym(handle, symbol);
					}
					else
					{
						intPtr = DynDll.dl_dlsym(handle, symbol);
					}
				}
				catch (DllNotFoundException obj) when (DynDll.dlVersion > 0)
				{
					DynDll.dlVersion--;
					continue;
				}
				break;
			}
			return intPtr;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0004C9C8 File Offset: 0x0004ABC8
		private static IntPtr dlerror()
		{
			IntPtr intPtr;
			for (;;)
			{
				try
				{
					int num = DynDll.dlVersion;
					if (num != 0 && num == 1)
					{
						intPtr = DynDll.dl2_dlerror();
					}
					else
					{
						intPtr = DynDll.dl_dlerror();
					}
				}
				catch (DllNotFoundException obj) when (DynDll.dlVersion > 0)
				{
					DynDll.dlVersion--;
					continue;
				}
				break;
			}
			return intPtr;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0004CA30 File Offset: 0x0004AC30
		static DynDll()
		{
			if (!PlatformHelper.Is(Platform.Windows))
			{
				DynDll.dlerror();
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0004CA54 File Offset: 0x0004AC54
		private static bool CheckError(out Exception exception)
		{
			if (PlatformHelper.Is(Platform.Windows))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 0)
				{
					exception = new Win32Exception(lastWin32Error);
					return false;
				}
			}
			else
			{
				IntPtr intPtr = DynDll.dlerror();
				if (intPtr != IntPtr.Zero)
				{
					exception = new Win32Exception(Marshal.PtrToStringAnsi(intPtr));
					return false;
				}
			}
			exception = null;
			return true;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0004CAA4 File Offset: 0x0004ACA4
		public static IntPtr OpenLibrary(string name, bool skipMapping = false, int? flags = null)
		{
			IntPtr intPtr;
			if (!DynDll.InternalTryOpenLibrary(name, out intPtr, skipMapping, flags))
			{
				throw new DllNotFoundException("Unable to load library '" + name + "'");
			}
			Exception ex;
			if (!DynDll.CheckError(out ex))
			{
				throw ex;
			}
			return intPtr;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0004CAE0 File Offset: 0x0004ACE0
		public static bool TryOpenLibrary(string name, out IntPtr libraryPtr, bool skipMapping = false, int? flags = null)
		{
			Exception ex;
			return DynDll.InternalTryOpenLibrary(name, out libraryPtr, skipMapping, flags) || DynDll.CheckError(out ex);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0004CB04 File Offset: 0x0004AD04
		private static bool InternalTryOpenLibrary(string name, out IntPtr libraryPtr, bool skipMapping, int? flags)
		{
			List<DynDllMapping> list;
			if (name != null && !skipMapping && DynDll.Mappings.TryGetValue(name, out list))
			{
				foreach (DynDllMapping dynDllMapping in list)
				{
					if (DynDll.InternalTryOpenLibrary(dynDllMapping.LibraryName, out libraryPtr, true, dynDllMapping.Flags))
					{
						return true;
					}
				}
				libraryPtr = IntPtr.Zero;
				return true;
			}
			if (PlatformHelper.Is(Platform.Windows))
			{
				libraryPtr = ((name == null) ? DynDll.GetModuleHandle(name) : DynDll.LoadLibrary(name));
			}
			else
			{
				int num = flags ?? 258;
				libraryPtr = DynDll.dlopen(name, num);
				if (libraryPtr == IntPtr.Zero && File.Exists(name))
				{
					libraryPtr = DynDll.dlopen(Path.GetFullPath(name), num);
				}
			}
			return libraryPtr != IntPtr.Zero;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0004CBFC File Offset: 0x0004ADFC
		public static bool CloseLibrary(IntPtr lib)
		{
			if (PlatformHelper.Is(Platform.Windows))
			{
				DynDll.CloseLibrary(lib);
			}
			else
			{
				DynDll.dlclose(lib);
			}
			Exception ex;
			return DynDll.CheckError(out ex);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0004CC2C File Offset: 0x0004AE2C
		public static IntPtr GetFunction(this IntPtr libraryPtr, string name)
		{
			IntPtr intPtr;
			if (!DynDll.InternalTryGetFunction(libraryPtr, name, out intPtr))
			{
				throw new MissingMethodException("Unable to load function '" + name + "'");
			}
			Exception ex;
			if (!DynDll.CheckError(out ex))
			{
				throw ex;
			}
			return intPtr;
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0004CC68 File Offset: 0x0004AE68
		public static bool TryGetFunction(this IntPtr libraryPtr, string name, out IntPtr functionPtr)
		{
			Exception ex;
			return DynDll.InternalTryGetFunction(libraryPtr, name, out functionPtr) || DynDll.CheckError(out ex);
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0004CC88 File Offset: 0x0004AE88
		private static bool InternalTryGetFunction(IntPtr libraryPtr, string name, out IntPtr functionPtr)
		{
			if (libraryPtr == IntPtr.Zero)
			{
				throw new ArgumentNullException("libraryPtr");
			}
			functionPtr = (PlatformHelper.Is(Platform.Windows) ? DynDll.GetProcAddress(libraryPtr, name) : DynDll.dlsym(libraryPtr, name));
			return functionPtr != IntPtr.Zero;
		}

		// Token: 0x04000FF6 RID: 4086
		public static Dictionary<string, List<DynDllMapping>> Mappings = new Dictionary<string, List<DynDllMapping>>();

		// Token: 0x04000FF7 RID: 4087
		private static int dlVersion = 1;

		// Token: 0x0200043F RID: 1087
		public static class DlopenFlags
		{
			// Token: 0x04000FF8 RID: 4088
			public const int RTLD_LAZY = 1;

			// Token: 0x04000FF9 RID: 4089
			public const int RTLD_NOW = 2;

			// Token: 0x04000FFA RID: 4090
			public const int RTLD_LOCAL = 0;

			// Token: 0x04000FFB RID: 4091
			public const int RTLD_GLOBAL = 256;
		}
	}
}
