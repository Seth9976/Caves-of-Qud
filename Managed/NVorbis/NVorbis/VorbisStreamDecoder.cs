using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NVorbis
{
	// Token: 0x0200002C RID: 44
	internal class VorbisStreamDecoder : IVorbisStreamStatus, IDisposable
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00009120 File Offset: 0x00007320
		internal VorbisStreamDecoder(IPacketProvider packetProvider)
		{
			this._packetProvider = packetProvider;
			this._packetProvider.ParameterChange += new EventHandler<ParameterChangeEventArgs>(this.SetParametersChanging);
			this._pagesSeen = new List<int>();
			this._lastPageSeen = -1;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000917C File Offset: 0x0000737C
		internal bool TryInit()
		{
			if (!this.ProcessStreamHeader(this._packetProvider.PeekNextPacket()))
			{
				return false;
			}
			this._packetProvider.GetNextPacket().Done();
			DataPacket dataPacket = this._packetProvider.GetNextPacket();
			if (!this.LoadComments(dataPacket))
			{
				throw new InvalidDataException("Comment header was not readable!");
			}
			dataPacket.Done();
			dataPacket = this._packetProvider.GetNextPacket();
			if (!this.LoadBooks(dataPacket))
			{
				throw new InvalidDataException("Book header was not readable!");
			}
			dataPacket.Done();
			this.InitDecoder();
			return true;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00009201 File Offset: 0x00007401
		private void SetParametersChanging(object sender, ParameterChangeEventArgs e)
		{
			this._parameterChangePacket = e.FirstPacket;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00009210 File Offset: 0x00007410
		public void Dispose()
		{
			if (this._packetProvider != null)
			{
				IPacketProvider packetProvider = this._packetProvider;
				this._packetProvider = null;
				packetProvider.ParameterChange -= new EventHandler<ParameterChangeEventArgs>(this.SetParametersChanging);
				packetProvider.Dispose();
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000924C File Offset: 0x0000744C
		private void ProcessParameterChange(DataPacket packet)
		{
			this._parameterChangePacket = null;
			bool flag = false;
			bool flag2 = false;
			if (this.ProcessStreamHeader(packet))
			{
				packet.Done();
				flag = true;
				flag2 = true;
				packet = this._packetProvider.PeekNextPacket();
				if (packet == null)
				{
					throw new InvalidDataException("Couldn't get next packet!");
				}
			}
			if (this.LoadComments(packet))
			{
				if (flag)
				{
					this._packetProvider.GetNextPacket().Done();
				}
				else
				{
					packet.Done();
				}
				flag = true;
				packet = this._packetProvider.PeekNextPacket();
				if (packet == null)
				{
					throw new InvalidDataException("Couldn't get next packet!");
				}
			}
			if (this.LoadBooks(packet))
			{
				if (flag)
				{
					this._packetProvider.GetNextPacket().Done();
				}
				else
				{
					packet.Done();
				}
			}
			this.ResetDecoder(flag2);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00009308 File Offset: 0x00007508
		private bool ProcessStreamHeader(DataPacket packet)
		{
			if (!Enumerable.SequenceEqual<byte>(packet.ReadBytes(7), new byte[] { 1, 118, 111, 114, 98, 105, 115 }))
			{
				this._glueBits += (long)(packet.Length * 8);
				return false;
			}
			if (!this._pagesSeen.Contains(this._lastPageSeen = packet.PageSequenceNumber))
			{
				this._pagesSeen.Add(this._lastPageSeen);
			}
			this._glueBits += 56L;
			long bitsRead = packet.BitsRead;
			if (packet.ReadInt32() != 0)
			{
				throw new InvalidDataException("Only Vorbis stream version 0 is supported.");
			}
			this._channels = (int)packet.ReadByte();
			this._sampleRate = packet.ReadInt32();
			this._upperBitrate = packet.ReadInt32();
			this._nominalBitrate = packet.ReadInt32();
			this._lowerBitrate = packet.ReadInt32();
			this.Block0Size = 1 << (int)packet.ReadBits(4);
			this.Block1Size = 1 << (int)packet.ReadBits(4);
			if (this._nominalBitrate == 0 && this._upperBitrate > 0 && this._lowerBitrate > 0)
			{
				this._nominalBitrate = (this._upperBitrate + this._lowerBitrate) / 2;
			}
			this._metaBits += packet.BitsRead - bitsRead + 8L;
			this._wasteHdrBits += (long)(8 * packet.Length) - packet.BitsRead;
			return true;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009478 File Offset: 0x00007678
		private bool LoadComments(DataPacket packet)
		{
			if (!Enumerable.SequenceEqual<byte>(packet.ReadBytes(7), new byte[] { 3, 118, 111, 114, 98, 105, 115 }))
			{
				return false;
			}
			if (!this._pagesSeen.Contains(this._lastPageSeen = packet.PageSequenceNumber))
			{
				this._pagesSeen.Add(this._lastPageSeen);
			}
			this._glueBits += 56L;
			this._vendor = Encoding.UTF8.GetString(packet.ReadBytes(packet.ReadInt32()));
			this._comments = new string[packet.ReadInt32()];
			for (int i = 0; i < this._comments.Length; i++)
			{
				this._comments[i] = Encoding.UTF8.GetString(packet.ReadBytes(packet.ReadInt32()));
			}
			this._metaBits += packet.BitsRead - 56L;
			this._wasteHdrBits += (long)(8 * packet.Length) - packet.BitsRead;
			return true;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00009580 File Offset: 0x00007780
		private bool LoadBooks(DataPacket packet)
		{
			if (!Enumerable.SequenceEqual<byte>(packet.ReadBytes(7), new byte[] { 5, 118, 111, 114, 98, 105, 115 }))
			{
				return false;
			}
			if (!this._pagesSeen.Contains(this._lastPageSeen = packet.PageSequenceNumber))
			{
				this._pagesSeen.Add(this._lastPageSeen);
			}
			long num = packet.BitsRead;
			this._glueBits += packet.BitsRead;
			this.Books = new VorbisCodebook[(int)(packet.ReadByte() + 1)];
			for (int i = 0; i < this.Books.Length; i++)
			{
				this.Books[i] = VorbisCodebook.Init(this, packet, i);
			}
			this._bookBits += packet.BitsRead - num;
			num = packet.BitsRead;
			this.Times = new VorbisTime[(int)packet.ReadBits(6) + 1];
			for (int j = 0; j < this.Times.Length; j++)
			{
				this.Times[j] = VorbisTime.Init(this, packet);
			}
			this._timeHdrBits += packet.BitsRead - num;
			num = packet.BitsRead;
			this.Floors = new VorbisFloor[(int)packet.ReadBits(6) + 1];
			for (int k = 0; k < this.Floors.Length; k++)
			{
				this.Floors[k] = VorbisFloor.Init(this, packet);
			}
			this._floorHdrBits += packet.BitsRead - num;
			num = packet.BitsRead;
			this.Residues = new VorbisResidue[(int)packet.ReadBits(6) + 1];
			for (int l = 0; l < this.Residues.Length; l++)
			{
				this.Residues[l] = VorbisResidue.Init(this, packet);
			}
			this._resHdrBits += packet.BitsRead - num;
			num = packet.BitsRead;
			this.Maps = new VorbisMapping[(int)packet.ReadBits(6) + 1];
			for (int m = 0; m < this.Maps.Length; m++)
			{
				this.Maps[m] = VorbisMapping.Init(this, packet);
			}
			this._mapHdrBits += packet.BitsRead - num;
			num = packet.BitsRead;
			this.Modes = new VorbisMode[(int)packet.ReadBits(6) + 1];
			for (int n = 0; n < this.Modes.Length; n++)
			{
				this.Modes[n] = VorbisMode.Init(this, packet);
			}
			this._modeHdrBits += packet.BitsRead - num;
			if (!packet.ReadBit())
			{
				throw new InvalidDataException();
			}
			this._glueBits += 1L;
			this._wasteHdrBits += (long)(8 * packet.Length) - packet.BitsRead;
			this._modeFieldBits = Utils.ilog(this.Modes.Length - 1);
			return true;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00009840 File Offset: 0x00007A40
		private void InitDecoder()
		{
			this._currentPosition = 0L;
			this._resyncQueue = new Stack<DataPacket>();
			this._bitsPerPacketHistory = new Queue<int>();
			this._sampleCountHistory = new Queue<int>();
			this.ResetDecoder(true);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00009874 File Offset: 0x00007A74
		private void ResetDecoder(bool isFullReset)
		{
			if (this._preparedLength > 0)
			{
				this.SaveBuffer();
			}
			if (isFullReset)
			{
				this._noExecuteChannel = new bool[this._channels];
				this._floorData = new VorbisFloor.PacketData[this._channels];
				this._residue = new float[this._channels][];
				for (int i = 0; i < this._channels; i++)
				{
					this._residue[i] = new float[this.Block1Size];
				}
				this._outputBuffer = new RingBuffer(this.Block1Size * 2 * this._channels);
				this._outputBuffer.Channels = this._channels;
			}
			else
			{
				this._outputBuffer.Clear();
			}
			this._preparedLength = 0;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000992C File Offset: 0x00007B2C
		private void SaveBuffer()
		{
			float[] array = new float[this._preparedLength * this._channels];
			this.ReadSamples(array, 0, array.Length);
			this._prevBuffer = array;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00009960 File Offset: 0x00007B60
		private bool UnpackPacket(DataPacket packet)
		{
			if (packet.ReadBit())
			{
				return false;
			}
			int num = this._modeFieldBits;
			this._mode = this.Modes[(int)packet.ReadBits(this._modeFieldBits)];
			if (this._mode.BlockFlag)
			{
				this._prevFlag = packet.ReadBit();
				this._nextFlag = packet.ReadBit();
				num += 2;
			}
			else
			{
				this._prevFlag = (this._nextFlag = false);
			}
			if (packet.IsShort)
			{
				return false;
			}
			long num2 = packet.BitsRead;
			int num3 = this._mode.BlockSize / 2;
			for (int i = 0; i < this._channels; i++)
			{
				this._floorData[i] = this._mode.Mapping.ChannelSubmap[i].Floor.UnpackPacket(packet, this._mode.BlockSize, i);
				this._noExecuteChannel[i] = !this._floorData[i].ExecuteChannel;
				Array.Clear(this._residue[i], 0, num3);
			}
			foreach (VorbisMapping.CouplingStep couplingStep in this._mode.Mapping.CouplingSteps)
			{
				if (this._floorData[couplingStep.Angle].ExecuteChannel || this._floorData[couplingStep.Magnitude].ExecuteChannel)
				{
					this._floorData[couplingStep.Angle].ForceEnergy = true;
					this._floorData[couplingStep.Magnitude].ForceEnergy = true;
				}
			}
			long num4 = packet.BitsRead - num2;
			num2 = packet.BitsRead;
			foreach (VorbisMapping.Submap submap in this._mode.Mapping.Submaps)
			{
				for (int l = 0; l < this._channels; l++)
				{
					if (this._mode.Mapping.ChannelSubmap[l] != submap)
					{
						this._floorData[l].ForceNoEnergy = true;
					}
				}
				float[][] array = submap.Residue.Decode(packet, this._noExecuteChannel, this._channels, this._mode.BlockSize);
				for (int m = 0; m < this._channels; m++)
				{
					float[] array2 = this._residue[m];
					float[] array3 = array[m];
					for (int n = 0; n < num3; n++)
					{
						array2[n] += array3[n];
					}
				}
			}
			this._glueBits += 1L;
			this._modeBits += (long)num;
			this._floorBits += num4;
			this._resBits += packet.BitsRead - num2;
			this._wasteBits += (long)(8 * packet.Length) - packet.BitsRead;
			this._packetCount++;
			return true;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009C40 File Offset: 0x00007E40
		private void DecodePacket()
		{
			VorbisMapping.CouplingStep[] couplingSteps = this._mode.Mapping.CouplingSteps;
			int num = this._mode.BlockSize / 2;
			for (int i = couplingSteps.Length - 1; i >= 0; i--)
			{
				if (this._floorData[couplingSteps[i].Angle].ExecuteChannel || this._floorData[couplingSteps[i].Magnitude].ExecuteChannel)
				{
					float[] array = this._residue[couplingSteps[i].Magnitude];
					float[] array2 = this._residue[couplingSteps[i].Angle];
					for (int j = 0; j < num; j++)
					{
						float num2;
						float num3;
						if (array[j] > 0f)
						{
							if (array2[j] > 0f)
							{
								num2 = array[j];
								num3 = array[j] - array2[j];
							}
							else
							{
								num3 = array[j];
								num2 = array[j] + array2[j];
							}
						}
						else if (array2[j] > 0f)
						{
							num2 = array[j];
							num3 = array[j] + array2[j];
						}
						else
						{
							num3 = array[j];
							num2 = array[j] - array2[j];
						}
						array[j] = num2;
						array2[j] = num3;
					}
				}
			}
			for (int k = 0; k < this._channels; k++)
			{
				VorbisFloor.PacketData packetData = this._floorData[k];
				float[] array3 = this._residue[k];
				if (packetData.ExecuteChannel)
				{
					this._mode.Mapping.ChannelSubmap[k].Floor.Apply(packetData, array3);
					Mdct.Reverse(array3, this._mode.BlockSize);
				}
				else
				{
					Array.Clear(array3, num, num);
				}
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00009DD8 File Offset: 0x00007FD8
		private int OverlapSamples()
		{
			float[] window = this._mode.GetWindow(this._prevFlag, this._nextFlag);
			int blockSize = this._mode.BlockSize;
			int num = blockSize;
			int num2 = num >> 1;
			int num3 = 0;
			int num4 = -num2;
			int num5 = num2;
			if (this._mode.BlockFlag)
			{
				if (!this._prevFlag)
				{
					num3 = this.Block1Size / 4 - this.Block0Size / 4;
					num2 = num3 + this.Block0Size / 2;
					num4 = this.Block0Size / -2 - num3;
				}
				if (!this._nextFlag)
				{
					num -= blockSize / 4 - this.Block0Size / 4;
					num5 = blockSize / 4 + this.Block0Size / 4;
				}
			}
			int num6 = this._outputBuffer.Length / this._channels + num4;
			for (int i = 0; i < this._channels; i++)
			{
				this._outputBuffer.Write(i, num6, num3, num2, num, this._residue[i], window);
			}
			int num7 = this._outputBuffer.Length / this._channels - num5;
			int num8 = num7 - this._preparedLength;
			this._preparedLength = num7;
			return num8;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009EF8 File Offset: 0x000080F8
		private void UpdatePosition(int samplesDecoded, DataPacket packet)
		{
			this._samples += (long)samplesDecoded;
			if (packet.IsResync)
			{
				this._currentPosition = -packet.PageGranulePosition;
				this._resyncQueue.Push(packet);
				return;
			}
			if (samplesDecoded > 0)
			{
				this._currentPosition += (long)samplesDecoded;
				packet.GranulePosition = this._currentPosition;
				if (this._currentPosition < 0L)
				{
					if (packet.PageGranulePosition > -this._currentPosition)
					{
						long num = this._currentPosition - (long)samplesDecoded;
						while (this._resyncQueue.Count > 0)
						{
							DataPacket dataPacket = this._resyncQueue.Pop();
							long num2 = dataPacket.GranulePosition + num;
							dataPacket.GranulePosition = num;
							num = num2;
						}
						return;
					}
					packet.GranulePosition = (long)(-(long)samplesDecoded);
					this._resyncQueue.Push(packet);
					return;
				}
				else if (packet.IsEndOfStream && this._currentPosition > packet.PageGranulePosition)
				{
					int num3 = (int)(this._currentPosition - packet.PageGranulePosition);
					if (num3 >= 0)
					{
						this._preparedLength -= num3;
						this._currentPosition -= (long)num3;
					}
					else
					{
						this._preparedLength = 0;
					}
					packet.GranulePosition = packet.PageGranulePosition;
					this._eosFound = true;
				}
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000A024 File Offset: 0x00008224
		private void DecodeNextPacket()
		{
			this._sw.Start();
			DataPacket dataPacket = null;
			try
			{
				IPacketProvider packetProvider = this._packetProvider;
				if (packetProvider != null)
				{
					dataPacket = packetProvider.GetNextPacket();
				}
				if (dataPacket == null)
				{
					this._eosFound = true;
				}
				else
				{
					if (!this._pagesSeen.Contains(this._lastPageSeen = dataPacket.PageSequenceNumber))
					{
						this._pagesSeen.Add(this._lastPageSeen);
					}
					if (dataPacket.IsResync)
					{
						this.ResetDecoder(false);
					}
					if (dataPacket == this._parameterChangePacket)
					{
						this._isParameterChange = true;
						this.ProcessParameterChange(dataPacket);
					}
					else if (!this.UnpackPacket(dataPacket))
					{
						dataPacket.Done();
						this._wasteBits += (long)(8 * dataPacket.Length);
					}
					else
					{
						dataPacket.Done();
						this.DecodePacket();
						int num = this.OverlapSamples();
						if (dataPacket.GranuleCount == null)
						{
							dataPacket.GranuleCount = new int?(num);
						}
						this.UpdatePosition(num, dataPacket);
						int i = Utils.Sum(this._sampleCountHistory) + num;
						this._bitsPerPacketHistory.Enqueue((int)dataPacket.BitsRead);
						this._sampleCountHistory.Enqueue(num);
						while (i > this._sampleRate)
						{
							this._bitsPerPacketHistory.Dequeue();
							i -= this._sampleCountHistory.Dequeue();
						}
					}
				}
			}
			catch
			{
				if (dataPacket != null)
				{
					dataPacket.Done();
				}
				throw;
			}
			finally
			{
				this._sw.Stop();
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000A1BC File Offset: 0x000083BC
		internal int GetPacketLength(DataPacket curPacket, DataPacket lastPacket)
		{
			if (lastPacket == null || curPacket.IsResync)
			{
				return 0;
			}
			if (curPacket.ReadBit())
			{
				return 0;
			}
			if (lastPacket.ReadBit())
			{
				return 0;
			}
			int num = (int)curPacket.ReadBits(this._modeFieldBits);
			if (num < 0 || num >= this.Modes.Length)
			{
				return 0;
			}
			VorbisMode vorbisMode = this.Modes[num];
			num = (int)lastPacket.ReadBits(this._modeFieldBits);
			if (num < 0 || num >= this.Modes.Length)
			{
				return 0;
			}
			VorbisMode vorbisMode2 = this.Modes[num];
			return vorbisMode.BlockSize / 4 + vorbisMode2.BlockSize / 4;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000A24C File Offset: 0x0000844C
		internal int ReadSamples(float[] buffer, int offset, int count)
		{
			int num = 0;
			lock (this._seekLock)
			{
				if (this._prevBuffer != null)
				{
					int num2 = Math.Min(count, this._prevBuffer.Length);
					Buffer.BlockCopy(this._prevBuffer, 0, buffer, offset, num2 * 4);
					if (num2 < this._prevBuffer.Length)
					{
						float[] array = new float[this._prevBuffer.Length - num2];
						Buffer.BlockCopy(this._prevBuffer, num2 * 4, array, 0, (this._prevBuffer.Length - num2) * 4);
						this._prevBuffer = array;
					}
					else
					{
						this._prevBuffer = null;
					}
					count -= num2;
					offset += num2;
					num = num2;
				}
				else if (this._isParameterChange)
				{
					throw new InvalidOperationException("Currently pending a parameter change.  Read new parameters before requesting further samples!");
				}
				int num3 = count + this.Block1Size * this._channels;
				this._outputBuffer.EnsureSize(num3);
				while (this._preparedLength * this._channels < count && !this._eosFound && !this._isParameterChange)
				{
					this.DecodeNextPacket();
					if (this._prevBuffer != null)
					{
						return this.ReadSamples(buffer, offset, this._prevBuffer.Length);
					}
				}
				if (this._preparedLength * this._channels < count)
				{
					count = this._preparedLength * this._channels;
				}
				this._outputBuffer.CopyTo(buffer, offset, count);
				this._preparedLength -= count / this._channels;
				this._reportedPosition = this._currentPosition - (long)this._preparedLength;
			}
			return num + count;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000A3E0 File Offset: 0x000085E0
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000A3E8 File Offset: 0x000085E8
		internal bool IsParameterChange
		{
			get
			{
				return this._isParameterChange;
			}
			set
			{
				if (value)
				{
					throw new InvalidOperationException("Only clearing is supported!");
				}
				this._isParameterChange = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000A3FF File Offset: 0x000085FF
		internal bool CanSeek
		{
			get
			{
				return this._packetProvider.CanSeek;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A40C File Offset: 0x0000860C
		internal void SeekTo(long granulePos)
		{
			if (!this._packetProvider.CanSeek)
			{
				throw new NotSupportedException();
			}
			if (granulePos < 0L)
			{
				throw new ArgumentOutOfRangeException("granulePos");
			}
			DataPacket dataPacket;
			if (granulePos > 0L)
			{
				dataPacket = this._packetProvider.FindPacket(granulePos, new Func<DataPacket, DataPacket, int>(this.GetPacketLength));
				if (dataPacket == null)
				{
					throw new ArgumentOutOfRangeException("granulePos");
				}
			}
			else
			{
				dataPacket = this._packetProvider.GetPacket(4);
			}
			lock (this._seekLock)
			{
				this._packetProvider.SeekToPacket(dataPacket, 1);
				DataPacket dataPacket2 = this._packetProvider.PeekNextPacket();
				this.CurrentPosition = dataPacket2.GranulePosition;
				int i = (int)((granulePos - this.CurrentPosition) * (long)this._channels);
				if (i > 0)
				{
					float[] array = new float[i];
					while (i > 0)
					{
						int num = this.ReadSamples(array, 0, i);
						if (num == 0)
						{
							break;
						}
						i -= num;
					}
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000A4FC File Offset: 0x000086FC
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000A504 File Offset: 0x00008704
		internal long CurrentPosition
		{
			get
			{
				return this._reportedPosition;
			}
			private set
			{
				this._reportedPosition = value;
				this._currentPosition = value;
				this._preparedLength = 0;
				this._eosFound = false;
				this.ResetDecoder(false);
				this._prevBuffer = null;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000A530 File Offset: 0x00008730
		internal long GetLastGranulePos()
		{
			return this._packetProvider.GetGranuleCount();
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000A53D File Offset: 0x0000873D
		internal long ContainerBits
		{
			get
			{
				return this._packetProvider.ContainerBits;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000A54C File Offset: 0x0000874C
		public void ResetStats()
		{
			this._clipped = false;
			this._packetCount = 0;
			this._floorBits = 0L;
			this._glueBits = 0L;
			this._modeBits = 0L;
			this._resBits = 0L;
			this._wasteBits = 0L;
			this._samples = 0L;
			this._sw.Reset();
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000A5A4 File Offset: 0x000087A4
		public int EffectiveBitRate
		{
			get
			{
				if (this._samples == 0L)
				{
					return 0;
				}
				double num = (double)(this._currentPosition - (long)this._preparedLength) / (double)this._sampleRate;
				return (int)((double)this.AudioBits / num);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000A5E0 File Offset: 0x000087E0
		public int InstantBitRate
		{
			get
			{
				int num = Enumerable.Sum(this._sampleCountHistory);
				if (num > 0)
				{
					return (int)((long)Enumerable.Sum(this._bitsPerPacketHistory) * (long)this._sampleRate / (long)num);
				}
				return -1;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000A617 File Offset: 0x00008817
		public TimeSpan PageLatency
		{
			get
			{
				return TimeSpan.FromTicks(this._sw.ElapsedTicks / (long)this.PagesRead);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000A631 File Offset: 0x00008831
		public TimeSpan PacketLatency
		{
			get
			{
				return TimeSpan.FromTicks(this._sw.ElapsedTicks / (long)this._packetCount);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000A64B File Offset: 0x0000884B
		public TimeSpan SecondLatency
		{
			get
			{
				return TimeSpan.FromTicks(this._sw.ElapsedTicks / this._samples * (long)this._sampleRate);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000A66C File Offset: 0x0000886C
		public long OverheadBits
		{
			get
			{
				return this._glueBits + this._metaBits + this._timeHdrBits + this._wasteHdrBits + this._wasteBits + this._packetProvider.ContainerBits;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000A69C File Offset: 0x0000889C
		public long AudioBits
		{
			get
			{
				return this._bookBits + this._floorHdrBits + this._resHdrBits + this._mapHdrBits + this._modeHdrBits + this._modeBits + this._floorBits + this._resBits;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000A6D5 File Offset: 0x000088D5
		public int PagesRead
		{
			get
			{
				return this._pagesSeen.IndexOf(this._lastPageSeen) + 1;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000A6EA File Offset: 0x000088EA
		public int TotalPages
		{
			get
			{
				return this._packetProvider.GetTotalPageCount();
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000A6F7 File Offset: 0x000088F7
		public bool Clipped
		{
			get
			{
				return this._clipped;
			}
		}

		// Token: 0x040000D0 RID: 208
		internal int _upperBitrate;

		// Token: 0x040000D1 RID: 209
		internal int _nominalBitrate;

		// Token: 0x040000D2 RID: 210
		internal int _lowerBitrate;

		// Token: 0x040000D3 RID: 211
		internal string _vendor;

		// Token: 0x040000D4 RID: 212
		internal string[] _comments;

		// Token: 0x040000D5 RID: 213
		internal int _channels;

		// Token: 0x040000D6 RID: 214
		internal int _sampleRate;

		// Token: 0x040000D7 RID: 215
		internal int Block0Size;

		// Token: 0x040000D8 RID: 216
		internal int Block1Size;

		// Token: 0x040000D9 RID: 217
		internal VorbisCodebook[] Books;

		// Token: 0x040000DA RID: 218
		internal VorbisTime[] Times;

		// Token: 0x040000DB RID: 219
		internal VorbisFloor[] Floors;

		// Token: 0x040000DC RID: 220
		internal VorbisResidue[] Residues;

		// Token: 0x040000DD RID: 221
		internal VorbisMapping[] Maps;

		// Token: 0x040000DE RID: 222
		internal VorbisMode[] Modes;

		// Token: 0x040000DF RID: 223
		private int _modeFieldBits;

		// Token: 0x040000E0 RID: 224
		internal long _glueBits;

		// Token: 0x040000E1 RID: 225
		internal long _metaBits;

		// Token: 0x040000E2 RID: 226
		internal long _bookBits;

		// Token: 0x040000E3 RID: 227
		internal long _timeHdrBits;

		// Token: 0x040000E4 RID: 228
		internal long _floorHdrBits;

		// Token: 0x040000E5 RID: 229
		internal long _resHdrBits;

		// Token: 0x040000E6 RID: 230
		internal long _mapHdrBits;

		// Token: 0x040000E7 RID: 231
		internal long _modeHdrBits;

		// Token: 0x040000E8 RID: 232
		internal long _wasteHdrBits;

		// Token: 0x040000E9 RID: 233
		internal long _modeBits;

		// Token: 0x040000EA RID: 234
		internal long _floorBits;

		// Token: 0x040000EB RID: 235
		internal long _resBits;

		// Token: 0x040000EC RID: 236
		internal long _wasteBits;

		// Token: 0x040000ED RID: 237
		internal long _samples;

		// Token: 0x040000EE RID: 238
		internal int _packetCount;

		// Token: 0x040000EF RID: 239
		internal Stopwatch _sw = new Stopwatch();

		// Token: 0x040000F0 RID: 240
		private IPacketProvider _packetProvider;

		// Token: 0x040000F1 RID: 241
		private DataPacket _parameterChangePacket;

		// Token: 0x040000F2 RID: 242
		private List<int> _pagesSeen;

		// Token: 0x040000F3 RID: 243
		private int _lastPageSeen;

		// Token: 0x040000F4 RID: 244
		private bool _eosFound;

		// Token: 0x040000F5 RID: 245
		private object _seekLock = new object();

		// Token: 0x040000F6 RID: 246
		private float[] _prevBuffer;

		// Token: 0x040000F7 RID: 247
		private RingBuffer _outputBuffer;

		// Token: 0x040000F8 RID: 248
		private Queue<int> _bitsPerPacketHistory;

		// Token: 0x040000F9 RID: 249
		private Queue<int> _sampleCountHistory;

		// Token: 0x040000FA RID: 250
		private int _preparedLength;

		// Token: 0x040000FB RID: 251
		internal bool _clipped;

		// Token: 0x040000FC RID: 252
		private Stack<DataPacket> _resyncQueue;

		// Token: 0x040000FD RID: 253
		private long _currentPosition;

		// Token: 0x040000FE RID: 254
		private long _reportedPosition;

		// Token: 0x040000FF RID: 255
		private VorbisMode _mode;

		// Token: 0x04000100 RID: 256
		private bool _prevFlag;

		// Token: 0x04000101 RID: 257
		private bool _nextFlag;

		// Token: 0x04000102 RID: 258
		private bool[] _noExecuteChannel;

		// Token: 0x04000103 RID: 259
		private VorbisFloor.PacketData[] _floorData;

		// Token: 0x04000104 RID: 260
		private float[][] _residue;

		// Token: 0x04000105 RID: 261
		private bool _isParameterChange;
	}
}
