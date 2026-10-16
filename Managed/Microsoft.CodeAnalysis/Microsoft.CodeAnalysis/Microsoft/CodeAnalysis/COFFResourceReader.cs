using System;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000161 RID: 353
	internal static class COFFResourceReader
	{
		// Token: 0x06000E4A RID: 3658 RVA: 0x00028D6A File Offset: 0x00026F6A
		private static void ConfirmSectionValues(SectionHeader hdr, long fileSize)
		{
			if ((long)hdr.PointerToRawData + (long)hdr.SizeOfRawData > fileSize)
			{
				throw new ResourceException(CodeAnalysisResources.CoffResourceInvalidSectionSize, null);
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00028D8C File Offset: 0x00026F8C
		[NullableContext(1)]
		internal static ResourceSection ReadWin32ResourcesFromCOFF(Stream stream)
		{
			PEHeaders peheaders = new PEHeaders(stream);
			SectionHeader sectionHeader = default(SectionHeader);
			SectionHeader sectionHeader2 = default(SectionHeader);
			int num = 0;
			foreach (SectionHeader sectionHeader3 in peheaders.SectionHeaders)
			{
				if (sectionHeader3.Name == ".rsrc$01")
				{
					sectionHeader = sectionHeader3;
					num++;
				}
				else if (sectionHeader3.Name == ".rsrc$02")
				{
					sectionHeader2 = sectionHeader3;
					num++;
				}
			}
			if (num != 2)
			{
				throw new ResourceException(CodeAnalysisResources.CoffResourceMissingSection, null);
			}
			COFFResourceReader.ConfirmSectionValues(sectionHeader, stream.Length);
			COFFResourceReader.ConfirmSectionValues(sectionHeader2, stream.Length);
			byte[] array = new byte[checked(sectionHeader.SizeOfRawData + sectionHeader2.SizeOfRawData)];
			stream.Seek((long)sectionHeader.PointerToRawData, 0);
			stream.TryReadAll(array, 0, sectionHeader.SizeOfRawData);
			stream.Seek((long)sectionHeader2.PointerToRawData, 0);
			stream.TryReadAll(array, sectionHeader.SizeOfRawData, sectionHeader2.SizeOfRawData);
			try
			{
				if ((long)(checked(sectionHeader.PointerToRelocations + (int)(sectionHeader.NumberOfRelocations * 10))) > stream.Length)
				{
					throw new ResourceException(CodeAnalysisResources.CoffResourceInvalidRelocation, null);
				}
			}
			catch (OverflowException)
			{
				throw new ResourceException(CodeAnalysisResources.CoffResourceInvalidRelocation, null);
			}
			uint[] array2 = new uint[(int)sectionHeader.NumberOfRelocations];
			uint[] array3 = new uint[(int)sectionHeader.NumberOfRelocations];
			BinaryReader binaryReader = new BinaryReader(stream, Encoding.Unicode);
			stream.Position = (long)sectionHeader.PointerToRelocations;
			for (int i = 0; i < (int)sectionHeader.NumberOfRelocations; i++)
			{
				array2[i] = binaryReader.ReadUInt32();
				array3[i] = binaryReader.ReadUInt32();
				binaryReader.ReadUInt16();
			}
			stream.Position = (long)peheaders.CoffHeader.PointerToSymbolTable;
			MemoryStream memoryStream;
			BinaryWriter binaryWriter;
			checked
			{
				try
				{
					if (unchecked((long)peheaders.CoffHeader.PointerToSymbolTable) + unchecked((long)peheaders.CoffHeader.NumberOfSymbols) * 18L > stream.Length)
					{
						throw new ResourceException(CodeAnalysisResources.CoffResourceInvalidSymbol, null);
					}
				}
				catch (OverflowException)
				{
					throw new ResourceException(CodeAnalysisResources.CoffResourceInvalidSymbol, null);
				}
				memoryStream = new MemoryStream(array);
				binaryWriter = new BinaryWriter(memoryStream);
			}
			for (int j = 0; j < array3.Length; j++)
			{
				if ((ulong)array3[j] > (ulong)((long)peheaders.CoffHeader.NumberOfSymbols))
				{
					throw new ResourceException(CodeAnalysisResources.CoffResourceInvalidRelocation, null);
				}
				long num2 = (long)peheaders.CoffHeader.PointerToSymbolTable + (long)((ulong)(array3[j] * 18U));
				stream.Position = num2;
				stream.Position += 8L;
				uint num3 = binaryReader.ReadUInt32();
				short num4 = binaryReader.ReadInt16();
				if (binaryReader.ReadUInt16() != 0 || num4 != 3)
				{
					throw new ResourceException(CodeAnalysisResources.CoffResourceInvalidSymbol, null);
				}
				memoryStream.Position = (long)((ulong)array2[j]);
				binaryWriter.Write((uint)((ulong)num3 + (ulong)((long)sectionHeader.SizeOfRawData)));
			}
			return new ResourceSection(array, array2);
		}
	}
}
