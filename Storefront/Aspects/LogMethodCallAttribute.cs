#region Namespace Import
using System;
using System.Diagnostics;
using log4net;
using PostSharp.Laos;
#endregion

namespace Storefront.Aspects
{
    [Serializable]
    public class LogMethodCallAttribute : OnMethodInvocationAspect
    {
        public override void OnInvocation(MethodInvocationEventArgs eventArgs)
        {
            var methodName = eventArgs.Delegate.Method.Name.Replace("~", string.Empty);
            var className = eventArgs.Delegate.Method.DeclaringType.ToString();
            className = className.Substring(className.LastIndexOf(".") + 1, (className.Length - className.LastIndexOf(".") - 1));
            var log = LogManager.GetLogger(className);
            var stopWatch = new Stopwatch();

            var contextId = Guid.NewGuid().ToString();
            NDC.Push(contextId);

            log.InfoFormat("{0}() called", methodName);

            stopWatch.Start();
            NDC.Pop();
            try
            {
                eventArgs.Proceed();
            }
            catch (Exception ex)
            {
                var innermostException = GetInnermostException(ex);
                MDC.Set("exception", innermostException.ToString().Substring(0, Math.Min(innermostException.ToString().Length, 2000)));
                log.ErrorFormat("{0}() failed with error: {1}", methodName, innermostException.Message);
                MDC.Remove("exception");

                // get passed all of the System.Reflection.TargetInvocationException as these are meaningless. They just are a 
                // result of using PostSharp which is using reflection here.
                throw innermostException;
            }
            NDC.Push(contextId);

            stopWatch.Stop();
            MDC.Set("DurationInMs", stopWatch.ElapsedMilliseconds.ToString());
            log.InfoFormat("{0}() completed", methodName);
            MDC.Remove("DurationInMs");
            stopWatch = null;

            NDC.Pop();
        }

        private static Exception GetInnermostException(Exception ex)
        {
            var exception = ex;

            while (null != exception.InnerException)
            {
                exception = exception.InnerException;
            }

            return exception;
        }
    }
}
