using System.Diagnostics.CodeAnalysis;
using VendorMachine.Enum;

namespace VendorMachine.Models
{
    [ExcludeFromCodeCoverage]
    public class MessageModel
    {

        public MessageModel() { }

        public MessageModel(string text, MessageType messageType) { }

        public string Text { get; set; }

        public MessageType MessageType { get; set; }

    }
}