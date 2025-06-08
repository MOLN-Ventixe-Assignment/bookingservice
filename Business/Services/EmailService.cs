using Azure;
using Azure.Communication.Email;

namespace Business.Services;

//This Class is a first draft in using an email service in Azure to send a Verification Email 
//To Do: Next step is to create an EmailService microservice and publish it to Azure. That service will then be called on to send the e-mail.

public class EmailService
{

    public static async void SendEmail(string emailAddress, string eventId, int ticketQuantity)
    {
        string connectionString = "endpoint=https://ventixe-cs.europe.communication.azure.com/;accesskey=67k1hKvSST5SwE72Ems7gShQ0bwuCRlVSRDkQmQbiFDPi7qPgjtJJQQJ99BFACULyCpq7IbPAAAAAZCS5Yzp";
        var emailClient = new EmailClient(connectionString);

        var message = "This is a verification that you have booked " + ticketQuantity + " tickets to the event " + eventId + ". Thank you for your booking!";

        var emailMessage = new EmailMessage(
            senderAddress: "DoNotReply@2f1ea1ae-b731-4085-90bf-19ea06d195fc.azurecomm.net",
            content: new EmailContent("Booking Verification")
            {
                PlainText = message,
                Html = $@"
		        <html>
			        <body>
				        <h3>{message}</h3>
			        </body>
		        </html>"
            },
             recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(emailAddress) })
        );
        
        //EmailSendOperation emailSendOperation = emailClient.Send(
        //    WaitUntil.Completed,
        //    emailMessage);

        var result = await emailClient.SendAsync(WaitUntil.Started, emailMessage);
    }
}