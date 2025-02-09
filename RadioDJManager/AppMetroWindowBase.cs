using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Messaging;
using RadioDJManager.Messages;

namespace RadioDJManager
{
    public abstract class AppMetroWindowBase :
          MetroWindow, //ISubscriber<ShowUiNotificationMsg>,
          ISubscriber<CloseWindowMsg>, 
          ISubscriber<CloseFlyoutMsg>,
          ISubscriber<OpenFlyoutMsg>,
          ISubscriber<ConfirmationRequestMsg>,
          ISubscriber<ShowUiMessage>
    {
        protected IEventAggregator _EventAggregator { get; set; }

        public AppMetroWindowBase()
        {
            _EventAggregator = EventAggregator.Instance;
            _EventAggregator.Subscribe(this);

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            GlowBrush = Brushes.Blue;

        }


        /// <summary>
        /// Closes an open window
        /// </summary>
        /// <param name="msg"></param>
        public void HandleMessage(CloseWindowMsg msg)
        {
            if (msg.TargetWindowType == this.GetType())
                this.Close();
        }

        /// <summary>
        /// Closes an open flyout
        /// </summary>
        /// <param name="msg"></param>
        public void HandleMessage(CloseFlyoutMsg msg)
        {
            var flyout = FindChild<Flyout>(this, msg.Name);

            if (flyout != null)
                flyout.IsOpen = false;
        }

        /// <summary>
        /// Closes an open flyout
        /// </summary>
        /// <param name="msg"></param>
        public void HandleMessage(OpenFlyoutMsg msg)
        {
            var flyout = FindChild<Flyout>(this, msg.Name);

            if (flyout != null)
                flyout.IsOpen = true;
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
            where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        /// <summary>
        /// Handles confirmation Messages 
        /// </summary>
        /// <param name="msg"></param>
        public async void HandleMessage(ConfirmationRequestMsg msg)
        {
            //if (this.IsActive == true)
            {
                var result = await this.ShowMessageAsync("Confirmation Required", "Are You Sure?", MessageDialogStyle.AffirmativeAndNegative);

                var message = new ConfirmationResponseMsg() { Type = msg.Type };

                if (result == MessageDialogResult.Affirmative)
                {
                    message.IsConfirmed = true;
                }
                else if (result == MessageDialogResult.Negative)
                {
                    message.IsConfirmed = false;
                }

                _EventAggregator.Publish(message);
            }
        }

        public async void HandleMessage(ShowUiMessage msg)
        {
            await this.ShowMessageAsync(msg.Title, msg.Message, MessageDialogStyle.Affirmative);
        }
    }
}
