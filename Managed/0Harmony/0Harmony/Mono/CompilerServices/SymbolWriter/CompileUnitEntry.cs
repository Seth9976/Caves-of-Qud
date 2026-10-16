using System;
using System.Collections.Generic;
using System.IO;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200030E RID: 782
	public class CompileUnitEntry : ICompileUnit
	{
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0003EFDF File Offset: 0x0003D1DF
		public static int Size
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00018380 File Offset: 0x00016580
		CompileUnitEntry ICompileUnit.Entry
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0003EFE2 File Offset: 0x0003D1E2
		public CompileUnitEntry(MonoSymbolFile file, SourceFileEntry source)
		{
			this.file = file;
			this.source = source;
			this.Index = file.AddCompileUnit(this);
			this.creating = true;
			this.namespaces = new List<NamespaceEntry>();
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0003F017 File Offset: 0x0003D217
		public void AddFile(SourceFileEntry file)
		{
			if (!this.creating)
			{
				throw new InvalidOperationException();
			}
			if (this.include_files == null)
			{
				this.include_files = new List<SourceFileEntry>();
			}
			this.include_files.Add(file);
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0003F046 File Offset: 0x0003D246
		public SourceFileEntry SourceFile
		{
			get
			{
				if (this.creating)
				{
					return this.source;
				}
				this.ReadData();
				return this.source;
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0003F064 File Offset: 0x0003D264
		public int DefineNamespace(string name, string[] using_clauses, int parent)
		{
			if (!this.creating)
			{
				throw new InvalidOperationException();
			}
			int nextNamespaceIndex = this.file.GetNextNamespaceIndex();
			NamespaceEntry namespaceEntry = new NamespaceEntry(name, nextNamespaceIndex, using_clauses, parent);
			this.namespaces.Add(namespaceEntry);
			return nextNamespaceIndex;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0003F0A4 File Offset: 0x0003D2A4
		internal void WriteData(MyBinaryWriter bw)
		{
			this.DataOffset = (int)bw.BaseStream.Position;
			bw.WriteLeb128(this.source.Index);
			int num = ((this.include_files != null) ? this.include_files.Count : 0);
			bw.WriteLeb128(num);
			if (this.include_files != null)
			{
				foreach (SourceFileEntry sourceFileEntry in this.include_files)
				{
					bw.WriteLeb128(sourceFileEntry.Index);
				}
			}
			bw.WriteLeb128(this.namespaces.Count);
			foreach (NamespaceEntry namespaceEntry in this.namespaces)
			{
				namespaceEntry.Write(this.file, bw);
			}
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0003F1A0 File Offset: 0x0003D3A0
		internal void Write(BinaryWriter bw)
		{
			bw.Write(this.Index);
			bw.Write(this.DataOffset);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0003F1BA File Offset: 0x0003D3BA
		internal CompileUnitEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			this.file = file;
			this.Index = reader.ReadInt32();
			this.DataOffset = reader.ReadInt32();
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0003F1E1 File Offset: 0x0003D3E1
		public void ReadAll()
		{
			this.ReadData();
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0003F1EC File Offset: 0x0003D3EC
		private void ReadData()
		{
			if (this.creating)
			{
				throw new InvalidOperationException();
			}
			MonoSymbolFile monoSymbolFile = this.file;
			lock (monoSymbolFile)
			{
				if (this.namespaces == null)
				{
					MyBinaryReader binaryReader = this.file.BinaryReader;
					int num = (int)binaryReader.BaseStream.Position;
					binaryReader.BaseStream.Position = (long)this.DataOffset;
					int num2 = binaryReader.ReadLeb128();
					this.source = this.file.GetSourceFile(num2);
					int num3 = binaryReader.ReadLeb128();
					if (num3 > 0)
					{
						this.include_files = new List<SourceFileEntry>();
						for (int i = 0; i < num3; i++)
						{
							this.include_files.Add(this.file.GetSourceFile(binaryReader.ReadLeb128()));
						}
					}
					int num4 = binaryReader.ReadLeb128();
					this.namespaces = new List<NamespaceEntry>();
					for (int j = 0; j < num4; j++)
					{
						this.namespaces.Add(new NamespaceEntry(this.file, binaryReader));
					}
					binaryReader.BaseStream.Position = (long)num;
				}
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0003F314 File Offset: 0x0003D514
		public NamespaceEntry[] Namespaces
		{
			get
			{
				this.ReadData();
				NamespaceEntry[] array = new NamespaceEntry[this.namespaces.Count];
				this.namespaces.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0003F348 File Offset: 0x0003D548
		public SourceFileEntry[] IncludeFiles
		{
			get
			{
				this.ReadData();
				if (this.include_files == null)
				{
					return new SourceFileEntry[0];
				}
				SourceFileEntry[] array = new SourceFileEntry[this.include_files.Count];
				this.include_files.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04000A0D RID: 2573
		public readonly int Index;

		// Token: 0x04000A0E RID: 2574
		private int DataOffset;

		// Token: 0x04000A0F RID: 2575
		private MonoSymbolFile file;

		// Token: 0x04000A10 RID: 2576
		private SourceFileEntry source;

		// Token: 0x04000A11 RID: 2577
		private List<SourceFileEntry> include_files;

		// Token: 0x04000A12 RID: 2578
		private List<NamespaceEntry> namespaces;

		// Token: 0x04000A13 RID: 2579
		private bool creating;
	}
}
