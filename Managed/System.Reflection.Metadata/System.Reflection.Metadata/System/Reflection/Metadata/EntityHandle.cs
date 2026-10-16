using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a metadata entity (such as a type reference, type definition, type specification, method definition, or custom attribute).</summary>
	// Token: 0x0200003D RID: 61
	public readonly struct EntityHandle : IEquatable<EntityHandle>
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00006F09 File Offset: 0x00005109
		internal EntityHandle(uint vToken)
		{
			this._vToken = vToken;
		}

		/// <param name="handle" />
		// Token: 0x06000222 RID: 546 RVA: 0x00006F12 File Offset: 0x00005112
		public static implicit operator Handle(EntityHandle handle)
		{
			return Handle.FromVToken(handle._vToken);
		}

		/// <param name="handle" />
		// Token: 0x06000223 RID: 547 RVA: 0x00006F1F File Offset: 0x0000511F
		public static explicit operator EntityHandle(Handle handle)
		{
			if (handle.IsHeapHandle)
			{
				Throw.InvalidCast();
			}
			return new EntityHandle(handle.EntityHandleValue);
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006F3B File Offset: 0x0000513B
		internal uint Type
		{
			get
			{
				return this._vToken & 2130706432U;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00006F49 File Offset: 0x00005149
		internal uint VType
		{
			get
			{
				return this._vToken & 4278190080U;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00006F57 File Offset: 0x00005157
		internal bool IsVirtual
		{
			get
			{
				return (this._vToken & 2147483648U) > 0U;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00006F68 File Offset: 0x00005168
		public bool IsNil
		{
			get
			{
				return (this._vToken & 2164260863U) == 0U;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00006F79 File Offset: 0x00005179
		internal int RowId
		{
			get
			{
				return (int)(this._vToken & 16777215U);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00006F87 File Offset: 0x00005187
		internal uint SpecificHandleValue
		{
			get
			{
				return this._vToken & 2164260863U;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00006F95 File Offset: 0x00005195
		public HandleKind Kind
		{
			get
			{
				return (HandleKind)(this.Type >> 24);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00006FA1 File Offset: 0x000051A1
		internal int Token
		{
			get
			{
				return (int)this._vToken;
			}
		}

		/// <summary>Returns a value that indicates whether the current instance and the specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an <see cref="T:System.Reflection.Metadata.EntityHandle" /> and is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600022C RID: 556 RVA: 0x00006FA9 File Offset: 0x000051A9
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is EntityHandle && this.Equals((EntityHandle)obj);
		}

		/// <summary>Returns a value that indicates whether the current instance and the specified <see cref="T:System.Reflection.Metadata.EntityHandle" /> are equal.</summary>
		/// <param name="other">The value to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="other" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600022D RID: 557 RVA: 0x00006FC1 File Offset: 0x000051C1
		public bool Equals(EntityHandle other)
		{
			return this._vToken == other._vToken;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x0600022E RID: 558 RVA: 0x00006FD1 File Offset: 0x000051D1
		public override int GetHashCode()
		{
			return (int)this._vToken;
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600022F RID: 559 RVA: 0x00006FD9 File Offset: 0x000051D9
		public static bool operator ==(EntityHandle left, EntityHandle right)
		{
			return left.Equals(right);
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000230 RID: 560 RVA: 0x00006FE3 File Offset: 0x000051E3
		public static bool operator !=(EntityHandle left, EntityHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006FF0 File Offset: 0x000051F0
		internal static int Compare(EntityHandle left, EntityHandle right)
		{
			return left._vToken.CompareTo(right._vToken);
		}

		// Token: 0x04000199 RID: 409
		private readonly uint _vToken;

		// Token: 0x0400019A RID: 410
		public static readonly ModuleDefinitionHandle ModuleDefinition = new ModuleDefinitionHandle(1);

		// Token: 0x0400019B RID: 411
		public static readonly AssemblyDefinitionHandle AssemblyDefinition = new AssemblyDefinitionHandle(1);
	}
}
