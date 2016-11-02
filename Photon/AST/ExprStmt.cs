﻿using Photon.OpCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.AST
{
    public class ExprStmt : Stmt
    {
        public List<Expr> X;
        public ExprStmt(List<Expr> x)
        {
            X = x;
        }

        public override IEnumerable<Node> Child()
        {
            foreach (var e in X)
            {
                yield return e;
            }
        }

        public override string ToString()
        {
            return "ExprStmt";
        }

        public override void Compile(Executable exe, CommandSet cm, bool lhs)
        {

            foreach (var b in X)
            {
                b.Compile(exe, cm, false);
            }
        }
    }
}