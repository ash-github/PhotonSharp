﻿using SharpLexer;

namespace Photon
{
    [Instruction(Cmd = Opcode.LOADK)]
    class CmdLoadK : Instruction
    {
        public override bool Execute( Command cmd)
        {            
            var c = vm.Exec.Constants.Get(cmd.DataA);

            vm.DataStack.Push(c);

            return true;
        }

        public override string Print( Command cmd)
        {            
            return string.Format("Const: {0}", cmd.DataA);
        }
    }

    [Instruction(Cmd = Opcode.LOADR)]
    class CmdLoadR : Instruction
    {
        public override bool Execute( Command cmd)
        {
            int regIndex = cmd.DataA + vm.RegBase;

            Value v = vm.LocalReg.Get(regIndex);

            vm.DataStack.Push(v);

            return true;  
        }

        public override string Print( Command cmd)
        {
            int regIndex = cmd.DataA + vm.RegBase;

            return string.Format("Reg: {0}", regIndex);
        }
    }

    [Instruction(Cmd = Opcode.SETR)]
    class CmdSetR : Instruction
    {
        public override bool Execute( Command cmd)
        {
            int regIndex = cmd.DataA + vm.RegBase;

            var d = vm.DataStack.Pop();

            vm.LocalReg.Set(regIndex, d);

            return true;
        }
        public override string Print( Command cmd)
        {
            int regIndex = cmd.DataA + vm.RegBase;

            return string.Format("Reg: {0}", regIndex);
        }

    }


    [Instruction(Cmd = Opcode.LOADG)]
    class CmdLoadG : Instruction
    {
        public override bool Execute( Command cmd)
        {

            Value v = vm.GetRuntimePackage(cmd.DataA).Reg.Get(cmd.DataB);

            vm.DataStack.Push(v);

            return true;
        }
        public override string Print( Command cmd)
        {
            return string.Format("Pkg: {0}  Reg: {1}", cmd.DataA, cmd.DataB);
        }
    }

    [Instruction(Cmd = Opcode.SETG)]
    class CmdSetG : Instruction
    {
        public override bool Execute( Command cmd)
        {            
            var d = vm.DataStack.Pop();

            vm.GetRuntimePackage(cmd.DataA).Reg.Set(cmd.DataB, d);            

            return true;
        }

        public override string Print( Command cmd)
        {
            return string.Format("Pkg: {0}  Reg: {1}", cmd.DataA, cmd.DataB);
        }
    }

    [Instruction(Cmd = Opcode.IDX)]
    class CmdIndex : Instruction
    {
        public override bool Execute( Command cmd)
        {
            var key = vm.DataStack.Pop();

            var main = vm.DataStack.Pop().CastObject();

            //var result = main.Get(key);

            //vm.DataStack.Push(result);
                        
            return true;
        }

        public override string Print( Command cmd)
        {
            return string.Empty;
        }
    }

    [Instruction(Cmd = Opcode.SEL)]
    class CmdSelect : Instruction
    {
        public override bool Execute( Command cmd)
        {
            var c = vm.DataStack.Pop().CastObject();

            vm.DataStack.Push(c.GetValue(cmd.DataA));

            return true;
        }

        public override string Print( Command cmd)
        {
            return string.Format("NameKey: {0}", cmd.DataA);
        }
    }


    [Instruction(Cmd = Opcode.LINKU)]
    class CmdLinkU : Instruction
    {
        public override bool Execute( Command cmd)
        {
            // 使用寄存器的地方和引用地方scope之差
            int mode = cmd.DataA;

            switch( mode )
            {
                    // 创建快照
                case 0:
                    {
                        int Index = cmd.DataB;

                        var closure = vm.DataStack.Get(-1).CastClosure();

                        closure.AddUpValue(vm.LocalReg, Index);
                    }
                    break;
                    // 直接引用
                case 1:
                    {
                        int Index = cmd.DataB;

                        var closure = vm.DataStack.Get(-1).CastClosure();

                        // 当前函数执行体也是个闭包
                        var v = vm.CurrFrame.Closure.GetUpValue(Index);
                        
                        closure.AddUpValue(v.Reg, v.Index);
                    }
                    break;
            }


            return true;
        }
        public override string Print( Command cmd)
        {
            return string.Format("Mode: {0} UpValue: {1}", cmd.DataA, cmd.DataB);
        }
    }


    [Instruction(Cmd = Opcode.LOADU)]
    class CmdLoadU : Instruction
    {
        public override bool Execute( Command cmd)
        {
            int regIndex =  cmd.DataA;

            var v = vm.CurrFrame.Closure.GetValue(regIndex);            

            vm.DataStack.Push(v);

            return true;
        }
        public override string Print( Command cmd)
        {
            return string.Format("UpValue: {0}", cmd.DataA);
        }
    }

    [Instruction(Cmd = Opcode.SETU)]
    class CmdSetU : Instruction
    {
        public override bool Execute( Command cmd)
        {
            
            var regIndex = cmd.DataA;

            var d = vm.DataStack.Pop();

            vm.CurrFrame.Closure.SetValue(regIndex, d );            

            return true;
        }

        public override string Print( Command cmd)
        {
            return string.Format("UpValue: {0}", cmd.DataA);
        }
    }

    [Instruction(Cmd = Opcode.LOADF)]
    class CmdLoadF : Instruction
    {
        public override bool Execute(Command cmd)
        {
            var proc = vm.Exec.GetFunc(cmd.DataA );

            vm.DataStack.Push(proc);

            return true;
        }

        public override string Print(Command cmd)
        {
            return string.Format("Proc: {0}", cmd.DataA);
        }
    }

    [Instruction(Cmd = Opcode.CLOSURE)]
    class CmdClosure : Instruction
    {
        public override bool Execute(Command cmd)
        {
            var proc = vm.Exec.GetFunc(cmd.DataA );

            vm.DataStack.Push(new ValueClosure(proc));

            return true;
        }

        public override string Print(Command cmd)
        {
            return string.Format("Proc: {0}", cmd.DataA);
        }
    }

    [Instruction(Cmd = Opcode.NEW)]
    class CmdNew : Instruction
    {
        public override bool Execute(Command cmd)
        {
            var ins = vm.Exec.GetClassType(cmd.DataA).CreateInstance();            

            vm.DataStack.Push(ins);

            return true;
        }

        public override string Print(Command cmd)
        {
            return string.Format("ClassNameKey : {0}", cmd.DataA);
        }
    }


    [Instruction(Cmd = Opcode.LOADM)]
    class CmdLoadM : Instruction
    {
        public override bool Execute(Command cmd)
        {
            var ci = vm.DataStack.Pop().CastObject();

            vm.DataStack.Push(ci.GetValue(cmd.DataA));

            return true;
        }

        public override string Print(Command cmd)
        {
            return string.Format("MemberKey: {0}", cmd.DataA);
        }
    }

    [Instruction(Cmd = Opcode.SETM)]
    class CmdSetM : Instruction
    {
        public override bool Execute(Command cmd)
        {
            var ci = vm.DataStack.Pop().CastObject();

            var v = vm.DataStack.Pop();

            ci.SetValue(cmd.DataA, v);

            return true;
        }

        public override string Print(Command cmd)
        {
            return string.Format("MemberKey: {0}", cmd.DataA);
        }
    }

}
