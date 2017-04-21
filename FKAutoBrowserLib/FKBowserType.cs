//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 需要使用的浏览器方式类型
    /// The browser type we are ready to use
    /// </summary>
    public enum ENUM_FKBowserType
    {
        eFKBrowserType_IEDriver_IE  = 0,
        eFKBrowserType_IEDriver_Firefox,
        eFKBrowserType_IEDriver_Chrome,
        eFKBrowserType_IEDriver_Safari,
        eFKBrowserType_System_IE,

        // todo: 之后添加例如携带版的Chrome,Gecko，或者系统Chrome，C++版IE等等

        eFKBrowserType_Max,
    }
}
