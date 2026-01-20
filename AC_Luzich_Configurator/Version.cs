using System.Reflection;

[assembly: AssemblyTitle("AC_Configurator_STDL")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Mach1ne C0de")]
[assembly: AssemblyProduct("Mach1ne C0de")]
[assembly: AssemblyCopyright("Copyright © 2026")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("0.0.9.0")]
[assembly: AssemblyFileVersion("0.0.9.0")]


namespace AC_Configurator_STDL
{
    public static class Version
    {
        static private string _sw_version = "0.0.9.1";  //Number of version Here!
        static private string _sw_title = "AC Configurator";
        //static private string _sw_title_server = "Puma CoRe Server ";


        public static string set()
        {
            return sw_title() + " v" + sw_version();
        }


        static public string sw_version()
        {

            return _sw_version;

        }

        static public string sw_title()
        {
            return _sw_title;

        }
    }
}