using TestProject.StaticData;

namespace TestProject.Services.StaticData
{
    public interface IStaticDataProvider
    {
        FieldStaticData FieldData { get; }
    }
}