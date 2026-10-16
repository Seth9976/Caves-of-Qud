using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200000E RID: 14
	public class MonoSymbolFile : IDisposable
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003A78 File Offset: 0x00001C78
		public int CompileUnitCount
		{
			get
			{
				return this.ot.CompileUnitCount;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003A85 File Offset: 0x00001C85
		public int SourceCount
		{
			get
			{
				return this.ot.SourceCount;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003A92 File Offset: 0x00001C92
		public int MethodCount
		{
			get
			{
				return this.ot.MethodCount;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003A9F File Offset: 0x00001C9F
		public int TypeCount
		{
			get
			{
				return this.ot.TypeCount;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003AAC File Offset: 0x00001CAC
		public int AnonymousScopeCount
		{
			get
			{
				return this.ot.AnonymousScopeCount;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003AB9 File Offset: 0x00001CB9
		public int NamespaceCount
		{
			get
			{
				return this.last_namespace_index;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003AC1 File Offset: 0x00001CC1
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003AC9 File Offset: 0x00001CC9
		public OffsetTable OffsetTable
		{
			get
			{
				return this.ot;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public SourceFileEntry[] Sources
		{
			get
			{
				if (this.reader == null)
				{
					throw new InvalidOperationException();
				}
				SourceFileEntry[] array = new SourceFileEntry[this.SourceCount];
				for (int i = 0; i < this.SourceCount; i++)
				{
					array[i] = this.GetSourceFile(i + 1);
				}
				return array;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003B1C File Offset: 0x00001D1C
		public CompileUnitEntry[] CompileUnits
		{
			get
			{
				if (this.reader == null)
				{
					throw new InvalidOperationException();
				}
				CompileUnitEntry[] array = new CompileUnitEntry[this.CompileUnitCount];
				for (int i = 0; i < this.CompileUnitCount; i++)
				{
					array[i] = this.GetCompileUnit(i + 1);
				}
				return array;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003B64 File Offset: 0x00001D64
		public MethodEntry[] Methods
		{
			get
			{
				if (this.reader == null)
				{
					throw new InvalidOperationException();
				}
				MethodEntry[] array2;
				lock (this)
				{
					this.read_methods();
					MethodEntry[] array = new MethodEntry[this.MethodCount];
					this.method_list.CopyTo(array, 0);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003BC4 File Offset: 0x00001DC4
		internal MyBinaryReader BinaryReader
		{
			get
			{
				if (this.reader == null)
				{
					throw new InvalidOperationException();
				}
				return this.reader;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003BDA File Offset: 0x00001DDA
		public MonoSymbolFile()
		{
			this.ot = new OffsetTable();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003C16 File Offset: 0x00001E16
		public int AddSource(SourceFileEntry source)
		{
			this.sources.Add(source);
			return this.sources.Count;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003C2F File Offset: 0x00001E2F
		public int AddCompileUnit(CompileUnitEntry entry)
		{
			this.comp_units.Add(entry);
			return this.comp_units.Count;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003C48 File Offset: 0x00001E48
		public void AddMethod(MethodEntry entry)
		{
			this.methods.Add(entry);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003C58 File Offset: 0x00001E58
		public MethodEntry DefineMethod(CompileUnitEntry comp_unit, int token, ScopeVariable[] scope_vars, LocalVariableEntry[] locals, LineNumberEntry[] lines, CodeBlockEntry[] code_blocks, string real_name, MethodEntry.Flags flags, int namespace_id)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException();
			}
			MethodEntry methodEntry = new MethodEntry(this, comp_unit, token, scope_vars, locals, lines, code_blocks, real_name, flags, namespace_id);
			this.AddMethod(methodEntry);
			return methodEntry;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003C91 File Offset: 0x00001E91
		internal void DefineAnonymousScope(int id)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException();
			}
			if (this.anonymous_scopes == null)
			{
				this.anonymous_scopes = new Dictionary<int, AnonymousScopeEntry>();
			}
			this.anonymous_scopes.Add(id, new AnonymousScopeEntry(id));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003CC6 File Offset: 0x00001EC6
		internal void DefineCapturedVariable(int scope_id, string name, string captured_name, CapturedVariable.CapturedKind kind)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException();
			}
			this.anonymous_scopes[scope_id].AddCapturedVariable(name, captured_name, kind);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003CEB File Offset: 0x00001EEB
		internal void DefineCapturedScope(int scope_id, int id, string captured_name)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException();
			}
			this.anonymous_scopes[scope_id].AddCapturedScope(id, captured_name);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003D10 File Offset: 0x00001F10
		internal int GetNextTypeIndex()
		{
			int num = this.last_type_index + 1;
			this.last_type_index = num;
			return num;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003D30 File Offset: 0x00001F30
		internal int GetNextMethodIndex()
		{
			int num = this.last_method_index + 1;
			this.last_method_index = num;
			return num;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003D50 File Offset: 0x00001F50
		internal int GetNextNamespaceIndex()
		{
			int num = this.last_namespace_index + 1;
			this.last_namespace_index = num;
			return num;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003D70 File Offset: 0x00001F70
		private void Write(MyBinaryWriter bw, Guid guid)
		{
			bw.Write(5037318119232611860L);
			bw.Write(this.MajorVersion);
			bw.Write(this.MinorVersion);
			bw.Write(guid.ToByteArray());
			long position = bw.BaseStream.Position;
			this.ot.Write(bw, this.MajorVersion, this.MinorVersion);
			this.methods.Sort();
			for (int i = 0; i < this.methods.Count; i++)
			{
				this.methods[i].Index = i + 1;
			}
			this.ot.DataSectionOffset = (int)bw.BaseStream.Position;
			foreach (SourceFileEntry sourceFileEntry in this.sources)
			{
				sourceFileEntry.WriteData(bw);
			}
			foreach (CompileUnitEntry compileUnitEntry in this.comp_units)
			{
				compileUnitEntry.WriteData(bw);
			}
			foreach (MethodEntry methodEntry in this.methods)
			{
				methodEntry.WriteData(this, bw);
			}
			this.ot.DataSectionSize = (int)bw.BaseStream.Position - this.ot.DataSectionOffset;
			this.ot.MethodTableOffset = (int)bw.BaseStream.Position;
			for (int j = 0; j < this.methods.Count; j++)
			{
				this.methods[j].Write(bw);
			}
			this.ot.MethodTableSize = (int)bw.BaseStream.Position - this.ot.MethodTableOffset;
			this.ot.SourceTableOffset = (int)bw.BaseStream.Position;
			for (int k = 0; k < this.sources.Count; k++)
			{
				this.sources[k].Write(bw);
			}
			this.ot.SourceTableSize = (int)bw.BaseStream.Position - this.ot.SourceTableOffset;
			this.ot.CompileUnitTableOffset = (int)bw.BaseStream.Position;
			for (int l = 0; l < this.comp_units.Count; l++)
			{
				this.comp_units[l].Write(bw);
			}
			this.ot.CompileUnitTableSize = (int)bw.BaseStream.Position - this.ot.CompileUnitTableOffset;
			this.ot.AnonymousScopeCount = ((this.anonymous_scopes != null) ? this.anonymous_scopes.Count : 0);
			this.ot.AnonymousScopeTableOffset = (int)bw.BaseStream.Position;
			if (this.anonymous_scopes != null)
			{
				foreach (AnonymousScopeEntry anonymousScopeEntry in this.anonymous_scopes.Values)
				{
					anonymousScopeEntry.Write(bw);
				}
			}
			this.ot.AnonymousScopeTableSize = (int)bw.BaseStream.Position - this.ot.AnonymousScopeTableOffset;
			this.ot.TypeCount = this.last_type_index;
			this.ot.MethodCount = this.methods.Count;
			this.ot.SourceCount = this.sources.Count;
			this.ot.CompileUnitCount = this.comp_units.Count;
			this.ot.TotalFileSize = (int)bw.BaseStream.Position;
			bw.Seek((int)position, 0);
			this.ot.Write(bw, this.MajorVersion, this.MinorVersion);
			bw.Seek(0, 2);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000418C File Offset: 0x0000238C
		public void CreateSymbolFile(Guid guid, FileStream fs)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException();
			}
			this.Write(new MyBinaryWriter(fs), guid);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000041AC File Offset: 0x000023AC
		private MonoSymbolFile(Stream stream)
		{
			this.reader = new MyBinaryReader(stream);
			try
			{
				long num = this.reader.ReadInt64();
				int num2 = this.reader.ReadInt32();
				int num3 = this.reader.ReadInt32();
				if (num != 5037318119232611860L)
				{
					throw new MonoSymbolFileException("Symbol file is not a valid", new object[0]);
				}
				if (num2 != 50)
				{
					throw new MonoSymbolFileException("Symbol file has version {0} but expected {1}", new object[] { num2, 50 });
				}
				if (num3 != 0)
				{
					throw new MonoSymbolFileException("Symbol file has version {0}.{1} but expected {2}.{3}", new object[] { num2, num3, 50, 0 });
				}
				this.MajorVersion = num2;
				this.MinorVersion = num3;
				this.guid = new Guid(this.reader.ReadBytes(16));
				this.ot = new OffsetTable(this.reader, num2, num3);
			}
			catch (Exception ex)
			{
				throw new MonoSymbolFileException("Cannot read symbol file", ex);
			}
			this.source_file_hash = new Dictionary<int, SourceFileEntry>();
			this.compile_unit_hash = new Dictionary<int, CompileUnitEntry>();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004308 File Offset: 0x00002508
		public static MonoSymbolFile ReadSymbolFile(Assembly assembly)
		{
			string text = assembly.Location + ".mdb";
			Guid moduleVersionId = assembly.GetModules()[0].ModuleVersionId;
			return MonoSymbolFile.ReadSymbolFile(text, moduleVersionId);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000433B File Offset: 0x0000253B
		public static MonoSymbolFile ReadSymbolFile(string mdbFilename)
		{
			return MonoSymbolFile.ReadSymbolFile(new FileStream(mdbFilename, 3, 1));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000434C File Offset: 0x0000254C
		public static MonoSymbolFile ReadSymbolFile(string mdbFilename, Guid assemblyGuid)
		{
			MonoSymbolFile monoSymbolFile = MonoSymbolFile.ReadSymbolFile(mdbFilename);
			if (assemblyGuid != monoSymbolFile.guid)
			{
				throw new MonoSymbolFileException("Symbol file `{0}' does not match assembly", new object[] { mdbFilename });
			}
			return monoSymbolFile;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004384 File Offset: 0x00002584
		public static MonoSymbolFile ReadSymbolFile(Stream stream)
		{
			return new MonoSymbolFile(stream);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000438C File Offset: 0x0000258C
		public SourceFileEntry GetSourceFile(int index)
		{
			if (index < 1 || index > this.ot.SourceCount)
			{
				throw new ArgumentException();
			}
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			SourceFileEntry sourceFileEntry2;
			lock (this)
			{
				SourceFileEntry sourceFileEntry;
				if (this.source_file_hash.TryGetValue(index, ref sourceFileEntry))
				{
					sourceFileEntry2 = sourceFileEntry;
				}
				else
				{
					long position = this.reader.BaseStream.Position;
					this.reader.BaseStream.Position = (long)(this.ot.SourceTableOffset + SourceFileEntry.Size * (index - 1));
					sourceFileEntry = new SourceFileEntry(this, this.reader);
					this.source_file_hash.Add(index, sourceFileEntry);
					this.reader.BaseStream.Position = position;
					sourceFileEntry2 = sourceFileEntry;
				}
			}
			return sourceFileEntry2;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000445C File Offset: 0x0000265C
		public CompileUnitEntry GetCompileUnit(int index)
		{
			if (index < 1 || index > this.ot.CompileUnitCount)
			{
				throw new ArgumentException();
			}
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			CompileUnitEntry compileUnitEntry2;
			lock (this)
			{
				CompileUnitEntry compileUnitEntry;
				if (this.compile_unit_hash.TryGetValue(index, ref compileUnitEntry))
				{
					compileUnitEntry2 = compileUnitEntry;
				}
				else
				{
					long position = this.reader.BaseStream.Position;
					this.reader.BaseStream.Position = (long)(this.ot.CompileUnitTableOffset + CompileUnitEntry.Size * (index - 1));
					compileUnitEntry = new CompileUnitEntry(this, this.reader);
					this.compile_unit_hash.Add(index, compileUnitEntry);
					this.reader.BaseStream.Position = position;
					compileUnitEntry2 = compileUnitEntry;
				}
			}
			return compileUnitEntry2;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000452C File Offset: 0x0000272C
		private void read_methods()
		{
			lock (this)
			{
				if (this.method_token_hash == null)
				{
					this.method_token_hash = new Dictionary<int, MethodEntry>();
					this.method_list = new List<MethodEntry>();
					long position = this.reader.BaseStream.Position;
					this.reader.BaseStream.Position = (long)this.ot.MethodTableOffset;
					for (int i = 0; i < this.MethodCount; i++)
					{
						MethodEntry methodEntry = new MethodEntry(this, this.reader, i + 1);
						this.method_token_hash.Add(methodEntry.Token, methodEntry);
						this.method_list.Add(methodEntry);
					}
					this.reader.BaseStream.Position = position;
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000045F8 File Offset: 0x000027F8
		public MethodEntry GetMethodByToken(int token)
		{
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			MethodEntry methodEntry2;
			lock (this)
			{
				this.read_methods();
				MethodEntry methodEntry;
				this.method_token_hash.TryGetValue(token, ref methodEntry);
				methodEntry2 = methodEntry;
			}
			return methodEntry2;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000464C File Offset: 0x0000284C
		public MethodEntry GetMethod(int index)
		{
			if (index < 1 || index > this.ot.MethodCount)
			{
				throw new ArgumentException();
			}
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			MethodEntry methodEntry;
			lock (this)
			{
				this.read_methods();
				methodEntry = this.method_list[index - 1];
			}
			return methodEntry;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000046B8 File Offset: 0x000028B8
		public int FindSource(string file_name)
		{
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			int num2;
			lock (this)
			{
				if (this.source_name_hash == null)
				{
					this.source_name_hash = new Dictionary<string, int>();
					for (int i = 0; i < this.ot.SourceCount; i++)
					{
						SourceFileEntry sourceFile = this.GetSourceFile(i + 1);
						this.source_name_hash.Add(sourceFile.FileName, i);
					}
				}
				int num;
				if (!this.source_name_hash.TryGetValue(file_name, ref num))
				{
					num2 = -1;
				}
				else
				{
					num2 = num;
				}
			}
			return num2;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004754 File Offset: 0x00002954
		public AnonymousScopeEntry GetAnonymousScope(int id)
		{
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			AnonymousScopeEntry anonymousScopeEntry2;
			lock (this)
			{
				if (this.anonymous_scopes != null)
				{
					AnonymousScopeEntry anonymousScopeEntry;
					this.anonymous_scopes.TryGetValue(id, ref anonymousScopeEntry);
					anonymousScopeEntry2 = anonymousScopeEntry;
				}
				else
				{
					this.anonymous_scopes = new Dictionary<int, AnonymousScopeEntry>();
					this.reader.BaseStream.Position = (long)this.ot.AnonymousScopeTableOffset;
					for (int i = 0; i < this.ot.AnonymousScopeCount; i++)
					{
						AnonymousScopeEntry anonymousScopeEntry3 = new AnonymousScopeEntry(this.reader);
						this.anonymous_scopes.Add(anonymousScopeEntry3.ID, anonymousScopeEntry3);
					}
					anonymousScopeEntry2 = this.anonymous_scopes[id];
				}
			}
			return anonymousScopeEntry2;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004818 File Offset: 0x00002A18
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004821 File Offset: 0x00002A21
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.reader != null)
			{
				this.reader.Close();
				this.reader = null;
			}
		}

		// Token: 0x04000044 RID: 68
		private List<MethodEntry> methods = new List<MethodEntry>();

		// Token: 0x04000045 RID: 69
		private List<SourceFileEntry> sources = new List<SourceFileEntry>();

		// Token: 0x04000046 RID: 70
		private List<CompileUnitEntry> comp_units = new List<CompileUnitEntry>();

		// Token: 0x04000047 RID: 71
		private Dictionary<int, AnonymousScopeEntry> anonymous_scopes;

		// Token: 0x04000048 RID: 72
		private OffsetTable ot;

		// Token: 0x04000049 RID: 73
		private int last_type_index;

		// Token: 0x0400004A RID: 74
		private int last_method_index;

		// Token: 0x0400004B RID: 75
		private int last_namespace_index;

		// Token: 0x0400004C RID: 76
		public readonly int MajorVersion = 50;

		// Token: 0x0400004D RID: 77
		public readonly int MinorVersion;

		// Token: 0x0400004E RID: 78
		public int NumLineNumbers;

		// Token: 0x0400004F RID: 79
		private MyBinaryReader reader;

		// Token: 0x04000050 RID: 80
		private Dictionary<int, SourceFileEntry> source_file_hash;

		// Token: 0x04000051 RID: 81
		private Dictionary<int, CompileUnitEntry> compile_unit_hash;

		// Token: 0x04000052 RID: 82
		private List<MethodEntry> method_list;

		// Token: 0x04000053 RID: 83
		private Dictionary<int, MethodEntry> method_token_hash;

		// Token: 0x04000054 RID: 84
		private Dictionary<string, int> source_name_hash;

		// Token: 0x04000055 RID: 85
		private Guid guid;

		// Token: 0x04000056 RID: 86
		internal int LineNumberCount;

		// Token: 0x04000057 RID: 87
		internal int LocalCount;

		// Token: 0x04000058 RID: 88
		internal int StringSize;

		// Token: 0x04000059 RID: 89
		internal int LineNumberSize;

		// Token: 0x0400005A RID: 90
		internal int ExtendedLineNumberSize;
	}
}
