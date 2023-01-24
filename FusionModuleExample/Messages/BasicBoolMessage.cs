using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LabFusion.Data;
using LabFusion.Network;

namespace FusionModuleExample.Messages {
    public class BasicBoolData : IFusionSerializable, IDisposable
    {
        public bool value;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Serialize(FusionWriter writer)
        {
            writer.Write(value);
        }

        public void Deserialize(FusionReader reader)
        {
            value = reader.ReadBoolean();
        }

        public static BasicBoolData Create(bool value)
        {
            return new BasicBoolData()
            {
                value = value,
            };
        }
    }

    public class BasicBoolMessage : ModuleMessageHandler {
        public override void HandleMessage(byte[] bytes, bool isServerHandled = false) {
            using (var reader = FusionReader.Create(bytes)) {
                using (var data = reader.ReadFusionSerializable<BasicBoolData>())
                {
                    // If this is handled by the socket server, and we are running the server, bounce it to all clients
                    // You can choose this way or other ways, but the behaviour is up to you!
                    if (NetworkInfo.IsServer && isServerHandled)
                    {
                        using (var message = FusionMessage.ModuleCreate<BasicBoolMessage>(bytes))
                        {
                            MessageSender.BroadcastMessage(NetworkChannel.Reliable, message);
                        }
                    }
                    // Otherwise, we handle the message
                    else {
                        var info = data.value;

                        TestModule.Instance.LoggerInstance.Log($"Basic Bool Message: {info}", ConsoleColor.Cyan);
                    }
                }
            }
        }
    }
}
