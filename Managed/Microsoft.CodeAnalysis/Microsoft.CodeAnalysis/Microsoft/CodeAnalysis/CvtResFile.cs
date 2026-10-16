using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000160 RID: 352
	[NullableContext(1)]
	[Nullable(0)]
	internal class CvtResFile
	{
		// Token: 0x06000E47 RID: 3655 RVA: 0x00028B70 File Offset: 0x00026D70
		internal static List<RESOURCE> ReadResFile(Stream stream)
		{
			BinaryReader binaryReader = new BinaryReader(stream, Encoding.Unicode);
			List<RESOURCE> list = new List<RESOURCE>();
			long position = stream.Position;
			if (binaryReader.ReadUInt32() != 0U)
			{
				throw new ResourceException("Stream does not begin with a null resource and is not in .RES format.", null);
			}
			stream.Position = position;
			while (stream.Position < stream.Length)
			{
				uint num = binaryReader.ReadUInt32();
				uint num2 = binaryReader.ReadUInt32();
				if (num2 < 8U)
				{
					throw new ResourceException(string.Format("Resource header beginning at offset 0x{0:x} is malformed.", stream.Position - 8L), null);
				}
				if (num == 0U)
				{
					stream.Position += (long)((ulong)(num2 - 8U));
				}
				else
				{
					RESOURCE resource = new RESOURCE
					{
						HeaderSize = num2,
						DataSize = num
					};
					resource.pstringType = CvtResFile.ReadStringOrID(binaryReader);
					resource.pstringName = CvtResFile.ReadStringOrID(binaryReader);
					stream.Position = (stream.Position + 3L) & -4L;
					resource.DataVersion = binaryReader.ReadUInt32();
					resource.MemoryFlags = binaryReader.ReadUInt16();
					resource.LanguageId = binaryReader.ReadUInt16();
					resource.Version = binaryReader.ReadUInt32();
					resource.Characteristics = binaryReader.ReadUInt32();
					resource.data = new byte[resource.DataSize];
					binaryReader.Read(resource.data, 0, resource.data.Length);
					stream.Position = (stream.Position + 3L) & -4L;
					if (resource.pstringType.theString != null || resource.pstringType.Ordinal != 17)
					{
						list.Add(resource);
					}
				}
			}
			return list;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00028D00 File Offset: 0x00026F00
		private static RESOURCE_STRING ReadStringOrID(BinaryReader fhIn)
		{
			RESOURCE_STRING resource_STRING = new RESOURCE_STRING();
			char c = fhIn.ReadChar();
			if (c == '\uffff')
			{
				resource_STRING.Ordinal = fhIn.ReadUInt16();
			}
			else
			{
				resource_STRING.Ordinal = ushort.MaxValue;
				StringBuilder stringBuilder = new StringBuilder();
				char c2 = c;
				do
				{
					stringBuilder.Append(c2);
					c2 = fhIn.ReadChar();
				}
				while (c2 != '\0');
				resource_STRING.theString = stringBuilder.ToString();
			}
			return resource_STRING;
		}

		// Token: 0x040003F8 RID: 1016
		private const ushort RT_DLGINCLUDE = 17;
	}
}
