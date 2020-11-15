using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Requests
{
    public abstract class BaseRequest
    {
        /// <summary>
        /// Имя метода вызываемого API
        /// </summary>
        public string ActionName { get; set; }

        public BaseRequest(string actionName)
        {
            if (string.IsNullOrWhiteSpace(actionName))
                throw new ArgumentNullException(nameof(actionName));
            ActionName = actionName;
        }
    }
}
