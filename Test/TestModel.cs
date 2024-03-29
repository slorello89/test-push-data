using Redis.OM.Modeling;

namespace Test;

[Document]
public class TestModel
{
    [Indexed]
    public string Name { get; set; }
    [Indexed]
    public int Age { get; set; }
    public string MetaData { get; set; }
}