using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000036 RID: 54
	internal class SqliteConnectionHandle : CriticalHandle
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000E4E2 File Offset: 0x0000C6E2
		public static implicit operator IntPtr(SqliteConnectionHandle db)
		{
			return db.handle;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E4EA File Offset: 0x0000C6EA
		public static implicit operator SqliteConnectionHandle(IntPtr db)
		{
			return new SqliteConnectionHandle(db);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000E4F2 File Offset: 0x0000C6F2
		private SqliteConnectionHandle(IntPtr db)
			: this()
		{
			base.SetHandle(db);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000E501 File Offset: 0x0000C701
		internal SqliteConnectionHandle()
			: base(IntPtr.Zero)
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000E510 File Offset: 0x0000C710
		protected override bool ReleaseHandle()
		{
			try
			{
				SQLiteBase.CloseConnection(this);
			}
			catch (SqliteException)
			{
			}
			return true;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000E53C File Offset: 0x0000C73C
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
