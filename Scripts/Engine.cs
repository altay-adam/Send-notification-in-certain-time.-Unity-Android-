using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    [SerializeField] InputField HourField = default;
    [SerializeField] Button SaveButton = default;
    public int identifier = default;
    public AndroidNotification notification = default;
    public AndroidNotificationChannel defaultNotificationChannel = default;
    public string QuitTime = default;
    public int difference = default;

    public void Start()
    {
        defaultNotificationChannel = new AndroidNotificationChannel() //Create the notification channel in Start method.
        {
            Id = "default_channel",
            Name = "default name",
            Description = "For generic notification",
            Importance = Importance.Default,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel); // Register the notification channel that you created when the application starts.
        AndroidNotificationCenter.CancelAllNotifications(); //The notification removes after clicking.
    }
    public void SaveClicked()
    {
        int input = int.Parse(HourField.text);    //We have the hour to determine notification time that we want. We enter the hour in inputfield.
        Application.Quit();
        QuitTime = System.DateTime.Now.ToString("HH"); //We save the time when we quit the app.
        Debug.Log(QuitTime);
        int qTime = int.Parse(QuitTime);
        if (qTime >= 0 && qTime <= input)       //Here, we calculate the difference between our quit time and our determined time.
        {
            difference = (input - qTime) + 24;
        }
        else if (qTime > input && qTime < 24)
        {
            difference = 24 - (qTime - input);
        }
        AndroidNotification notification = new AndroidNotification() //Here, we create our notification.
        {
            Title = "Play again :)",
            Text = "Did you miss me?",
            SmallIcon = "default",
            LargeIcon = "defualt",
            FireTime = System.DateTime.Now.AddHours(difference), //We determine FireTime as difference.
        };
        Debug.Log(difference);
        identifier = AndroidNotificationCenter.SendNotification(notification, "default_channel"); 

    }
}
