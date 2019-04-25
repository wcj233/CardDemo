using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace CardDemo
{
    public class ToastUtil
    {

        private ToastContent toastStyle(string title,string content) {
            ToastContent toastContent = new ToastContent()
            {
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children = {
                            new AdaptiveText(){
                                Text = "Alarm!"
                            },
                            new AdaptiveText(){
                                Text = title
                            },
                            new AdaptiveText(){
                                Text = content
                            }
                        }
                    }
                }
            };
            return toastContent;
        }
        public void removeToast(CardContent content) 
        {
            IReadOnlyList<ScheduledToastNotification> scheduled =
    ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications();
            foreach (ScheduledToastNotification notify in scheduled)
            {
                if (notify.Id == content.ToastId)
                {
                    ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(notify);
                }
            }
        }

        public string showToast(string timeStr,string title,string content) {
            DateTime dt = DateTime.Parse(timeStr);
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(dt - startTime).TotalMilliseconds / 1000;
            DateTime nowDateTime = DateTime.Now;
            long nowTimeStamp = (long)(nowDateTime - startTime).TotalMilliseconds / 1000;
            if (timeStamp <= nowTimeStamp)
            {
                return null;
            }

            //ToastNotification no = new ToastNotification(toastContent.GetXml());
            //ToastNotificationManager.CreateToastNotifier().Show(no);
            ScheduledToastNotification toast = new ScheduledToastNotification(toastStyle(title, content).GetXml(), DateTimeOffset.Now.AddSeconds(timeStamp - nowTimeStamp));
            toast.Id = nowTimeStamp.ToString();
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
            return toast.Id;
        }
    }
}
