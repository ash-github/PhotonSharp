﻿using System;

namespace Photon
{
    class ValueObject : Value
    {
        internal override bool Equal(Value other)
        {
            throw new NotImplementedException();
        }

        internal virtual void SetMember( int nameKey, Value v )
        {

        }

        internal virtual Value GetMember(int nameKey)
        {
            return Value.Nil;
        }

        internal virtual object Instance
        {
            get { return null; }
        }


    }

}
