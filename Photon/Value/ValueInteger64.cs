﻿using System;

namespace Photon
{
    class ValueInteger64 : Value
    {
        Int64 _data = 0;

        public ValueInteger64(Int64 data)
        {
            _data = data;
        }

        public Int64 RawValue
        {
            get { return _data; }
        }

        internal override object Raw
        {
            get { return _data; }
        }

        public override bool Equals(object other)
        {
            var otherT = other as ValueInteger64;
            if (otherT == null)
                return false;

            return otherT._data == _data;
        }
        public override string DebugString()
        {
            return _data.ToString() + " (int64)";
        }

        public override ValueKind Kind
        {
            get { return ValueKind.Integer64; }
        }

        internal override Value BinaryOperate(Opcode code, Value other )
        {
            var a = RawValue;
            Int64 b;

            // 类型匹配
            switch (other.Kind)
            {
                case ValueKind.Integer32:
                    return new ValueInteger32((Int32)this.RawValue).BinaryOperate(code, other);
                case ValueKind.Integer64:
                    b = (other as ValueInteger64).RawValue;
                    break;
                case ValueKind.Float32:
                    return new ValueFloat32((float)this.RawValue).BinaryOperate(code, other);
                case ValueKind.Float64:
                    return new ValueFloat64((double)this.RawValue).BinaryOperate(code, other);
                default:
                    throw new RuntimeException("Binary operator value type not match:" + other.ToString());
            }

            switch (code)
            {
                case Opcode.ADD:
                    return new ValueInteger64(a + b);
                case Opcode.SUB:
                    return new ValueInteger64(a - b);
                case Opcode.MUL:
                    return new ValueInteger64(a * b);
                case Opcode.DIV:
                    return new ValueInteger64(a / b);
                case Opcode.GT:
                    return new ValueBool(a > b);
                case Opcode.GE:
                    return new ValueBool(a >= b);                    
                case Opcode.LT:
                    return new ValueBool(a < b);                    
                case Opcode.LE:
                    return new ValueBool(a <= b);                    
                case Opcode.EQ:
                    return new ValueBool(a == b);                    
                case Opcode.NE:
                    return new ValueBool(a != b);                    
                default:
                    throw new RuntimeException("Unknown binary operator:"+ code.ToString() );
            }        
        }

        internal override Value UnaryOperate(Opcode code)
        {
            var a = RawValue;

            switch (code)
            {
                case Opcode.MINUS:
                    return new ValueInteger64(-a);
                case Opcode.INT32:
                    return new ValueInteger32((Int32)a);
                case Opcode.INT64:
                    return this;
                case Opcode.FLOAT32:
                    return new ValueFloat32((float)a);
                case Opcode.FLOAT64:
                    return new ValueFloat64((double)a);
                default:
                    throw new RuntimeException("Unknown unary operator:" + code.ToString());
            }
        }
    }



 
}
