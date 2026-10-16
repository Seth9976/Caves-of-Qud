using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000037 RID: 55
	internal class SqliteStatementHandle : CriticalHandle
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000E54E File Offset: 0x0000C74E
		public static implicit operator IntPtr(SqliteStatementHandle stmt)
		{
			return stmt.handle;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000E556 File Offset: 0x0000C756
		public static implicit operator SqliteStatementHandle(IntPtr stmt)
		{
			return new SqliteStatementHandle(stmt);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000E55E File Offset: 0x0000C75E
		private SqliteStatementHandle(IntPtr stmt)
			: this()
		{
			base.SetHandle(stmt);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000E56D File Offset: 0x0000C76D
		internal SqliteStatementHandle()
			: base(IntPtr.Zero)
		{
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000E57C File Offset: 0x0000C77C
		protected override bool ReleaseHandle()
		{
			try
			{
				SQLiteBase.FinalizeStatement(this);
			}
			catch (SqliteException)
			{
			}
			return true;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
