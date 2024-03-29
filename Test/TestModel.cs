using Redis.OM.Modeling;

namespace Test;

[Document]
public class TestModel
{
    [Indexed(Sortable = true)]
    public string Name { get; set; }
    [Indexed(Sortable = true)]
    public int Age { get; set; }
    [Searchable]
    public string MetaData { get; set; }
}