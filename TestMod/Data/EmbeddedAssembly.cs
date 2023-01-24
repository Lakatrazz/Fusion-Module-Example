using System.Linq;
using System.Reflection;
using System.IO;

namespace TestMod.Data
{
    /// <summary>
    /// Example class for loading a module as an embedded resource.
    /// </summary>
    public static class EmbeddedAssembly {
        // Used to load the bytes of the embedded resource.
        private static byte[] Internal_LoadFromAssembly(Assembly assembly, string name) {
            string[] manifestResources = assembly.GetManifestResourceNames();

            if (manifestResources.Contains(name))
            {
                using (Stream str = assembly.GetManifestResourceStream(name))
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    str.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }

            return null;
        }

        /// <summary>
        /// <para> Loads an embedded assembly into the application. </para>
        /// <para> If this is a Fusion module, it will automatically be initialized. </para>
        /// </summary>
        /// <param name="name"></param>
        public static void LoadAssembly(string name) {
            var bytes = Internal_LoadFromAssembly(Assembly.GetExecutingAssembly(), name);
            Assembly.Load(bytes);
        }
    }
}
