/*************************************************
   Copyright (c) 2021 Undersoft

   System.Uniques.Uniqueness.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Uniques
{
    using System.Collections;

    #region Enums

    public enum HashBits
    {
        /// <summary>
        /// Defines the bit64.
        /// </summary>
        bit64,

        /// <summary>
        /// Defines the bit32.
        /// </summary>
        bit32
    }

    #endregion

    #region Interfaces

    public interface IUniqueness
    {
        #region Methods

        public Byte[] Bytes(Byte[] obj, ulong seed = 0);

        public Byte[] Bytes(IList obj, ulong seed = 0);

        public Byte[] Bytes(IUnique obj);

        public Byte[] Bytes(Object obj, ulong seed = 0);

        public Byte[] Bytes(string obj, ulong seed = 0);

        public UInt64 Key(Byte[] obj, ulong seed = 0);

        public UInt64 Key(IList obj, ulong seed = 0);

        public UInt64 Key(IUnique obj);

        public UInt64 Key(IUnique obj, ulong seed);

        public UInt64 Key<V>(IUnique<V> obj);

        public UInt64 Key(Object obj, ulong seed = 0);

        public UInt64 Key(string obj, ulong seed = 0);

        #endregion
    }

    #endregion

    public class Unique32 : Uniqueness
    {
        #region Constructors

        public Unique32() : base(HashBits.bit32)
        {
        }

        #endregion

        #region Methods

        public override Byte[] Bytes(Byte[] obj, ulong seed = 0)
        {
            return obj.UniqueBytes32(seed);
        }

        public override Byte[] Bytes(IList obj, ulong seed = 0)
        {
            return obj.UniqueBytes32(seed);
        }

        public override unsafe Byte[] Bytes(IntPtr obj, int length, ulong seed = 0)
        {
            return ComputeBytes((byte*)obj.ToPointer(), length, seed);
        }

        public override Byte[] Bytes(IUnique obj)
        {
            return obj.GetBytes();
        }

        public override Byte[] Bytes(Object obj, ulong seed = 0)
        {
            return obj.UniqueBytes32(seed);
        }

        public override
                        Byte[] Bytes(string obj, ulong seed = 0)
        {
            return obj.UniqueBytes32(seed);
        }

        public override
            Byte[] Bytes(Type obj, ulong seed = 0)
        {
            return obj.UniqueBytes32(seed);
        }

        public override unsafe Byte[] ComputeBytes(byte* bytes, int length, ulong seed = 0)
        {
            return Hasher32.ComputeBytes(bytes, length, seed);
        }

        public override unsafe Byte[] ComputeBytes(byte[] bytes, ulong seed = 0)
        {
            return Hasher32.ComputeBytes(bytes, seed);
        }

        public override unsafe UInt64 ComputeKey(byte* bytes, int length, ulong seed = 0)
        {
            return Hasher32.ComputeKey(bytes, length, seed);
        }

        public override unsafe UInt64 ComputeKey(byte[] bytes, ulong seed = 0)
        {
            return Hasher32.ComputeKey(bytes, seed);
        }

        public override UInt64 Key(Byte[] obj, ulong seed = 0)
        {
            return obj.UniqueKey32(seed);
        }

        public override UInt64 Key(IList obj, ulong seed = 0)
        {
            return obj.UniqueKey32(seed);
        }

        public override unsafe UInt64 Key(IntPtr obj, int length, ulong seed = 0)
        {
            return ComputeKey((byte*)obj.ToPointer(), length, seed);
        }

        public override UInt64 Key(IUnique obj)
        {
            return obj.UniqueKey();
        }

        public override UInt64 Key(IUnique obj, ulong seed)
        {
            return Key(obj.GetBytes(), seed);
        }

        public override UInt64 Key<V>(IUnique<V> obj)
        {
            return obj.CompactKey();
        }

        public override UInt64 Key<V>(IUnique<V> obj, ulong seed)
        {
            return Key(obj.UniqueValues(), seed);
        }

        public override UInt64 Key(Object obj, ulong seed = 0)
        {
            return obj.UniqueKey32(seed);
        }

        public override UInt64 Key(string obj, ulong seed = 0)
        {
            return obj.UniqueKey32(seed);
        }

        public override UInt64 Key(Type obj, ulong seed = 0)
        {
            return obj.UniqueKey32(seed);
        }

        protected override unsafe Byte[] Bytes(byte* obj, int length, ulong seed = 0)
        {
            return ComputeBytes(obj, length, seed);
        }

        protected override unsafe UInt64 Key(byte* obj, int length, ulong seed = 0)
        {
            return ComputeKey(obj, length, seed);
        }

        #endregion
    }

    public class Unique64 : Uniqueness
    {
        #region Constructors

        public Unique64() : base(HashBits.bit64)
        {
        }

        #endregion

        #region Methods

        public override Byte[] Bytes(Byte[] obj, ulong seed = 0)
        {
            return obj.UniqueBytes64(seed);
        }

        public override Byte[] Bytes(IList obj, ulong seed = 0)
        {
            return obj.UniqueBytes64(seed);
        }

        public override unsafe Byte[] Bytes(IntPtr obj, int length, ulong seed = 0)
        {
            return ComputeBytes((byte*)obj.ToPointer(), length, seed);
        }

        public override Byte[] Bytes(IUnique obj)
        {
            return obj.GetBytes();
        }

        public override Byte[] Bytes(Object obj, ulong seed = 0)
        {
            return obj.UniqueBytes64(seed);
        }

        public override Byte[] Bytes(string obj, ulong seed = 0)
        {
            return obj.UniqueBytes64(seed);
        }

        public override Byte[] Bytes(Type obj, ulong seed = 0)
        {
            return obj.UniqueBytes64(seed);
        }

        public override unsafe Byte[] ComputeBytes(byte* bytes, int length, ulong seed = 0)
        {
            return Hasher64.ComputeBytes(bytes, length, seed);
        }

        public override Byte[] ComputeBytes(byte[] bytes, ulong seed = 0)
        {
            return Hasher64.ComputeBytes(bytes, seed);
        }

        public override unsafe UInt64 ComputeKey(byte* bytes, int length, ulong seed = 0)
        {
            return Hasher64.ComputeKey(bytes, length, seed);
        }

        public override UInt64 ComputeKey(byte[] bytes, ulong seed = 0)
        {
            return Hasher64.ComputeKey(bytes, seed);
        }

        public override UInt64 Key(Byte[] obj, ulong seed = 0)
        {
            return obj.UniqueKey64(seed);
        }

        public override UInt64 Key(IList obj, ulong seed = 0)
        {
            return obj.UniqueKey64(seed);
        }

        public override unsafe UInt64 Key(IntPtr obj, int length, ulong seed = 0)
        {
            return ComputeKey((byte*)obj.ToPointer(), length, seed);
        }

        public override UInt64 Key(IUnique obj)
        {
            return obj.UniqueKey;
        }

        public override UInt64 Key(IUnique obj, ulong seed)
        {
            return ComputeKey(obj.GetUniqueBytes(), seed);
        }

        public override UInt64 Key<V>(IUnique<V> obj)
        {
            return obj.CompactKey();
        }

        public override UInt64 Key<V>(IUnique<V> obj, ulong seed)
        {
            return Key(obj.UniqueValues(), seed);
        }

        public override UInt64 Key(Object obj, ulong seed = 0)
        {
            return obj.UniqueKey64(seed);
        }

        public override UInt64 Key(string obj, ulong seed = 0)
        {
            return obj.UniqueKey64(seed);
        }

        public override UInt64 Key(Type obj, ulong seed = 0)
        {
            return obj.UniqueKey64(seed);
        }

        protected override unsafe Byte[] Bytes(byte* obj, int length, ulong seed = 0)
        {
            return ComputeBytes(obj, length, seed);
        }

        protected override unsafe UInt64 Key(byte* obj, int length, ulong seed = 0)
        {
            return ComputeKey(obj, length, seed);
        }

        #endregion
    }

    public abstract class Uniqueness : IUniqueness
    {
        #region Fields

        protected Uniqueness unique;

        #endregion

        #region Constructors

        public Uniqueness()
        {
            unique = Unique.Bit64;
        }
        public Uniqueness(HashBits hashBits)
        {
            if(hashBits == HashBits.bit32)
                unique = Unique.Bit32;
            else
                unique = Unique.Bit64;
        }

        #endregion

        #region Methods

        public virtual Byte[] Bytes(Byte[] obj, ulong seed = 0)
        {
            return unique.Bytes(obj, seed);
        }

        public virtual Byte[] Bytes(IList obj, ulong seed = 0)
        {
            return unique.Bytes(obj, seed);
        }

        public virtual Byte[] Bytes(IntPtr obj, int length, ulong seed = 0)
        {
            return unique.Bytes(obj, length, seed);
        }

        public virtual Byte[] Bytes(IUnique obj)
        {
            return obj.GetBytes();
        }

        public virtual Byte[] Bytes(Object obj, ulong seed = 0)
        {
            return unique.Bytes(obj, seed);
        }

        public virtual Byte[] Bytes(string obj, ulong seed = 0)
        {
            return unique.Bytes(obj, seed);
        }

        public virtual Byte[] Bytes(Type obj, ulong seed = 0)
        {
            return unique.Bytes(obj, seed);
        }

        public virtual unsafe Byte[] ComputeBytes(byte* bytes, int length, ulong seed = 0)
        {
            return unique.ComputeBytes(bytes, length, seed);
        }

        public virtual unsafe Byte[] ComputeBytes(byte[] bytes, ulong seed = 0)
        {
            return unique.ComputeBytes(bytes, seed);
        }

        public virtual unsafe UInt64 ComputeKey(byte* bytes, int length, ulong seed = 0)
        {
            return unique.ComputeKey(bytes, length, seed);
        }

        public virtual unsafe UInt64 ComputeKey(byte[] bytes, ulong seed = 0)
        {
            return unique.ComputeKey(bytes, seed);
        }

        public virtual UInt64 Key(Byte[] obj, ulong seed = 0)
        {
            return unique.Key(obj, seed);
        }

        public virtual UInt64 Key(IList obj, ulong seed = 0)
        {
            return unique.Key(obj, seed);
        }

        public virtual UInt64 Key(IntPtr obj, int length, ulong seed = 0)
        {
            return unique.Key(obj, length, seed);
        }

        public virtual UInt64 Key(IUnique obj)
        {
            return obj.UniqueKey;
        }

        public virtual UInt64 Key(IUnique obj, ulong seed)
        {
            return unique.ComputeKey(obj.GetUniqueBytes(), seed);
        }

        public virtual UInt64 Key<V>(IUnique<V> obj)
        {
            return obj.CompactKey();
        }

        public virtual UInt64 Key<V>(IUnique<V> obj, ulong seed)
        {
            return unique.Key(obj.UniqueValues(), seed);
        }

        public virtual UInt64 Key(Object obj, ulong seed = 0)
        {
            return unique.Key(obj, seed);
        }

        public virtual UInt64 Key(string obj, ulong seed = 0)
        {
            return unique.Key(obj, seed);
        }

        public virtual UInt64 Key(Type obj, ulong seed = 0)
        {
            return unique.Key(obj, seed);
        }

        protected virtual unsafe Byte[] Bytes(byte* obj, int length, ulong seed = 0)
        {
            return unique.Bytes(obj, length, seed);
        }

        protected virtual unsafe UInt64 Key(byte* obj, int length, ulong seed = 0)
        {
            return unique.Key(obj, length, seed);
        }

        #endregion
    }
}
