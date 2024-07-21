using ConsoleApp.Tasks;

namespace ConsoleApp.Factory
{
    public abstract class Creator
    {
        public abstract IBackendTask FactoryMethod(bool isUseLogger);
    }
}
