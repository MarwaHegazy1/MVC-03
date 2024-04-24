using System.Threading.Tasks;

namespace ASP.NET_Core_MVC_03.PL.Services.EmailSender
{
	public interface  IEmailSender
	{
		Task SendAsync(string from, string recipients, string subject, string body);
	}
}
