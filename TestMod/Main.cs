using MelonLoader;

using TestMod.Data;

namespace TestMod {
    public static class BuildInfo {
        public const string Name = "TestMod"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "Mod for Testing"; // Description for the Mod.  (Set as null if none)
        public const string Author = "TestAuthor"; // Author of the Mod.  (MUST BE SET)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class TestMod : MelonMod {
        // The embedded resource path of the module.
        public const string ModuleResource = "TestMod.dependencies.FusionModuleExample.dll";

        public override void OnInitializeMelon() {
            // Load the module so that it can be recognized by Fusion
            // Unless autoRegister is disabled, this will automatically be registered
            // If Fusion is not installed, this won't cause any issues
            EmbeddedAssembly.LoadAssembly(ModuleResource);
        }
    }
}
