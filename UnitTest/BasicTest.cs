﻿using Photon;

namespace UnitTest
{
    partial class Program
    {
        static void TestBasic()
        {
            new TestBox().RunFile("Math.pho")
                .CheckGlobalVarMatchValue("a", -1)
                .CheckGlobalVarMatchValue("b", (System.Int64)5)
                .CheckGlobalVarMatchValue("c", 3.0f)
                .CheckGlobalVarMatchValue("m", -1)
                .CheckGlobalVarMatchValue("s", "hello world");

            new TestBox().RunFile("SwapVar.pho")
                .CheckGlobalVarMatchValue("x", 2)
                .CheckGlobalVarMatchValue("y", 1);

            new TestBox().RunFile("Constant.pho")
                .CheckGlobalVarMatchValue("b", 22);                

        }
    }
}
