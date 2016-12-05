﻿using Photon;

namespace UnitTest
{
    class NativeClass
    {
        public string MyProp
        {
            get;
            set;
        }

        public string outAsRetValue(int a, string c, out int b)
        {
            b = 89;

            return "xx";
        }


        public string foo(int a)
        {
            return "cat";
        }

        public NativeClass()
        {
            MyProp = "HP";
        }

        // 手动额外绑定
        [NativeEntry(NativeEntryType.ClassMethod, "manualBinding")]
        public static int VMFoo(VMachine vm)
        {
            var instance = vm.DataStack.GetNativeInstance<NativeClass>(0);

            var a = vm.DataStack.GetInteger32(1);

            vm.DataStack.PushString("wa");

            return 1;
        }
    }


    partial class Program
    {
        static void TestNativeClass()
        {
            //WrapperCodeGenerator.GenerateClass(typeof(NativeClass), "UnitTest", "../UnitTest/NativeClassWrapper.cs");

            var testbox = new TestBox();
            testbox.Exe.RegisterNativeClass(typeof(NativeClassWrapper), "NativeClassTest");

            testbox.RunFile("NativeClass.pho")
                .TestGlobalRegEqualString(1, "cat")
                .TestGlobalRegEqualNumber(2, 89)
                .TestGlobalRegEqualString(3, "xx")
                .TestGlobalRegEqualString(4, "wa")
                .TestGlobalRegEqualString(5, "HP");
        }
    }
}
