using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B9 RID: 185
	public readonly struct LocalScopeHandleCollection : IReadOnlyCollection<LocalScopeHandle>, IEnumerable<LocalScopeHandle>, IEnumerable
	{
		// Token: 0x060006FF RID: 1791 RVA: 0x000117DC File Offset: 0x0000F9DC
		[NullableContext(1)]
		internal LocalScopeHandleCollection(MetadataReader reader, int methodDefinitionRowId)
		{
			this._reader = reader;
			if (methodDefinitionRowId == 0)
			{
				this._firstRowId = 1;
				this._lastRowId = reader.LocalScopeTable.NumberOfRows;
				return;
			}
			reader.LocalScopeTable.GetLocalScopeRange(methodDefinitionRowId, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00011819 File Offset: 0x0000FA19
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001182A File Offset: 0x0000FA2A
		public LocalScopeHandleCollection.Enumerator GetEnumerator()
		{
			return new LocalScopeHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000702 RID: 1794 RVA: 0x00011843 File Offset: 0x0000FA43
		IEnumerator<LocalScopeHandle> IEnumerable<LocalScopeHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000703 RID: 1795 RVA: 0x00011850 File Offset: 0x0000FA50
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400044A RID: 1098
		private readonly MetadataReader _reader;

		// Token: 0x0400044B RID: 1099
		private readonly int _firstRowId;

		// Token: 0x0400044C RID: 1100
		private readonly int _lastRowId;

		// Token: 0x020001B0 RID: 432
		public struct Enumerator : IEnumerator<LocalScopeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CCB RID: 3275 RVA: 0x00025B3D File Offset: 0x00023D3D
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700033F RID: 831
			// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00025B56 File Offset: 0x00023D56
			public LocalScopeHandle Current
			{
				get
				{
					return LocalScopeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CCD RID: 3277 RVA: 0x00025B6C File Offset: 0x00023D6C
			public bool MoveNext()
			{
				if (this._currentRowId >= this._lastRowId)
				{
					this._currentRowId = 16777216;
					return false;
				}
				this._currentRowId++;
				return true;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000340 RID: 832
			// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00025B98 File Offset: 0x00023D98
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CCF RID: 3279 RVA: 0x00025BA5 File Offset: 0x00023DA5
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CD0 RID: 3280 RVA: 0x00025BAC File Offset: 0x00023DAC
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A77 RID: 2679
			private readonly MetadataReader _reader;

			// Token: 0x04000A78 RID: 2680
			private readonly int _lastRowId;

			// Token: 0x04000A79 RID: 2681
			private int _currentRowId;

			// Token: 0x04000A7A RID: 2682
			private const int EnumEnded = 16777216;
		}

		// Token: 0x020001B1 RID: 433
		public struct ChildrenEnumerator : IEnumerator<LocalScopeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CD1 RID: 3281 RVA: 0x00025BAE File Offset: 0x00023DAE
			[NullableContext(1)]
			internal ChildrenEnumerator(MetadataReader reader, int parentRowId)
			{
				this._reader = reader;
				this._parentEndOffset = reader.LocalScopeTable.GetEndOffset(parentRowId);
				this._parentMethodRowId = reader.LocalScopeTable.GetMethod(parentRowId);
				this._currentRowId = 0;
				this._parentRowId = parentRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00025BE9 File Offset: 0x00023DE9
			public LocalScopeHandle Current
			{
				get
				{
					return LocalScopeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CD3 RID: 3283 RVA: 0x00025C00 File Offset: 0x00023E00
			public bool MoveNext()
			{
				int currentRowId = this._currentRowId;
				if (currentRowId == 16777216)
				{
					return false;
				}
				int num;
				int num2;
				if (currentRowId == 0)
				{
					num = -1;
					num2 = this._parentRowId + 1;
				}
				else
				{
					num = this._reader.LocalScopeTable.GetEndOffset(currentRowId);
					num2 = currentRowId + 1;
				}
				int numberOfRows = this._reader.LocalScopeTable.NumberOfRows;
				while (num2 <= numberOfRows && !(this._parentMethodRowId != this._reader.LocalScopeTable.GetMethod(num2)))
				{
					int endOffset = this._reader.LocalScopeTable.GetEndOffset(num2);
					if (endOffset > num)
					{
						if (endOffset > this._parentEndOffset)
						{
							this._currentRowId = 16777216;
							return false;
						}
						this._currentRowId = num2;
						return true;
					}
					else
					{
						num2++;
					}
				}
				this._currentRowId = 16777216;
				return false;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000342 RID: 834
			// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00025CC1 File Offset: 0x00023EC1
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CD5 RID: 3285 RVA: 0x00025CCE File Offset: 0x00023ECE
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CD6 RID: 3286 RVA: 0x00025CD5 File Offset: 0x00023ED5
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A7B RID: 2683
			private readonly MetadataReader _reader;

			// Token: 0x04000A7C RID: 2684
			private readonly int _parentEndOffset;

			// Token: 0x04000A7D RID: 2685
			private readonly int _parentRowId;

			// Token: 0x04000A7E RID: 2686
			private readonly MethodDefinitionHandle _parentMethodRowId;

			// Token: 0x04000A7F RID: 2687
			private int _currentRowId;

			// Token: 0x04000A80 RID: 2688
			private const int EnumEnded = 16777216;
		}
	}
}
