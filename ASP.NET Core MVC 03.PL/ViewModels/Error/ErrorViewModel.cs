namespace ASP.NET_Core_MVC_03.PL.ViewModels.Error
{
	public class ErrorViewModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
