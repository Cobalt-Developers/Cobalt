using System;
using System.Reflection;

namespace TShockCobaltBridge.Helper
{
    public class DllHelper
    {
        public static void ApplyDllMagic()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }
        
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string asmName = new AssemblyName(args.Name).Name;

            if (asmName.Contains(".resources"))
                return null;

            foreach (Assembly an in AppDomain.CurrentDomain.GetAssemblies())
                if (an.GetName().Name == asmName)
                    return an;

            Console.WriteLine($"Missing dll: {asmName}");
            return null;
        }
    }
}