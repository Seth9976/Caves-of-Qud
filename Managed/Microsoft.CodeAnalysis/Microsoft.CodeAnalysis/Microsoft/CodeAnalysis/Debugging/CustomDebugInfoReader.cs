using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.Debugging
{
	// Token: 0x0200035D RID: 861
	internal static class CustomDebugInfoReader
	{
		// Token: 0x060024D8 RID: 9432 RVA: 0x000725C1 File Offset: 0x000707C1
		private static void ReadGlobalHeader(byte[] bytes, ref int offset, out byte version, out byte count)
		{
			version = bytes[offset];
			count = bytes[offset + 1];
			offset += 4;
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000725D7 File Offset: 0x000707D7
		private static void ReadRecordHeader(byte[] bytes, ref int offset, out byte version, out CustomDebugInfoKind kind, out int size, out int alignmentSize)
		{
			version = bytes[offset];
			kind = (CustomDebugInfoKind)bytes[offset + 1];
			alignmentSize = (int)bytes[offset + 3];
			size = BitConverter.ToInt32(bytes, offset + 4);
			offset += 8;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00072604 File Offset: 0x00070804
		public static ImmutableArray<byte> TryGetCustomDebugInfoRecord(byte[] customDebugInfo, CustomDebugInfoKind recordKind)
		{
			foreach (CustomDebugInfoRecord customDebugInfoRecord in CustomDebugInfoReader.GetCustomDebugInfoRecords(customDebugInfo))
			{
				if (customDebugInfoRecord.Kind == recordKind)
				{
					return customDebugInfoRecord.Data;
				}
			}
			return default(ImmutableArray<byte>);
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x00072668 File Offset: 0x00070868
		public static IEnumerable<CustomDebugInfoRecord> GetCustomDebugInfoRecords(byte[] customDebugInfo)
		{
			if (customDebugInfo.Length < 4)
			{
				throw new InvalidOperationException("Invalid header.");
			}
			int offset = 0;
			byte b;
			byte b2;
			CustomDebugInfoReader.ReadGlobalHeader(customDebugInfo, ref offset, out b, out b2);
			if (b != 4)
			{
				yield break;
			}
			while (offset <= customDebugInfo.Length - 8)
			{
				byte b3;
				CustomDebugInfoKind customDebugInfoKind;
				int num;
				int num2;
				CustomDebugInfoReader.ReadRecordHeader(customDebugInfo, ref offset, out b3, out customDebugInfoKind, out num, out num2);
				if (num < 8)
				{
					throw new InvalidOperationException("Invalid header.");
				}
				if (customDebugInfoKind - CustomDebugInfoKind.EditAndContinueLocalSlotMap > 2)
				{
					num2 = 0;
				}
				int bodySize = num - 8;
				if (offset > customDebugInfo.Length - bodySize || num2 > 3 || num2 > bodySize)
				{
					throw new InvalidOperationException("Invalid header.");
				}
				yield return new CustomDebugInfoRecord(customDebugInfoKind, b3, ImmutableArray.Create<byte>(customDebugInfo, offset, bodySize - num2));
				offset += bodySize;
			}
			yield break;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x00072678 File Offset: 0x00070878
		public static ImmutableArray<short> DecodeUsingRecord(ImmutableArray<byte> bytes)
		{
			int num = 0;
			short num2 = CustomDebugInfoReader.ReadInt16(bytes, ref num);
			ArrayBuilder<short> instance = ArrayBuilder<short>.GetInstance((int)num2);
			for (int i = 0; i < (int)num2; i++)
			{
				instance.Add(CustomDebugInfoReader.ReadInt16(bytes, ref num));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000726B8 File Offset: 0x000708B8
		public static int DecodeForwardRecord(ImmutableArray<byte> bytes)
		{
			int num = 0;
			return CustomDebugInfoReader.ReadInt32(bytes, ref num);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000726D0 File Offset: 0x000708D0
		public static int DecodeForwardToModuleRecord(ImmutableArray<byte> bytes)
		{
			int num = 0;
			return CustomDebugInfoReader.ReadInt32(bytes, ref num);
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000726E8 File Offset: 0x000708E8
		public static ImmutableArray<StateMachineHoistedLocalScope> DecodeStateMachineHoistedLocalScopesRecord(ImmutableArray<byte> bytes)
		{
			int num = 0;
			int num2 = CustomDebugInfoReader.ReadInt32(bytes, ref num);
			ArrayBuilder<StateMachineHoistedLocalScope> instance = ArrayBuilder<StateMachineHoistedLocalScope>.GetInstance(num2);
			for (int i = 0; i < num2; i++)
			{
				int num3 = CustomDebugInfoReader.ReadInt32(bytes, ref num);
				int num4 = CustomDebugInfoReader.ReadInt32(bytes, ref num);
				if (num3 != 0 || num4 != 0)
				{
					num4++;
				}
				instance.Add(new StateMachineHoistedLocalScope(num3, num4));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x0007274C File Offset: 0x0007094C
		public static string DecodeForwardIteratorRecord(ImmutableArray<byte> bytes)
		{
			int num = 0;
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			for (;;)
			{
				char c = (char)CustomDebugInfoReader.ReadInt16(bytes, ref num);
				if (c == '\0')
				{
					break;
				}
				builder.Append(c);
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00072788 File Offset: 0x00070988
		public static ImmutableArray<DynamicLocalInfo> DecodeDynamicLocalsRecord(ImmutableArray<byte> bytes)
		{
			ArrayBuilder<bool> instance = ArrayBuilder<bool>.GetInstance(64);
			PooledStringBuilder instance2 = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance2.Builder;
			int i = 0;
			int num = CustomDebugInfoReader.ReadInt32(bytes, ref i);
			ArrayBuilder<DynamicLocalInfo> instance3 = ArrayBuilder<DynamicLocalInfo>.GetInstance(num);
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < 64; k++)
				{
					instance.Add(CustomDebugInfoReader.ReadByte(bytes, ref i) > 0);
				}
				int num2 = CustomDebugInfoReader.ReadInt32(bytes, ref i);
				if (num2 < instance.Count)
				{
					instance.Count = num2;
				}
				int num3 = CustomDebugInfoReader.ReadInt32(bytes, ref i);
				int num4 = i + 128;
				while (i < num4)
				{
					char c = (char)CustomDebugInfoReader.ReadInt16(bytes, ref i);
					if (c == '\0')
					{
						i = num4;
						break;
					}
					builder.Append(c);
				}
				instance3.Add(new DynamicLocalInfo(instance.ToImmutable(), num3, builder.ToString()));
				instance.Clear();
				builder.Clear();
			}
			instance.Free();
			instance2.Free();
			return instance3.ToImmutableAndFree();
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x00072888 File Offset: 0x00070A88
		public static ImmutableArray<TupleElementNamesInfo> DecodeTupleElementNamesRecord(ImmutableArray<byte> bytes)
		{
			int num = 0;
			int num2 = CustomDebugInfoReader.ReadInt32(bytes, ref num);
			ArrayBuilder<TupleElementNamesInfo> instance = ArrayBuilder<TupleElementNamesInfo>.GetInstance(num2);
			for (int i = 0; i < num2; i++)
			{
				instance.Add(CustomDebugInfoReader.DecodeTupleElementNamesInfo(bytes, ref num));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000728C8 File Offset: 0x00070AC8
		private static TupleElementNamesInfo DecodeTupleElementNamesInfo(ImmutableArray<byte> bytes, ref int offset)
		{
			int num = CustomDebugInfoReader.ReadInt32(bytes, ref offset);
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance(num);
			for (int i = 0; i < num; i++)
			{
				string text = CustomDebugInfoReader.ReadUtf8String(bytes, ref offset);
				instance.Add(string.IsNullOrEmpty(text) ? null : text);
			}
			int num2 = CustomDebugInfoReader.ReadInt32(bytes, ref offset);
			int num3 = CustomDebugInfoReader.ReadInt32(bytes, ref offset);
			int num4 = CustomDebugInfoReader.ReadInt32(bytes, ref offset);
			string text2 = CustomDebugInfoReader.ReadUtf8String(bytes, ref offset);
			return new TupleElementNamesInfo(instance.ToImmutableAndFree(), num2, text2, num3, num4);
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x00072944 File Offset: 0x00070B44
		private static void ReadRawRecordBody(byte[] bytes, ref int offset, int size, out ImmutableArray<byte> body)
		{
			int num = size - 8;
			body = ImmutableArray.Create<byte>(bytes, offset, num);
			offset += num;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0007296A File Offset: 0x00070B6A
		private static void SkipRecord(byte[] bytes, ref int offset, int size)
		{
			offset += size - 8;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00072974 File Offset: 0x00070B74
		public static ImmutableArray<ImmutableArray<string>> GetCSharpGroupedImportStrings<TArg>(int methodToken, TArg arg, Func<int, TArg, byte[]> getMethodCustomDebugInfo, Func<int, TArg, ImmutableArray<string>> getMethodImportStrings, out ImmutableArray<string> externAliasStrings)
		{
			externAliasStrings = default(ImmutableArray<string>);
			ImmutableArray<short> immutableArray = default(ImmutableArray<short>);
			bool flag = false;
			for (;;)
			{
				IL_0012:
				byte[] array = getMethodCustomDebugInfo.Invoke(methodToken, arg);
				if (array == null)
				{
					break;
				}
				foreach (CustomDebugInfoRecord customDebugInfoRecord in CustomDebugInfoReader.GetCustomDebugInfoRecords(array))
				{
					switch (customDebugInfoRecord.Kind)
					{
					case CustomDebugInfoKind.UsingGroups:
						if (!immutableArray.IsDefault)
						{
							throw new InvalidOperationException(string.Format("Expected at most one Using record for method {0}", CustomDebugInfoReader.FormatMethodToken(methodToken)));
						}
						immutableArray = CustomDebugInfoReader.DecodeUsingRecord(customDebugInfoRecord.Data);
						break;
					case CustomDebugInfoKind.ForwardMethodInfo:
						if (!externAliasStrings.IsDefault)
						{
							throw new InvalidOperationException(string.Format("Did not expect both Forward and ForwardToModule records for method {0}", CustomDebugInfoReader.FormatMethodToken(methodToken)));
						}
						methodToken = CustomDebugInfoReader.DecodeForwardRecord(customDebugInfoRecord.Data);
						if (!flag)
						{
							flag = true;
							goto IL_0012;
						}
						break;
					case CustomDebugInfoKind.ForwardModuleInfo:
					{
						if (!externAliasStrings.IsDefault)
						{
							throw new InvalidOperationException(string.Format("Expected at most one ForwardToModule record for method {0}", CustomDebugInfoReader.FormatMethodToken(methodToken)));
						}
						int num = CustomDebugInfoReader.DecodeForwardToModuleRecord(customDebugInfoRecord.Data);
						ImmutableArray<string> immutableArray2 = getMethodImportStrings.Invoke(num, arg);
						ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
						foreach (string text in immutableArray2)
						{
							if (CustomDebugInfoReader.IsCSharpExternAliasInfo(text))
							{
								instance.Add(text);
							}
						}
						externAliasStrings = instance.ToImmutableAndFree();
						break;
					}
					}
				}
				goto IL_0166;
			}
			return default(ImmutableArray<ImmutableArray<string>>);
			IL_0166:
			if (immutableArray.IsDefault)
			{
				return default(ImmutableArray<ImmutableArray<string>>);
			}
			ImmutableArray<string> immutableArray3 = getMethodImportStrings.Invoke(methodToken, arg);
			ArrayBuilder<ImmutableArray<string>> instance2 = ArrayBuilder<ImmutableArray<string>>.GetInstance(immutableArray.Length);
			ArrayBuilder<string> instance3 = ArrayBuilder<string>.GetInstance();
			int i = 0;
			foreach (short num2 in immutableArray)
			{
				int j = 0;
				while (j < (int)num2)
				{
					if (i >= immutableArray3.Length)
					{
						throw new InvalidOperationException(string.Format("Group size indicates more imports than there are import strings (method {0}).", CustomDebugInfoReader.FormatMethodToken(methodToken)));
					}
					string text2 = immutableArray3[i];
					if (CustomDebugInfoReader.IsCSharpExternAliasInfo(text2))
					{
						throw new InvalidOperationException(string.Format("Encountered extern alias info before all import strings were consumed (method {0}).", CustomDebugInfoReader.FormatMethodToken(methodToken)));
					}
					instance3.Add(text2);
					j++;
					i++;
				}
				instance2.Add(instance3.ToImmutable());
				instance3.Clear();
			}
			if (externAliasStrings.IsDefault)
			{
				while (i < immutableArray3.Length)
				{
					string text3 = immutableArray3[i];
					if (!CustomDebugInfoReader.IsCSharpExternAliasInfo(text3))
					{
						throw new InvalidOperationException(string.Format("Expected only extern alias info strings after consuming the indicated number of imports (method {0}).", CustomDebugInfoReader.FormatMethodToken(methodToken)));
					}
					instance3.Add(text3);
					i++;
				}
				externAliasStrings = instance3.ToImmutableAndFree();
			}
			else
			{
				instance3.Free();
				if (i < immutableArray3.Length)
				{
					throw new InvalidOperationException(string.Format("Group size indicates fewer imports than there are import strings (method {0}).", CustomDebugInfoReader.FormatMethodToken(methodToken)));
				}
			}
			return instance2.ToImmutableAndFree();
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00072C6C File Offset: 0x00070E6C
		public static ImmutableArray<string> GetVisualBasicImportStrings<TArg>(int methodToken, TArg arg, Func<int, TArg, ImmutableArray<string>> getMethodImportStrings)
		{
			ImmutableArray<string> immutableArray = getMethodImportStrings.Invoke(methodToken, arg);
			if (immutableArray.IsEmpty)
			{
				return ImmutableArray<string>.Empty;
			}
			string text = immutableArray[0];
			if (text.Length >= 2 && text.get_Chars(0) == '@')
			{
				char c = text.get_Chars(1);
				int num;
				if ('0' <= c && c <= '9' && int.TryParse(text.Substring(1), 0, CultureInfo.InvariantCulture, ref num))
				{
					immutableArray = getMethodImportStrings.Invoke(num, arg);
				}
			}
			return immutableArray;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00072CE0 File Offset: 0x00070EE0
		private static void CheckVersion(byte globalVersion, int methodToken)
		{
			if (globalVersion != 4)
			{
				throw new InvalidOperationException(string.Format("Method {0}: Expected version {1}, but found version {2}.", CustomDebugInfoReader.FormatMethodToken(methodToken), 4, globalVersion));
			}
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x00072D08 File Offset: 0x00070F08
		private static int ReadInt32(ImmutableArray<byte> bytes, ref int offset)
		{
			int num = offset;
			if (num + 4 > bytes.Length)
			{
				throw new InvalidOperationException("Read out of buffer.");
			}
			offset += 4;
			return (int)bytes[num] | ((int)bytes[num + 1] << 8) | ((int)bytes[num + 2] << 16) | ((int)bytes[num + 3] << 24);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00072D68 File Offset: 0x00070F68
		private static short ReadInt16(ImmutableArray<byte> bytes, ref int offset)
		{
			int num = offset;
			if (num + 2 > bytes.Length)
			{
				throw new InvalidOperationException("Read out of buffer.");
			}
			offset += 2;
			return (short)((int)bytes[num] | ((int)bytes[num + 1] << 8));
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00072DAC File Offset: 0x00070FAC
		private static byte ReadByte(ImmutableArray<byte> bytes, ref int offset)
		{
			int num = offset;
			if (num + 1 > bytes.Length)
			{
				throw new InvalidOperationException("Read out of buffer.");
			}
			offset++;
			return bytes[num];
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x00072DE1 File Offset: 0x00070FE1
		private static bool IsCSharpExternAliasInfo(string import)
		{
			return import.Length > 0 && import.get_Chars(0) == 'Z';
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00072DFC File Offset: 0x00070FFC
		public static bool TryParseCSharpImportString(string import, out string alias, out string externAlias, out string target, out ImportTargetKind kind)
		{
			alias = null;
			externAlias = null;
			target = null;
			kind = ImportTargetKind.Namespace;
			if (string.IsNullOrEmpty(import))
			{
				return false;
			}
			char c = import.get_Chars(0);
			if (c != 'A')
			{
				if (c != 'E')
				{
					switch (c)
					{
					case 'T':
						alias = null;
						externAlias = null;
						target = import.Substring(1);
						kind = ImportTargetKind.Type;
						return true;
					case 'U':
						alias = null;
						externAlias = null;
						target = import.Substring(1);
						kind = ImportTargetKind.Namespace;
						return true;
					case 'X':
						externAlias = null;
						alias = import.Substring(1);
						target = null;
						kind = ImportTargetKind.Assembly;
						return true;
					case 'Z':
						if (!CustomDebugInfoReader.TrySplit(import, 1, ' ', out alias, out target))
						{
							return false;
						}
						externAlias = null;
						kind = ImportTargetKind.Assembly;
						return true;
					}
					return false;
				}
				if (!CustomDebugInfoReader.TrySplit(import, 1, ' ', out target, out externAlias))
				{
					return false;
				}
				alias = null;
				kind = ImportTargetKind.Namespace;
				return true;
			}
			else
			{
				if (!CustomDebugInfoReader.TrySplit(import, 1, ' ', out alias, out target))
				{
					return false;
				}
				char c2 = target.get_Chars(0);
				if (c2 == 'E')
				{
					kind = ImportTargetKind.Namespace;
					return CustomDebugInfoReader.TrySplit(target, 1, ' ', out target, out externAlias);
				}
				if (c2 == 'T')
				{
					kind = ImportTargetKind.Type;
					target = target.Substring(1);
					externAlias = null;
					return true;
				}
				if (c2 == 'U')
				{
					kind = ImportTargetKind.Namespace;
					target = target.Substring(1);
					externAlias = null;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00072F34 File Offset: 0x00071134
		public static bool TryParseVisualBasicImportString(string import, out string alias, out string target, out ImportTargetKind kind, out VBImportScopeKind scope)
		{
			alias = null;
			target = null;
			kind = ImportTargetKind.Namespace;
			scope = VBImportScopeKind.Unspecified;
			if (import == null)
			{
				return false;
			}
			if (import.Length == 0)
			{
				alias = null;
				target = import;
				kind = ImportTargetKind.CurrentNamespace;
				scope = VBImportScopeKind.Unspecified;
				return true;
			}
			int num = 0;
			char c = import.get_Chars(num);
			switch (c)
			{
			case '#':
			case '$':
			case '&':
				alias = null;
				target = import;
				kind = ImportTargetKind.Defunct;
				scope = VBImportScopeKind.Unspecified;
				return true;
			case '%':
				break;
			default:
				if (c == '*')
				{
					num++;
					alias = null;
					target = import.Substring(num);
					kind = ImportTargetKind.DefaultNamespace;
					scope = VBImportScopeKind.Unspecified;
					return true;
				}
				if (c == '@')
				{
					num++;
					if (num >= import.Length)
					{
						return false;
					}
					scope = VBImportScopeKind.Unspecified;
					char c2 = import.get_Chars(num);
					if (c2 != 'F')
					{
						if (c2 == 'P')
						{
							scope = VBImportScopeKind.Project;
							num++;
						}
					}
					else
					{
						scope = VBImportScopeKind.File;
						num++;
					}
					if (num >= import.Length)
					{
						return false;
					}
					c2 = import.get_Chars(num);
					if (c2 <= 'A')
					{
						if (c2 == ':')
						{
							num++;
							alias = null;
							target = import.Substring(num);
							kind = ImportTargetKind.Namespace;
							return true;
						}
						if (c2 == 'A')
						{
							num++;
							if (import.get_Chars(num) != ':')
							{
								return false;
							}
							num++;
							if (!CustomDebugInfoReader.TrySplit(import, num, '=', out alias, out target))
							{
								return false;
							}
							kind = ImportTargetKind.NamespaceOrType;
							return true;
						}
					}
					else if (c2 != 'T')
					{
						if (c2 == 'X')
						{
							num++;
							if (import.get_Chars(num) != ':')
							{
								return false;
							}
							num++;
							if (!CustomDebugInfoReader.TrySplit(import, num, '=', out alias, out target))
							{
								return false;
							}
							kind = ImportTargetKind.XmlNamespace;
							return true;
						}
					}
					else
					{
						num++;
						if (import.get_Chars(num) != ':')
						{
							return false;
						}
						num++;
						alias = null;
						target = import.Substring(num);
						kind = ImportTargetKind.Type;
						return true;
					}
					alias = null;
					target = import.Substring(num);
					kind = ImportTargetKind.MethodToken;
					return true;
				}
				break;
			}
			alias = null;
			target = import;
			kind = ImportTargetKind.CurrentNamespace;
			scope = VBImportScopeKind.Unspecified;
			return true;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000730E4 File Offset: 0x000712E4
		private static bool TrySplit(string input, int offset, char separator, out string before, out string after)
		{
			int num = input.IndexOf(separator, offset);
			if (offset <= num && num < input.Length)
			{
				before = input.Substring(offset, num - offset);
				after = ((num + 1 == input.Length) ? "" : input.Substring(num + 1));
				return true;
			}
			before = null;
			after = null;
			return false;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x0007313B File Offset: 0x0007133B
		private static string FormatMethodToken(int methodToken)
		{
			return string.Format("0x{0:x8}", methodToken);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00073150 File Offset: 0x00071350
		private static string ReadUtf8String(ImmutableArray<byte> bytes, ref int offset)
		{
			ArrayBuilder<byte> instance = ArrayBuilder<byte>.GetInstance();
			for (;;)
			{
				byte b = CustomDebugInfoReader.ReadByte(bytes, ref offset);
				if (b == 0)
				{
					break;
				}
				instance.Add(b);
			}
			byte[] array = instance.ToArrayAndFree();
			return Encoding.UTF8.GetString(array, 0, array.Length);
		}
	}
}
