﻿using System;

namespace Photon.Model
{    
    public class ValueDelegate : Value
    {
        public Func<Photon.VM.VMachine, int> Entry;        

        public override bool Equal(Value other)
        {
            var otherT = other as ValueDelegate;
            if (otherT == null)
                return false;

            return otherT.Entry == Entry;
        }


        public override string ToString()
        {
            return "(delegate)";
        }
    }

 
}
