using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000BA RID: 186
	internal static class NativeResourceWriter
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x000160CC File Offset: 0x000142CC
		private static int CompareResources(IWin32Resource left, IWin32Resource right)
		{
			int num = NativeResourceWriter.CompareResourceIdentifiers(left.TypeId, left.TypeName, right.TypeId, right.TypeName);
			if (num != 0)
			{
				return num;
			}
			return NativeResourceWriter.CompareResourceIdentifiers(left.Id, left.Name, right.Id, right.Name);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00016119 File Offset: 0x00014319
		private static int CompareResourceIdentifiers(int xOrdinal, string xString, int yOrdinal, string yString)
		{
			if (xString == null)
			{
				if (yString == null)
				{
					return xOrdinal - yOrdinal;
				}
				return 1;
			}
			else
			{
				if (yString == null)
				{
					return -1;
				}
				return string.Compare(xString, yString, 5);
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00016134 File Offset: 0x00014334
		internal static IEnumerable<IWin32Resource> SortResources(IEnumerable<IWin32Resource> resources)
		{
			return resources.OrderBy<IWin32Resource>(new Comparison<IWin32Resource>(NativeResourceWriter.CompareResources));
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00016148 File Offset: 0x00014348
		public static void SerializeWin32Resources(BlobBuilder builder, IEnumerable<IWin32Resource> theResources, int resourcesRva)
		{
			theResources = NativeResourceWriter.SortResources(theResources);
			NativeResourceWriter.Directory directory = new NativeResourceWriter.Directory(string.Empty, 0);
			NativeResourceWriter.Directory directory2 = null;
			NativeResourceWriter.Directory directory3 = null;
			int num = int.MinValue;
			string text = null;
			int num2 = int.MinValue;
			string text2 = null;
			uint num3 = 16U;
			foreach (IWin32Resource win32Resource in theResources)
			{
				bool flag = (win32Resource.TypeId < 0 && win32Resource.TypeName != text) || win32Resource.TypeId > num;
				if (flag)
				{
					num = win32Resource.TypeId;
					text = win32Resource.TypeName;
					if (num < 0)
					{
						NativeResourceWriter.Directory directory4 = directory;
						directory4.NumberOfNamedEntries += 1;
					}
					else
					{
						NativeResourceWriter.Directory directory5 = directory;
						directory5.NumberOfIdEntries += 1;
					}
					num3 += 24U;
					directory.Entries.Add(directory2 = new NativeResourceWriter.Directory(text, num));
				}
				if (flag || (win32Resource.Id < 0 && win32Resource.Name != text2) || win32Resource.Id > num2)
				{
					num2 = win32Resource.Id;
					text2 = win32Resource.Name;
					if (num2 < 0)
					{
						NativeResourceWriter.Directory directory6 = directory2;
						directory6.NumberOfNamedEntries += 1;
					}
					else
					{
						NativeResourceWriter.Directory directory7 = directory2;
						directory7.NumberOfIdEntries += 1;
					}
					num3 += 24U;
					directory2.Entries.Add(directory3 = new NativeResourceWriter.Directory(text2, num2));
				}
				NativeResourceWriter.Directory directory8 = directory3;
				directory8.NumberOfIdEntries += 1;
				num3 += 8U;
				directory3.Entries.Add(win32Resource);
			}
			BlobBuilder blobBuilder = new BlobBuilder(256);
			NativeResourceWriter.WriteDirectory(directory, builder, 0U, 0U, num3, resourcesRva, blobBuilder);
			builder.LinkSuffix(blobBuilder);
			builder.WriteByte(0);
			builder.Align(4);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00016318 File Offset: 0x00014518
		private static void WriteDirectory(NativeResourceWriter.Directory directory, BlobBuilder writer, uint offset, uint level, uint sizeOfDirectoryTree, int virtualAddressBase, BlobBuilder dataWriter)
		{
			writer.WriteUInt32(0U);
			writer.WriteUInt32(0U);
			writer.WriteUInt32(0U);
			writer.WriteUInt16(directory.NumberOfNamedEntries);
			writer.WriteUInt16(directory.NumberOfIdEntries);
			uint count = (uint)directory.Entries.Count;
			uint num = offset + 16U + count * 8U;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)count))
			{
				uint num3 = (uint)(dataWriter.Count + (int)sizeOfDirectoryTree);
				uint num4 = num;
				NativeResourceWriter.Directory directory2 = directory.Entries[num2] as NativeResourceWriter.Directory;
				int num5;
				string text;
				if (directory2 != null)
				{
					num5 = directory2.ID;
					text = directory2.Name;
					if (level == 0U)
					{
						num += NativeResourceWriter.SizeOfDirectory(directory2);
					}
					else
					{
						num += (uint)(16 + 8 * directory2.Entries.Count);
					}
				}
				else
				{
					IWin32Resource win32Resource = (IWin32Resource)directory.Entries[num2];
					num5 = ((level == 0U) ? win32Resource.TypeId : ((level == 1U) ? win32Resource.Id : ((int)win32Resource.LanguageId)));
					text = ((level == 0U) ? win32Resource.TypeName : ((level == 1U) ? win32Resource.Name : null));
					dataWriter.WriteUInt32((uint)((long)virtualAddressBase + (long)((ulong)sizeOfDirectoryTree) + 16L + (long)dataWriter.Count));
					byte[] array = new List<byte>(win32Resource.Data).ToArray();
					dataWriter.WriteUInt32((uint)array.Length);
					dataWriter.WriteUInt32(win32Resource.CodePage);
					dataWriter.WriteUInt32(0U);
					dataWriter.WriteBytes(array);
					while (dataWriter.Count % 4 != 0)
					{
						dataWriter.WriteByte(0);
					}
				}
				if (num5 >= 0)
				{
					writer.WriteInt32(num5);
				}
				else
				{
					if (text == null)
					{
						text = string.Empty;
					}
					writer.WriteUInt32(num3 | 2147483648U);
					dataWriter.WriteUInt16((ushort)text.Length);
					dataWriter.WriteUTF16(text);
				}
				if (directory2 != null)
				{
					writer.WriteUInt32(num4 | 2147483648U);
				}
				else
				{
					writer.WriteUInt32(num3);
				}
				num2++;
			}
			num = offset + 16U + count * 8U;
			int num6 = 0;
			while ((long)num6 < (long)((ulong)count))
			{
				NativeResourceWriter.Directory directory3 = directory.Entries[num6] as NativeResourceWriter.Directory;
				if (directory3 != null)
				{
					NativeResourceWriter.WriteDirectory(directory3, writer, num, level + 1U, sizeOfDirectoryTree, virtualAddressBase, dataWriter);
					if (level == 0U)
					{
						num += NativeResourceWriter.SizeOfDirectory(directory3);
					}
					else
					{
						num += (uint)(16 + 8 * directory3.Entries.Count);
					}
				}
				num6++;
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00016560 File Offset: 0x00014760
		private static uint SizeOfDirectory(NativeResourceWriter.Directory directory)
		{
			uint count = (uint)directory.Entries.Count;
			uint num = 16U + 8U * count;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)count))
			{
				NativeResourceWriter.Directory directory2 = directory.Entries[num2] as NativeResourceWriter.Directory;
				if (directory2 != null)
				{
					num += (uint)(16 + 8 * directory2.Entries.Count);
				}
				num2++;
			}
			return num;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000165B8 File Offset: 0x000147B8
		public static void SerializeWin32Resources(BlobBuilder builder, ResourceSection resourceSections, int resourcesRva)
		{
			BlobWriter blobWriter = new BlobWriter(builder.ReserveBytes(resourceSections.SectionBytes.Length));
			blobWriter.WriteBytes(resourceSections.SectionBytes);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(resourceSections.SectionBytes));
			foreach (int num in resourceSections.Relocations)
			{
				blobWriter.Offset = num;
				binaryReader.BaseStream.Position = (long)num;
				blobWriter.WriteUInt32(binaryReader.ReadUInt32() + (uint)resourcesRva);
			}
		}

		// Token: 0x0200052C RID: 1324
		private class Directory
		{
			// Token: 0x06003374 RID: 13172 RVA: 0x00091850 File Offset: 0x0008FA50
			internal Directory(string name, int id)
			{
				this.Name = name;
				this.ID = id;
				this.Entries = new List<object>();
			}

			// Token: 0x040014B4 RID: 5300
			internal readonly string Name;

			// Token: 0x040014B5 RID: 5301
			internal readonly int ID;

			// Token: 0x040014B6 RID: 5302
			internal ushort NumberOfNamedEntries;

			// Token: 0x040014B7 RID: 5303
			internal ushort NumberOfIdEntries;

			// Token: 0x040014B8 RID: 5304
			internal readonly List<object> Entries;
		}
	}
}
