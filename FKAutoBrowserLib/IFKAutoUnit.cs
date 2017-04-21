//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    public interface IFKAutoUnit
    {
        void RunScript(string strFile, Delegate preActionDelegate, Delegate actionResultDelegate);
    }
}
