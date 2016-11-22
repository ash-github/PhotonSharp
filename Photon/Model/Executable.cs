﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Photon
{

    public class Executable
    {


        List<Package> _packages = new List<Package>();
       
        // 外部代理函数
        Dictionary<string, ValueDelegate> _delegateByName = new Dictionary<string, ValueDelegate>();

        public void DebugPrint( )
        {
            // 源文件
            //_sourcefile.DebugPrint();

            // 语法树
            foreach (var pkg in _packages)
            {
                pkg.DebugPrint();
            }

            // 符号
            //Debug.WriteLine("symbols:");
            //_globalScope.DebugPrint("");
            //Debug.WriteLine("");

            foreach( var pkg in _packages )
            {
                // 汇编
               // pkg.DebugPrint(_sourcefile);

            }

            

            Debug.WriteLine("");
        }
        internal Package AddPackage( string name, Scope top )
        {
            var pkg = new Package( _packages.Count,  name, this, top);

            _packages.Add(pkg);

            return pkg;

        }
        internal Package GetPackage(int pkgid)
        {
            return _packages[pkgid];
        }

        internal Package GetPackageByName(string name)
        {
            foreach( var pkg in _packages )
            {
                if ( pkg.Name == name )
                {
                    return pkg;
                }
            }

            return null;
        }

        internal int PackageCount
        {
            get { return _packages.Count; }
        }

        internal Procedure GetProcedure( int packageID, int cmdSetID )
        {            
            var pkg = _packages[packageID];
            if (pkg == null)
                return null;

            return pkg.GetProcedure(cmdSetID);
        }

        internal bool FindCmdSetInPackage(string name, out Procedure outP, out Package outPkg)
        {
            outP = null;
            outPkg = null;

            foreach( var pkg in _packages )
            {
                outP = pkg.FindProcedureByName(name);
                if (outP != null)
                {
                    outPkg = pkg;
                    return true;
                }
            }
            

            return false;
        }

        internal void AddDelegate( string name, ValueDelegate d )
        {
            _delegateByName.Add(name, d);
        }

        public void RegisterDelegate(string name, Func<VMachine, int> callback)
        {
            ValueDelegate v = null;
            if (!_delegateByName.TryGetValue(name, out v))
            {
                Debug.WriteLine("extern func not define in code: " + name);
                return;
            }

            v.Entry = callback;
        }
    }
}
