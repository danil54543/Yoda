namespace Yoda.Domain.ViewModel.Error
{
	public class ErrorViewModel
	{
		public string? RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
		public string Responce { get; set; }
	}
}
