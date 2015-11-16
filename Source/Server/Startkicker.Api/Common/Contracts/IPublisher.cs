namespace Startkicker.Api.Common.Contracts
{
    public interface IPublisher
    {
        void Emit(string channel, string message);
    }
}