namespace Domain.Model.Api
{
	public class ValidGameSettingsInRuleSet
	{
		public string Ships { get; set; } = "";
		public int BoardWidth { get; set; }
		public int BoardHeight { get; set; }
		public int AllowedPlacementType { get; set; }
	}
}
