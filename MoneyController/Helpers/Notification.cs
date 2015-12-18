namespace MoneyController.Helpers
{
    using System;
    using Windows.Data.Xml.Dom;
    using Windows.UI.Notifications;

    public class Notification
    {
        public static void ShowNotification(string notificationMessage)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode("Money Controller"));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(notificationMessage));
            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("duration", "short");
            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(6);

            toast.SuppressPopup = false;
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
