using System.Reflection;

[assembly: AssemblyTitle("AC_CoRe_LZ")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Mach1ne C0de")]
[assembly: AssemblyProduct("AC_CoRe_LZ")]
[assembly: AssemblyCopyright("@Mach1ne_C0de_2026")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace AC_CoRe
{

    static class Version
    {
        static private string _sw_version = "1.0.0.1"; //Number of version Here!
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