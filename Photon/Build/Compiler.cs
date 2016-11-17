﻿using System.Diagnostics;
using System;
using SharpLexer;

namespace Photon
{   
    public class Compiler
    {
        public static Executable Compile(SourceFile file)
        {            
            var parser = new CodeParser();

            // 编译生成AST
            var chunk = parser.ParseSource(file.Source);
            
            var exe = new Executable(chunk, parser.GlobalScope, file );

            var cmdSet = new CommandSet("global", TokenPos.Init, parser.GlobalScope.CalcUsedReg(), true);

            exe.AddCmdSet(cmdSet);

            // 遍历AST,生成代码
            chunk.Compile(exe, cmdSet, false);

            cmdSet.Add(new Command(Opcode.Exit));

            exe.RegisterDelegate("array", (vm) =>
            {
                vm.DataStack.Push(new ValueArray());

                return 1;
            });


            return exe;
        }

    }
}