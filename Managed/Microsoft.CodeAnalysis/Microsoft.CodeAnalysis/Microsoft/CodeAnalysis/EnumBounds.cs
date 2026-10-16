using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200014A RID: 330
	internal static class EnumBounds
	{
		// Token: 0x06000D23 RID: 3363 RVA: 0x00026FDA File Offset: 0x000251DA
		internal static bool IsValid(this OptimizationLevel value)
		{
			return value >= OptimizationLevel.Debug && value <= OptimizationLevel.Release;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00026FE9 File Offset: 0x000251E9
		internal static bool IsValid(this Platform value)
		{
			return value >= Platform.AnyCpu && value <= Platform.Arm64;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00026FF8 File Offset: 0x000251F8
		internal static bool Requires64Bit(this Platform value)
		{
			return value == Platform.X64 || value == Platform.Itanium || value == Platform.Arm64;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00027008 File Offset: 0x00025208
		internal static bool Requires32Bit(this Platform value)
		{
			return value == Platform.X86;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002700E File Offset: 0x0002520E
		internal static bool IsValid(this MetadataImportOptions value)
		{
			return value >= MetadataImportOptions.Public && value <= MetadataImportOptions.All;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002701D File Offset: 0x0002521D
		internal static bool IsValid(this MetadataImageKind kind)
		{
			return kind >= MetadataImageKind.Assembly && kind <= MetadataImageKind.Module;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002702C File Offset: 0x0002522C
		internal static bool IsValid(this OutputKind value)
		{
			return value >= OutputKind.ConsoleApplication && value <= OutputKind.WindowsRuntimeApplication;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0002703B File Offset: 0x0002523B
		[NullableContext(1)]
		internal static string GetDefaultExtension(this OutputKind kind)
		{
			switch (kind)
			{
			case OutputKind.ConsoleApplication:
			case OutputKind.WindowsApplication:
			case OutputKind.WindowsRuntimeApplication:
				return ".exe";
			case OutputKind.DynamicallyLinkedLibrary:
				return ".dll";
			case OutputKind.NetModule:
				return ".netmodule";
			case OutputKind.WindowsRuntimeMetadata:
				return ".winmdobj";
			default:
				return ".dll";
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0002707A File Offset: 0x0002527A
		internal static bool IsApplication(this OutputKind kind)
		{
			switch (kind)
			{
			case OutputKind.ConsoleApplication:
			case OutputKind.WindowsApplication:
			case OutputKind.WindowsRuntimeApplication:
				return true;
			case OutputKind.DynamicallyLinkedLibrary:
			case OutputKind.NetModule:
			case OutputKind.WindowsRuntimeMetadata:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000270A1 File Offset: 0x000252A1
		internal static bool IsNetModule(this OutputKind kind)
		{
			return kind == OutputKind.NetModule;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000270A7 File Offset: 0x000252A7
		internal static bool IsWindowsRuntime(this OutputKind kind)
		{
			return kind == OutputKind.WindowsRuntimeMetadata;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000270AD File Offset: 0x000252AD
		internal static bool IsValid(this SymbolDisplayPartKind value)
		{
			return (value >= SymbolDisplayPartKind.AliasName && value <= SymbolDisplayPartKind.RecordStructName) || (value >= (SymbolDisplayPartKind)33 && value <= (SymbolDisplayPartKind)34);
		}
	}
}
