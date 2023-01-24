using System.Reflection;

using FusionModuleExample;

using LabFusion.SDK.Modules;

[assembly: AssemblyProduct(ExampleInfo.Name)]
[assembly: AssemblyCopyright("Created by " + ExampleInfo.Author)]
[assembly: AssemblyVersion(ExampleInfo.Version)]
[assembly: AssemblyFileVersion(ExampleInfo.Version)]

// The assembly attribute that defines information for your module
// Without this, the module cannot be loaded!
// For this example, all of the info points to the ExampleInfo class
[assembly: ModuleInfo(typeof(TestModule), ExampleInfo.Name, ExampleInfo.Version, ExampleInfo.Author, ExampleInfo.Abbreviation, ExampleInfo.AutoRegister, ExampleInfo.Color)]