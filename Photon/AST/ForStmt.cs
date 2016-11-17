﻿
using SharpLexer;
using System.Collections.Generic;

namespace Photon
{
    public class ForStmt : Stmt
    {
        public Stmt Init;

        public Expr Condition;

        public Stmt Post;

        public BlockStmt Body;

        public TokenPos ForPos;

        public ForStmt(Stmt init, Expr con, Stmt post, BlockStmt body, TokenPos forpos)
        {
            Condition = con;
            Body = body;
            Init = init;
            Post = post;

            BuildRelation();
        }


        public override string ToString()
        {
            return "ForStmt";
        }

        public override IEnumerable<Node> Child()
        {
            if (Init != null)
            {
                yield return Init;
            }

            yield return Condition;

            if (Post != null)
            {
                yield return Post;
            }

            yield return Body;
        }

        public override void Compile(Executable exe, CommandSet cm, bool lhs)
        {
            Init.Compile(exe, cm, false);            

            var loopStart = cm.CurrGenIndex;

            Condition.Compile(exe, cm, false);           

            var jnzCmd = cm.Add(new Command(Opcode.Jz, 0))
                .SetCodePos(ForPos);

            Body.Compile(exe, cm, false);

            Post.Compile(exe, cm, false);

            cm.Add(new Command(Opcode.Jmp, loopStart))
                .SetCodePos(ForPos);

            // false body跳入
            jnzCmd.DataA = cm.CurrGenIndex;
        }

    }
}
