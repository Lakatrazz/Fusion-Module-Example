using System;

using UnityEngine;

using FusionModuleExample.Messages;

using LabFusion.Network;
using LabFusion.Representation;
using LabFusion.SDK.Modules;
using LabFusion.Utilities;

namespace FusionModuleExample
{
    public static class ExampleInfo
    {
        public const string Name = "TestModule"; // Name of the Module.  (MUST BE SET)
        public const string Version = "1.0.0"; // Version of the Module.  (MUST BE SET)
        public const string Author = "TestAuthor"; // Author of the Module.  (MUST BE SET)
        public const string Abbreviation = null; // Abbreviation of the Module. (Set as null if none)
        public const bool AutoRegister = true; // Should the Module auto register when the assembly is loaded?
        public const ConsoleColor Color = ConsoleColor.Magenta; // The color of the logged load info. (MUST BE SET)
    }

    public class TestModule : Module {
        public static TestModule Instance { get; private set; }

        public override void OnModuleLoaded() {
            Instance = this;

            LoggerInstance.Log("Hi! This is an example module!", ConsoleColor.Cyan);

            // Example method hooks
            MultiplayerHooking.OnUpdate += OnUpdate;
            MultiplayerHooking.OnStartServer += OnStartServer;
            MultiplayerHooking.OnPlayerJoin += OnPlayerJoin;
        }

        private void OnUpdate()
        {
            // Example message sending
            // Make sure we have an active server!
            if (NetworkInfo.HasServer) {
                // String message
                if (Input.GetKeyDown(KeyCode.U)) {
                    LoggerInstance.Log("Sending string message!", ConsoleColor.Green);

                    // Create the writer
                    using (var writer = FusionWriter.Create()) {
                        // Create the data
                        // For this you can simply write a string, but this shows the IFusionSerializable system
                        using (var data = BasicStringData.Create("Hi! This is a module message!")) {
                            // Write the data to the writer
                            writer.Write(data);

                            // Create the message and send it!
                            // FusionMessage.Create is for native message types, such as physics sync.
                            // ModuleCreate is for sending your own custom types.
                            using (var message = FusionMessage.ModuleCreate<BasicStringMessage>(writer))
                            {
                                // This will send it directly to the socket server
                                // You can choose how the message is sent (see MessageSender)
                                MessageSender.SendToServer(NetworkChannel.Reliable, message);
                            }
                        }
                    }
                }

                // Bool message
                if (Input.GetKeyDown(KeyCode.I))
                {
                    // Do it all again!
                    // Recommended to make static methods for sending messages you commonly use!

                    LoggerInstance.Log("Sending bool message!", ConsoleColor.Green);

                    using (var writer = FusionWriter.Create())
                    {
                        using (var data = BasicBoolData.Create(true))
                        {
                            writer.Write(data);

                            using (var message = FusionMessage.ModuleCreate<BasicBoolMessage>(writer))
                            {
                                MessageSender.SendToServer(NetworkChannel.Reliable, message);
                            }
                        }
                    }
                }
            }
        }

        private void OnStartServer() {
            LoggerInstance.Log("A server was started!", ConsoleColor.Cyan);
        }

        private void OnPlayerJoin(PlayerId id)
        {
            LoggerInstance.Log($"A player joined! Their small id is {id.SmallId}, their long id is {id.LongId}.", ConsoleColor.Cyan);
        }
    }
}
