// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ez.Messenger
{
    /// <summary>
    /// Communication system through messages between objects that do not know each
    /// other the methods of the class to which it communicates
    /// </summary>
    public static class MessengerSender
    {
        private static readonly Dictionary<Type, DynamicMessengerRecipient> recipients = new Dictionary<Type, DynamicMessengerRecipient>();

        public static DynamicMessengerRecipient GetRecipient(this object receiver)
        {
            Type type = receiver.GetType();
            if (recipients.ContainsKey(type))
            {
                return recipients[type];
            }
            else
            {
                DynamicMessengerRecipient output = new DynamicMessengerRecipient(type);
                recipients.Add(type, output);
                return output;
            }
        }

        /// <summary>
        /// Sende messenger event named eventMethodName to receiver.
        /// </summary>
        /// <param name="receiver">Receiver of messenger</param>
        /// <param name="eventMethodName">Event message receiving method</param>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">Event arguments</param>
        public static void SendMessenger(this object receiver, string eventMethodName, object sender, EventArgs eventArgs)
        {
            if (receiver is null)
            {
                throw new ArgumentNullException(nameof(receiver));
            }

            DynamicMessengerRecipient recipient = receiver.GetRecipient();
            recipient.SendMessenger(receiver, eventMethodName, sender, eventArgs);
        }

        /// <summary>
        /// Get delegate of event method
        /// </summary>
        /// <param name="target">Target of delegate</param>
        /// <param name="methodName">Method name</param>
        /// <returns>Delegate of method in target</returns>
        public static Delegate GetDelegate(object target, string methodName)
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (String.IsNullOrWhiteSpace(methodName) || methodName.Contains(" "))
            {
                throw new ArgumentException("Argument cannot be null, empty, or whitespace, nor can it contain whitespace.", nameof(methodName));
            }

            DynamicMessengerRecipient recipient = target.GetRecipient();
            return recipient.GetDelegateEvent(target, methodName);
        }
    }
}
