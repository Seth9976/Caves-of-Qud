using System;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200001E RID: 30
	public struct UnixPipes : IEquatable<UnixPipes>
	{
		// Token: 0x0600017D RID: 381 RVA: 0x0000661D File Offset: 0x0000481D
		public UnixPipes(UnixStream reading, UnixStream writing)
		{
			this.Reading = reading;
			this.Writing = writing;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006630 File Offset: 0x00004830
		public static UnixPipes CreatePipes()
		{
			int num;
			int num2;
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.pipe(out num, out num2));
			return new UnixPipes(new UnixStream(num), new UnixStream(num2));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000665C File Offset: 0x0000485C
		public override bool Equals(object value)
		{
			if (value == null || value.GetType() != base.GetType())
			{
				return false;
			}
			UnixPipes unixPipes = (UnixPipes)value;
			return this.Reading.Handle == unixPipes.Reading.Handle && this.Writing.Handle == unixPipes.Writing.Handle;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000066C4 File Offset: 0x000048C4
		public bool Equals(UnixPipes value)
		{
			return this.Reading.Handle == value.Reading.Handle && this.Writing.Handle == value.Writing.Handle;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000066F8 File Offset: 0x000048F8
		public override int GetHashCode()
		{
			return this.Reading.Handle.GetHashCode() ^ this.Writing.Handle.GetHashCode();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000672C File Offset: 0x0000492C
		public static bool operator ==(UnixPipes lhs, UnixPipes rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006736 File Offset: 0x00004936
		public static bool operator !=(UnixPipes lhs, UnixPipes rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000081 RID: 129
		public UnixStream Reading;

		// Token: 0x04000082 RID: 130
		public UnixStream Writing;
	}
}
