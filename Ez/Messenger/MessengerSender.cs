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
    /// Describes a dynamic messaging communication system between objects.
    /// </summary>
    public static class MessengerSender
    {
        private static readonly Dictionary<Type, DynamicMessengerRecipient> recipients = new Dictionary<Type, DynamicMessengerRecipient>();

        /// <summary>
        /// Gets a <see cref="DynamicMessengerRecipient"/> associated with a specified type.
        /// </summary>
        /// <param name="receiverType">The type to locate.</param>
        /// <returns>A <see cref="DynamicMessengerRecipient"/> that is associated with the <paramref name="receiverType"/> type.</returns>
        public static DynamicMessengerRecipient GetRecipient(this Type receiverType)
        {
            if (recipients.ContainsKey(receiverType))
            {
                return recipients[receiverType];
            }
            else
            {
                DynamicMessengerRecipient output = new DynamicMessengerRecipient(receiverType);
                recipients.Add(receiverType, output);
                return output;
            }
        }

        /// <summary>
        /// Gets a <see cref="DynamicMessengerRecipient"/> associated with a specified type.
        /// </summary>
        /// <typeparam name="T">The type to locate.</typeparam>
        /// <returns>A <see cref="DynamicMessengerRecipient"/> that is associated with the <typeparamref name="T"/> type.</returns>
        public static DynamicMessengerRecipient GetRecipient<T>() => typeof(T).GetRecipient();

        /// <summary>
        /// Gets a <see cref="DynamicMessengerRecipient"/> associated with a specified type of the <paramref name="receiver"/>.
        /// </summary>
        /// <param name="receiver">A object to locate a compatible <see cref="DynamicMessengerRecipient"/>.</param>
        /// <returns>A <see cref="DynamicMessengerRecipient"/> that is associated with the type of <paramref name="receiver"/>.</returns>
        public static DynamicMessengerRecipient GetRecipient(this object receiver) =>
            receiver.GetType().GetRecipient();

        /// <summary>
        /// Sends messenger event named <paramref name="eventMethodName"/> to receiver.
        /// </summary>
        /// <param name="receiver">Receiver of messenger</param>
        /// <param name="eventMethodName">Event message receiving method</param>
        /// <param name="sender">The sender argument of event method.</param>
        /// <param name="eventArgs">The e argument of event method.</param>
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

            if (string.IsNullOrWhiteSpace(methodName) || methodName.Contains(" "))
            {
                throw new ArgumentException("Argument cannot be null, empty, or whitespace, nor can it contain whitespace.", nameof(methodName));
            }

            DynamicMessengerRecipient recipient = target.GetRecipient();
            return recipient.GetDelegateEvent(target, methodName);
        }
    }
}
