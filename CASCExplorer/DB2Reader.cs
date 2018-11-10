using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace CASCLib
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ArraySizeAttribute : Attribute
    {
        public int Size { get; private set; }

        public ArraySizeAttribute(int size)
        {
            Size = size;
        }
    }

    public abstract class ClientDBRow
    {
        private FieldInfo[] Fields;

        public abstract int GetId();

        public ClientDBRow()
        {
            Fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance).OrderBy(f => f.MetadataToken).ToArray();
        }

        public void Read(BitReader r, long recordsOffset, Dictionary<long, string> stringsTable, FieldMetaData[] fieldMeta, ColumnMetaData[] columnMeta, Value32[][] palletData, Dictionary<int, Value32>[] commonData, int refId = -1, int id = -1, bool isSparse = false)
        {
            int fieldIndex = 0;

            foreach (var f in Fields)
            {
                Type t = f.FieldType;

                if (f.Name == "Id" && id != -1)
                {
                    f.SetValue(this, id);
                    continue;
                }

                if (fieldIndex >= fieldMeta.Length)
                {
                    if (refId != -1)
                        f.SetValue(this, refId);
                    continue;
                }

                if (t == typeof(float))
                    f.SetValue(this, FieldReader.GetFieldValue<float>(GetId(), r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(long))
                    f.SetValue(this, FieldReader.GetFieldValue<long>(GetId(), r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(ulong))
                    f.SetValue(this, FieldReader.GetFieldValue<ulong>(GetId(), r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(int))
                    f.SetValue(this, FieldReader.GetFieldValue<int>(GetId(), r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(uint))
                    f.SetValue(this, FieldReader.GetFieldValue<uint>(GetId(), r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(short))
                    f.SetValue(this, FieldReader.GetFieldValue<short>(GetId(), r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(ushort))
                    f.SetValue(this, FieldReader.GetFieldValue<ushort>(GetId(), r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(byte))
                    f.SetValue(this, FieldReader.GetFieldValue<byte>(GetId(), r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(sbyte))
                    f.SetValue(this, FieldReader.GetFieldValue<sbyte>(GetId(), r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(string))
                {
                    if (isSparse)
                    {
                        f.SetValue(this, r.ReadCString());
                    }
                    else
                    {
                        var pos = recordsOffset + r.Offset + (r.Position >> 3);
                        int ofs = FieldReader.GetFieldValue<int>(GetId(), r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]);
                        f.SetValue(this, stringsTable[pos + ofs]);
                    }
                }
                else if (t.IsArray)
                {
                    Type arrayElementType = f.FieldType.GetElementType();

                    ArraySizeAttribute atr = (ArraySizeAttribute)f.GetCustomAttribute(typeof(ArraySizeAttribute));

                    if (atr == null)
                        throw new Exception(GetType().Name + "." + f.Name + " missing ArraySizeAttribute");

                    if (arrayElementType == typeof(int))
                    {
                        f.SetValue(this, FieldReader.GetFieldValueArray<int>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    }
                    else if (arrayElementType == typeof(uint))
                    {
                        f.SetValue(this, FieldReader.GetFieldValueArray<uint>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    }
                    //else if (arrayElementType == typeof(ulong))
                    //{
                    //    f.SetValue(this, FieldReader.GetFieldValueArray<ulong>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    //}
                    else if (arrayElementType == typeof(float))
                    {
                        f.SetValue(this, FieldReader.GetFieldValueArray<float>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    }
                    else if (arrayElementType == typeof(ushort))
                    {
                        f.SetValue(this, FieldReader.GetFieldValueArray<ushort>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    }
                    else if (arrayElementType == typeof(byte))
                    {
                        f.SetValue(this, FieldReader.GetFieldValueArray<byte>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    }
                    else if (arrayElementType == typeof(string))
                    {
                        string[] array = new string[atr.Size];

                        if (isSparse)
                        {
                            for (int i = 0; i < array.Length; i++)
                                array[i] = r.ReadCString();
                        }
                        else
                        {
                            var pos = recordsOffset + r.Offset + (r.Position >> 3);

                            int[] strIdx = FieldReader.GetFieldValueArray<int>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size);

                            for (int i = 0; i < array.Length; i++)
                                array[i] = stringsTable[pos + i * 4 + strIdx[i]];
                        }

                        f.SetValue(this, array);
                    }
                    else
                        throw new Exception("Unhandled array type: " + arrayElementType.Name);
                }
                else
                    throw new Exception("Unhandled DbcTable type: " + t.Name);

                fieldIndex++;
            }
        }

        public T Clone<T>() where T : ClientDBRow
        {
            return (T)MemberwiseClone();
        }

        public void SetId(int id)
        {
            GetType().GetField("Id").SetValue(this, id);
        }
    }

    public interface IDB2Row
    {
        int Id { get; set; }
        T GetField<T>(int fieldIndex, int arrayIndex = -1);
        T As<T>() where T : ClientDBRow, new();
        IDB2Row Clone();
    }

    public abstract class DB2Reader : IEnumerable<KeyValuePair<int, IDB2Row>>
    {
        public int RecordsCount { get; protected set; }
        public int FieldsCount { get; protected set; }
        public int RecordSize { get; protected set; }
        public int StringTableSize { get; protected set; }
        public uint TableHash { get; protected set; }
        public uint LayoutHash { get; protected set; }
        public int MinIndex { get; protected set; }
        public int MaxIndex { get; protected set; }
        public int IdFieldIndex { get; protected set; }

        protected FieldMetaData[] m_meta;
        public FieldMetaData[] Meta => m_meta;

        protected ColumnMetaData[] m_columnMeta;
        public ColumnMetaData[] ColumnMeta => m_columnMeta;

        protected Value32[][] m_palletData;
        public Value32[][] PalletData => m_palletData;

        protected Dictionary<int, Value32>[] m_commonData;
        public Dictionary<int, Value32>[] CommonData => m_commonData;

        protected Dictionary<int, IDB2Row> _Records = new Dictionary<int, IDB2Row>();

        public bool HasRow(int id)
        {
            return _Records.ContainsKey(id);
        }

        public IDB2Row GetRow(int id)
        {
            _Records.TryGetValue(id, out IDB2Row row);
            return row;
        }

        public IEnumerator<KeyValuePair<int, IDB2Row>> GetEnumerator()
        {
            return _Records.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Records.GetEnumerator();
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
    }

    public struct ColumnCompressionData_Pallet
    {
        public int BitOffset;
        public int BitWidth;
        public int Cardinality;
    }

    public struct ColumnCompressionData_Common
    {
        public Value32 DefaultValue;
        public int B;
        public int C;
    }

    public struct Value32
    {
        private uint Value;

        public T As<T>() where T : unmanaged
        {
            return Unsafe.As<uint, T>(ref Value);
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
        public int Id;
        public int Index;
    }

    public class ReferenceData
    {
        public int NumRecords { get; set; }
        public int MinId { get; set; }
        public int MaxId { get; set; }
        public Dictionary<int, int> Entries { get; set; }
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
        public int Offset;
        public ushort Size;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SectionHeader_WDC2
    {
        public ulong TactKeyLookup;
        public int FileOffset;
        public int NumRecords;
        public int StringTableSize;
        public int CopyTableSize;
        public int SparseTableOffset; // CatalogDataOffset, absolute value, {uint offset, ushort size}[MaxId - MinId + 1]
        public int IndexDataSize; // int indexData[IndexDataSize / 4]
        public int ParentLookupDataSize; // uint NumRecords, uint minId, uint maxId, {uint id, uint index}[NumRecords], questionable usefulness...
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SectionHeader_WDC3
    {
        public ulong TactKeyLookup;
        public int FileOffset;
        public int NumRecords;
        public int StringTableSize;
        public int SparseDataEndOffset; // CatalogDataOffset, absolute value, {uint offset, ushort size}[MaxId - MinId + 1]
        public int IndexDataSize; // int indexData[IndexDataSize / 4]
        public int ParentLookupDataSize; // uint NumRecords, uint minId, uint maxId, {uint id, uint index}[NumRecords], questionable usefulness...
        public int NumSparseRecords;
        public int NumCopyRecords;
    }

    public class BitReader
    {
        private byte[] m_data;
        private int m_bitPosition;
        private int m_offset;

        public int Position { get => m_bitPosition; set => m_bitPosition = value; }
        public int Offset { get => m_offset; set => m_offset = value; }
        public byte[] Data { get => m_data; set => m_data = value; }

        public BitReader(byte[] data)
        {
            m_data = data;
        }

        public BitReader(byte[] data, int offset)
        {
            m_data = data;
            m_offset = offset;
        }

        public T Read<T>(int numBits) where T : unmanaged
        {
            ulong result = Unsafe.As<byte, ulong>(ref m_data[m_offset + (m_bitPosition >> 3)]) << (64 - numBits - (m_bitPosition & 7)) >> (64 - numBits);
            m_bitPosition += numBits;
            return Unsafe.As<ulong, T>(ref result);
        }

        public T ReadSigned<T>(int numBits) where T : unmanaged
        {
            ulong result = Unsafe.As<byte, ulong>(ref m_data[m_offset + (m_bitPosition >> 3)]) << (64 - numBits - (m_bitPosition & 7)) >> (64 - numBits);
            m_bitPosition += numBits;
            ulong signedShift = (1UL << (numBits - 1));
            result = (signedShift ^ result) - signedShift;
            return Unsafe.As<ulong, T>(ref result);
        }

        public string ReadCString()
        {
            int start = m_bitPosition;

            while (m_data[m_offset + (m_bitPosition >> 3)] != 0)
                m_bitPosition += 8;

            string result = Encoding.UTF8.GetString(m_data, m_offset + (start >> 3), (m_bitPosition - start) >> 3);
            m_bitPosition += 8;
            return result;
        }
    }

    public class FieldReader
    {
        public static T GetFieldValue<T>(int id, BitReader r, FieldMetaData fieldMeta, ColumnMetaData columnMeta, Value32[] palletData, Dictionary<int, Value32> commonData) where T : unmanaged
        {
            switch (columnMeta.CompressionType)
            {
                case CompressionType.None:
                    int bitSize = 32 - fieldMeta.Bits;
                    if (bitSize > 0)
                        return r.Read<T>(bitSize);
                    else
                        return r.Read<T>(columnMeta.Immediate.BitWidth);
                case CompressionType.Immediate:
                    return r.Read<T>(columnMeta.Immediate.BitWidth);
                case CompressionType.SignedImmediate:
                    return r.ReadSigned<T>(columnMeta.Immediate.BitWidth);
                case CompressionType.Common:
                    if (commonData.TryGetValue(id, out Value32 val))
                        return val.As<T>();
                    else
                        return columnMeta.Common.DefaultValue.As<T>();
                case CompressionType.Pallet:
                    uint palletIndex = r.Read<uint>(columnMeta.Pallet.BitWidth);
                    return palletData[palletIndex].As<T>();
            }
            throw new Exception(string.Format("Unexpected compression type {0}", columnMeta.CompressionType));
        }

        public static T[] GetFieldValueArray<T>(BitReader r, FieldMetaData fieldMeta, ColumnMetaData columnMeta, Value32[] palletData, Dictionary<int, Value32> commonData, int arraySize) where T : unmanaged
        {
            switch (columnMeta.CompressionType)
            {
                case CompressionType.None:
                    int bitSize = 32 - fieldMeta.Bits;

                    T[] arr1 = new T[arraySize];

                    for (int i = 0; i < arr1.Length; i++)
                    {
                        if (bitSize > 0)
                            arr1[i] = r.Read<T>(bitSize);
                        else
                            arr1[i] = r.Read<T>(columnMeta.Immediate.BitWidth);
                    }

                    return arr1;
                case CompressionType.Immediate:
                    T[] arr2 = new T[arraySize];

                    for (int i = 0; i < arr2.Length; i++)
                        arr2[i] = r.Read<T>(columnMeta.Immediate.BitWidth);

                    return arr2;
                case CompressionType.SignedImmediate:
                    T[] arr4 = new T[arraySize];

                    for (int i = 0; i < arr4.Length; i++)
                        arr4[i] = r.ReadSigned<T>(columnMeta.Immediate.BitWidth);

                    return arr4;
                case CompressionType.PalletArray:
                    int cardinality = columnMeta.Pallet.Cardinality;

                    if (arraySize != cardinality)
                        throw new Exception("Struct missmatch for pallet array field?");

                    uint palletArrayIndex = r.Read<uint>(columnMeta.Pallet.BitWidth);

                    T[] arr3 = new T[cardinality];

                    for (int i = 0; i < arr3.Length; i++)
                        arr3[i] = palletData[i + cardinality * (int)palletArrayIndex].As<T>();

                    return arr3;
            }
            throw new Exception(string.Format("Unexpected compression type {0}", columnMeta.CompressionType));
        }
    }
}
