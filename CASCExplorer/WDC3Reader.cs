using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public void Read(BitReader r, long recordsOffset, Dictionary<long, string> stringsTable, FieldMetaData[] fieldMeta, ColumnMetaData[] columnMeta, Value32[][] palletData, Dictionary<int, Value32>[] commonData, ReferenceData refData, int index = -1, bool isSparse = false)
        {
            int fieldIndex = 0;

            foreach (var f in Fields)
            {
                Type t = f.FieldType;

                if (f.Name == "Id" && index != -1)
                {
                    f.SetValue(this, index);
                    continue;
                }

                if (fieldIndex >= fieldMeta.Length && refData.Entries.TryGetValue(GetId(), out int refId))
                {
                    f.SetValue(this, refId);
                    continue;
                }

                if (t == typeof(float))
                    f.SetValue(this, GetFieldValue<float>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(long))
                    f.SetValue(this, GetFieldValue<long>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(ulong))
                    f.SetValue(this, GetFieldValue<ulong>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(int))
                    f.SetValue(this, GetFieldValue<int>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(uint))
                    f.SetValue(this, GetFieldValue<uint>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(short))
                    f.SetValue(this, GetFieldValue<short>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(ushort))
                    f.SetValue(this, GetFieldValue<ushort>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(byte))
                    f.SetValue(this, GetFieldValue<byte>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(sbyte))
                    f.SetValue(this, GetFieldValue<sbyte>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]));
                else if (t == typeof(string))
                {
                    if (isSparse)
                    {
                        f.SetValue(this, r.ReadCString());
                    }
                    else
                    {
                        var pos = recordsOffset + r.Offset + (r.Position >> 3);
                        int ofs = GetFieldValue<int>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex]);
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
                        f.SetValue(this, GetFieldValueArray<int>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    }
                    else if (arrayElementType == typeof(uint))
                    {
                        f.SetValue(this, GetFieldValueArray<uint>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    }
                    //else if (arrayElementType == typeof(ulong))
                    //{
                    //    f.SetValue(this, GetFieldValueArray<ulong>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    //}
                    else if (arrayElementType == typeof(float))
                    {
                        f.SetValue(this, GetFieldValueArray<float>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    }
                    else if (arrayElementType == typeof(ushort))
                    {
                        f.SetValue(this, GetFieldValueArray<ushort>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
                    }
                    else if (arrayElementType == typeof(byte))
                    {
                        f.SetValue(this, GetFieldValueArray<byte>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size));
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

                            int[] strIdx = GetFieldValueArray<int>(r, fieldMeta[fieldIndex], columnMeta[fieldIndex], palletData[fieldIndex], commonData[fieldIndex], atr.Size);

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

        private T GetFieldValue<T>(BitReader r, FieldMetaData fieldMeta, ColumnMetaData columnMeta, Value32[] palletData, Dictionary<int, Value32> commonData) where T : unmanaged
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
                    if (commonData.TryGetValue(GetId(), out Value32 val))
                        return val.As<T>();
                    else
                        return columnMeta.Common.DefaultValue.As<T>();
                case CompressionType.Pallet:
                    uint palletIndex = r.Read<uint>(columnMeta.Pallet.BitWidth);
                    return palletData[palletIndex].As<T>();
            }
            throw new Exception(string.Format("Unexpected compression type {0}", columnMeta.CompressionType));
        }

        private T[] GetFieldValueArray<T>(BitReader r, FieldMetaData fieldMeta, ColumnMetaData columnMeta, Value32[] palletData, Dictionary<int, Value32> commonData, int arraySize) where T : unmanaged
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

        public T Clone<T>() where T : ClientDBRow
        {
            return (T)MemberwiseClone();
        }

        public void SetId(int id)
        {
            GetType().GetField("Id").SetValue(this, id);
        }
    }

    public class WDC3Row : IDB2Row
    {
        private BitReader m_data;
        private DB2Reader m_reader;
        private int m_dataOffset;
        private int m_recordsOffset;
        private bool m_isSparse;
        private bool m_idRead;

        public int Id { get; set; }

        private FieldMetaData[] m_fieldMeta;
        private ColumnMetaData[] m_columnMeta;
        private Value32[][] m_palletData;
        private Dictionary<int, Value32>[] m_commonData;
        private ReferenceData m_refData;

        public WDC3Row(DB2Reader reader, BitReader data, int recordsOffset, int id, bool isSparse)
        {
            m_reader = reader;
            m_data = data;
            m_recordsOffset = recordsOffset;
            m_isSparse = isSparse;

            m_dataOffset = m_data.Offset;

            m_fieldMeta = reader.Meta;
            m_columnMeta = reader.ColumnMeta;
            m_palletData = reader.PalletData;
            m_commonData = reader.CommonData;
            m_refData = reader.ReferenceData;

            if (id != -1)
                Id = id;
            else
            {
                int idFieldIndex = reader.IdFieldIndex;

                m_data.Position = m_columnMeta[idFieldIndex].RecordOffset;

                Id = GetFieldValue<int>(0, m_data, m_fieldMeta[idFieldIndex], m_columnMeta[idFieldIndex], m_palletData[idFieldIndex], m_commonData[idFieldIndex]);

                m_idRead = true;
            }
        }

        private static Dictionary<Type, Func<int, BitReader, int, FieldMetaData, ColumnMetaData, Value32[], Dictionary<int, Value32>, Dictionary<long, string>, object>> simpleReaders = new Dictionary<Type, Func<int, BitReader, int, FieldMetaData, ColumnMetaData, Value32[], Dictionary<int, Value32>, Dictionary<long, string>, object>>
        {
            [typeof(float)] = (id, data, recordsOffset, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<float>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(int)] = (id, data, recordsOffset, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<int>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(uint)] = (id, data, recordsOffset, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<uint>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(short)] = (id, data, recordsOffset, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<short>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(ushort)] = (id, data, recordsOffset, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<ushort>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(sbyte)] = (id, data, recordsOffset, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<sbyte>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(byte)] = (id, data, recordsOffset, fieldMeta, columnMeta, palletData, commonData, stringTable) => GetFieldValue<byte>(id, data, fieldMeta, columnMeta, palletData, commonData),
            [typeof(string)] = (id, data, recordsOffset, fieldMeta, columnMeta, palletData, commonData, stringTable) => { var pos = recordsOffset + data.Offset + (data.Position >> 3); int strOfs = GetFieldValue<int>(id, data, fieldMeta, columnMeta, palletData, commonData); return stringTable[pos + strOfs]; },
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

        public T As<T>() where T : ClientDBRow, new()
        {
            T row = new T();
            m_data.Position = 0;
            m_data.Offset = m_dataOffset;
            row.Read(m_data, m_recordsOffset, m_reader.StringTable, m_fieldMeta, m_columnMeta, m_palletData, m_commonData, m_refData, m_idRead ? -1 : Id, m_isSparse);
            return row;
        }

        public T GetField<T>(int fieldIndex, int arrayIndex = -1)
        {
            object value = null;

            if (fieldIndex >= m_reader.Meta.Length && m_refData.Entries.TryGetValue(Id, out int refId))
            {
                value = refId;
                return (T)value;
            }

            m_data.Position = m_columnMeta[fieldIndex].RecordOffset;
            m_data.Offset = m_dataOffset;

            if (arrayIndex >= 0)
            {
                if (arrayReaders.TryGetValue(typeof(T), out var reader))
                    value = reader(m_data, m_fieldMeta[fieldIndex], m_columnMeta[fieldIndex], m_palletData[fieldIndex], m_commonData[fieldIndex], m_reader.StringTable, arrayIndex);
                else
                    throw new Exception("Unhandled array type: " + typeof(T).Name);
            }
            else
            {
                if (simpleReaders.TryGetValue(typeof(T), out var reader))
                    value = reader(Id, m_data, m_recordsOffset, m_fieldMeta[fieldIndex], m_columnMeta[fieldIndex], m_palletData[fieldIndex], m_commonData[fieldIndex], m_reader.StringTable);
                else
                    throw new Exception("Unhandled field type: " + typeof(T).Name);
            }

            return (T)value;
        }

        private static T GetFieldValue<T>(int Id, BitReader r, FieldMetaData fieldMeta, ColumnMetaData columnMeta, Value32[] palletData, Dictionary<int, Value32> commonData) where T : unmanaged
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
                    if (commonData.TryGetValue(Id, out Value32 val))
                        return val.As<T>();
                    else
                        return columnMeta.Common.DefaultValue.As<T>();
                case CompressionType.Pallet:
                    uint palletIndex = r.Read<uint>(columnMeta.Pallet.BitWidth);

                    T val1 = palletData[palletIndex].As<T>();

                    return val1;
            }
            throw new Exception(string.Format("Unexpected compression type {0}", columnMeta.CompressionType));
        }

        private static T[] GetFieldValueArray<T>(BitReader r, FieldMetaData fieldMeta, ColumnMetaData columnMeta, Value32[] palletData, Dictionary<int, Value32> commonData, int arraySize) where T : unmanaged
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

        public IDB2Row Clone()
        {
            return (IDB2Row)MemberwiseClone();
        }
    }

    public class WDC3Reader : DB2Reader
    {
        private const int HeaderSize = 72 + 1 * 40;
        private const uint WDC3FmtSig = 0x33434457; // WDC3

        public WDC3Reader(string dbcFile) : this(new FileStream(dbcFile, FileMode.Open)) { }

        public WDC3Reader(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.UTF8))
            {
                if (reader.BaseStream.Length < HeaderSize)
                    throw new InvalidDataException(String.Format("WDC3 file is corrupted!"));

                uint magic = reader.ReadUInt32();

                if (magic != WDC3FmtSig)
                    throw new InvalidDataException(String.Format("WDC3 file is corrupted!"));

                RecordsCount = reader.ReadInt32();
                FieldsCount = reader.ReadInt32();
                RecordSize = reader.ReadInt32();
                StringTableSize = reader.ReadInt32();
                TableHash = reader.ReadUInt32();
                LayoutHash = reader.ReadUInt32();
                MinIndex = reader.ReadInt32();
                MaxIndex = reader.ReadInt32();
                int locale = reader.ReadInt32();
                int flags = reader.ReadUInt16();
                IdFieldIndex = reader.ReadUInt16();
                int totalFieldsCount = reader.ReadInt32();
                int packedDataOffset = reader.ReadInt32(); // Offset within the field where packed data starts
                int lookupColumnCount = reader.ReadInt32(); // count of lookup columns
                int columnMetaDataSize = reader.ReadInt32(); // 24 * NumFields bytes, describes column bit packing, {ushort recordOffset, ushort size, uint additionalDataSize, uint compressionType, uint packedDataOffset or commonvalue, uint cellSize, uint cardinality}[NumFields], sizeof(DBC2CommonValue) == 8
                int commonDataSize = reader.ReadInt32();
                int palletDataSize = reader.ReadInt32(); // in bytes, sizeof(DBC2PalletValue) == 4
                int sectionsCount = reader.ReadInt32();

                //if (sectionsCount > 1)
                //    throw new Exception("sectionsCount > 1");

                SectionHeader_WDC3[] sections = reader.ReadArray<SectionHeader_WDC3>(sectionsCount);

                // field meta data
                m_meta = reader.ReadArray<FieldMetaData>(FieldsCount);

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

                bool isSparse = (flags & 0x1) != 0;
                bool hasIndex = (flags & 0x4) != 0;

                for (int sectionIndex = 0; sectionIndex < sectionsCount; sectionIndex++)
                {
                    if (sections[sectionIndex].TactKeyLookup != 0)
                    {
                        //Console.WriteLine("Detected db2 with encrypted section! HasKey {0}", CASC.HasKey(Sections[sectionIndex].TactKeyLookup));
                        continue;
                    }

                    reader.BaseStream.Position = sections[sectionIndex].FileOffset;

                    if (isSparse)
                    {
                        // sparse data with inlined strings
                        recordsData = reader.ReadBytes(sections[sectionIndex].SparseDataEndOffset - sections[sectionIndex].FileOffset);

                        if (reader.BaseStream.Position != sections[sectionIndex].SparseDataEndOffset)
                            throw new Exception("reader.BaseStream.Position != sections[sectionIndex].SparseDataEndOffset");
                    }
                    else
                    {
                        // records data
                        recordsData = reader.ReadBytes(sections[sectionIndex].NumRecords * RecordSize);

                        // string data
                        m_stringsTable = new Dictionary<long, string>();

                        long stringDataOffset = (RecordsCount - sections[sectionIndex].NumRecords) * RecordSize;

                        for (int i = 0; i < sections[sectionIndex].StringTableSize;)
                        {
                            long oldPos = reader.BaseStream.Position;

                            m_stringsTable[oldPos + stringDataOffset] = reader.ReadCString();

                            i += (int)(reader.BaseStream.Position - oldPos);
                        }
                    }

                    Array.Resize(ref recordsData, recordsData.Length + 8); // pad with extra zeros so we don't crash when reading

                    // index data
                    m_indexData = reader.ReadArray<int>(sections[sectionIndex].IndexDataSize / 4);

                    // duplicate rows data
                    Dictionary<int, int> copyData = new Dictionary<int, int>();

                    for (int i = 0; i < sections[sectionIndex].NumCopyRecords; i++)
                        copyData[reader.ReadInt32()] = reader.ReadInt32();

                    if (sections[sectionIndex].NumSparseRecords > 0)
                        sparseEntries = reader.ReadArray<SparseEntry>(sections[sectionIndex].NumSparseRecords);

                    // reference data
                    ReferenceData refData = null;

                    if (sections[sectionIndex].ParentLookupDataSize > 0)
                    {
                        refData = new ReferenceData
                        {
                            NumRecords = reader.ReadInt32(),
                            MinId = reader.ReadInt32(),
                            MaxId = reader.ReadInt32()
                        };

                        ReferenceEntry[] entries = reader.ReadArray<ReferenceEntry>(refData.NumRecords);
                        refData.Entries = entries.ToDictionary(e => e.Index, e => e.Id);
                    }

                    if (sections[sectionIndex].NumSparseRecords > 0)
                    {
                        // TODO: use this shit
                        int[] sparseIndexData = reader.ReadArray<int>(sections[sectionIndex].NumSparseRecords);

                        if (m_indexData.Length != sparseIndexData.Length)
                            throw new Exception("m_indexData.Length != sparseIndexData.Length");

                        m_indexData = sparseIndexData;
                    }

                    BitReader bitReader = new BitReader(recordsData);

                    if (sections[sectionIndex].NumSparseRecords > 0 && sections[sectionIndex].NumRecords != sections[sectionIndex].NumSparseRecords)
                        throw new Exception("sections[sectionIndex].NumSparseRecords > 0 && sections[sectionIndex].NumRecords != sections[sectionIndex].NumSparseRecords");

                    for (int i = 0; i < sections[sectionIndex].NumRecords; ++i)
                    {
                        bitReader.Position = 0;

                        if (isSparse)
                            bitReader.Offset = sparseEntries[i].Offset - sections[sectionIndex].FileOffset;
                        else
                            bitReader.Offset = i * RecordSize;

                        IDB2Row rec = new WDC3Row(this, bitReader, sections[sectionIndex].FileOffset, sections[sectionIndex].IndexDataSize != 0 ? m_indexData[i] : -1, isSparse);

                        if (sections[sectionIndex].IndexDataSize != 0)
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
    }
}
