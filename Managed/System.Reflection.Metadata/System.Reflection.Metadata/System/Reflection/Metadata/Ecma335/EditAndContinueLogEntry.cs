using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000109 RID: 265
	public readonly struct EditAndContinueLogEntry : IEquatable<EditAndContinueLogEntry>
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0001C512 File Offset: 0x0001A712
		public EntityHandle Handle { get; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001C51A File Offset: 0x0001A71A
		public EditAndContinueOperation Operation { get; }

		/// <param name="handle" />
		/// <param name="operation" />
		// Token: 0x060009E3 RID: 2531 RVA: 0x0001C522 File Offset: 0x0001A722
		public EditAndContinueLogEntry(EntityHandle handle, EditAndContinueOperation operation)
		{
			this.Handle = handle;
			this.Operation = operation;
		}

		/// <param name="obj" />
		// Token: 0x060009E4 RID: 2532 RVA: 0x0001C532 File Offset: 0x0001A732
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is EditAndContinueLogEntry && this.Equals((EditAndContinueLogEntry)obj);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060009E5 RID: 2533 RVA: 0x0001C54A File Offset: 0x0001A74A
		public bool Equals(EditAndContinueLogEntry other)
		{
			return this.Operation == other.Operation && this.Handle == other.Handle;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0001C570 File Offset: 0x0001A770
		public override int GetHashCode()
		{
			return (int)(this.Operation ^ (EditAndContinueOperation)this.Handle.GetHashCode());
		}
	}
}
