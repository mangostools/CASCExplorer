﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CASCLib
{
    public interface IDB2Row
    {
        int Id { get; set; }
        T GetField<T>(int fieldIndex, int arrayIndex = -1);
        IDB2Row Clone();
    }

    public class WDC1Row : IDB2Row
    {
        private BitReader m_data;
        private WDC1Reader m_reader;
        private int Offset;

        public int Id { get; set; }

        private FieldMetaData[] fieldMeta;
        private ColumnMetaData[] columnMeta;
        private Value32[][] palletData;
        private Dictionary<int, Value32>[] commonData;
        private ReferenceEntry? refData;

        public WDC1Row(WDC1Reader reader, BitReader data, int id, ReferenceEntry? refData)
        {
            m_reader = reader;
            m_data = data;

            Offset = m_data.Offset;

            fieldMeta = reader.Meta;
            columnMeta = reader.ColumnMeta;
            palletData = reader.PalletData;
            commonData = reader.CommonData;
            this.refData = refData;

            if (id != -1)
                Id = id;
            else
            {
                int idFieldIndex = reader.IdFieldIndex;

                m_data.Position = columnMeta[idFieldIndex].RecordOffset;
                m_data.Offset = Offset;

                Id = GetFieldValue<int>(0, m_data, fieldMeta[idFieldIndex], columnMeta[idFieldIndex], palletData[idFieldIndex], commonData[idFieldIndex]);
            }
        }

        private static Dictionary<Type, Func<int, BitReader, FieldMetaData, ColumnMetaData, Value32[], Dictionary<int, Value32>, Dictionary<long, string>, object>> simpleReaders = new Dictionary<Type, Func<int, BitReader, FieldMetaData, ColumnMetaData, Value32[], Dictionary<int, Value32>, Dictionary<long, string>, object>>
        {
            [typeof(float)] = (id, data, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<float>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(int)] = (id, data, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<int>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(uint)] = (id, data, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<uint>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(short)] = (id, data, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<short>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(ushort)] = (id, data, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<ushort>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(sbyte)] = (id, data, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<sbyte>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(byte)] = (id, data, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<byte>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(string)] = (id, data, fieldMeta, columnMeta, palletData, commonData, stringTable) => { int strOfs = GetFieldValue<int>(id, data, fieldMeta, columnMeta, palletData, commonData); return stringTable[strOfs]; },
        };

        private static Dictionary<Type, Func<BitReader, FieldMetaData, ColumnMetaData, Value32[], Dictionary<int, Value32>, Dictionary<long, string>, int, object>> arrayReaders = new Dictionary<Type, Func<BitReader, FieldMetaData, ColumnMetaData, Value32[], Dictionary<int, Value32>, Dictionary<long, string>, int, object>>
        {
            [typeof(float)] = (data, fieldMeta, columnMeta, palletData, commonData, stringTable, arrayIndex) => GetFieldValueArray<float>(data, fieldMeta, columnMeta, palletData, commonData, arrayIndex + 1)[arrayIndex],
            [typeof(int)] = (data, fieldMeta, columnMeta, palletData, commonData, stringTable, arrayIndex) => GetFieldValueArray<int>(data, fieldMeta, columnMeta, palletData, commonData, arrayIndex + 1)[arrayIndex],
            [typeof(uint)] = (data, fieldMeta, columnMeta, palletData, commonData, stringTable, arrayIndex) => GetFieldValueArray<uint>(data, fieldMeta, columnMeta, palletData, commonData, arrayIndex + 1)[arrayIndex],
            [typeof(ulong)] = (data, fieldMeta, columnMeta, palletData, commonData, stringTable, arrayIndex) => GetFieldValueArray<ulong>(data, fieldMeta, columnMeta, palletData, commonData, arrayIndex + 1)[arrayIndex],
            [typeof(ushort)] = (data, fieldMeta, columnMeta, palletData, commonData, stringTable, arrayIndex) => GetFieldValueArray<ushort>(data, fieldMeta, columnMeta, palletData, commonData, arrayIndex + 1)[arrayIndex],
            [typeof(byte)] = (data, fieldMeta, columnMeta, palletData, commonData, stringTable, arrayIndex) => GetFieldValueArray<byte>(data, fieldMeta, columnMeta, palletData, commonData, arrayIndex + 1)[arrayIndex],
            [typeof(string)] = (data, fieldMeta, columnMeta, palletData, commonData, stringTable, arrayIndex) => { int strOfs = GetFieldValueArray<int>(data, fieldMeta, columnMeta, palletData, commonData, arrayIndex + 1)[arrayIndex]; return stringTable[strOfs]; },
        };

        public T GetField<T>(int fieldIndex, int arrayIndex = -1)
        {
            object value = null;

            if (fieldIndex >= m_reader.Meta.Length)
            {
                value = refData?.Id ?? 0;
                return (T)value;
            }

            m_data.Position = columnMeta[fieldIndex].RecordOffset;
            m_data.Offset = Offset;

            if (arrayIndex >= 0)
            {
                if (arrayReaders.TryGetValue(typeof(T), out var reader))
                    value = reader(m_data, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], m_reader.StringTable, arrayIndex);
                else
                    throw new Exception("Unhandled array type: " + typeof(T).Name);
            }
            else
            {
                if (simpleReaders.TryGetValue(typeof(T), out var reader))
                    value = reader(Id, m_data, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], m_reader.StringTable);
                else
                    throw new Exception("Unhandled field type: " + typeof(T).Name);
            }

            return (T)value;
        }

        private static T GetFieldValue<T>(int Id, BitReader r, FieldMetaData fieldMeta, ColumnMetaData columnMeta, Value32[] palletData, Dictionary<int, Value32> commonData) where T : struct
        {
            switch (columnMeta.CompressionType)
            {
                case CompressionType.None:
                    int bitSize = 32 - fieldMeta.Bits;
                    if (bitSize > 0)
                        return r.ReadValue64(bitSize).GetValue<T>();
                    else
                        return r.ReadValue64(columnMeta.Immediate.BitWidth).GetValue<T>();
                case CompressionType.Immediate:
                    return r.ReadValue64(columnMeta.Immediate.BitWidth).GetValue<T>();
                case CompressionType.Common:
                    if (commonData.TryGetValue(Id, out Value32 val))
                        return val.GetValue<T>();
                    else
                        return columnMeta.Common.DefaultValue.GetValue<T>();
                case CompressionType.Pallet:
                    uint palletIndex = r.ReadUInt32(columnMeta.Pallet.BitWidth);

                    T val1 = palletData[palletIndex].GetValue<T>();

                    return val1;
            }
            throw new Exception(string.Format("Unexpected compression type {0}", columnMeta.CompressionType));
        }

        private static T[] GetFieldValueArray<T>(BitReader r, FieldMetaData fieldMeta, ColumnMetaData columnMeta, Value32[] palletData, Dictionary<int, Value32> commonData, int arraySize) where T : struct
        {
            switch (columnMeta.CompressionType)
            {
                case CompressionType.None:
                    int bitSize = 32 - fieldMeta.Bits;

                    T[] arr1 = new T[arraySize];

                    for (int i = 0; i < arr1.Length; i++)
                    {
                        if (bitSize > 0)
                            arr1[i] = r.ReadValue64(bitSize).GetValue<T>();
                        else
                            arr1[i] = r.ReadValue64(columnMeta.Immediate.BitWidth).GetValue<T>();
                    }

                    return arr1;
                case CompressionType.Immediate:
                    T[] arr2 = new T[arraySize];

                    for (int i = 0; i < arr2.Length; i++)
                        arr2[i] = r.ReadValue64(columnMeta.Immediate.BitWidth).GetValue<T>();

                    return arr2;
                case CompressionType.PalletArray:
                    int cardinality = columnMeta.Pallet.Cardinality;

                    if (arraySize != cardinality)
                        throw new Exception("Struct missmatch for pallet array field?");

                    uint palletArrayIndex = r.ReadUInt32(columnMeta.Pallet.BitWidth);

                    T[] arr3 = new T[cardinality];

                    for (int i = 0; i < arr3.Length; i++)
                        arr3[i] = palletData[i + cardinality * (int)palletArrayIndex].GetValue<T>();

                    return arr3;
            }
            throw new Exception(string.Format("Unexpected compression type {0}", columnMeta.CompressionType));
        }

        public IDB2Row Clone()
        {
            return (IDB2Row)MemberwiseClone();
        }
    }

    public class WDC1Reader : DB2Reader
    {
        private const int HeaderSize = 84;
        private const uint WDC1FmtSig = 0x31434457; // WDC1

        public WDC1Reader(string dbcFile) : this(new FileStream(dbcFile, FileMode.Open)) { }

        public WDC1Reader(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.UTF8))
            {
                if (reader.BaseStream.Length < HeaderSize)
                {
                    throw new InvalidDataException(String.Format("DB6 file is corrupted!"));
                }

                uint magic = reader.ReadUInt32();

                if (magic != WDC1FmtSig)
                {
                    throw new InvalidDataException(String.Format("DB6 file is corrupted!"));
                }

                RecordsCount = reader.ReadInt32();
                FieldsCount = reader.ReadInt32();
                RecordSize = reader.ReadInt32();
                StringTableSize = reader.ReadInt32();

                uint tableHash = reader.ReadUInt32();
                uint layoutHash = reader.ReadUInt32();
                MinIndex = reader.ReadInt32();
                MaxIndex = reader.ReadInt32();
                int locale = reader.ReadInt32();
                int copyTableSize = reader.ReadInt32();
                int flags = reader.ReadUInt16();
                IdFieldIndex = reader.ReadUInt16();

                int totalFieldsCount = reader.ReadInt32();
                int packedDataOffset = reader.ReadInt32(); // Offset within the field where packed data starts
                int lookupColumnCount = reader.ReadInt32(); // count of lookup columns
                int sparseTableOffset = reader.ReadInt32(); // absolute value, {uint offset, ushort size}[MaxId - MinId + 1]
                int indexDataSize = reader.ReadInt32(); // int indexData[IndexDataSize / 4]
                int columnMetaDataSize = reader.ReadInt32(); // 24 * NumFields bytes, describes column bit packing, {ushort recordOffset, ushort size, uint additionalDataSize, uint compressionType, uint packedDataOffset or commonvalue, uint cellSize, uint cardinality}[NumFields], sizeof(DBC2CommonValue) == 8
                int commonDataSize = reader.ReadInt32();
                int palletDataSize = reader.ReadInt32(); // in bytes, sizeof(DBC2PalletValue) == 4
                int referenceDataSize = reader.ReadInt32(); // uint NumRecords, uint minId, uint maxId, {uint id, uint index}[NumRecords], questionable usefulness...

                // field meta data
                m_meta = reader.ReadArray<FieldMetaData>(FieldsCount);

                if ((flags & 0x1) == 0)
                {
                    // records data
                    recordsData = reader.ReadBytes(RecordsCount * RecordSize);

                    Array.Resize(ref recordsData, recordsData.Length + 8); // pad with extra zeros so we don't crash when reading

                    // string data
                    m_stringsTable = new Dictionary<long, string>();

                    for (int i = 0; i < StringTableSize;)
                    {
                        long oldPos = reader.BaseStream.Position;

                        m_stringsTable[i] = reader.ReadCString();

                        i += (int)(reader.BaseStream.Position - oldPos);
                    }
                }
                else
                {
                    // sparse data with inlined strings
                    sparseData = reader.ReadBytes(sparseTableOffset - HeaderSize - Marshal.SizeOf<FieldMetaData>() * FieldsCount);

                    if (reader.BaseStream.Position != sparseTableOffset)
                        throw new Exception("r.BaseStream.Position != sparseTableOffset");

                    sparseEntries = reader.ReadArray<SparseEntry>(MaxIndex - MinIndex + 1);

                    if (sparseTableOffset != 0)
                        throw new Exception("Sparse Table NYI!");
                    else
                        throw new Exception("Sparse Table with zero offset?");
                }

                // index data
                m_indexData = reader.ReadArray<int>(indexDataSize / 4);

                // duplicate rows data
                Dictionary<int, int> copyData = new Dictionary<int, int>();

                for (int i = 0; i < copyTableSize / 8; i++)
                    copyData[reader.ReadInt32()] = reader.ReadInt32();

                // column meta data
                m_columnMeta = reader.ReadArray<ColumnMetaData>(FieldsCount);

                // pallet data
                m_palletData = new Value32[m_columnMeta.Length][];

                for (int i = 0; i < m_columnMeta.Length; i++)
                {
                    if (m_columnMeta[i].CompressionType == CompressionType.Pallet || m_columnMeta[i].CompressionType == CompressionType.PalletArray)
                    {
                        m_palletData[i] = reader.ReadArray<Value32>((int)m_columnMeta[i].AdditionalDataSize / 4);
                    }
                }

                // common data
                m_commonData = new Dictionary<int, Value32>[m_columnMeta.Length];

                for (int i = 0; i < m_columnMeta.Length; i++)
                {
                    if (m_columnMeta[i].CompressionType == CompressionType.Common)
                    {
                        Dictionary<int, Value32> commonValues = new Dictionary<int, Value32>();
                        m_commonData[i] = commonValues;

                        for (int j = 0; j < m_columnMeta[i].AdditionalDataSize / 8; j++)
                            commonValues[reader.ReadInt32()] = reader.Read<Value32>();
                    }
                }

                // reference data
                ReferenceData refData = null;

                if (referenceDataSize > 0)
                {
                    refData = new ReferenceData
                    {
                        NumRecords = reader.ReadInt32(),
                        MinId = reader.ReadInt32(),
                        MaxId = reader.ReadInt32()
                    };

                    refData.Entries = reader.ReadArray<ReferenceEntry>(refData.NumRecords);
                }

                BitReader bitReader = new BitReader(recordsData);

                for (int i = 0; i < RecordsCount; ++i)
                {
                    bitReader.Position = 0;
                    bitReader.Offset = i * RecordSize;

                    IDB2Row rec = new WDC1Row(this, bitReader, indexDataSize != 0 ? m_indexData[i] : -1, refData?.Entries[i]);

                    if (indexDataSize != 0)
                        _Records.Add(m_indexData[i], rec);
                    else
                        _Records.Add(rec.Id, rec);

                    if (i % 1000 == 0)
                        Console.Write("\r{0} records read", i);
                }

                foreach (var copyRow in copyData)
                {
                    IDB2Row rec = _Records[copyRow.Value].Clone();
                    rec.Id = copyRow.Key;
                    _Records.Add(copyRow.Key, rec);
                }
            }
        }
    }

    public struct FieldMetaData
    {
        public short Bits;
        public short Offset;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ColumnMetaData
    {
        [FieldOffset(0)]
        public ushort RecordOffset;
        [FieldOffset(2)]
        public ushort Size;
        [FieldOffset(4)]
        public uint AdditionalDataSize;
        [FieldOffset(8)]
        public CompressionType CompressionType;
        [FieldOffset(12)]
        public ColumnCompressionData_Immediate Immediate;
        [FieldOffset(12)]
        public ColumnCompressionData_Pallet Pallet;
        [FieldOffset(12)]
        public ColumnCompressionData_Common Common;
    }

    public struct ColumnCompressionData_Immediate
    {
        public int BitOffset;
        public int BitWidth;
        public int Flags; // 0x1 signed
    };

    public struct ColumnCompressionData_Pallet
    {
        public int BitOffset;
        public int BitWidth;
        public int Cardinality;
    };

    public struct ColumnCompressionData_Common
    {
        public Value32 DefaultValue;
        public int B;
        public int C;
    };

    public struct Value32
    {
        unsafe fixed byte Value[4];

        public T GetValue<T>() where T : struct
        {
            unsafe
            {
                fixed (byte* ptr = Value)
                    return FastStruct<T>.ArrayToStructure(ref ptr[0]);
            }
        }
    }

    public struct Value64
    {
        unsafe fixed byte Value[8];

        public T GetValue<T>() where T : struct
        {
            unsafe
            {
                fixed (byte* ptr = Value)
                    return FastStruct<T>.ArrayToStructure(ref ptr[0]);
            }
        }
    }

    public enum CompressionType
    {
        None = 0,
        Immediate = 1,
        Common = 2,
        Pallet = 3,
        PalletArray = 4,
        SignedImmediate = 5
    }

    public struct ReferenceEntry
    {
        public uint Id;
        public uint Index;
    }

    public class ReferenceData
    {
        public int NumRecords { get; set; }
        public int MinId { get; set; }
        public int MaxId { get; set; }
        public ReferenceEntry[] Entries { get; set; }
    }

    [Flags]
    public enum DB2Flags
    {
        None = 0x0,
        Sparse = 0x1,
        SecondaryKey = 0x2,
        Index = 0x4,
        Unknown1 = 0x8,
        Unknown2 = 0x10
    }

    public struct SparseEntry
    {
        public uint Offset;
        public uint Size;
    }

    public class BitReader
    {
        private byte[] m_array;
        private int m_readPos;
        private int m_readOffset;

        public int Position { get => m_readPos; set => m_readPos = value; }
        public int Offset { get => m_readOffset; set => m_readOffset = value; }
        public byte[] Data { get => m_array; set => m_array = value; }

        public BitReader(byte[] data)
        {
            m_array = data;
        }

        public BitReader(byte[] data, int offset)
        {
            m_array = data;
            m_readOffset = offset;
        }

        public uint ReadUInt32(int numBits)
        {
            uint result = FastStruct<uint>.ArrayToStructure(ref m_array[m_readOffset + (m_readPos >> 3)]) << (32 - numBits - (m_readPos & 7)) >> (32 - numBits);
            m_readPos += numBits;
            return result;
        }

        public ulong ReadUInt64(int numBits)
        {
            ulong result = FastStruct<ulong>.ArrayToStructure(ref m_array[m_readOffset + (m_readPos >> 3)]) << (64 - numBits - (m_readPos & 7)) >> (64 - numBits);
            m_readPos += numBits;
            return result;
        }

        public Value32 ReadValue32(int numBits)
        {
            unsafe
            {
                ulong result = ReadUInt32(numBits);
                return *(Value32*)&result;
            }
        }

        public Value64 ReadValue64(int numBits)
        {
            unsafe
            {
                ulong result = ReadUInt64(numBits);
                return *(Value64*)&result;
            }
        }

        // this will probably work in C# 7.3 once blittable generic constrain added, or not...
        //public unsafe T Read<T>(int numBits) where T : struct
        //{
        //    //fixed (byte* ptr = &m_array[m_readOffset + (m_readPos >> 3)])
        //    //{
        //    //    T val = *(T*)ptr << (sizeof(T) - numBits - (m_readPos & 7)) >> (sizeof(T) - numBits);
        //    //    m_readPos += numBits;
        //    //    return val;
        //    //}
        //    //T result = FastStruct<T>.ArrayToStructure(ref m_array[m_readOffset + (m_readPos >> 3)]) << (32 - numBits - (m_readPos & 7)) >> (32 - numBits);
        //    //m_readPos += numBits;
        //    //return result;
        //}
    }
}
