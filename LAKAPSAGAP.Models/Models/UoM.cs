namespace LAKAPSAGAP.Models.Models;

[Table("UoM")]
public class UoM : CommonModel
{
    public string Name { get; set; }
    public string Symbol { get; set; }
}
