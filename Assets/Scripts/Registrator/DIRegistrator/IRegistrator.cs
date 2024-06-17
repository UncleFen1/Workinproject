namespace Registrator
{
    public interface IRegistrator
    {
        void SetData(Construction registrator);
        Construction SetObjectHash(int hash);
        Construction[] SetList();
    }
}