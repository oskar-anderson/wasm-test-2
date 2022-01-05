namespace Domain.Model.Api
{
	public class ValidGameSettingsOut
	{
		public bool AreSettingsValid { get; set; }
		public string ErrorMessage { get; set; } = "";
	}
}
