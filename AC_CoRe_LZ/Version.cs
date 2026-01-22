using System.Reflection;

[assembly: AssemblyTitle("AC CoRe")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Mach1ne C0de")]
[assembly: AssemblyProduct("AC CoRe")]
[assembly: AssemblyCopyright("Mach1ne C0de")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace AC_CoRe
{

    static class Version
    {
        static private string _sw_version = "0.9.0.0"; //Number of version Here!
        static private string _sw_title = "AC CoRe LZ ";

        static public string sw_version()
        {

            return _sw_version;

        }

        static public string sw_title()
        {
            return _sw_title;

        }


        public static string set()
        {
            return sw_title() + " v" + sw_version();
        }





    }

}