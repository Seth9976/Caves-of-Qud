using System;
using System.Collections.Generic;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for relating one member to another.</summary>
	// Token: 0x02000490 RID: 1168
	public abstract class MemberRelationshipService
	{
		/// <summary>Establishes a relationship between a source and target object.</summary>
		/// <returns>The current relationship associated with <paramref name="source" />, or <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> if there is no relationship.</returns>
		/// <param name="source">The source relationship. This is the left-hand side of a relationship assignment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is empty, or the relationship is not supported by the service.</exception>
		// Token: 0x17000787 RID: 1927
		public MemberRelationship this[MemberRelationship source]
		{
			get
			{
				if (source.Owner == null)
				{
					throw new ArgumentNullException("Owner");
				}
				if (source.Member == null)
				{
					throw new ArgumentNullException("Member");
				}
				return this.GetRelationship(source);
			}
			set
			{
				if (source.Owner == null)
				{
					throw new ArgumentNullException("Owner");
				}
				if (source.Member == null)
				{
					throw new ArgumentNullException("Member");
				}
				this.SetRelationship(source, value);
			}
		}

		/// <summary>Establishes a relationship between a source and target object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure encapsulating the relationship between a source and target object, or null if there is no relationship.</returns>
		/// <param name="sourceOwner">The owner of a source relationship.</param>
		/// <param name="sourceMember">The member of a source relationship.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceOwner" /> or <paramref name="sourceMember" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceOwner" /> or <paramref name="sourceMember" /> is empty, or the relationship is not supported by the service.</exception>
		// Token: 0x17000788 RID: 1928
		public MemberRelationship this[object sourceOwner, MemberDescriptor sourceMember]
		{
			get
			{
				if (sourceOwner == null)
				{
					throw new ArgumentNullException("sourceOwner");
				}
				if (sourceMember == null)
				{
					throw new ArgumentNullException("sourceMember");
				}
				return this.GetRelationship(new MemberRelationship(sourceOwner, sourceMember));
			}
			set
			{
				if (sourceOwner == null)
				{
					throw new ArgumentNullException("sourceOwner");
				}
				if (sourceMember == null)
				{
					throw new ArgumentNullException("sourceMember");
				}
				this.SetRelationship(new MemberRelationship(sourceOwner, sourceMember), value);
			}
		}

		/// <summary>Gets a relationship to the given source relationship.</summary>
		/// <returns>A relationship to <paramref name="source" />, or <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> if no relationship exists.</returns>
		/// <param name="source">The source relationship.</param>
		// Token: 0x06002545 RID: 9541 RVA: 0x00082F6C File Offset: 0x0008116C
		protected virtual MemberRelationship GetRelationship(MemberRelationship source)
		{
			MemberRelationshipService.RelationshipEntry relationshipEntry;
			if (this._relationships != null && this._relationships.TryGetValue(new MemberRelationshipService.RelationshipEntry(source), out relationshipEntry) && relationshipEntry.Owner.IsAlive)
			{
				return new MemberRelationship(relationshipEntry.Owner.Target, relationshipEntry.Member);
			}
			return MemberRelationship.Empty;
		}

		/// <summary>Creates a relationship between the source object and target relationship.</summary>
		/// <param name="source">The source relationship.</param>
		/// <param name="relationship">The relationship to set into the source.</param>
		/// <exception cref="T:System.ArgumentException">The relationship is not supported by the service.</exception>
		// Token: 0x06002546 RID: 9542 RVA: 0x00082FC0 File Offset: 0x000811C0
		protected virtual void SetRelationship(MemberRelationship source, MemberRelationship relationship)
		{
			if (!relationship.IsEmpty && !this.SupportsRelationship(source, relationship))
			{
				string text = TypeDescriptor.GetComponentName(source.Owner);
				string text2 = TypeDescriptor.GetComponentName(relationship.Owner);
				if (text == null)
				{
					text = source.Owner.ToString();
				}
				if (text2 == null)
				{
					text2 = relationship.Owner.ToString();
				}
				throw new ArgumentException(SR.Format("Relationships between {0}.{1} and {2}.{3} are not supported.", new object[]
				{
					text,
					source.Member.Name,
					text2,
					relationship.Member.Name
				}));
			}
			if (this._relationships == null)
			{
				this._relationships = new Dictionary<MemberRelationshipService.RelationshipEntry, MemberRelationshipService.RelationshipEntry>();
			}
			this._relationships[new MemberRelationshipService.RelationshipEntry(source)] = new MemberRelationshipService.RelationshipEntry(relationship);
		}

		/// <summary>Gets a value indicating whether the given relationship is supported.</summary>
		/// <returns>true if a relationship between the given two objects is supported; otherwise, false.</returns>
		/// <param name="source">The source relationship.</param>
		/// <param name="relationship">The relationship to set into the source.</param>
		// Token: 0x06002547 RID: 9543
		public abstract bool SupportsRelationship(MemberRelationship source, MemberRelationship relationship);

		// Token: 0x040014BC RID: 5308
		private Dictionary<MemberRelationshipService.RelationshipEntry, MemberRelationshipService.RelationshipEntry> _relationships = new Dictionary<MemberRelationshipService.RelationshipEntry, MemberRelationshipService.RelationshipEntry>();

		// Token: 0x02000491 RID: 1169
		private struct RelationshipEntry
		{
			// Token: 0x06002549 RID: 9545 RVA: 0x00083096 File Offset: 0x00081296
			internal RelationshipEntry(MemberRelationship rel)
			{
				this.Owner = new WeakReference(rel.Owner);
				this.Member = rel.Member;
				this._hashCode = ((rel.Owner == null) ? 0 : rel.Owner.GetHashCode());
			}

			// Token: 0x0600254A RID: 9546 RVA: 0x000830D8 File Offset: 0x000812D8
			public override bool Equals(object o)
			{
				if (o is MemberRelationshipService.RelationshipEntry)
				{
					MemberRelationshipService.RelationshipEntry relationshipEntry = (MemberRelationshipService.RelationshipEntry)o;
					return this == relationshipEntry;
				}
				return false;
			}

			// Token: 0x0600254B RID: 9547 RVA: 0x00083104 File Offset: 0x00081304
			public static bool operator ==(MemberRelationshipService.RelationshipEntry re1, MemberRelationshipService.RelationshipEntry re2)
			{
				object obj = (re1.Owner.IsAlive ? re1.Owner.Target : null);
				object obj2 = (re2.Owner.IsAlive ? re2.Owner.Target : null);
				return obj == obj2 && re1.Member.Equals(re2.Member);
			}

			// Token: 0x0600254C RID: 9548 RVA: 0x0008315E File Offset: 0x0008135E
			public static bool operator !=(MemberRelationshipService.RelationshipEntry re1, MemberRelationshipService.RelationshipEntry re2)
			{
				return !(re1 == re2);
			}

			// Token: 0x0600254D RID: 9549 RVA: 0x0008316A File Offset: 0x0008136A
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x040014BD RID: 5309
			internal WeakReference Owner;

			// Token: 0x040014BE RID: 5310
			internal MemberDescriptor Member;

			// Token: 0x040014BF RID: 5311
			private int _hashCode;
		}
	}
}
