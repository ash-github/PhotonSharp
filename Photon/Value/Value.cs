﻿using MarkSerializer;

namespace Photon
{
    public enum ValueKind
    {
        Nil = 0,
        Float32,
        Float64,
        Integer32,
        Integer64,
        Bool,
        String,
        Func,
        ArrayIterator,
        MapIterator,
        ClassType,
        ClassInstance,
        NativeClassType,
        NativeClassInstance,
    }

    internal interface IContainer
    {
        void SetKeyValue(Value k, Value v);

        Value GetKeyValue(Value k);

        int GetCount();

        ValueIterator GetIterator( );
    }

    class Value : IMarkSerializable
    {     
        internal static Value Nil = new ValueNil();

        public override string ToString()
        {
            return DebugString();
        }

        public virtual void Serialize(BinarySerializer ser)
        {
            
        }
        public virtual bool Visit(Value iter, DataStack ds)
        {
            return false;
        }

        public virtual string DebugString( )
        {
            throw new RuntimeException("NotImplementToString");            
        }

        public virtual ValueKind Kind
        {
            get { return ValueKind.Nil; }
        }

        public virtual string TypeName
        {
            get { return string.Empty; }
        }

        internal virtual object Raw
        {
            get { throw new System.NotImplementedException(); }
        }

        internal virtual Value OperateBinary(Opcode code, Value other)
        {
            throw new RuntimeException("Expect 'OperateBinary' operand");
        }

        internal virtual Value OperateUnary(Opcode code)
        {
            throw new RuntimeException("Expect 'OperateUnary' operand");
        }

        // 动态获取
        internal virtual void OperateSetKeyValue(Value k, Value v)
        {
            throw new RuntimeException("Expect 'OperateSetKeyValue' operand");
        }

        internal virtual Value OperateGetKeyValue(Value k)
        {
            throw new RuntimeException("Expect 'OperateGetKeyValue' operand");
        }

        // 编译期确认的成员函数和变量
        internal virtual void OperateSetMemberValue(int nameKey, Value v)
        {
            throw new RuntimeException("Expect 'OperateSetMemberValue' operand");
        }

        internal virtual Value OperateGetMemberValue(int nameKey)
        {
            throw new RuntimeException("Expect 'OperateGetMemberValue' operand");
        }
    }



}
