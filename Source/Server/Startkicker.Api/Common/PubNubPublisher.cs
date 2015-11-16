namespace Startkicker.Api.Common
{
    using System.Net;
    using System.Web.Script.Serialization;

    using Startkicker.Api.Common.Contracts;

    public class PubNubNotifier : IPublisher
    {
        public const string PublishKey = "pub-c-2dab3a26-3c7f-4957-8099-d8eb0354d373";
        public const string SubscribeKey = "sub-c-0d5d244e-8b92-11e5-83e3-02ee2ddab7fe";
        private const string PublishRequestFormat = "http://pubsub.pubnub.com/publish/{0}/{1}/0/{2}/0/{3}";
        
        public void Emit(string channel, string message)
        {
            var serializer = new JavaScriptSerializer();
            var url = string.Format(PublishRequestFormat, PublishKey, SubscribeKey, channel, serializer.Serialize(message));

            HttpWebRequest.Create(url).GetResponse();
        }
    }
}