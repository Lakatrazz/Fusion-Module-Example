using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LabFusion.Data;
using LabFusion.Network;

namespace FusionModuleExample.Messages {
    public class BasicStringData : IFusionSerializable, IDisposable
    {
        public string message;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Serialize(FusionWriter writer)
        {
            writer.Write(message);
        }

        public void Deserialize(FusionReader reader)
        {
            message = reader.ReadString();
        }

        public static BasicStringData Create(string message)
        {
            return new BasicStringData()
            {
                message = message,
            };
        }
    }

    public class BasicStringMessage : ModuleMessageHandler {
        public override void HandleMessage(byte[] bytes, bool isServerHandled = false) {
            using (var reader = FusionReader.Create(bytes)) {
                using (var data = reader.ReadFusionSerializable<BasicStringData>())
                {
                    // If this is handled by the socket server, and we are running the server, bounce it to all clients
                    // You can choose this way or other ways, but the behaviour is up to you!
                    if (NetworkInfo.IsServer && isServerHandled)
                    {
                        using (var message = FusionMessage.ModuleCreate<BasicStringMessage>(bytes))
                        {
                            MessageSender.BroadcastMessage(NetworkChannel.Reliable, message);
                        }
                    }
                    // Otherwise, we handle the message
                    else {
                        var info = data.message;

                        TestModule.Instance.LoggerInstance.Log($"Basic String Message: {info}", ConsoleColor.Cyan);
                    }
                }
            }
        }
    }
}
