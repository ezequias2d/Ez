// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ez.Messenger
{
    /// <summary>
    /// Parses class and traces methods compatible with EventHandler or pre configured events in the dictionary
    /// </summary>
    public class DynamicMessengerRecipient
    {
        private static IDictionary<string, Type> EventArgsTypes { get; }

        static DynamicMessengerRecipient()
        {
            EventArgsTypes = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Add delegate type to dicionary of compatible event
        /// </summary>
        /// <param name="eventMethodName">Event name(example OnUpdate)</param>
        /// <param name="eventHandler">Event delegate</param>
        public static void AddReceiverHandler(string eventMethodName, Type eventHandler)
        {
            EventArgsTypes.Add(eventMethodName, eventHandler);
        }

        /// <summary>
        /// Class type traced in this instance
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Dicionary of method name and method
        /// </summary>
        private readonly Dictionary<string, MethodInfo> events;

        /// <summary>
        /// Create new instance that parses the specified type
        /// </summary>
        /// <param name="type">Class type to parse</param>
        public DynamicMessengerRecipient(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));

            events = new Dictionary<string, MethodInfo>();

            MethodInfo[] methods = FilterEventsMethods(type);

            foreach (MethodInfo method in methods)
            {
                string name = method.Name;
                events.Add(name, method);
            }
        }

        /// <summary>
        /// Get delegate of event method in target instance
        /// </summary>
        /// <param name="target">Target instance</param>
        /// <param name="methodName">Event method name</param>
        /// <returns></returns>
        public Delegate GetDelegateEvent(object target, string methodName)
        {
            if (target != null && Type.Equals(target.GetType()) && events.ContainsKey(methodName))
            {
                if (EventArgsTypes.ContainsKey(methodName))
                {
                    return Delegate.CreateDelegate(EventArgsTypes[methodName], target, events[methodName]);
                }
                else
                {
                    return Delegate.CreateDelegate(typeof(EventHandler), target, events[methodName]);
                }
            }
            return null;
        }

        /// <summary>
        /// Call the event-compatible method named methodName on all targets.
        /// </summary>
        /// <param name="targets">Targets that is instance of class of type Type</param>
        /// <param name="methodName">Method event name</param>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">Event arguments</param>
        public void SendMessenger(object[] targets, string methodName, object sender, EventArgs eventArgs)
        {
            if (targets is null)
            {
                throw new ArgumentNullException(nameof(targets));
            };

            if (events.ContainsKey(methodName))
            {
                foreach (object target in targets)
                {
                    SendMessenger(target, methodName, sender, eventArgs);
                }
            }
        }

        /// <summary>
        /// Call the event-compatible method named methodName on target.
        /// </summary>
        /// <param name="target">Target that is instance of class of type Type</param>
        /// <param name="methodName">Method event name</param>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">Event arguments</param>
        public void SendMessenger(object target, string methodName, object sender, EventArgs eventArgs)
        {
            if (target != null && Type.Equals(target.GetType()) && events.ContainsKey(methodName))
            {
                events[methodName].Invoke(target, new object[] { sender, eventArgs });
            }
        }

        private static MethodInfo[] FilterEventsMethods(Type type)
        {
            Type objectType = typeof(object);
            Type eventArgsType = typeof(EventArgs);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Stack<MethodInfo> stack = new Stack<MethodInfo>();
            foreach (MethodInfo method in methods)
            {
                if (!method.IsConstructor)
                {
                    ParameterInfo[] paramts = method.GetParameters();
                    if (paramts.Length == 2 && paramts[0].ParameterType.IsAssignableFrom(objectType) && eventArgsType.IsAssignableFrom(paramts[1].ParameterType))
                    {
                        stack.Push(method);
                    }
                }
            }
            return stack.ToArray();
        }

        /// <summary>
        /// Check if have event name in type Type
        /// </summary>
        /// <param name="eventMethodName">Event method name</param>
        /// <returns>True if yes.</returns>
        public bool MethodEventExists(string eventMethodName)
        {
            return events.ContainsKey(eventMethodName);
        }
    }
}
