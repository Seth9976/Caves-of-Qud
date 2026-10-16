using System;
using System.Collections.Generic;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200030D RID: 781
	public class AnonymousScopeEntry
	{
		// Token: 0x06001375 RID: 4981 RVA: 0x0003EDD9 File Offset: 0x0003CFD9
		public AnonymousScopeEntry(int id)
		{
			this.ID = id;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0003EE00 File Offset: 0x0003D000
		internal AnonymousScopeEntry(MyBinaryReader reader)
		{
			this.ID = reader.ReadLeb128();
			int num = reader.ReadLeb128();
			for (int i = 0; i < num; i++)
			{
				this.captured_vars.Add(new CapturedVariable(reader));
			}
			int num2 = reader.ReadLeb128();
			for (int j = 0; j < num2; j++)
			{
				this.captured_scopes.Add(new CapturedScope(reader));
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0003EE7D File Offset: 0x0003D07D
		internal void AddCapturedVariable(string name, string captured_name, CapturedVariable.CapturedKind kind)
		{
			this.captured_vars.Add(new CapturedVariable(name, captured_name, kind));
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0003EE94 File Offset: 0x0003D094
		public CapturedVariable[] CapturedVariables
		{
			get
			{
				CapturedVariable[] array = new CapturedVariable[this.captured_vars.Count];
				this.captured_vars.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0003EEC0 File Offset: 0x0003D0C0
		internal void AddCapturedScope(int scope, string captured_name)
		{
			this.captured_scopes.Add(new CapturedScope(scope, captured_name));
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0003EED4 File Offset: 0x0003D0D4
		public CapturedScope[] CapturedScopes
		{
			get
			{
				CapturedScope[] array = new CapturedScope[this.captured_scopes.Count];
				this.captured_scopes.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0003EF00 File Offset: 0x0003D100
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.ID);
			bw.WriteLeb128(this.captured_vars.Count);
			foreach (CapturedVariable capturedVariable in this.captured_vars)
			{
				capturedVariable.Write(bw);
			}
			bw.WriteLeb128(this.captured_scopes.Count);
			foreach (CapturedScope capturedScope in this.captured_scopes)
			{
				capturedScope.Write(bw);
			}
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0003EFC8 File Offset: 0x0003D1C8
		public override string ToString()
		{
			return string.Format("[AnonymousScope {0}]", this.ID);
		}

		// Token: 0x04000A0A RID: 2570
		public readonly int ID;

		// Token: 0x04000A0B RID: 2571
		private List<CapturedVariable> captured_vars = new List<CapturedVariable>();

		// Token: 0x04000A0C RID: 2572
		private List<CapturedScope> captured_scopes = new List<CapturedScope>();
	}
}
